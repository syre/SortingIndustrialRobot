using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WrapperTester
{
    class Program
    {
        static void Main(string[] args)
        {
            // Members
            Wrapper wrapA = Wrapper.getInstance();
            bool status;
            string sBuffer;
            
            // Testing
            status = wrapA.initializationWrapped(Wrapper.MODE_DEFAULT, Wrapper.SYSTEM_TYPE_ER4USB);
            System.Console.WriteLine("Init status: {0}", status);
            /*
             * Alt:
             * status = wrapA.initializationWrapped(1, 0);
            System.Console.WriteLine("Init status: {0}", status);
             */

            System.Console.ReadLine();
        }
    }
}
