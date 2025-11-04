using Domen.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderLogic
    {
        Task<float> PlaceOrderAsync(int userId, Price amount, CancellationToken cancel);
    }
}
