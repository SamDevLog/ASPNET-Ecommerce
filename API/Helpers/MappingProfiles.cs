using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers {
    public class MappingProfiles : Profile {
        public MappingProfiles () {
            CreateMap<Product, ProductToReturnDto> ()
                .ForMember (x => x.ProductBrand, d => d.MapFrom (s => s.ProductBrand.Name))
                .ForMember (x => x.ProductType, d => d.MapFrom (s => s.ProductType.Name))
                .ForMember (x => x.PictureUrl, d => d.MapFrom<ProductUrlResolver> ());
            CreateMap<Address, AddressDto> ().ReverseMap ();
            CreateMap<CustomerBasketDto, CustomerBasket> ();
            CreateMap<BasketItemDto, BasketItem> ();
        }
    }
}