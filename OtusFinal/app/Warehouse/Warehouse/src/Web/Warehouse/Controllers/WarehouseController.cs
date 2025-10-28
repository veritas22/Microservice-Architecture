
using Application.Prod.Commands.CreateProduct;
using Application.Prod.Queries.GetProductDetails;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController : BaseController
    {
        private readonly IMapper _mapper;

        public WarehouseController(IMapper mapper) => _mapper = mapper;



        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailsVm>> Get(int id)
        {
            var query = new GetProductQuery
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }


        [HttpPost("CreateProduct")]
        public async Task<ActionResult<Guid>> CreateProduct([FromBody] CreateProductDto createNoteDto)
        {
            var command = _mapper.Map<CreateProductCommand>(createNoteDto);
            var noteId = await Mediator.Send(command);
            return Ok(noteId);
        }

    }
}
