using AutoMapper;
using RentalStore.Domain.Models;
using RentalStore.SharedKernel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalStore.Application.Mappings
{
    public class RentalStoreMappingProfile : Profile
    {
        public RentalStoreMappingProfile()
        {
           
            CreateMap<Equipment, EquipmentDto>();
            CreateMap<EquipmentDto, Equipment>();

            CreateMap<Equipment, EquipmentDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom((src, dest, destMember, context) =>
                {
                    var category = context.Items["categories"] as IEnumerable<Category>;
                    return category?.FirstOrDefault(c => c.CategoryId == src.CategoryId)?.CategoryName;
                }));

            CreateMap<CreateEquipmentDto, Equipment>();
            CreateMap<UpdateEquipmentDto, Equipment>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<CreateCategoryDto, Category>();

            CreateMap<Rental, RentalDto>()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details))
                .ReverseMap();

            CreateMap<CreateRentalDto, Rental>()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details));

            CreateMap<UpdateRentalDto, Rental>();

            CreateMap<RentalDetail, RentalDetailDto>().ReverseMap();
            CreateMap<CreateRentalDetailDto, RentalDetail>();

            CreateMap<RentalStatus, RentalStatusDto>().ReverseMap();

            CreateMap<Feedback, FeedbackDto>();
            CreateMap<FeedbackDto, Feedback>();

            CreateMap<RentalStatus, RentalStatusDto>().ReverseMap();

        }
    }
}
