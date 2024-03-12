using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace kin4.notsynched
{
    internal class MyThread
    {

        notsynched.Data dd;

        private String dorothyFav = "";
        private String characer = "";
        private String color = "";

        public MyThread(notsynched.Data dd, string dorothyFav, string characer, string color)
        {
            // the object reference to update, each thread gets the same
            this.dd = dd;

            // the data that this thread will update with
            this.dorothyFav = dorothyFav;
            this.characer = characer;
            this.color = color;
        }

        public void ThreadProc()
        {
            for (int i = 0; i < 100; i++)
            {
                dd.setThreadPerson(this.dorothyFav, this.characer, this.color);
                Console.WriteLine("ThreadProc: " + this.dorothyFav);
                // Yield the rest of the time slice.
                Thread.Sleep(1);
            }
        }
    }   
}
