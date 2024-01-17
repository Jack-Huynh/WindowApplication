using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    internal abstract class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public double Salary { get; set; }
        public int PerformanceRating { get; set; }
        public abstract double ComputePayCheck();
        public abstract double ComputePayRaise();
    }
}
