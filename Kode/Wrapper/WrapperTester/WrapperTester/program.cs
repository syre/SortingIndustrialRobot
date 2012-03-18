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
            Wrapper wrapA = new Wrapper();
            bool status;
            status = wrapA.controlWrapped(Wrapper.enumAxis.AXIS_ROBOT, true);
            Console.WriteLine("Status: {0}", status);
            System.Console.ReadLine();
        }
    }
}
