using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Ordering.Application.Contacts.Infrastructure;
using Ordering.Application.Contacts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);
            bool isOrderPlaced = await _orderRepository.AddAsync(order);
            if (isOrderPlaced)
            {
                EmailMessage email = new EmailMessage()
                {
                    Subject = "Your order has been placed",
                    To = order.UserName,
                    Body = "Dear " + order.FirstName + " " + order.LastName + " ,</br> </br>" + "We are excited for you to receive your order #" + order.Id + " soon. </br> </br> Thank you for shopping with us."
                };
                await _emailService.SendEmailAsync(email);
            }
            return isOrderPlaced;
        }
    }
}
