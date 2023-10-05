using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;

        private readonly IProductReadRepository _productReadRepository;

        public ProductController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }



        [HttpGet]
        public async Task Get()
        {
           await _productWriteRepository.AddRangeAsync(new()
            {
                new() { Id = Guid.NewGuid(), Name = "Product 1", Price = 100, CreatedDate = DateTime.Now, Stock = 10 },
                new() {Id = Guid.NewGuid(), Name= "Product 2", Price = 200, CreatedDate = DateTime.Now, Stock = 20},
                new() {Id = Guid.NewGuid(), Name= "Product 3", Price = 300, CreatedDate = DateTime.Now, Stock = 30}
            });
            
            var count = await _productWriteRepository.SaveAsync();
 
        }
    }
}
