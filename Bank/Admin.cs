using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    class Admin : User
    {
        public Admin(string name, string surname, string username, string password, Bank bank) : base(name, surname, username, password, bank)
        {
            BankRecords.Add(new Account(bank.GetFreeId(), this, bank));
        }
}
}
