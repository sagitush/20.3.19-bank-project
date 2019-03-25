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
        
        public string Adress { get; set; }

        public int CustomerCount { get; set; }

        public string Name { get; set; }

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

        internal List<Account> GetAccountsByCustomer(Customer customer)
        {
            if (dictionaryCustomers.TryGetValue(customer, out List<Account> accounts))
            {
                return accounts;
            }
            else
            {
                throw new AccountNotFoundException($"The costumer {customer} dont have accounts in our bank");
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
            if (this.accounts.Contains(account))
            {
                throw new AccountAlreadyExistException($"The account {account.AccountNunber} already exist in our bank");
            }
            this.accounts.Add(account);
            dictionaryAccNumber.Add(account.AccountNunber, account);
            totalMoneyInBank = totalMoneyInBank + account.Balance;
            if(dictionaryCustomers.TryGetValue(customer,out List<Account> accounts1))
            {
                accounts1.Add(account);
            }
            else
            {
                dictionaryCustomers.Add(customer,this.accounts);
            }
        }

        internal int Deposit(Account account, int amoumt)
        {
            totalMoneyInBank = totalMoneyInBank + amoumt;
            account.Add(amoumt);
            return account.Balance;
        }

        internal int Withdraw(Account account, int amoumt)
        {
            totalMoneyInBank = totalMoneyInBank - amoumt;
            account.Substract(amoumt);
            if(account.Balance<account.MaxMinusAllowed)
            {
                throw new BalanceException($"The account {account} went out from the minus");
            }
            return account.Balance;
        }

        internal int GetCostumerTotalBalance(Customer customer)
        {
            int CostumerTotalBalance = 0;
            foreach (Account item in GetAccountsByCustomer(customer))
            {
                CostumerTotalBalance = CostumerTotalBalance + item.Balance;
            }
            return CostumerTotalBalance;
        }

        internal void CloseAccount(Account account, Customer customer)
        {
            if (accounts.Contains(account))
            {
                accounts.Remove(account);
                dictionaryAccNumber.Remove(account.AccountNunber);
                if (dictionaryCustomers.TryGetValue(customer, out List<Account> accounts1))
                {
                    if (accounts1.Count == 1)
                    {
                        dictionaryCustomers.Remove(customer);
                        customers.Remove(customer);
                        dictionaryCustomers.Remove(customer);
                        dictionaryCustNumber.Remove(customer.CustomerNumber);
                    }
                    else
                    {
                        accounts1.Remove(account);
                    }
                }


                totalMoneyInBank = totalMoneyInBank - account.Balance;
            }
            else
                throw new AccountNotFoundException($"The account {account} not in the bank so we cant close it");
        }

        internal void ChargeAnnualComission(int percentage)
        {
            int comission = 1;
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
            CloseAccount(account1,account1.AccountOwner);
            CloseAccount(account2, account2.AccountOwner);
            OpenNewAccount(account3, account3.AccountOwner);
        }

        public Bank(string adress, int customerCount, string name)
        {
            Adress = adress;
            CustomerCount = customerCount;
            Name = name;
        }
    }
}
