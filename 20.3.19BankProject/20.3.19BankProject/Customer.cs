using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20._3._19BankProject
{
    public class Customer
    {
        private static int numberOfCust = 1;
        private readonly int customerNumber;
        private readonly int customerID;
        public string Name { get;private set; }
        public int PhNumber { get;private set; }
        public int CustomerID
        {
            get
            {
                return this.customerID;
            }
        }
        public int CustomerNumber
        {
            get
            {
                return this.customerNumber;
            }
        }

        public Customer(int customerID, string name, int phNumber)
        {
            this.customerID = customerID;
            Name = name;
            PhNumber = phNumber;
            this.customerNumber = numberOfCust++;
        }
        public static bool operator == (Customer customer,Customer other)
        {
            if(ReferenceEquals(customer,null)&& ReferenceEquals(other,null))
            {
                return true;
            }
            if (ReferenceEquals(customer, null) || ReferenceEquals(other, null))
            {
                return false;
            }
            return (customer.customerNumber == other.customerNumber);
        }
       public static bool operator !=(Customer customer,Customer other)
        {
            return !(customer==other);
        }

        public override bool Equals(object obj)
        {
            Customer other = obj as Customer;
            return this == other;
        }

        public override int GetHashCode()
        {
            return this.customerNumber;
        }
    }
}
