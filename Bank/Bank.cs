using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Bank
{
    class Bank
    {
        private static int ACCOUNT_ID = 1234;
        public string BankName;
        private ArrayList AccountList = new ArrayList();
        private ArrayList UserList = new ArrayList();

        public Bank(string bankName)
        {
            BankName = bankName;
        }

        public int GetFreeId()
        {
            return ACCOUNT_ID++;
        }

        public Account FindAccount(int id)
        {
            foreach (Account a in AccountList)
            {
                if (a.GetAccId() == id)
                    return a;
            }
            return null;
        }

        public void AddUserToList(User u)
        {
            UserList.Add(u);
        }
        public void AddAccountToList(Account a)
        {
            AccountList.Add(a);
        }
        public User Auth(string login, string password)
        {
            foreach (User u in UserList)
            {
                if (u.Auth(login, password))
                    return u;
            }
            return null;
        }

        public bool Block(string login)
        {
            foreach (User u in UserList)
            {
                if (u.GetUsername() == login)
                {
                    u.Ban();
                    Console.WriteLine("Pomyślnie zablokowano");
                    return true;
                }
            }
            Console.WriteLine("Błąd");
            return false;
        }

        public void AddAccount()
        {
            Console.WriteLine("Podaj imię: ");
            string name = Console.ReadLine();
            Console.WriteLine("Podaj nazwisko: ");
            string surname = Console.ReadLine();
            Console.WriteLine("Podaj login: ");
            string login = Console.ReadLine();
            Console.WriteLine("Podaj hasło: ");
            string pass = Console.ReadLine();
            new User(name, surname, login, pass, this);
        }

        public void DelAccount(string login)
        {
            foreach (User u in UserList)
            {
                if (u.GetUsername() == login)
                {
                    UserList.Remove(u);
                    break;
                }
            }
        }

        public void GenerateBankSummary()
        {
            decimal AllCash = 0, AllCredit = 0;
            foreach (Account a in AccountList)
            {
                AllCash += a.GetCash();
                AllCredit += a.GetCredit();
            }
            Console.WriteLine("Kwota pieniędzy w banku: " + AllCash + "zł");
            Console.WriteLine("Kwota pożyczonych pieniędzy: " + AllCredit + "zł");
        }

        public void CheckUserHistory(string username)
        {
            string[] files = Directory.GetFiles(username + "/");
            foreach (string file in files)
            {
                string[] lines = File.ReadAllLines(file);
                Console.WriteLine(file);
                foreach (string line in lines)
                {
                    Console.WriteLine(" " + line);
                }
            }
        }

        public void CheckDebtorsList()
        {
            Console.WriteLine("Aktualni dłużnicy:");
            foreach (Account a in AccountList)
            {
                if (a.GetCredit() > 0)
                    a.GetOwner().GetSurname();
            }
        }
    }
}
