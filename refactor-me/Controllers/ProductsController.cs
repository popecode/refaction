using System;
using System.Collections.Generic;
using System.Web.Http;
using refactor_me.Data;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private readonly IRepository _repository;

        public ProductsController(IRepository repository)
        {
            _repository = repository;
        }

        [Route]
        [HttpGet]
        public object GetAllProducts()
        {
            return new { Items = _repository.GetAllProducts() };
        }

        [Route]
        [HttpGet]
        public object GetProductsbyName(string name)
        {
            return new { Items = _repository.GetProductsbyName(name) };
        }

        [Route("{id}")]
        [HttpGet]
        public List<Product> GetProductbyId(Guid id)
        {
            return _repository.GetProductbyId(id);
        }

        [Route]
        [HttpPost]
        public void CreateNewProduct(Product newProduct)
        {
            _repository.CreateProduct(newProduct);
        }

        [Route("{id}")]
        [HttpPut]
        public void UpdateProductById(Guid id, Product newProduct)
        {
            _repository.UpdateProductById(id, newProduct);
        }

        [Route("{id}")]
        [HttpDelete]
        public void DeleteProductbyId(Guid id)
        {
            _repository.DeleteProductById(id);
        }

        [Route("{productId}/options")]
        [HttpGet]
        public object GetAllProductOptionsByProductId(Guid productId)
        {
            return new { Items = _repository.GetAllProductOptionsByProductId(productId) };
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public List<ProductOption> GetProductOptionByIdAndProductId(Guid productId, Guid id)
        {
            return _repository.GetProductOptionByIdAndProductId(productId, id);
        }

        [Route("{productId}/options")]
        [HttpPost]
        public void CreateNewProductOptionForProductById(Guid productId, ProductOption option)
        {
            _repository.CreateNewProductOptionForProductById(productId, option);
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateProductOptionByProductId(Guid productId, Guid id, ProductOption option)
        {
            _repository.UpdateProductOptionByProductId(productId, id, option);
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteProductOptionByIdAndProductId(Guid productId, Guid id)
        {
            _repository.DeleteProductOptionByIdAndProductId(productId, id);
        }
    }
}
