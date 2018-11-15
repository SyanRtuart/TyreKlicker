using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.TestHelper;
using TyreKlicker.Application.User.Command.CreateUser;
using Xunit;

namespace TyreKlicker.Application.Tests.User.Command.CreateUser
{
    public class CreateUserCommandValidatorTests
    {
        private CreateUserCommandValidator _validator;

        public CreateUserCommandValidatorTests()
        {
            _validator = new CreateUserCommandValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("MyNameIsVeryLongAndIShouldReturnAnError")]
        public void CreateUserCommandValidator_FirstName_ReturnsValidationErrors(string firstName)
        {
            _validator.ShouldHaveValidationErrorFor(u => u.FirstName, firstName);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("abcdefghijk")]
        public void CreateUserCommandValidator_First_ReturnsNoValidationErrors(string lastName)
        {
            _validator.ShouldNotHaveValidationErrorFor(u => u.LastName, lastName);
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("MyNameIsVeryLongAndIShouldReturnAnError")]
        public void CreateUserCommandValidator_LastName_ReturnsValidationErrors(string lastName)
        {
            _validator.ShouldHaveValidationErrorFor(u => u.LastName, lastName);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("abcdefghijk")]
        public void CreateUserCommandValidator_LastName_ReturnsNoValidationErrors(string lastName)
        {
            _validator.ShouldNotHaveValidationErrorFor(u => u.LastName, lastName);
        }

        [Fact]
        public void CreateUserCommandValidator_PhoneNumberIsEmpty_ReturnsValidationErrors()
        {
            _validator.ShouldHaveValidationErrorFor(u => u.PhoneNumber, "");
        }
    }
}