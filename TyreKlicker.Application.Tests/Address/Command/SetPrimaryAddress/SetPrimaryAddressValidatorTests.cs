using System;
using FluentValidation.TestHelper;
using TyreKlicker.Application.Address.Command.SetPrimaryAddress;
using Xunit;

namespace TyreKlicker.Application.Tests.Address.Command.SetPrimaryAddress
{
    public class SetPrimaryAddressValidatorTests
    {
        public SetPrimaryAddressValidatorTests()
        {
            _validator = new SetPrimaryAddressValidator();
        }

        private readonly SetPrimaryAddressValidator _validator;

        [Fact]
        public void SetPrimaryAddressValidator_IdIsEmpty_ShouldHaveValidationErrors()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
        }

        [Fact]
        public void SetPrimaryAddressValidator_IdIsValid_ShouldNotHaveValidationErrors()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Id, Guid.NewGuid());
        }

        [Fact]
        public void SetPrimaryAddressValidator_IsPrimaryIsFalse_ShouldNotHaveValidationErrors()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.IsPrimary, true);
        }

        [Fact]
        public void SetPrimaryAddressValidator_IsPrimaryIsTrue_ShouldNotHaveValidationErrors()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.IsPrimary, true);
        }

        [Fact]
        public void SetPrimaryAddressValidator_UserIdIsEmpty_ShouldHaveValidationErrors()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.UserId, Guid.Empty);
        }

        [Fact]
        public void SetPrimaryAddressValidator_UserIdIsValid_ShouldNotHaveValidationErrors()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.UserId, Guid.NewGuid());
        }
    }
}