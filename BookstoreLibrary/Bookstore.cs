using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BookstoreLibrary
{
    [DataContract, KnownType(typeof(Product)), KnownType(typeof(Book))]
    public class Bookstore<T> : IEnumerable<T> where T : Product
    {
        [DataMember]
        private List<T> items; //лист продуктов

        /// <summary>
        /// конструктор
        /// </summary>
        public Bookstore()
        {
            items = new List<T>();
        }

        /// <summary>
        /// Добавить элемент в лист
        /// </summary>
        /// <param name="item">Продукт</param>
        public void Add(T item)
        {
            if (item == null)
            {
                throw new NullReferenceException("Item is null.");
            }
            items.Add(item);
        }

        /// <summary>
        /// Возвращает енумератор
        /// </summary>
        /// <returns>енумератор</returns>
        public IEnumerator<T> GetEnumerator() =>
            items.GetEnumerator();

        /// <summary>
        /// Возвращает енумератор
        /// </summary>
        /// <returns>енумератор</returns>
        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}
