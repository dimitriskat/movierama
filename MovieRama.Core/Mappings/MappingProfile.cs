using AutoMapper;
using MovieRama.Core.Dtos;
using MovieRama.Core.Models;
using System;
using System.Collections.Generic;

namespace MovieRama.Core.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<User, UserDto>();
			CreateMap<UserDto, User>();
			CreateMap<Movie, MovieDto>();
			CreateMap<MoviePostDto, Movie>();
		}
	}
}
