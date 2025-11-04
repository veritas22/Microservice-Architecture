using Domen.ValueObject;

namespace Domen
{
    public class Order
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public Price Price { get; set; }
        public string Status { get; set; }

    }
}
