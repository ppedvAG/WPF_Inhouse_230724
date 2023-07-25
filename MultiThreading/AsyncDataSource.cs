using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading
{
    class AsyncDataSource
    {
        public string Fast
        {
            get => "FAST";
        }

        public string Slow
        {
            get
            {
                Thread.Sleep(3000);
                return "SLOW";
            }
        }
        public string Slower
        {
            get
            {
                Thread.Sleep(5000);
                return "SLOWER";
            }
        }

    }
}
