using System;
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
            #region Проверки
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
            #endregion
            CurrentUser = new User(name, secondName, login, password);
            // Save(CurrentUser);
        }

        /// <summary>
        /// Существующий пользователь
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public UserController(string login, string password)
        {
            #region Проверки
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentNullException("Логин не может быть пустым", nameof(login));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("Пароль не может быть пустым", nameof(password));
            }
            #endregion
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
            RegistryKey subAuth = null;
            RegistryKey keyAuth = _currentUserKey.OpenSubKey("keyAuth", true);
            if (keyAuth != null)
            {
                subAuth = keyAuth.OpenSubKey(user.Login, true);
                if (subAuth != null && (subAuth.GetValue("login").ToString() == user.Login) && (subAuth.GetValue("password").ToString() == user.Password))
                {
                    return true;
                }
            }
            return false;
        }
            

        /// <summary>
        /// Сохранение пользователя
        /// </summary>
        public void Save(User user)
        {
            _currentUserKey = Registry.CurrentUser;
            var keyAuth = _currentUserKey.CreateSubKey("KeyAuth");
            var curentKey = keyAuth.CreateSubKey(user.Login);
            curentKey.SetValue("Name", user.Name);
            curentKey.SetValue("SecondName", user.SecondName);
            curentKey.SetValue("login", user.Login); 
            curentKey.SetValue("password", user.Password);
            keyAuth.Close();
            curentKey.Close();
        }
        
        
    }
}
