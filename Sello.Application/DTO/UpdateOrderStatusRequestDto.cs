using Sello.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.DTO
{
    public class UpdateOrderStatusRequestDto
    {
        public OrderStatus Status { get; set; }
    }
}
