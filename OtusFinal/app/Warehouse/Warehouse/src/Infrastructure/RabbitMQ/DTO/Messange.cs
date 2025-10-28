using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQNeuro.DTO
{
    public class Messange
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public float Price { get; set; }
        public bool Status { get; set; }
        public string Text { get; set; }
    }
}
