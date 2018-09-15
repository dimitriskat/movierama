using MovieRama.Core.Dtos;
using MovieRama.Core.Validators;
using System;
using System.Linq;
using Xunit;

namespace MovieRama.Core.Tests.Validators
{
	public class MoviePostDtoValidatorTests
	{
		[Fact]
		public void Validate_EmptyTitle_NotValid()
		{
			MoviePostDto moviePostDto = new MoviePostDto();

			MoviePostDtoValidator validator = new MoviePostDtoValidator();

			Assert.Contains(validator.Validate(moviePostDto).Errors, x => "Title".Equals(x.PropertyName));

			Assert.False(validator.Validate(moviePostDto).IsValid);
		}

		[Fact]
		public void Validate_EmptyDescription_NotValid()
		{
			MoviePostDto moviePostDto = new MoviePostDto();

			MoviePostDtoValidator validator = new MoviePostDtoValidator();

			Assert.Contains(validator.Validate(moviePostDto).Errors, x => "Description".Equals(x.PropertyName));

			Assert.False(validator.Validate(moviePostDto).IsValid);
		}

		[Fact]
		public void Validate_FilledProperties_Valid()
		{
			MoviePostDto moviePostDto = new MoviePostDto()
			{
				Title = "sample",
				Description = "sample"
			};

			MoviePostDtoValidator validator = new MoviePostDtoValidator();

			Assert.True(validator.Validate(moviePostDto).IsValid);
		}
	}
}
