using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    internal abstract class Employee : Person
    {
        public override double ComputePayCheck()
        {
            return Salary;
        }

        public override double ComputePayRaise()
        {
            return Salary * 0.05;
        }
    }
}
