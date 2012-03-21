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
            /*status = wrapA.initializationWrapped(Wrapper.MODE_DEFAULT, Wrapper.SYSTEM_TYPE_ER4USB);
            System.Console.WriteLine("Init status: {0}", status);*/
            /*
             * Alt:
             **/ //status = wrapA.initializationWrapped(1, 0);
            System.Console.WriteLine("Initializing.");
            status = wrapA.initializationWrapped(Wrapper.enumSystemModes.MODE_ONLINE, Wrapper.enumSystemTypes.SYSTEM_TYPE_DEFAULT);
            char cInput;
            System.Console.WriteLine("Init status: {0}", status);
            System.Console.ReadLine();
            System.Console.WriteLine("Setting control on.");
            //status = wrapA.controlWrapped(Wrapper.enumAxisSettings.AXIS_ALL, true); // Default
            status = wrapA.control2('&');
            System.Console.WriteLine("Control status: {0}", status);
            System.Console.ReadLine();
            System.Console.WriteLine("Trying homing: ");
            cInput = (char) System.Console.Read();
            //status = wrapA.homeWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT); // Default
            status = wrapA.home2(cInput);
            System.Console.WriteLine("Homing status: {0}", status);

            System.Console.ReadLine();
            System.Console.ReadLine();
        }
    }
}
