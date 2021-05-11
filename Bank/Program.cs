using System;
using System.Collections;

namespace Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            User ActiveUser;
            Account ActiveAcc;
            int CaseSwitch;
            Bank SkokStefczyka = new Bank("Skok Stefczyka");
            Bank ActiveBank = SkokStefczyka;
            Admin Patryk = new Admin("Patryk", "Cieszyński", "pciesz", "ytrewq", SkokStefczyka);
            User Jan = new User("Jan", "Nowak", "jnowak123", "qwerty", SkokStefczyka);

            //Logowanie
            do
            {
                for (; ; )
                {
                    Console.WriteLine("Podaj login: ");
                    string login = Console.ReadLine();
                    Console.WriteLine("Podaj hasło: ");
                    string password = Console.ReadLine();
                    ActiveUser = ActiveBank.Auth(login, password);
                    if (ActiveUser != null)
                        break;
                    else
                        Console.WriteLine("Błąd");
                }
                Console.Clear();

                //Dane kont

                Console.WriteLine("-------------------");
                Console.Write("Aktualny użytkownik: ");
                ActiveUser.GetSurname();
                ActiveUser.ListBankRecords();

                //Wybieranie rachunku
                do
                {
                    if (ActiveUser.GetAccNumbers() > 1)
                    {
                        for (; ; )
                        {
                            Console.WriteLine("Który rachunek chcesz modyfikować?");
                            if (Int32.TryParse(Console.ReadLine(), out int AccountNumber))
                            {
                                AccountNumber -= 1;
                                if (AccountNumber >= 0 && AccountNumber < ActiveUser.GetAccNumbers())
                                {
                                    ActiveAcc = ActiveUser.GetAcc(AccountNumber);
                                    break;
                                }
                                else
                                    Console.WriteLine("Rachunek nie istnieje");
                            }
                            else
                                Console.WriteLine("To nawet nie jest liczba!");
                        }
                    }
                    else
                        ActiveAcc = ActiveUser.GetAcc(0);


                    //Wybieranie opcji

                    do
                    {
                        Console.Clear();
                        Console.WriteLine("-------------------");
                        Console.Write("Aktualny użytkownik: ");
                        ActiveUser.GetSurname();
                        ActiveUser.ListBankRecords();

                        Console.WriteLine("1. Wpłać");
                        Console.WriteLine("2. Wypłać");
                        Console.WriteLine("3. Sprawdź historię");
                        Console.WriteLine("4. Weź kredyt");
                        Console.WriteLine("5. Spłać kredyt");
                        Console.WriteLine("6. Przelew na inne konto");
                        Console.WriteLine("7. Dodaj nowy rachunek");
                        Console.WriteLine("8. Wyloguj");
                        if (ActiveUser.GetAccNumbers() > 1)
                            Console.WriteLine("9. Zmień rachunek");

                        if (ActiveUser is Admin)
                        {
                            Console.WriteLine("-------------------");
                            Console.WriteLine("Panel admina!!!");
                            Console.WriteLine("-------------------");
                            Console.WriteLine("-1. Zablokuj czyjeś konto");
                            Console.WriteLine("-2. Dodaj konto");
                            Console.WriteLine("-3. Usuń konto");
                            Console.WriteLine("-4. Generuj podsumowanie");
                            Console.WriteLine("-5. Sprwadź czyjąć historię");
                            Console.WriteLine("-6. Sprawdź dłużników");
                        }
                        if (Int32.TryParse(Console.ReadLine(), out CaseSwitch))
                        {
                            switch (CaseSwitch)
                            {
                                case 1:
                                    Console.WriteLine("Jaką kwotę chcesz wpłacić?");
                                    ActiveAcc.Deposit(Convert.ToDecimal(Console.ReadLine()));
                                    break;
                                case 2:
                                    Console.WriteLine("Jaką kwotę chcesz wypłacić?");
                                    ActiveAcc.Withdraw(Convert.ToDecimal(Console.ReadLine()));
                                    break;
                                case 3:
                                    ActiveAcc.CheckHistory();
                                    break;
                                case 4:
                                    Console.WriteLine("Jaką kwotę pieniędzy chcesz pożyczyć?");
                                    ActiveAcc.TakeCredit(Convert.ToDecimal(Console.ReadLine()));
                                    break;
                                case 5:
                                    Console.WriteLine("Jaką kwotę pieniędzy chcesz spłacić?");
                                    ActiveAcc.PayCredit(Convert.ToDecimal(Console.ReadLine()));
                                    break;
                                case 6:
                                    Console.WriteLine("Podaj ID konta na które chcesz przelać pieniądze");
                                    int id;
                                    if (Int32.TryParse(Console.ReadLine(), out id))
                                    { 
                                        Console.WriteLine("Podaj kwotę");
                                        decimal cash = Convert.ToDecimal(Console.ReadLine());
                                        ActiveAcc.Transfer(id,cash,ActiveBank);
                                    }
                                    else
                                        Console.WriteLine("To nawet nie jest liczba!");
                                    break;
                                case 7:
                                    Console.WriteLine("Otwieram nowy rachunek");
                                    ActiveUser.NewAccount(ActiveBank);
                                    break;
                                case 8:
                                    break;
                                case 9:
                                    break;
                                case -1:
                                    if (ActiveUser is Admin)
                                    {
                                        Console.WriteLine("Podaj login osoby którą chcesz zablokować:/n");
                                        ActiveBank.Block(Console.ReadLine());
                                    }
                                    else
                                        Console.WriteLine("Nie masz uprawnień");
                                    break;
                                case -2:
                                    if (ActiveUser is Admin)
                                    {
                                        ActiveBank.AddAccount();
                                    }
                                    else
                                        Console.WriteLine("Nie masz uprawnień");
                                    break;
                                case -3:
                                    if (ActiveUser is Admin)
                                    {
                                        Console.WriteLine("Podaj login osoby którą chcesz zablokować:/n");
                                        ActiveBank.DelAccount(Console.ReadLine());
                                    }
                                    else
                                        Console.WriteLine("Nie masz uprawnień");
                                    break;
                                case -4:
                                    if (ActiveUser is Admin)
                                    {
                                        ActiveBank.GenerateBankSummary();
                                    }
                                    else
                                        Console.WriteLine("Nie masz uprawnień");
                                    break;
                                case -5:
                                    if (ActiveUser is Admin)
                                    {
                                        Console.WriteLine("Podaj login osoby której historie chcesz zobaczyć");
                                        ActiveBank.CheckUserHistory(Console.ReadLine());
                                    }
                                    else
                                        Console.WriteLine("Nie masz uprawnień");
                                    break;
                                case -6:
                                    if (ActiveUser is Admin)
                                    {
                                        ActiveBank.CheckDebtorsList();
                                    }
                                    else
                                        Console.WriteLine("Nie masz uprawnień");
                                    break;
                                default:
                                    Console.WriteLine("Nieprawidłowa opcja");
                                    break;
                            }
                            Console.WriteLine("\nNaciśnij klawisz aby przejść dalej \n");
                            Console.ReadKey();
                        }
                    } while (CaseSwitch != 9 && CaseSwitch != 8);
                } while (CaseSwitch != 8);
            } while (true);
        }
    }
}
