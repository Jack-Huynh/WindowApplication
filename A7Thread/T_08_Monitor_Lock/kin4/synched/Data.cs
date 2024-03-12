using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Policy;

namespace kin4.synched
{
    internal class Data
    {
        // object for lock.  Note same lock for reading as with writing
        private readonly String read_write_data = "LOCK";

        private String dorothyFav = "";
        private String characer = "";
        private String color = "";

        public Data(String dorothyFav, String characer, String color)
        {
            this.dorothyFav = dorothyFav;
            this.characer = characer;
            this.color = color;
            //Thread.Sleep(0);
        }

        public void setThreadPerson(String dorothyFav, String characer, String color)
        {
            // lock during read/writes
            lock (read_write_data)
            {
                // update the data
                this.dorothyFav = dorothyFav;
                Thread.Sleep(1);
                this.characer = characer;
                Thread.Sleep(1);
                this.color = color;
            }
        }

        public String getThreadperson()
        {
            String r = "";
            // lock during read/writes
            lock (read_write_data)
            {
                // update the data
                r = r + this.dorothyFav;
                Thread.Sleep(1);
                r = r + this.characer;
                Thread.Sleep(1);
                r = r + this.color;
            }
            return r;
        }
    }
}
