using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class ProductsController : ControllerBase {
        private readonly IProductRepository _repo;

        public ProductsController (IProductRepository repo) {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts () {
            return Ok (await _repo.GetProductsAsync ());
        }

        [HttpGet ("{ id}")]
        public async Task<ActionResult<Product>> GetProduct (int id) {
            return await _repo.GetProductByIdAsync (id);
        }

        [HttpGet ("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes () {
            return Ok (await _repo.GetProductTypesAsync ());
        }

        [HttpGet ("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands () {
            return Ok (await _repo.GetProductBrandsAsync ());
        }
    }
}