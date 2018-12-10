using System;
using System.Collections.Generic;
using TyreKlicker.Application.Tests.Infrastructure;
using Shouldly;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using TyreKlicker.Application.Order.Command.AcceptOrder;
using Xunit;

namespace TyreKlicker.Application.Tests.Order.Command.AcceptOrder
{
    public class AcceptOrderCommandValidatorTests
    {
        private readonly AcceptOrderCommandValidator _validator;

        public AcceptOrderCommandValidatorTests()
        {
            _validator = new AcceptOrderCommandValidator();
        }

        [Fact]
        public void AcceptOrderCommandValidator_UserIdIsEmpty_ShouldHaveValidationErrors()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.UserId, Guid.Empty);
        }

        [Fact]
        public void AcceptOrderCommandValidator_UserIdValid_ShouldNotHaveValidationErrors()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.UserId, Guid.NewGuid());
        }

        [Fact]
        public void AcceptOrderCommandValidator_OrderIdIsEmpty_ShouldHaveValidationErrors()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.OrderId, Guid.Empty);
        }

        [Fact]
        public void AcceptOrderCommandValidator_OrderIdIsValid_ShouldNotHaveValidationErrors()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.OrderId, Guid.NewGuid());
        }
        
    }
}
