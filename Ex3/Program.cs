using System;
using System.Collections.Generic;

/*
* 3.Создать телефонную книгу, в которую пользователь может добавлять контакты, 
* удалять контакты, обновлять контакты, и выводить все контакты в виде имен и их номеров телефонов, 
* либо выводить какой-то один номер для определенно контакта.
* Необходимо использовать Dictionary при решении данной задачи. 
* Учесть все негативные сценарии в связи с работой с Dictionary. Программа должна работать циклично.
*/

namespace Ex3
{
    internal class Program
    {
        private static Dictionary<string, string> _contactBook = new Dictionary<string, string>();
        static void Main()
        {
            while (true)
                ChooseOperation();
        }

        private static void ChooseOperation()
        {
            Console.WriteLine("Телефонная книга.\n Выберете действие:\n 1. Добавить контакт\n " +
                "2. Обновить контакт\n 3. Отобразить контакт\n 4. Отобразить все контакты");
            var selectedOperation = Console.ReadLine();
            switch (selectedOperation)
            {
                case "1": AddContact(); break;
                case "2": EditContact(); break;
                case "3": ShowOneContact(); break;
                case "4": ShowAllContacts(); break;
                default: throw new ArgumentException($"Недопустимая операция {selectedOperation}");
            }
        }

        private static void ShowAllContacts()
        {
            if (_contactBook.Count > 0)
                foreach (var (key, value) in _contactBook)
                    Console.WriteLine(key + ": " + value);

            else Console.WriteLine("В Книге нет контактов");
        }

        private static void ShowOneContact()
        {
            Console.WriteLine("Введите имя:");
            var name = Console.ReadLine();

            if (_contactBook.ContainsKey(name))
            {
                _contactBook.TryGetValue(name, out var phone);
                Console.WriteLine(name + ": " + phone);
            }
            else Console.WriteLine("Нет контакта с таким именем");
        }

        private static void EditContact()
        {
            Console.WriteLine("Введите имя:");
            var name = Console.ReadLine();

            if (_contactBook.ContainsKey(name))
            {
                Console.WriteLine($"Введите новый номер для контакта {name}");
                _contactBook[name] = Console.ReadLine();
                Console.WriteLine($"Контакт с именем {name} переписан");
            }
            else Console.WriteLine("Нет контакта с таким именем");
        }

        private static void AddContact()
        {
            Console.WriteLine("Введите имя:");
            var name = Console.ReadLine();
            Console.WriteLine("Введите номер телефона:");
            var phone = Console.ReadLine();
            Console.WriteLine(_contactBook.TryAdd(name, phone)
                ? $"Контакт {name} успешно записан в книгу"
                : $"Контакт {name} уже записан в книгу");
            
        }
    }
}
