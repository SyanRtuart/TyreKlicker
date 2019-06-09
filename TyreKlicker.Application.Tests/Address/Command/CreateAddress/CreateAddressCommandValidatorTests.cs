using System;
using FluentValidation.TestHelper;
using TyreKlicker.Application.Address.Command.CreateAddress;
using Xunit;

namespace TyreKlicker.Application.Tests.Address.Command.CreateAddress
{
    public class CreateAddressCommandValidatorTests
    {
        public CreateAddressCommandValidatorTests()
        {
            _validator = new CreateAddressCommandValidator();
        }

        private readonly CreateAddressCommandValidator _validator;

        [Fact]
        public void CreateAddressCommandValidator_CityIsEmpty_ShouldHaveValidationErrors()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.City, string.Empty);
        }

        [Fact]
        public void CreateAddressCommandValidator_CityIsNotNull_ShouldNotHaveValidationErrors()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.City, "Main City");
        }

        [Fact]
        public void CreateAddressCommandValidator_PhoneNumberIsEmpty_ShouldHaveValidationErrors()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.PhoneNumber, string.Empty);
        }

        [Fact]
        public void CreateAddressCommandValidator_PhoneNumberIsNotNull_ShouldNotHaveValidationErrors()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.PhoneNumber, "0141 444 4444");
        }

        [Fact]
        public void CreateAddressCommandValidator_PostcodeIsEmpty_ShouldHaveValidationErrors()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Postcode, string.Empty);
        }

        [Fact]
        public void CreateAddressCommandValidator_PostcodeIsNotNull_ShouldNotHaveValidationErrors()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Postcode, "G55 5GY");
        }

        [Fact]
        public void CreateAddressCommandValidator_StreetIsEmpty_ShouldHaveValidationErrors()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Street, string.Empty);
        }


        [Fact]
        public void CreateAddressCommandValidator_StreetIsNotNull_ShouldNotHaveValidationErrors()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Street, "Main Street");
        }

        [Fact]
        public void CreateAddressCommandValidator_UserIdIsEmpty_ShouldHaveValidationErrors()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.UserId, Guid.Empty);
        }

        [Fact]
        public void CreateAddressCommandValidator_UserIdIsGuid_ShouldNotHaveValidationErrors()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.UserId, Guid.NewGuid());
        }
    }
}