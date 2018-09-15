using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using MovieRama.Core.Dtos;

namespace MovieRama.Core.Validators
{
	public class MoviePostDtoValidator : AbstractValidator<MoviePostDto>
	{
		public MoviePostDtoValidator()
		{
			RuleFor(customer => customer.Title).NotNull().NotEmpty().MaximumLength(250);
			RuleFor(customer => customer.Description).NotNull().NotEmpty().MaximumLength(4000);
		}
	}
}
