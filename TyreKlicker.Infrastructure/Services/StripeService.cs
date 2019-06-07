using System;
using System.Threading.Tasks;
using Stripe;
using TyreKlicker.Application.Interfaces;
using TyreKlicker.Application.Payment.Models;

namespace TyreKlicker.Infrastructure.Services
{
    public class StripeService : IPaymentService
    {
        public async Task<string> MakePayment(PaymentDto payment, string secretKey)
        {
            try
            {
                var customerService = new CustomerService();
                var chargeService = new ChargeService();

                var customer = customerService.Create(new CustomerCreateOptions
                {
                    Email = payment.Email,
                    SourceToken = payment.Token
                });

                var charge = await chargeService.CreateAsync(new ChargeCreateOptions
                {
                    Amount = payment.Amount,
                    Description = payment.Description,
                    Currency = payment.Currency,
                    CustomerId = customer.Id
                });

                return charge.Id;
            }
            catch (StripeException e)
            {
                throw;
                //ToDo Stripe Exceptions
                switch (e.StripeError.ErrorType)
                {
                    case "card_error":
                        Console.WriteLine("Code: " + e.StripeError.Code);
                        Console.WriteLine("Message: " + e.StripeError.Message);
                        break;
                    case "api_connection_error":
                        break;
                    case "api_error":
                        break;
                    case "authentication_error":
                        break;
                    case "invalid_request_error":
                        break;
                    case "rate_limit_error":
                        break;
                    case "validation_error":
                        break;
                }
            }
        }
    }
}