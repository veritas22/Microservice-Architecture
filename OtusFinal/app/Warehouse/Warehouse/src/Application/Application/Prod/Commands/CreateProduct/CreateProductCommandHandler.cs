using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Prod.Commands.CreateProduct
{
    public class CreateProductCommandHandler
        :IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductStore _productStore;

        public CreateProductCommandHandler(IProductStore productStore) =>
            _productStore = productStore;

        public async Task<int> Handle(CreateProductCommand request,
            CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Price = request.Price,
                Name = request.ProductName,
                CreationDate = DateTime.Now,
                Description = "тестовое описание"
            };

            await _productStore.AddProduct(product, cancellationToken);

            return product.Id;
        }
    }
}
