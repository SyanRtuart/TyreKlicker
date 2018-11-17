﻿using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Shouldly;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Application.User.Command.CreateUser;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.User.Command.CreateUser
{
    [Collection("QueryCollection")]
    public class CreateUserCommandValidatorTests
    {
        private CreateUserCommandValidator _validator;
        private readonly TyreKlickerDbContext _context;

        public CreateUserCommandValidatorTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _validator = new CreateUserCommandValidator();
        }

        [Fact]
        public void CreateUserCommandValidator_ValidProperties_ShouldReturnValid()
        {
            ValidationResult validationResult = _validator.Validate(new CreateUserCommand
            {
                Email = "aaa@hotmail.co.uk",
                FirstName = "aaa",
                LastName = "aaa",
                PhoneNumber = "0141 444 4444"
            });
            validationResult.IsValid.ShouldBe(true);
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        public void CreateUserCommandValidator_FirstNameIsTooShort_ShouldReturnValidationError(string firstName)
        {
            _validator.ShouldHaveValidationErrorFor(u => u.FirstName, firstName);
        }

        [Theory]
        [InlineData("IAmMoreThan30CharactersAndIShouldReturnAnError")]
        [InlineData("31CharactersExactly------------")]
        public void CreateUserCommandValidator_FirstNameIsTooLong_ShouldReturnValidationError(string firstName)
        {
            _validator.ShouldHaveValidationErrorFor(u => u.FirstName, firstName);
        }

        [Theory]
        [InlineData("ab")]
        [InlineData("abc")]
        [InlineData("abcdefghijk")]
        [InlineData("30CharactersExactly-----------")]
        public void CreateUserCommandValidator_FirstNameIsCorrectLength_ShouldNotValidationError(string firstName)
        {
            _validator.ShouldNotHaveValidationErrorFor(u => u.FirstName, firstName);
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        public void CreateUserCommandValidator_LastNameIsTooShort_ShouldReturnValidationError(string lastName)
        {
            _validator.ShouldHaveValidationErrorFor(u => u.LastName, lastName);
        }

        [Theory]
        [InlineData("IAmMoreThan30CharactersAndIShouldReturnAnError")]
        [InlineData("31CharactersExactly------------")]
        public void CreateUserCommandValidator_LastNameIsTooLong_ShouldReturnValidationError(string lastName)
        {
            _validator.ShouldHaveValidationErrorFor(u => u.LastName, lastName);
        }

        [Theory]
        [InlineData("ab")]
        [InlineData("abc")]
        [InlineData("abcdefghijk")]
        [InlineData("30CharactersExactly-----------")]
        public void CreateUserCommandValidator_LastNameIsCorrectLength_ShouldNotValidationError(string lastName)
        {
            _validator.ShouldNotHaveValidationErrorFor(u => u.LastName, lastName);
        }

        [Fact]
        public void CreateUserCommandValidator_PhoneNumberIsEmpty_ShouldReturnsValidationError()
        {
            _validator.ShouldHaveValidationErrorFor(u => u.PhoneNumber, "");
        }
    }
}