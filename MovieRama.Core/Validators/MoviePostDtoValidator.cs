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
			RuleFor(movie => movie.Title).NotNull().NotEmpty().MaximumLength(250);
			RuleFor(movie => movie.Description).NotNull().NotEmpty().MaximumLength(4000);
		}
	}
}
