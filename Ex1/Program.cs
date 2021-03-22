using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static System.Console;

/*
 * 1.Необходимо создать программу по регистрации и авторизации пользователя. 
 * Пользователь может авторизироваться, регистрироваться, и выходит из учетки, если авторизирован. 
 * Решить с использованием Dictionary.
 *
 * + к уже написаной программе добавить валидацию имени пользователя(нельзя использовать кириллицу и спец.символы),
 * также нужно расширить функционал для админа, чтобы у него была возможность редактировать(имя, пароль) и удалять пользователей.
*/

namespace Ex1
{
    internal static class Program
    {
        private static Dictionary<string, string> _users = new Dictionary<string, string> { { "admin", "admin" } };
        private static bool _isLogin;
        private static bool _isAdmin;

        static void Main()
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            while (true)
                if (_isAdmin == false)
                    ChooseOperation();
                else AdminChooseOperation();
        }

        private static void ChooseOperation()
        {
            WriteLine("Система пользователей. Выберете действие:\n 1. Регистрация.\n 2. Авторизация. \n 3. Выход.");
            var selectedOperation = ReadLine();

            switch (selectedOperation)
            {
                case "1": Registration(); break;
                case "2": Login(); break;
                case "3": Logout(); break;
                default: throw new ArgumentException($"Недопустимая операция {selectedOperation}");
            }
        }
        private static void AdminChooseOperation()
        {
            WriteLine("Система пользователей. Выберете действие:\n 1. Регистрация.\n " +
                      "2. Авторизация.\n 3. Удаление пользователя.\n 4. Редактирование пользователя. \n 5. Выход.");
            var selectedOperation = ReadLine();

            switch (selectedOperation)
            {
                case "1": Registration(); break;
                case "2": Login(); break;
                case "3": DeleteUser(); break;
                case "4": EditUser(); break;
                case "5": Logout(); break;
                default: throw new ArgumentException($"Недопустимая операция {selectedOperation}");
            }
        }

        private static void Registration()
        {
            if (!_isLogin)
            {
                WriteLine("Введите логин:");
                var login = ReadLine();
                if (ValidateName(login))
                {
                    WriteLine("Введите пароль:");
                    var password = ReadLine();
                    WriteLine(_users.TryAdd(login, password)
                        ? $"Пользователь {login} успешно зарегестрирован"
                        : $"Пользователь {login} уже зарегестрирован в системе!");
                }
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
                if (login == "admin")
                    _isAdmin = true;
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
                _isAdmin = false;
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

        private static bool ValidateName(string login)
        {
            var regex = new Regex("[a-zA-Z0-9]+");

            if (regex.IsMatch(login))
                return true;
            WriteLine("Логин не должен содержать кириллицу и спецсимволы");
            return false;

        }

        private static void DeleteUser()
        {
            WriteLine("Введите имя пользователя для удаления:");
            var login = ReadLine();
            if (_users.ContainsKey(login))
            {
                if (login != "admin")
                {
                    _users.Remove(login);
                    WriteLine($"Пользователь {login} удален");
                }
                else WriteLine("Нельзя удалить администратора!");
            }
            else
                WriteLine("Нет пользователя с таким именем");
        }

        private static void EditUser()
        {
            WriteLine("Введите имя пользователя, для его редактирования");
            var login = ReadLine();
            if (IsLoginExist(login))
            {
                WriteLine("Выберите действие:");
                WriteLine($"1. Поменять пароль пользователю {login}");
                WriteLine($"2. Поменять имя пользователя {login}");
                var selectedOperation = ReadLine();

                switch (selectedOperation)
                {
                    case "1": ChangePassword(login); break;
                    case "2": ChangePassword(login); break;
                    default: throw new ArgumentException($"Недопустимая операция {selectedOperation}");
                }
            }
            else
                WriteLine($"Пользователя с именем {login} нет в системе.");
        }

        private static void ChangePassword(string login)
        {
            WriteLine("Введите новый пароль");
            var newPassword = ReadLine();
            _users[login] = newPassword;
            Console.WriteLine($"Пароль пользователя {login} изменен");
        }
    }
}
