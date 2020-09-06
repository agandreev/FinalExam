using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BookstoreLibrary
{
    [DataContract]
    public class Product : IComparable<Product>
    {
        private double price; //цена
        private string title; //название

        /// <summary>
        /// Конструктор продукта
        /// </summary>
        /// <param name="price">цена</param>
        /// <param name="title">название</param>
        public Product(double price, string title)
        {
            Price = price;
            Title = title;
        }

        [DataMember]
        //св-во цены с проверкой 
        public double Price
        {
            get => price;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Price должeн быть положительной.");
                }
                price = value;
            }
        }

        [DataMember]
        //св-во названия с проверко
        public string Title
        {
            get => title;
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new NullReferenceException("Title не должен быть пустым или null.");
                }
                title = value;
            }
        }

        /// <summary>
        /// явное преобразование
        /// </summary>
        /// <param name="product">продукт</param>
        public static explicit operator double(Product product) =>
             product.Price;

        /// <summary>
        /// сравнение
        /// </summary>
        /// <param name="other">продукт</param>
        /// <returns>1 0 -1</returns>
        public int CompareTo(Product other)
        {
            if ((double)this > (double)other)
            {
                return 1;
            }
            if ((double)this > (double)other)
            {
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// переопределнный 
        /// </summary>
        /// <returns>строка</returns>
        public override string ToString() =>
            $"Price = ${Price:F2}.";

    }
}
