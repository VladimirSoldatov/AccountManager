using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager
{
    enum Users
    {
        Гость, Пользователь, Администратор
    };
    internal class PasswordManager
    {

        Dictionary<string, int> _password;
        Dictionary<int, List<string>> _mandatory;
        string _username;
        int _mandatoryValue;
        public int MandatoryStatus()
        {
            return _mandatoryValue;
        }
        public string getUserName()
        {
            return _username;
        }
        public PasswordManager(string pathToFile)
        {
            _password = new Dictionary<string, int>();
            _mandatory = new Dictionary<int, List<string>>();
            _mandatory.Add((int)Users.Гость, new List<string>());
            _mandatory.Add((int)Users.Пользователь, new List<string>());
            _mandatory.Add((int)Users.Администратор, new List<string>() {"admin"});
            
            if (!File.Exists(pathToFile))
                File.Create(pathToFile);
            OpenPasswordFile(pathToFile);
        }
        public int GetMandatory(string login)
        {
            foreach(KeyValuePair<int, List<string>> keyValuePair in _mandatory)
            {
                if (keyValuePair.Value.Contains(login))
                    return keyValuePair.Key;
            }
            _mandatory[0].Add(login);
            return 0;
        }
        void OpenPasswordFile(string pathToFile)
        {
            using (StreamReader sr = new StreamReader(pathToFile))
            {
                string line;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    line = line.Trim();
                    string[] keyValue = line.Split(':');
                    if (keyValue.Length < 1)
                    {
                        return;
                    }
                    _password.Add(keyValue[0], int.Parse(keyValue[1]));
                }
            }
            
        }
        (bool, string) GetAuthority(string login, string password)
        {
            login = login.ToLower();
            if(_password.ContainsKey(login.ToLower())&&_password[login.ToLower()] == password.GetHashCode())
            {

                    return (true, login);
              

            }
            else if (_password.ContainsKey(login.ToLower()) &&_password[login] != password.GetHashCode())
            {
                throw new Exception("Пользователь не существует!");

            }
            else
            {
                return (false, login);
            }
        }
       void Message((bool, string) result)
        {
            bool auth;
            string login;
            (auth, login) = result;
            if(auth)
            {
                Console.WriteLine($"Привет пользователь {login}");
  
            }
            else
            {
                Console.WriteLine($"Извените, {login}, пользователь не существует ");
            }
        }
        public void Authentication(string login, string password)
        {
            try
            {
                Message(GetAuthority(login, password));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            _mandatoryValue = GetMandatory(login);
            switch ((Users)_mandatoryValue)
            {
                case Users.Гость:
                    Console.WriteLine("Гостевой доступ предоставлен");
                    break;
                case Users.Пользователь:
                    Console.WriteLine("Пользовательский доступ предоставлен");
                    break;
                case Users.Администратор:
                    Console.WriteLine("Адмистраторский доступ");
                    break;
                default:
                    Console.WriteLine("Ошибка!");
                    break;
            }
            _username = login;
        }
    }
}
