using System;

namespace TechnProcessMonitoring.BL.Model
{
    [Serializable]
    public class User
    {
        #region Свойства
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
        #endregion
        public User(string name, string secondName, string login, string password)
        {
            #region Проверка условий
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Имя не может быть пустым или null", nameof(name));
            }
            if (string.IsNullOrWhiteSpace(secondName))
            {
                throw new ArgumentNullException("Фамилия не может быть пустым или null", nameof(secondName));
            }
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentNullException("Логин не может быть пустым или null", nameof(login));
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("Пароль не может быть пустым или null", nameof(login));
            }
            #endregion
            Name = name;
            SecondName = secondName;
            Login = login;
            Password = password;
        }

        public User(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentNullException("Логин не может быть пустым или null", nameof(login));
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("Пароль не может быть пустым или null", nameof(login));
            }

            Login = login;
            Password = password;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
