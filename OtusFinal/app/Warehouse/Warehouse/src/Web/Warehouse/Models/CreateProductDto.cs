using Application.Common.Mappings;
using Application.Prod.Commands.CreateProduct;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.Controllers
{
    public class CreateProductDto : IMapWith<CreateProductCommand>
    {
        public float Price { get; set; }
        public string ProductName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProductDto, CreateProductCommand>()
                .ForMember(noteCommand => noteCommand.ProductName,
                    opt => opt.MapFrom(noteDto => noteDto.ProductName))
                .ForMember(noteCommand => noteCommand.Price,
                    opt => opt.MapFrom(noteDto => noteDto.Price));
        }
    }
}
