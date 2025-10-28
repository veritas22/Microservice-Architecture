namespace Entity
{
    public class ReserveCourier
    {
        public int Id { get; set; }
        public int CourierId { get; set; }
        public int UserId { get; set; }
        public float OrderId { get; set; }
        public DateTime TimeSlot { get; set; }

    }
}
