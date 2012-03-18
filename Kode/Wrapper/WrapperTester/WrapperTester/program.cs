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
            status = wrapA.controlWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, true);
            wrapA.homeWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT);
            Console.WriteLine("Status: {0}", status);
            System.Console.ReadLine();
        }
    }
}
