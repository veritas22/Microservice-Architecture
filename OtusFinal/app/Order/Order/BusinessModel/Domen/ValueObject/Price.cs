using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen.ValueObject
{
    public class Price
    {
        public float Value { get; }
        public Price(float price)
        {
            if (price < 0)
                throw new ArgumentException("Price cannot be negative.", nameof(price));

            // Можно добавить проверку точности, например до 2 знаков после запятой
            if (Math.Abs(Math.Round(price, 2) - price) < 0.00000001)
                throw new ArgumentException("Price cannot have more than 2 decimal places.", nameof(price));

            Value = price;
        }
    }

}
