using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading
{
    public class AsyncDataSource
    {
        public AsyncDataSource() { }

        public string FastDP
        {
            get => "FAST";
        }

        public string SlowerDP
        {
            get
            {
                // This simulates a lengthy time before the
                // data being bound to is actualy available.
                Thread.Sleep(3000);
                return "SLOW";
            }
        }

        public string SlowestDP
        {
            get
            {
                // This simulates a lengthy time before the
                // data being bound to is actualy available.
                Thread.Sleep(5000);
                return "SLOWEST";
            }
        }
    }
}
