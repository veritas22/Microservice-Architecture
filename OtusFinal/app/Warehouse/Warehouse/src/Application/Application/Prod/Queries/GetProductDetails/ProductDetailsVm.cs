using System;
using Application.Common.Mappings;
using AutoMapper;
using Domain;

namespace Application.Prod.Queries.GetProductDetails
{
    public class ProductDetailsVm : IMapWith<Domain.Product>
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string ProductName { get; set; }
        public DateTime CreationDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDetailsVm>()
                .ForMember(noteVm => noteVm.Description,
                    opt => opt.MapFrom(note => note.Description))
                .ForMember(noteVm => noteVm.ProductName,
                    opt => opt.MapFrom(note => note.Name))
                .ForMember(noteVm => noteVm.Id,
                    opt => opt.MapFrom(note => note.Id))
                .ForMember(noteVm => noteVm.CreationDate,
                    opt => opt.MapFrom(note => note.CreationDate));
        }
    }
}
