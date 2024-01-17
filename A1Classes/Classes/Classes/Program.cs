using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            Programmer programmer = new Programmer { FirstName = "Alice", LastName = "Johnson", Address = "123 Code St", Salary = 60000, PerformanceRating = 4 };
            Manager manager = new Manager { FirstName = "Bob", LastName = "Smith", Address = "456 Manager Ave", Salary = 80000, PerformanceRating = 5 };
            Client client = new Client { Name = "XYZ Corp", ContactInfo = "info@xyz.com" };

            ArrayList persons = new ArrayList { programmer, manager, client };

            foreach (var item in persons)
            {
                if (item is Employee employee)
                {
                    Console.WriteLine($"Employee: {employee.FirstName} {employee.LastName}");
                    Console.WriteLine($"Original Salary: {employee.Salary}");

                    double payRaise = employee.ComputePayRaise();
                    employee.Salary += payRaise;
                    Console.WriteLine($"Pay Raise: {payRaise}");
                    Console.WriteLine($"New Salary: {employee.Salary}");

                    double payCheck = employee.ComputePayCheck();
                    Console.WriteLine($"Paycheck: {payCheck}\n");
                }
            }
        }
    }
}
