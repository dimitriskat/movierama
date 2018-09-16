using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using MovieRama.Core.Dtos;

namespace MovieRama.Core.Validators
{
	public class UserDtoValidator : AbstractValidator<UserDto>
	{
		public UserDtoValidator()
		{
			RuleFor(user => user.FirstName).NotNull().NotEmpty().MaximumLength(250);
			RuleFor(user => user.LastName).NotNull().NotEmpty().MaximumLength(250);
			RuleFor(user => user.Username).NotNull().NotEmpty().MaximumLength(250);
			RuleFor(user => user.Password).NotNull().NotEmpty().MaximumLength(250);
		}
	}
}
