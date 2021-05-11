using System;
using System.IO;
using System.Threading.Tasks;

namespace Bank
{
    class Account
    {
        private int AccId;
        private decimal AccCash;
        private decimal AccCredit;
        private User Owner;

        public Account(int accId, User owner, Bank bank)
        {
            AccId = accId;
            AccCash = 0;
            AccCredit = 0;
            Owner = owner;
            bank.AddAccountToList(this);
        }

        public int GetAccId()
        {
            return AccId;
        }

        public decimal GetCash()
        {
            return AccCash;
        }

        public decimal GetCredit()
        {
            return AccCredit;
        }
        public User GetOwner()
        {
            return (User)Owner;
        }

        public void AccDetails() 
        {
            Console.WriteLine("ID konta: " + AccId);
            Console.WriteLine("Pieniądze na koncie: " + AccCash + " zł");
            Console.WriteLine("Kredyt: " + AccCredit + " zł");
        }

        public void Deposit(decimal cash)
        {
            AccCash += cash;
            Directory.CreateDirectory(Owner.GetUsername() + "/");
            using (StreamWriter file = new StreamWriter(Owner.GetUsername() + "/" + AccId + ".txt", append: true))
            {
                file.WriteLine(DateTime.Now + " Wpłacono " + cash + "zł");
            }
        }

        public void Withdraw(decimal cash)
        {
            AccCash -= cash;
            Directory.CreateDirectory(Owner.GetUsername() + "/");
            using (StreamWriter file = new StreamWriter(Owner.GetUsername() + "/" + AccId + ".txt", append: true))
            {
                file.WriteLine(DateTime.Now + " Wypłacono " + cash + "zł");
            }
        }

        public void CheckHistory()
        {
            string[] lines = File.ReadAllLines(Owner.GetUsername() + "/" + AccId + ".txt");
            foreach (string line in lines)
            {
                Console.WriteLine(" " + line);
            }
        }

        public void TakeCredit(decimal cash)
        {
            if (AccCredit == 0)
            {
                AccCredit += cash;
                AccCash += cash;
                Directory.CreateDirectory(Owner.GetUsername() + "/");
                using (StreamWriter file = new StreamWriter(Owner.GetUsername() + "/" + AccId + ".txt", append: true))
                {
                    file.WriteLine(DateTime.Now + " Wzięto kredyt za " + cash + "zł");
                }
            }
            else
                Console.WriteLine("Już masz kredyt!");
        }

        public void PayCredit(decimal cash)
        {
            if (AccCredit > 0)
            {
                if (cash <= AccCash)
                {
                    if (cash > AccCredit)
                    {
                        Directory.CreateDirectory(Owner.GetUsername() + "/");
                        using (StreamWriter file = new StreamWriter(Owner.GetUsername() + "/" + AccId + ".txt", append: true))
                        {
                            file.WriteLine(DateTime.Now + " Spłacono " + AccCredit + " zł kredytu");
                        }
                        AccCash -= AccCredit;
                        AccCredit = 0;
                    }
                    else
                    {
                        AccCash -= cash;
                        AccCredit -= cash;
                        Directory.CreateDirectory(Owner.GetUsername() + "/");
                        using (StreamWriter file = new StreamWriter(Owner.GetUsername() + "/" + AccId + ".txt", append: true))
                        {
                            file.WriteLine(DateTime.Now + " Spłacono " + cash + " zł kredytu");
                        }
                    }
                }
                else
                    Console.WriteLine("Nie masz tyle pieniędzy!");
            }
            else
                Console.WriteLine("Nie masz kredytu!");
        }

        public void Transfer(int id, decimal cash, Bank bank)
        {
            if (bank.FindAccount(id) != null)
            {
                if (cash < AccCash)
                {
                    AccCash -= cash;
                    bank.FindAccount(id).AccCash += cash;
                    Directory.CreateDirectory(Owner.GetUsername() + "/");
                    using (StreamWriter file = new StreamWriter(Owner.GetUsername() + "/" + AccId + ".txt", append: true))
                    {
                        file.WriteLine(DateTime.Now + " Przelano " + cash + "zł na konto " + id);
                    }
                }
                else
                    Console.WriteLine("Za mało pieniędzy na koncie");
            }
            else
                Console.WriteLine("Nie znaleziono konta");
        }
    }
}
