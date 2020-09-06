using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BookstoreLibrary
{
    [DataContract, KnownType(typeof(Product))]
    public class Book : Product
    {
        private short numberOfPages, year; //кол-во страниц и год
        private double rating; //рейтинг

        /// <summary>
        /// Конструктор книги
        /// </summary>
        /// <param name="price">цена</param>
        /// <param name="title">название</param>
        /// <param name="numberOfPages">кол-во страниц</param>
        /// <param name="year">год</param>
        /// <param name="rating">рейтинг</param>
        public Book(double price, string title, short numberOfPages, short year, double rating) 
            : base(price, title)
        {
            NumberOfPages = numberOfPages;
            Year = year;
            Rating = rating;
        }

        [DataMember]
        //св-во кол-ва страниц с проверкой
        public short NumberOfPages
        {
            get => numberOfPages;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("NumberOfPages должен быть положительным.");
                }
                numberOfPages = value;
            }
        }

        [DataMember]
        //св-во года выпуска с проверкой
        public short Year
        {
            get => year;
            set
            {
                if (value < 1990 || value > 2020)
                {
                    throw new ArgumentException("Year должен принадлежать этому диапозону [1990, 2020].");
                }
                year = value;
            }
        }

        [DataMember]
        //св-во рейтинга с проверкой
        public double Rating
        {
            get => rating;
            set
            {
                if (value < 0 || value >= 5) 
                {
                    throw new ArgumentException("Rating должен принадлежать этому диапазону [0, 5).");
                }
                rating = value;
            }
        }

        /// <summary>
        /// Краткая информация
        /// </summary>
        /// <returns>строка</returns>
        public string GetShortInfo() =>
            $"{NumberOfPages}.{Year}.{Title.Distinct().ToList().Count}." +
            $"{Math.Round(Rating * 100)}";

        /// <summary>
        /// переопределение
        /// </summary>
        /// <returns>строка</returns>
        public override string ToString() =>
            $"{base.ToString()} NumberOfPages: {NumberOfPages}. Year: {Year}. Rating: {Rating:F2}. " +
            $"{GetShortInfo()}";
    }
}
