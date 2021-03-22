using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

/*
 * 1.Необходимо создать программу по регистрации и авторизации пользователя. 
 * Пользователь может авторизироваться, регистрироваться, и выходит из учетки, если авторизирован. 
 * Решить с использованием Dictionary.
*/

namespace Ex1
{
    internal static class Program
    {
        private static Dictionary<string, string> _users = new Dictionary<string, string>();
        private static bool _isLogin;

        static void Main()
        {
            while (true)
                ChooseOperation();
        }

        private static void ChooseOperation()
        {
            WriteLine("Система пользователей. Выберете действие:\n 1. Регистрация\n 2. Авторизация. \n 3. Выход");
            var selectedOperation = ReadLine();

            switch (selectedOperation)
            {
                case "1": Registration(); break;
                case "2": Login(); break;
                case "3": Logout(); break;
                default: throw new ArgumentException($"Недопустимая операция {selectedOperation}");
            }
        }

        private static void Registration()
        {
            if (!_isLogin)
            {
                WriteLine("Введите логин:");
                var login = ReadLine();
                WriteLine("Введите пароль:");
                var password = ReadLine();
                WriteLine(_users.TryAdd(login, password)
                    ? $"Пользователь {login} успешно зарегестрирован"
                    : $"Пользователь {login} уже зарегестрирован в системе!");
            }
            else
                WriteLine("Нельзя зарегистрироваться, вы уже авторизованы в системе");
        }

        private static void Login()
        {
            if (!_isLogin)
            {
                WriteLine("Введите логин:");
                var login = ReadLine();
                
                if (!ValidateLogin(ref login))
                    return;

                WriteLine("Введите пароль");
                var password = ReadLine();
                
                if (!ValidatePassword(login, password))
                    return;

                WriteLine($"Вы зашли в систему как {login}.");
                _isLogin = true;
            }
            else
                WriteLine("Вы уже авторизованы в системе");
        }

        private static void Logout()
        {
            if (_isLogin)
            {
                WriteLine("Вы вышли из системы!");
                _isLogin = false;
            }
            else
                WriteLine("Вы не авторизованы в системе, и не можете выйти!");
        }

        private static bool ValidateLogin(ref string login)
        {
            while (!IsLoginExist(login))
            {
                WriteLine("Хотите попробовать еще раз? да/нет");
                
                if (ReadLine()?.ToLower() == "да")
                {
                    WriteLine("Введите логин:");
                    login = ReadLine();
                }
                else
                    return false;
            }
            return true;
        }

        private static bool IsLoginExist(string login)
        {
            var result = _users.Keys.Any(key => key.Equals(login));

            if (!result)
                WriteLine($"Пользователя с именем {login} нет в системе.");

            return result;
        }

        private static bool ValidatePassword(string login, string password)
        {
            while (!IsPasswordCorrect(login, password))
            {
                WriteLine("Хотите попробовать еще раз? да/нет");

                if (ReadLine()?.ToLower() == "да")
                {
                    WriteLine("Введите пароль:");
                    password = ReadLine();
                }
                else
                    return false;
            }

            return true;
        }

        private static bool IsPasswordCorrect(string login, string password)
        {
            var result = _users[login].Equals(password);

            if (!result)
                WriteLine("Неправильный пароль.");

            return result;
        }
    }
}
