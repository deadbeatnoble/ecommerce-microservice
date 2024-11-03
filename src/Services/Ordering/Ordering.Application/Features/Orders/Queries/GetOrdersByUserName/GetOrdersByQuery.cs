using MediatR;
using Ordering.Application.Contacts.Infrastructure;
using Ordering.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersByUserName
{
    public class GetOrdersByQuery: IRequest<List<OrderVm>>
    {
        public string UserName { get; set; }
        public GetOrdersByQuery(string userName)
        {
            UserName = userName;
        }
    }
}
