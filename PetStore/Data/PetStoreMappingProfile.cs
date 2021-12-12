using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PetStore.Data.Entities;
using PetStore.Data.ViewModels;

namespace PetStore.Data
{
    public class PetStoreMappingProfile : Profile
    {
        public PetStoreMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(o => o.OrderId, ex => ex.MapFrom(i => i.Id))
                .ReverseMap();
            CreateMap<OrderItem, OrderItemViewModel>()
                 .ReverseMap()
                 .ForMember(m => m.Product, opt => opt.Ignore());

            CreateMap<StoreUser, RegisterViewModel>()
                .ReverseMap();
            CreateMap<Pets, Product>()
                .ForMember(dest => dest.Category, src => src.MapFrom(src => src.Breeds[0].BreedGroup))
                .ForMember(dest => dest.Title, src => src.MapFrom(src => src.Breeds[0].Name))
                .ForMember(dest => dest.ImageUrl, src => src.MapFrom(src => src.Url))
                .ForMember(dest => dest.ImageUrl, src => src.MapFrom(src => src.Url))
                .ForMember(dest => dest.Description, src => 
                    src.MapFrom(src => src.Breeds[0].BredFor + " " + src.Breeds[0].Temperament + " , life span: " + src.Breeds[0].LifeSpan))
                .ReverseMap();





        }
    }
}
