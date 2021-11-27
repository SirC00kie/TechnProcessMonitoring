using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using TechnProcessMonitoring.BL.Model;
using Microsoft.Win32;

namespace TechnProcessMonitoring.BL.Controller
{
    public class UserController
    {
        private RegistryKey _currentUserKey;

        /// <summary>
        /// Пользователь
        /// </summary>
        public User CurrentUser { get; }

        public UserController() { }

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public UserController(string name, string secondName, string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentNullException("Логин не может быть пустым", nameof(login));
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("Пароль не может быть пустым", nameof(password));
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Имя не может быть пустым", nameof(name));
            }
            if (string.IsNullOrWhiteSpace(secondName))
            {
                throw new ArgumentNullException("Фамилия не может быть пустым", nameof(secondName));
            }

            CurrentUser = new User(name, secondName, login, password);
            Save(CurrentUser);
        }

        /// <summary>
        /// Существующий пользователь
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public UserController(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentNullException("Логин не может быть пустым", nameof(login));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("Пароль не может быть пустым", nameof(password));
            }
            
            CurrentUser = new User(login,password);
        }

        /// <summary>
        /// Получить данные пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Load(User user)
        {
            _currentUserKey = Registry.CurrentUser;
            RegistryKey keyAuth = _currentUserKey.OpenSubKey("keyAuth", true);
            if ((keyAuth.GetValue("login").ToString() == user.Login) && (keyAuth.GetValue("password").ToString() == user.Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Сохранение пользователя
        /// </summary>
        public void Save(User user)
        {
            _currentUserKey = Registry.CurrentUser;
            var keyAuth = _currentUserKey.CreateSubKey("KeyAuth");
            keyAuth.SetValue("Name", user.Name);
            keyAuth.SetValue("SecondName", user.SecondName);
            keyAuth.SetValue("login", user.Login);
            keyAuth.SetValue("password", user.Password);
            keyAuth.Close();
        }
        
        
    }
}
