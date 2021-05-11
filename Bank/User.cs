using System;
using System.Collections;
using System.Text;


namespace Bank
{
    class User
    {
        private string Name, Surname, Username, Password;
        private bool IsBlocked;
        protected ArrayList BankRecords = new ArrayList();
        public User(string name, string surname, string username, string password, Bank bank)
        {
            Name = name;
            Surname = surname;
            Username = username;
            Password = password;
            IsBlocked = false;
            bank.AddUserToList(this);
            BankRecords.Add(new Account(bank.GetFreeId(), this,bank));
        }

        public int GetAccNumbers()
        {
            return BankRecords.Count;
        }

        public Account GetAcc(int AccountNumber)
        {
            return (Account)BankRecords[AccountNumber];
        }

        public string GetUsername()
        {
            return Username;
        }

        public void GetSurname()
        {
            Console.WriteLine(Name + " " + Surname);
        }

        public bool Auth(string login, string password)
        {
            if (this.IsBlocked == true)
                return false;
            if (this.Username == login)
                if (this.Password == password)
                    return true;
            return false;
        }

        public void ListBankRecords() 
        {
            int i = 1;
            Console.WriteLine("-------------------");
            foreach (Account acc in BankRecords)
            {
                Console.WriteLine("Rachunek nr " + i++);
                acc.AccDetails();
            }
            Console.WriteLine("-------------------");
        }

        public void Ban()
        {
            IsBlocked = true;
        }
        public void NewAccount(Bank bank) 
        {
            BankRecords.Add(new Account(bank.GetFreeId(), this, bank));
        }
    }
}
