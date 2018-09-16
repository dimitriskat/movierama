using AutoMapper;
using MovieRama.Core.Criteria;
using MovieRama.Core.Dtos;
using MovieRama.Core.Models;
using MovieRama.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRama.Core.Services
{
	public class MovieService : IMovieService
	{
		private readonly IMapper _mapper;
		private readonly IApplicationContext _applicationContext;
        private readonly IMovieRepository _movieRepository;
		private readonly IUserRepository _userRepository;
		private readonly IUserOpinionRepository _userOpinionRepository;

		public MovieService(
			IMapper mapper,
			IApplicationContext applicationContext,
            IMovieRepository movieRepository,
			IUserRepository userRepository,
			IUserOpinionRepository userOpinionRepository)
		{
			_mapper = mapper;
			_applicationContext = applicationContext;
            _movieRepository = movieRepository;
			_userRepository = userRepository;
			_userOpinionRepository = userOpinionRepository;
		}

		public async Task<IEnumerable<MovieDto>> ListMoviesAsync(MovieCriteriaDto criteriaDto, int? userId)
		{
			var criteria = _mapper.Map<MovieCriteria>(criteriaDto);

			IEnumerable<Movie> movies = null;
			IEnumerable<User> users = null;
			IEnumerable<UserOpinion> opinions = null;

			_applicationContext.BeginTransaction();
			try
			{
				movies = await _movieRepository.ListMoviesAsync(criteria);

				var userIds = movies.Select(x => x.User).ToHashSet();
				users = await _userRepository.GetUsersAsync(userIds);

				opinions = userId.HasValue ?
					await _userOpinionRepository.GetUserOpinionsAsync(userId.Value, movies.Select(x => x.Id).ToList()) :
					Enumerable.Empty<UserOpinion>();

				_applicationContext.CommitTransaction();
			}
			catch (Exception)
			{
				_applicationContext.RollbackTransaction();
				throw;
			}

			var usersDic = users.ToDictionary(x => x.Id);
			var opinionsDic = opinions.ToDictionary(x => x.Movie);

			var dtos = _mapper.Map<IEnumerable<Movie>, IEnumerable<MovieDto>>(movies);
			foreach (var dto in dtos)
			{
				User creator = usersDic[dto.User];
				dto.UserFirstName = creator.FirstName;
				dto.UserLastName = creator.LastName;
				dto.UserOpinion = opinionsDic.ContainsKey(dto.Id) ?
					(UserOpinionType)opinionsDic[dto.Id].Opinion : UserOpinionType.Undefined;
			}

			return dtos;
		}

		public async Task<MovieDto> GetByIdAsync(int id)
		{
			var movie = await _movieRepository.GetMovieAsync(id);
			if (movie == null) return null;

			var creator = await _userRepository.GetUserAsync(movie.User);

			var dto = _mapper.Map<MovieDto>(movie);
			dto.UserFirstName = creator.FirstName;
			dto.UserLastName = creator.LastName;
			return dto;
		}

		public async Task<int> PostAsync(int user, MoviePostDto moviePostDto)
        {
			Movie movie = _mapper.Map<Movie>(moviePostDto);
			movie.User = user;
			movie.CreationTime = DateTime.UtcNow;
			movie.LastUpdateTime = DateTime.UtcNow;
			_movieRepository.Add(movie);
			await _applicationContext.SaveChangesAsync();
			return movie.Id;
        }
	}
}
