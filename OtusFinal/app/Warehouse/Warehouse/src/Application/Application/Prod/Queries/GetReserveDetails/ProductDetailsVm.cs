using System;
using Application.Common.Mappings;
using AutoMapper;
using Domain;

namespace Application.Prod.Queries.GetReserveDetails
{
    public class ReserveDetailsVm : IMapWith<Domain.Reserve>
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public int Status { get; set; }
        public DateTime CreationDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Reserve, ReserveDetailsVm>()
                .ForMember(noteVm => noteVm.ProductId,
                    opt => opt.MapFrom(note => note.ProductId))
                .ForMember(noteVm => noteVm.Status,
                    opt => opt.MapFrom(note => note.Status));

        }
    }
}
