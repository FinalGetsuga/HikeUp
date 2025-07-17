using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IT_Project.Models
{
    public class ShoppingCart
    {
        [Key]
        public int ShoppingCartId { get; set; }
        public string UserId { get; set; }

        public virtual ICollection<Product> products { get; set; }

        public ShoppingCart()
        {
            products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            
                products.Remove(product);
            
        }

        public ICollection<Product> GetProducts()
        {
            return products;
        }

        public decimal CalculateTotal()
        {
            return products.Sum(p => p.Price);
        }

        public void ClearCart()
        {
            products.Clear();
        }
    }
}