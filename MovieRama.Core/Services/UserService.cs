using AutoMapper;
using MovieRama.Core.Dtos;
using MovieRama.Core.Models;
using MovieRama.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRama.Core.Services
{
    public class UserService : IUserService
    {
		private readonly IUserRepository _userRepository;
        private readonly IApplicationContext _applicationContext;
		private readonly IMapper _mapper;

		public UserService(
			IUserRepository userRepository,
			IApplicationContext applicationContext,
			IMapper mapper)
		{
			_userRepository = userRepository;
			_applicationContext = applicationContext;
			_mapper = mapper;
		}

		public async Task<UserDto> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = await _userRepository.GetUserByUserNameAsync(username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

			// authentication successful
			var userDto = _mapper.Map<UserDto>(user);

			return userDto;
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
			var user = await _userRepository.GetUserAsync(id);
			if (user == null) return null;
			var userDto = _mapper.Map<UserDto>(user);
			return userDto;
        }

		public async Task<int> CreateAsync(UserDto userDto, string password)
		{
			// validation
			if (string.IsNullOrWhiteSpace(password))
				throw new Exception("Password is required");

			_applicationContext.BeginTransaction();
			try
			{
				bool exists = await _userRepository.Exists(userDto.Username);
				if (exists) throw new Exception("Username '" + userDto.Username + "' is already taken");

				var user = _mapper.Map<User>(userDto);

				byte[] passwordHash, passwordSalt;
				CreatePasswordHash(password, out passwordHash, out passwordSalt);

				user.PasswordHash = passwordHash;
				user.PasswordSalt = passwordSalt;

				user.CreationTime = DateTime.UtcNow;
				user.LastUpdateTime = DateTime.UtcNow;

				_userRepository.Add(user);

				await _applicationContext.SaveChangesAsync();

				_applicationContext.CommitTransaction();

				return user.Id;
			}
			catch (Exception)
			{
				_applicationContext.RollbackTransaction();
				throw;
			}
		}

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
