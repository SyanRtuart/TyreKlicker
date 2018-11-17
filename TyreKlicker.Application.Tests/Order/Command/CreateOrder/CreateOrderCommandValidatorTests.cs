using FluentValidation.Results;
using Shouldly;
using TyreKlicker.Application.Job.Command.CreateJob;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Persistence;
using FluentValidation.TestHelper;

using Xunit;

namespace TyreKlicker.Application.Tests.Order.Command.CreateOrder
{
    [Collection("QueryCollection")]
    public class CreateOrderCommandValidatorTests
    {
        private CreateOrderCommandValidator _validator;
        private readonly TyreKlickerDbContext _context;

        public CreateOrderCommandValidatorTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _validator = new CreateOrderCommandValidator();
        }

        [Fact]
        public void CreateOrderCommandValidator_ValidProperties_ShouldReturnValid()
        {
            ValidationResult validationResult = _validator.Validate(new CreateOrderCommand
            {
                Description = "aaa",
                Registration = "aaa",
            });
            validationResult.IsValid.ShouldBe(true);
        }

        [Fact]
        public void CreateOrderCommandValidator_DescriptionIsEmpty_ShouldReturnValidationError()
        {
            _validator.ShouldHaveValidationErrorFor(o => o.Description, "");
        }

        [Fact]
        public void CreateOrderCommandValidator_DescriptionIsNotEmpty_ShouldNotHaveValidationError()
        {
            _validator.ShouldNotHaveValidationErrorFor(o => o.Description, "a");
        }

        [Fact]
        public void CreateOrderCommandValidator_RegistrationIsEmpty_ShouldReturnValidationError()
        {
            _validator.ShouldHaveValidationErrorFor(o => o.Registration, "");
        }

        [Fact]
        public void CreateOrderCommandValidator_RegistrationIsNotEmpty_ShouldNotHaveValidationError()
        {
            _validator.ShouldNotHaveValidationErrorFor(o => o.Registration, "a");
        }
    }
}