using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20._3._19BankProject
{
    public class Account
    {
        private static int numberOfAcc=1;
        private readonly int accountNumber;
        private readonly Customer accountOwner;
        private int maxMinusAllowed;
        public int AccountNunber 
        {
            get
            {
                return this.accountNumber;
            }
        }
        public int Balance { get; set; }
        public Customer AccountOwner
        {
            get
            {
                return this.accountOwner;
            }
        }
        public int MaxMinusAllowed
        {
            get
            {
                return this.maxMinusAllowed;
            }
        }

  
         public Account(Customer customer, int monthlyIncome, int balance)
         {
          this.accountNumber = numberOfAcc++;
          maxMinusAllowed = (monthlyIncome * 3)*-1;
          this.accountOwner = customer;
          Balance = balance;
         }

        public void Add(int amount)
        {
            Balance = Balance + amount;
        }

        public void Substract(int amount)
        {
            Balance = Balance - amount;
        }

        public static bool operator ==(Account account, Account other)
        {
            if (ReferenceEquals(account, null) && ReferenceEquals(other, null))
            {
                return true;
            }
            if (ReferenceEquals(account, null) || ReferenceEquals(other, null))
            {
                return false;
            }
            return (account.accountNumber == other.accountNumber);
        }
        public static bool operator !=(Account account, Account other)
        {
            return !(account == other);
        }

        public override bool Equals(object obj)
        {
            Account other = obj as Account;
            return this == other;
        }

        public override int GetHashCode()
        {
            return this.accountNumber;
        }
        public static Account operator +(Account account1,Account account2)
        {
            Customer customer = new Customer(account1.accountOwner.CustomerID, account1.accountOwner.Name, account1.accountOwner.PhNumber);
            return new Account(customer,( account1.maxMinusAllowed / 3) +( account2.maxMinusAllowed / 3),account1.Balance+account2.Balance);
        }

    }
}
