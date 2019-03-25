using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20._3._19BankProject
{
    public interface IBank
    {
        string Name { get;  }
        String Adress { get;  }
        int CustomerCount { get; }
    }
}
