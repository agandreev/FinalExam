using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreLibrary;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;

/* Выполнил: Андреев Аркадий
 * Группа: БПИ195
 * Вариант: 1
 * Альтернативные решения:
 * 1) сравнение даблов через Epsilon
 * 2) CompareTo через dynamic
 * 3) генерация имени через интервалы (получится равновероятнее)
 */

namespace FirstTask
{
    class Program
    {
        const int MAX_VALUE = 100000; //верхняя граница кол-ва книг
        const string PATH_SER = @"../../../books.json"; // path

        static readonly Random rnd = new Random();
        static void Main()
        {
            do
            {
                int n; //ввод кол-ва книг
                do
                {
                    Console.WriteLine("Enter books quantity.");
                } while (!int.TryParse(Console.ReadLine(), out n) || (n < 0) || (n > MAX_VALUE));

                Bookstore<Product> books = CreateBooks(n); //создание книг
                books.Add(new Product(1, "Товар1")); 
                PrintBooks(books);
                Serialization(books);

                Console.WriteLine("Enter ESC to exit programm...");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        /// <summary>
        /// Создание имени
        /// </summary>
        /// <param name="n">длинна имени</param>
        /// <returns>строка</returns>
        static string CreateName(int n)
        {
            string name = "";
            for (int i = 0; i < n; i++)
            {
                switch (rnd.Next(3))
                {
                    case 0:
                        name += (char)rnd.Next('A', 'Z' + 1);
                        break;
                    case 1:
                        name += (char)rnd.Next('a', 'z' + 1);
                        break;
                    case 2:
                        name += " ";
                        break;
                }
            }  
            return name;
        }

        /// <summary>
        /// Создание магазина книг
        /// </summary>
        /// <param name="n">кол-во книг</param>
        /// <returns>магазин книг</returns>
        static Bookstore<Product> CreateBooks(int n)
        {
            Bookstore<Product> books = new Bookstore<Product>();

            for (int i = 0; i < n; i++)
            {
                try
                {
                    books.Add(CreateBook());
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine($"Проблемы с создание книги: {e.Message}");
                    i--;
                    continue;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Проблемы с создание книги: {e.Message}");
                    i--;
                    continue;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Проблемы с создание книги: {e.Message}");
                    i--;
                    continue;
                }
            }
            return books;
        }

        /// <summary>
        /// Создание книги
        /// </summary>
        /// <returns>книга</returns>
        static Book CreateBook()
        {
            double price = rnd.Next(0, 20) + rnd.NextDouble(); // [0, 20)
            short numberOfPages = (short)rnd.Next(0, 701);// [0, 700]
            short year = (short)rnd.Next(1980, 2030); //[1980, 2030)
            string title = CreateName(rnd.Next(3, 16));//[3, 15]
            double rating = rnd.Next(-2, 7) + rnd.NextDouble(); //[-2; 7)

            return new Book(price, title, numberOfPages, year, rating);
        }

        /// <summary>
        /// Вывод книг
        /// </summary>
        /// <param name="books">книги</param>
        static void PrintBooks(Bookstore<Product> books)
        {
            foreach (Product book in books)
            {
                Console.WriteLine(book);
            }
        }

        /// <summary>
        /// Сериализация
        /// </summary>
        /// <param name="books">книги</param>
        static void Serialization(Bookstore<Product> books)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Bookstore<Product>));
                using (FileStream fs = new FileStream(PATH_SER, FileMode.Create))
                {
                    ser.WriteObject(fs, books);
                }
                Console.WriteLine("Успешная сериализация");
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка при записи в файл");
                return;
            }
            catch (SerializationException e)
            {
                Console.WriteLine($"Ошибка при десериализации: {e.Message}");
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Произошла непредвиденная ошибка: {e.Message}");
                return;
            }
        }
    }
}
