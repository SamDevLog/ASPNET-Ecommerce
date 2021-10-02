using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers {
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string> {
        private readonly IConfiguration configuration;

        public ProductUrlResolver (IConfiguration configuration) {
            this.configuration = configuration;
        }

        public string Resolve (Product source, ProductToReturnDto destination, string destMember, ResolutionContext context) {

            if (!string.IsNullOrEmpty (source.PictureUrl)) {
                return configuration["ApiUrl"] + source.PictureUrl;
            }

            return null;
        }
    }
}