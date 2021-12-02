using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class Cart
    {
        public Dictionary<int, CartItem> Items { get; set; }

        public Cart()
        {
            Items = new Dictionary<int, CartItem>();
        }

        /// <summary>
        /// Количество объектов в корзине
        /// </summary>
        public int Count
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity);
            }
        }

        /// <summary>
        /// Цена
        /// </summary>
        public int Price
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity * item.Value.Product.ProductPrice);
            }
        }

        /// <summary>
        /// Добавление в корзину
        /// </summary>
        /// <param name="dish">добавляемый объект</param>
        public virtual void AddToCart(Product product)
        {
            // если объект есть в корзине
            // то увеличить количество
            if (Items.ContainsKey(product.ProductId))
                Items[product.ProductId].Quantity++;
            // иначе - добавить объект в корзину
            else
                Items.Add(product.ProductId, new CartItem
                {
                    Product = product,
                    Quantity = 1
                });
        }

        /// <summary>
        /// Удалить объект из корзины
        /// </summary>
        /// <param name="id">id удаляемого объекта</param>
        public virtual void RemoveFromCart(int id)
        {
            Items.Remove(id);
        }

        /// <summary>
        /// Очистить корзину
        /// </summary>
        public virtual void ClearAll()
        {
            Items.Clear();
        }
    }
}
