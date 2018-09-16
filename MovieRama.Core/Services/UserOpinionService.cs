using MovieRama.Core.Models;
using MovieRama.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRama.Core.Services
{
	public class UserOpinionService : IUserOpinionService
	{
		private readonly IApplicationContext _applicationContext;
        private readonly IMovieRepository _movieRepository;
		private readonly IUserOpinionRepository _userOpinionRepository;

		public UserOpinionService(
			IApplicationContext applicationContext,
            IMovieRepository movieRepository,
			IUserOpinionRepository userOpinionRepository)
		{
			_applicationContext = applicationContext;
            _movieRepository = movieRepository;
            _userOpinionRepository = userOpinionRepository;
		}

		public async Task LikeAsync(int userId, int movieId)
		{
			_applicationContext.BeginTransaction();
			try
			{
				Movie movie = await _movieRepository.GetMovieAsync(movieId);
				UserOpinion opinion = await _userOpinionRepository.GetUserOpinionAsync(userId, movieId);
				if (opinion == null)
				{
					opinion = new UserOpinion();
					opinion.User = userId;
					opinion.Movie = movieId;
					opinion.Opinion = OpinionType.Like;
					opinion.CreationTime = DateTime.UtcNow;
					opinion.LastUpdateTime = DateTime.UtcNow;
					_userOpinionRepository.Add(opinion);

					movie.Likes++;
					_movieRepository.Update(movie);
				}
				else if (opinion.Opinion == OpinionType.Hate)
				{
					opinion.Opinion = OpinionType.Like;
					opinion.LastUpdateTime = DateTime.UtcNow;
					_userOpinionRepository.Update(opinion);

					movie.Likes++;
					movie.Hates--;
					_movieRepository.Update(movie);
				}

				await _applicationContext.SaveChangesAsync();

				_applicationContext.CommitTransaction();
			}
			catch (Exception)
			{
				_applicationContext.RollbackTransaction();
				throw;
			}
		}

		public async Task HateAsync(int userId, int movieId)
		{
			_applicationContext.BeginTransaction();
			try
			{
				Movie movie = await _movieRepository.GetMovieAsync(movieId);
				UserOpinion opinion = await _userOpinionRepository.GetUserOpinionAsync(userId, movieId);
				if (opinion == null)
				{
					opinion = new UserOpinion();
					opinion.User = userId;
					opinion.Movie = movieId;
					opinion.Opinion = OpinionType.Hate;
					opinion.CreationTime = DateTime.UtcNow;
					opinion.LastUpdateTime = DateTime.UtcNow;
					_userOpinionRepository.Add(opinion);

					movie.Hates++;
					_movieRepository.Update(movie);
				}
				else if (opinion.Opinion == OpinionType.Like)
				{
					opinion.Opinion = OpinionType.Hate;
					opinion.LastUpdateTime = DateTime.UtcNow;
					_userOpinionRepository.Update(opinion);

					movie.Hates++;
					movie.Likes--;
					_movieRepository.Update(movie);
				}

				await _applicationContext.SaveChangesAsync();

				_applicationContext.CommitTransaction();
			}
			catch (Exception)
			{
				_applicationContext.RollbackTransaction();
				throw;
			}
		}

		public async Task RevokeAsync(int userId, int movieId)
		{
			_applicationContext.BeginTransaction();
			try
			{
				Movie movie = await _movieRepository.GetMovieAsync(movieId);
				UserOpinion opinion = await _userOpinionRepository.GetUserOpinionAsync(userId, movieId);
				if (opinion != null && opinion.Opinion == OpinionType.Like)
				{
					_userOpinionRepository.Remove(opinion);

					movie.Likes--;
					_movieRepository.Update(movie);
				}
				else if (opinion != null && opinion.Opinion == OpinionType.Hate)
				{
					_userOpinionRepository.Remove(opinion);

					movie.Hates--;
					_movieRepository.Update(movie);
				}

				await _applicationContext.SaveChangesAsync();

				_applicationContext.CommitTransaction();
			}
			catch (Exception)
			{
				_applicationContext.RollbackTransaction();
				throw;
			}
		}
	}
}
