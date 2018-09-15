using AutoMapper;
using Moq;
using MovieRama.Core.Repositories;
using MovieRama.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using MovieRama.Core.Models;
using MovieRama.Core.Criteria;

namespace MovieRama.Core.Tests.Services
{
	public class MovieServiceTests
	{
		[Fact]
		public async void PostAsync_ListMovies_CreatesProperDto()
		{
			var config = new MapperConfiguration(cfg => cfg.AddProfile<Mappings.MappingProfile>());

			Movie movie = new Movie()
			{
				Id = 1,
				User = 1,
				Likes = 1,
				Hates = 1,
				Title = "test_title",
				Description = "test_description",
				PublicationDate = new DateTime(2000, 1, 1),
				CreationTime = new DateTime(2010, 1, 1),
				LastUpdateTime = new DateTime(2010, 1, 1)
			};
			User user = new User()
			{
				Id = 1,
				FirstName = "test_first_name",
				LastName = "test_last_name"
			};
			UserOpinion userOpinion = new UserOpinion()
			{
				User = 1,
				Movie = 1,
				Opinion = OpinionType.Like
			};

			var mockedUnitOfWork = new Mock<IUnitOfWork>();
			var mockedMovieRepository = new Mock<IMovieRepository>();
			mockedMovieRepository.Setup(x => x.ListMoviesAsync(It.IsAny<MovieCriteria>())).ReturnsAsync(new Movie[] { movie });
			var mockedUserRepository = new Mock<IUserRepository>();
			mockedUserRepository.Setup(x => x.GetUsersAsync(It.IsAny<IEnumerable<int>>())).ReturnsAsync(new User[] { user });
			var mockedUserOpinionRepository = new Mock<IUserOpinionRepository>();
			mockedUserOpinionRepository.Setup(x => x.GetUserOpinionsAsync(1, It.IsAny<IEnumerable<int>>())).ReturnsAsync(new UserOpinion[] { userOpinion });

			MovieService movieService = new MovieService(config.CreateMapper(),
				mockedUnitOfWork.Object,
				mockedMovieRepository.Object,
				mockedUserRepository.Object,
				mockedUserOpinionRepository.Object);

			IEnumerable<Dtos.MovieDto> movies = await movieService.ListMoviesAsync(new Dtos.MovieCriteriaDto(), 1);

			Assert.Single(movies);

			Dtos.MovieDto movieDto = movies.Single();

			Assert.Equal(movieDto.Id, movie.Id);
			Assert.Equal(movieDto.Title, movie.Title);
			Assert.Equal(movieDto.User, movie.User);
			Assert.Equal(movieDto.Likes, movie.Likes);
			Assert.Equal(movieDto.Hates, movie.Hates);
			Assert.Equal(movieDto.Title, movie.Title);
			Assert.Equal(movieDto.Description, movie.Description);
			Assert.Equal(movieDto.PublicationDate, movie.PublicationDate);
			Assert.Equal(movieDto.CreationTime, movie.CreationTime);
			Assert.Equal(movieDto.LastUpdateTime, movie.LastUpdateTime);
			Assert.Equal(movieDto.UserFirstName, user.FirstName);
			Assert.Equal(movieDto.UserLastName, user.LastName);
			Assert.Equal(Dtos.UserOpinionType.Like, movieDto.UserOpinion);
		}

		[Fact]
		public async void PostAsync_AddAndSave_TimesOnce()
		{
			var config = new MapperConfiguration(cfg => cfg.AddProfile<Mappings.MappingProfile>());

			var mockedUnitOfWork = new Mock<IUnitOfWork>();
			var mockedMovieRepository = new Mock<IMovieRepository>();
			var mockedUserRepository = new Mock<IUserRepository>();
			var mockedUserOpinionRepository = new Mock<IUserOpinionRepository>();

			MovieService movieService = new MovieService(config.CreateMapper(),
				mockedUnitOfWork.Object,
				mockedMovieRepository.Object,
				mockedUserRepository.Object,
				mockedUserOpinionRepository.Object);

			await movieService.PostAsync(0, new Dtos.MoviePostDto());

			mockedMovieRepository.Verify(x => x.Add(It.IsAny<Movie>()), Times.Once());
			mockedUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once());
		}
	}
}
