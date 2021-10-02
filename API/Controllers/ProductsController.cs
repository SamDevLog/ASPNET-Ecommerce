using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    public class ProductsController : BaseApiController {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<ProductBrand> productBrandRepo;
        private readonly IGenericRepository<ProductType> productTypesRepo;
        private readonly IMapper mapper;

        public ProductsController (IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypesRepo, IMapper mapper) {

            this.productRepo = productRepo;
            this.productBrandRepo = productBrandRepo;
            this.productTypesRepo = productTypesRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<ProductToReturnDto>>> GetProducts () {
            var spec = new ProductsWithTypesAndBrandsSpecification ();
            var products = await productRepo.ListAsync (spec);
            return Ok (mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>> (products));
        }

        [HttpGet ("{id}")]
        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType (typeof (ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ProductToReturnDto>> GetProduct (int id) {
            var spec = new ProductsWithTypesAndBrandsSpecification (id);
            var product = await productRepo.GetEntityWithSpec (spec);

            if (product == null) return NotFound (new ApiResponse (404));

            return mapper.Map<Product, ProductToReturnDto> (product);
        }

        [HttpGet ("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes () {
            var types = await productTypesRepo.ListAllAsync ();
            return Ok (types);
        }

        [HttpGet ("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands () {
            var brands = await productBrandRepo.ListAllAsync ();
            return Ok (brands);
        }
    }
}