using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace kin4.notsynched
{
    internal class Main
    {
        private Dispatcher dis;
        public Main(Dispatcher dis)
        {
            this.dis = dis;
        }

        public void Main1(TextBox outBox)
        {
            // invoke GUI update using GUI thread...to output on textbox
            this.dis.Invoke(new Action(() =>
            {
                outBox.AppendText("\r\rNot Synched interleave started");
            }));

            notsynched.Data data = new notsynched.Data("Data1", "Data2", "Data3");

            for (int i = 0; i < 100; i++) 
            {
                notsynched.MyThread a = new notsynched.MyThread(data, "DorothyTM  ", "Tin Man", "  Silver");
                notsynched.MyThread b = new notsynched.MyThread(data, "DorothySC  ", "Scarecrow", "  Brown");
                notsynched.MyThread c = new notsynched.MyThread(data, "DorothyCL  ", "Cowardly Lion", "  Yellow");

                Action MethodToRun_a = a.ThreadProc;
                Action MethodToRun_b = b.ThreadProc;
                Action MethodToRun_c = c.ThreadProc;

                Thread t1 = new Thread(new ThreadStart(MethodToRun_a));
                Thread t2 = new Thread(new ThreadStart(MethodToRun_b));
                Thread t3 = new Thread(new ThreadStart(MethodToRun_c));
               

                t1.Start();
                t2.Start();
                t3.Start();
               
            }

            for (int i = 0; i < 40; i++)
            {
                Thread.Sleep(2);
                Console.WriteLine(data.getThreadperson());

                // invoke GUI update using GUI thread...to output on textbox
                this.dis.Invoke(new Action(() =>
                {
                    outBox.AppendText("\rMAIN=>" + data.getThreadperson());
                }));

            }

        }
    }
}
