using System;
using System.Collections.Generic;
using System.Linq;

namespace refactor_me.Data
{
    public interface IRepository
    {
        List<Product> GetAllProducts();
        List<Product> GetProductsbyName(string name);
        List<Product> GetProductbyId(Guid id);
        void CreateProduct(Product newProduct);
        void UpdateProductById(Guid id, Product newProduct);
        void DeleteProductById(Guid id);
        List<ProductOption> GetAllProductOptionsByProductId(Guid id);
        List<ProductOption> GetProductOptionByIdAndProductId(Guid productId, Guid id);
        void CreateNewProductOptionForProductById(Guid productId, ProductOption option);
        void UpdateProductOptionByProductId(Guid productId, Guid prodcutOptionId, ProductOption option);
        void DeleteProductOptionByIdAndProductId(Guid productId, Guid prodcutOptionId);
    }

    public class Repository : IRepository
    {
        public List<Product> GetAllProducts()
        {
            using (var context = new ProductContext())
                return context.Products.ToList();
        }

        public List<Product> GetProductsbyName(string name)
        {
            using (var context = new ProductContext())
            {
                return context.Products
                    .Where(product => product.Name.Contains(name)).ToList();
            }
        }

        public List<Product> GetProductbyId(Guid id)
        {
            using (var context = new ProductContext())
            {
                return context.Products
                    .Where(product => product.Id == id).ToList();
            }
        }

        public void CreateProduct(Product newProduct)
        {
            if (newProduct == null) throw new ArgumentNullException(nameof(newProduct));

            using (var context = new ProductContext())
            {
                var currentProduct = context.Products.SingleOrDefault(product =>
                    product.Id == newProduct.Id);

                if (currentProduct != (null))
                {
                    throw new InvalidOperationException("Product already existis");
                }

                context.Products.Add(new Product
                {
                    Id = newProduct.Id,
                    DeliveryPrice = newProduct.DeliveryPrice,
                    Description = newProduct.Description,
                    Price = newProduct.Price,
                    Name = newProduct.Name
                });

                context.SaveChanges();
            }
        }

        public void UpdateProductById(Guid id, Product newProduct)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (newProduct == null) throw new ArgumentNullException(nameof(newProduct));
            
            using (var context = new ProductContext())
            {
                var currentProduct = context.Products.SingleOrDefault(product =>
                    product.Id == id);

                if (currentProduct == (null))
                {
                    throw new InvalidOperationException("Product does not exist, cannot update product");
                }

                currentProduct.Name = newProduct.Name;
                currentProduct.DeliveryPrice = newProduct.DeliveryPrice;
                currentProduct.Description = newProduct.Description;
                currentProduct.Price = newProduct.Price;

                context.SaveChanges();
            }
        }

        public void DeleteProductById(Guid id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            using (var context = new ProductContext())
            {
                var currentProduct = context.Products.SingleOrDefault(product =>
                    product.Id == id);

                if (currentProduct == (null))
                {
                    throw new InvalidOperationException($"Product {id} does not exist, cannot delete product");
                }

                context.Products.Remove(currentProduct);

                context.SaveChanges();
            }
        }

        public List<ProductOption> GetAllProductOptionsByProductId(Guid id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            using (var context = new ProductContext())
            {
                return context.ProductOptions
                    .Where(productOption => productOption.ProductId == id).ToList();

            }
        }

        public List<ProductOption> GetProductOptionByIdAndProductId(Guid productId, Guid id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (productId == null) throw new ArgumentNullException(nameof(productId));

            using (var context = new ProductContext())
            {
                return context.ProductOptions
                    .Where(productOption => productOption.ProductId == productId && productOption.Id == id).ToList();
            }
        }

        public void CreateNewProductOptionForProductById(Guid productId, ProductOption option)
        {
            if (productId == null) throw new ArgumentNullException(nameof(productId));
            if (option == null) throw new ArgumentNullException(nameof(option));

            using (var context = new ProductContext())
            {
                var currentProduct = context.Products.SingleOrDefault(product =>
                    product.Id == productId);

                if (currentProduct == null)
                {
                    throw new InvalidOperationException("Current product does not exist");
                }

                var currentProductOption = context.ProductOptions.SingleOrDefault(productOption =>
                    productOption.Id == productId);

                if (currentProductOption != null)
                {
                    throw new InvalidOperationException("Product Option already existis");
                }

                context.ProductOptions.Add(new ProductOption
                {
                    Id = option.Id,
                    ProductId = productId,
                    Name = option.Name,
                    Description = option.Description
                });

                context.SaveChanges();
            }
        }

        public void UpdateProductOptionByProductId(Guid productId, Guid prodcutOptionId, ProductOption option)
        {
            if (productId == null) throw new ArgumentNullException(nameof(productId));
            if (prodcutOptionId == null) throw new ArgumentNullException(nameof(prodcutOptionId));
            if (option == null) throw new ArgumentNullException(nameof(option));

            using (var context = new ProductContext())
            {
                var currentProduct = context.Products.SingleOrDefault(product =>
                    product.Id == productId);

                if (currentProduct == null)
                {
                    throw new InvalidOperationException("Current product does not exist");
                }

                var currentProductOption = context.ProductOptions.SingleOrDefault(productOption =>
                    productOption.Id == prodcutOptionId);

                if (currentProductOption == null)
                {
                    throw new InvalidOperationException("Product Option does not existis");
                }

                currentProductOption.Name = option.Name;
                currentProductOption.Description = option.Description;

                context.SaveChanges();
            }
        }

        public void DeleteProductOptionByIdAndProductId(Guid productId, Guid prodcutOptionId)
        {
            if (productId == null) throw new ArgumentNullException(nameof(productId));
            if (prodcutOptionId == null) throw new ArgumentNullException(nameof(prodcutOptionId));

            using (var context = new ProductContext())
            {
                var currentProduct = context.Products.SingleOrDefault(product =>
                    product.Id == productId);

                if (currentProduct == null)
                {
                    throw new InvalidOperationException("Current product does not exist");
                }

                var currentProductOption = context.ProductOptions.SingleOrDefault(productOption =>
                    productOption.Id == prodcutOptionId);

                if (currentProductOption == (null))
                {
                    throw new InvalidOperationException($"Product option {prodcutOptionId} does not exist, cannot delete product");
                }

                context.ProductOptions.Remove(currentProductOption);

                context.SaveChanges();
            }
        }
    }
}