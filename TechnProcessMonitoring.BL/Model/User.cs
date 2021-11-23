﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnProcessMonitoring.BL.Model
{
    class User
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string SecondName { get; private set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; private set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        ///  Полное имя
        /// </summary>
        public string FullName
        {
            get
            {
                return $"{SecondName} {Name}.";
            }
        }

        public User(string name, string secondName, string login, string password)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (string.IsNullOrWhiteSpace(secondName))
            {
                throw new ArgumentNullException(nameof(secondName));
            }
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentNullException(nameof(login));
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(nameof(login));
            }

            Name = name;
            SecondName = secondName;
            Login = login;
            Password = password;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}