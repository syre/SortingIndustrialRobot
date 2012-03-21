using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WrapperTester
{
    class Program
    {
        private static Wrapper wrapA;
        private static void manualMovement()
        {
            int iSpeed = 10;
            while(true)
            {
                ConsoleKeyInfo ckiArg;
                System.Console.Clear();
                System.Console.WriteLine("Press arrow keys.(<q> to stop.)");
                ckiArg = System.Console.ReadKey();

                if (ckiArg.Key == ConsoleKey.DownArrow)
                    wrapA.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_SHOULDER, iSpeed);
                else if (ckiArg.Key == ConsoleKey.UpArrow)
                    wrapA.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_SHOULDER, -iSpeed);
                else if (ckiArg.Key == ConsoleKey.LeftArrow)
                    wrapA.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_BASE, iSpeed);
                else if (ckiArg.Key == ConsoleKey.RightArrow)
                    wrapA.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_BASE, -iSpeed);
                else if (ckiArg.Key == ConsoleKey.Q)
                    break;
            }
        }
        private static void clearBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(false);
            }
            Console.ReadKey();
        }
        static void homeEvent(Byte _bArg)
        {
            System.Console.WriteLine("Home Event: " + _bArg);
        }

        // Main
        static void Main(string[] args)
        {
            // Members
            wrapA = Wrapper.getInstance();
            bool status;

            #region OLD
            /*System.Console.WriteLine("Initializing.");
            status = wrapA.initializationWrapped(Wrapper.enumSystemModes.MODE_ONLINE, Wrapper.enumSystemTypes.SYSTEM_TYPE_DEFAULT);
            char cInput;
            System.Console.WriteLine("Init status: {0}", status);
            System.Console.ReadLine();
            System.Console.WriteLine("Setting control on.");
            //status = wrapA.controlWrapped(Wrapper.enumAxisSettings.AXIS_ALL, true); // Default
            status = wrapA.control2('A');
            System.Console.WriteLine("Control status: {0}", status);
            System.Console.ReadLine();
            System.Console.WriteLine("Trying homing: ");
            cInput = (char) System.Console.Read();
            //status = wrapA.homeWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT); // Default
            status = wrapA.home2(cInput);
            System.Console.WriteLine("Homing status: {0}", status);

            System.Console.ReadLine();
            System.Console.ReadLine();*/
            #endregion


            // Testing
            // -Main setup
            System.Console.WriteLine("Initializing.(Press <Enter> when successfully initialized.)");
            status = wrapA.initializationWrapped(Wrapper.enumSystemModes.MODE_ONLINE, Wrapper.enumSystemTypes.SYSTEM_TYPE_DEFAULT);
            System.Console.WriteLine("Status<wait for event too>: {0}", status);
            System.Console.ReadLine();
            System.Console.WriteLine("Turning control on.(Press <Enter> when done.)");
            status = wrapA.controlWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, true);
            System.Console.WriteLine("Status: {0}", status);
            System.Console.ReadLine();

            // -Menu
            while (true)
            {
                System.Console.Clear();
                ConsoleKeyInfo ckiArg;
                System.Console.WriteLine("What now:");
                System.Console.WriteLine("\t(H)Home all.");
                System.Console.WriteLine("\t(E)Enable manual movement.");
                ckiArg = System.Console.ReadKey();
                System.Console.Clear();
                // --Check
                if(ckiArg.Key == ConsoleKey.H)
                {
                    System.Console.WriteLine("Homing.(Press <Enter> when successfully done.)");
                    status = wrapA.homeWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, homeEvent);
                    System.Console.WriteLine("Status: {0}", status);
                    System.Console.ReadLine();
                }
                else if(ckiArg.Key == ConsoleKey.E)
                {
                    System.Console.WriteLine("Enabling manual movement[By axes].(Press <Enter> when successfully done.)");
                    status = wrapA.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES);
                    System.Console.WriteLine("Status: {0}", status);
                    System.Console.ReadLine();
                    manualMovement();
                }
                else
                {
                    System.Console.WriteLine("Invalid.");
                }
            }
        }
    }
}
