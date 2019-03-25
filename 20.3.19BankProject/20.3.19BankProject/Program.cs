using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20._3._19BankProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer sagit = new Customer(4, "sagit", 555);
            Customer noam = new Customer(2, "noam", 666);
            Customer shir = new Customer(3, "shir", 888);
            Account account1 = new Account(sagit, 2500, 5000);
            Account account2 = new Account(sagit, 2800, 8000);
            Account account3 = new Account(shir, 5000, 100000);
            Bank bank = new Bank("PetachTikva", 5, "Diskont");
            bank.AddNewCustomer(sagit);
            bank.OpenNewAccount(account1, sagit);
            bank.OpenNewAccount(account2, sagit);
            bank.JoinAccounts(account1, account2);
            try
            {
                bank.CloseAccount(account3, shir);
            }
            catch(AccountNotFoundException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            try
            {
                bank.GetCustomerByID(666);
            }
            catch(CustomerNotFoundException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
