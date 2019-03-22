using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20._3._19BankProject
{
    class Bank:IBank
    {
        private List<Account> accounts = new List<Account>();
        private List<Customer> customers = new List<Customer>();
        private Dictionary<int, Customer> dictionaryID = new Dictionary<int, Customer>();
        private Dictionary<int, Customer> dictionaryCustNumber = new Dictionary<int, Customer>();
        private Dictionary<int, Account> dictionaryAccNumber = new Dictionary<int, Account>();
        private Dictionary<Customer, List<Account>> dictionaryCustomers = new Dictionary<Customer, List<Account>>();
        private int totalMoneyInBank=0;
        private int profits = 0;

        public string Name
        {
            get
            {
                return Name;
            }
        }

        public int Adress
        {
            get
            {
                return Adress;
            }
        }

        public int CustomerCount
        {
            get
            {
                return CustomerCount;
            }
        }

        internal Customer GetCustomerByID(int customerid)
        {
            if(dictionaryID.TryGetValue(customerid,out Customer customer))
            {
                return customer;
            }
            else
            {
                throw new CustomerNotFoundException($"The costumer ID number{customerid} not in our bank");
            }
        }

        internal Customer GetCustomerByNumber(int customerNumber)
        {
            if (dictionaryCustNumber.TryGetValue(customerNumber, out Customer customer))
            {
                return customer;
            }
            else
            {
                throw new CustomerNotFoundException($"The costumer number{customerNumber} not in our bank");
            }
        }

        internal Account GetAccountByNumber(int accountNumber)
        {
            if (dictionaryAccNumber.TryGetValue(accountNumber, out Account account))
            {
                return account;
            }
            else
            {
                throw new AccountNotFoundException($"The account number{accountNumber} not in our bank");
            }
        }

        internal List<Account> GetAccountByCustomer(Customer customer)
        {
            if (dictionaryCustomers.TryGetValue(customer, out List<Account> accounts))
            {
                return accounts;
            }
            else
            {
                throw new AccountNotFoundException($"The costumer {customer} not have account in our bank");
            }
        }

        internal void AddNewCustomer(Customer customer)
        {
            if(customers.Contains(customer))
            {
                throw new CustomerAlreadyExistException($"The customer {customer.Name} already exist in our bank");
            }
            customers.Add(customer);
            dictionaryID.Add(customer.CustomerID, customer);
            dictionaryCustNumber.Add(customer.CustomerNumber, customer);
            
        }

        internal void OpenNewAccount(Account account, Customer customer)
        {
            if (accounts.Contains(account))
            {
                throw new AccountAlreadyExistException($"The account {account.AccountNunber} already exist in our bank");
            }
            accounts.Add(account);
            dictionaryAccNumber.Add(account.AccountNunber, account);
            dictionaryCustomers.Add(customer, accounts);
            totalMoneyInBank = totalMoneyInBank + account.Balance;
        }

        internal int Deposit(Account account, int amoumt)
        {
            totalMoneyInBank = totalMoneyInBank + amoumt;
            account.Balance = account.Balance + amoumt;
            return account.Balance;
        }

        internal int Withdraw(Account account, int amoumt)
        {
            totalMoneyInBank = totalMoneyInBank - amoumt;
            account.Balance = account.Balance - amoumt;
            if(account.Balance<account.MaxMinusAllowed)
            {
                throw new BalanceException($"The account went out from the balance");
            }
            return account.Balance;
        }

        internal int GetCostumerTotalBalance(Customer customer)
        {
            int sum = 0;
            foreach (Account item in GetAccountByCustomer(customer))
            {
                sum = sum + item.Balance;
            }
            return sum;
        }

        internal void CloseAccount(Account account, Customer customer)
        {
            accounts.Remove(account);
            customers.Remove(customer);
            dictionaryAccNumber.Remove(account.AccountNunber);
            dictionaryCustNumber.Remove(customer.CustomerNumber);
            dictionaryID.Remove(customer.CustomerID);
            dictionaryCustomers.Remove(customer);
            totalMoneyInBank = totalMoneyInBank - account.Balance;
        }

        internal void ChargeAnnualComission(int percentage)
        {
            int comission = 0;
            foreach (Account item in accounts)
            {
                comission = item.Balance * percentage;
                if (item.Balance >= 0)
                {
                    item.Balance = item.Balance - comission;
                }
                else
                {
                    comission = comission * 2;
                    item.Balance = item.Balance - comission;

                }
                profits = profits + comission;
            }
        }

        internal void JoinAccounts(Account account1, Account account2)
        {
            if(account1.AccountOwner.CustomerID!=account2.AccountOwner.CustomerID)
            {
                throw new NotSameCustomerException($"The accounts {account1.AccountNunber} + {account2.AccountNunber} not belong to the same customer");
            }
            Account account3 = account1 + account2;
            accounts.Remove(account1);
            accounts.Remove(account2);
            dictionaryAccNumber.Remove(account1.AccountNunber);
            dictionaryAccNumber.Remove(account2.AccountNunber);
            dictionaryAccNumber.Add(account3.AccountNunber,account3);
            accounts.Add(account3);
        }

        public Bank()
        {
        }
    }
}
