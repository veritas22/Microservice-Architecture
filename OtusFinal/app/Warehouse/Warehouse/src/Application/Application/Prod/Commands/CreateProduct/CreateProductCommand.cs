using System;
using MediatR;

namespace Application.Prod.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public string ProductName { get; set; }
    }
}
