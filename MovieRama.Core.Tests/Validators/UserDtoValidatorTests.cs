using MovieRama.Core.Dtos;
using MovieRama.Core.Validators;
using System;
using System.Linq;
using Xunit;

namespace MovieRama.Core.Tests.Validators
{
	public class UserDtoValidatorTests
	{
		[Fact]
		public void Validate_EmptyFirstName_NotValid()
		{
			UserDto userDto = new UserDto();

			UserDtoValidator validator = new UserDtoValidator();

			Assert.Contains(validator.Validate(userDto).Errors, x => "FirstName".Equals(x.PropertyName));

			Assert.False(validator.Validate(userDto).IsValid);
		}

		[Fact]
		public void Validate_EmptyLastName_NotValid()
		{
			UserDto userDto = new UserDto();

			UserDtoValidator validator = new UserDtoValidator();

			Assert.Contains(validator.Validate(userDto).Errors, x => "LastName".Equals(x.PropertyName));

			Assert.False(validator.Validate(userDto).IsValid);
		}

		[Fact]
		public void Validate_EmptyUsername_NotValid()
		{
			UserDto userDto = new UserDto();

			UserDtoValidator validator = new UserDtoValidator();

			Assert.Contains(validator.Validate(userDto).Errors, x => "Username".Equals(x.PropertyName));

			Assert.False(validator.Validate(userDto).IsValid);
		}

		[Fact]
		public void Validate_FilledProperties_Valid()
		{
			UserDto userDto = new UserDto()
			{
				FirstName = "sample",
				LastName = "sample",
				Username = "sample",
				Password = "sample"
			};

			UserDtoValidator validator = new UserDtoValidator();

			Assert.True(validator.Validate(userDto).IsValid);
		}
	}
}
