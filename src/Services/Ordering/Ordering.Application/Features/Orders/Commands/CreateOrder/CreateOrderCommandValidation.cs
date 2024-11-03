using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidation : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidation()
        {
            RuleFor(p => p.UserName).NotEmpty().WithMessage("UserName is required.")
                .NotNull()
                .EmailAddress().WithMessage("UserName is not a valid email address.");
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("FirstName is required.")
                .MaximumLength(100).WithMessage("FirstName must not exceed 100 characters.");
            RuleFor(p => p.TotalPrice).GreaterThan(0).WithMessage("TotalPrice must be Grater than 0");
            RuleFor(p => p.EmailAddress).EmailAddress().WithMessage("EmailAddress is not a valid email address.");
        }
    }
}
