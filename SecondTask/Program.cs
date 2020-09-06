using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreLibrary;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;

namespace SecondTask
{
    class Program
    {
        const string PATH_SER = @"../../../books.json"; //path
        static void Main()
        {
            do
            {
                Bookstore<Book> books = Deserialization(); //десериализация
                if (books is null) //если что-то пойдет не так
                {
                    continue;
                }

                PrintBooks(books); //вывод книг

                PrintLinq(FirstLinq(books)); //first linq printing
                SecondLinq(books); //with printing inside
                var linq3 = ThirdLinq(books); //third linq
                PrintLinq(linq3);//third linq printing
                Console.WriteLine($"Длинна: {linq3.Count()}");

                Console.WriteLine("Enter ESC to exit programm...");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        /// <summary>
        /// Десериализация
        /// </summary>
        /// <returns>список книг</returns>
        static Bookstore<Book> Deserialization()
        {
            Bookstore<Book> books = default(Bookstore<Book>);
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Bookstore<Book>));
                using (FileStream fs = new FileStream(PATH_SER, FileMode.Open))
                {
                    books = (Bookstore<Book>)ser.ReadObject(fs);
                }
                Console.WriteLine("Успешная десериализация");
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка при чтении из файла");
                return books;
            }
            catch (SerializationException e)
            {
                Console.WriteLine($"Ошибка при десериализации: {e.Message}");
                return books;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Произошла непредвиденная ошибка: {e.Message}");
                return books;
            }
            return books;
        }

        /// <summary>
        /// Вывод книг
        /// </summary>
        /// <param name="books">список книг</param>
        static void PrintBooks(Bookstore<Book> books)
        {
            foreach (Product book in books)
            {
                Console.WriteLine(book);
            }
        }

        /// <summary>
        /// Вывод линков
        /// </summary>
        /// <param name="products">книги</param>
        static void PrintLinq(IEnumerable<Book> products)
        {
            Console.WriteLine();
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }

        /// <summary>
        /// Первый линк
        /// </summary>
        /// <param name="books">книги</param>
        /// <returns>последовательность</returns>
        static IEnumerable<Book> FirstLinq(Bookstore<Book> books) =>
            books.Where(x => x.GetShortInfo().Length > 14).
            OrderByDescending(x => (double)x);

        /// <summary>
        /// второй линк с выводом
        /// </summary>
        /// <param name="books">книги</param>
        static void SecondLinq(Bookstore<Book> books)
        {
            var secondLinq = books.OrderBy(x => (double)x).GroupBy(x => (int)x.Rating).OrderBy(x => x.Key);
            Console.WriteLine();
            foreach (var key in secondLinq)
            {
                Console.WriteLine($"Key : {key.Key}");
                foreach (var item in key)
                {
                    Console.WriteLine($"\t\t{item}");
                }
            }
        }

        /// <summary>
        /// третий линк
        /// </summary>
        /// <param name="books">книги</param>
        /// <returns>последовательность</returns>
        static IEnumerable<Book> ThirdLinq(Bookstore<Book> books) =>
            books.Where(x => x is Book).Where(x => x.Year == books.Max(y => y.Year));
    }
}
