using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Prod.Queries.GetProductDetails
{
    public class GetProductDetailsQueryHandler
        : IRequestHandler<GetProductQuery, ProductDetailsVm>
    {
        private readonly IProductStore _productStore;
        private readonly IMapper _mapper;

        public GetProductDetailsQueryHandler(IProductStore productStore,IMapper mapper) 
        {
            _productStore = productStore;
            _mapper = mapper;


        }

        public async Task<ProductDetailsVm> Handle(GetProductQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _productStore.GetProduct(request.Id, cancellationToken);

            return _mapper.Map<ProductDetailsVm>(entity.FirstOrDefault());
        }
    }
}
