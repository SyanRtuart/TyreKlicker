using FluentValidation.TestHelper;
using System;
using TyreKlicker.Application.Order.Command.CompleteOrder;
using Xunit;

namespace TyreKlicker.Application.Tests.Order.Command.CompleteOrder
{
    public class CompleteOrderCommandValidatorTests
    {
        private readonly CompleteOrderCommandValidator _validator;

        public CompleteOrderCommandValidatorTests()
        {
            _validator = new CompleteOrderCommandValidator();
        }

        [Fact]
        public void CompleteOrderCommandValidator_OrderIdIsEmpty_ShouldHaveValidationErrors()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.OrderId, Guid.Empty);
        }

        [Fact]
        public void CompleteOrderCommandValidator_OrderIdIsValid_ShouldNotHaveValidationErrors()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.OrderId, Guid.NewGuid());
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CompleteOrderCommandValidator_CompleteIsFalse_ShouldNotHaveValidationErrors(bool IsComplete)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Complete, IsComplete);
        }
    }
}