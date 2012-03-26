using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WrapperTester
{
    class Program
    {
        private static Wrapper wrapA;
        private static int iSpeed = 10;
        private static void manualMovement()
        {
            wrapA.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_BASE, iSpeed);
            System.Console.Clear();
            while(true)
            {
                ConsoleKeyInfo ckiArg;
                System.Console.WriteLine("Press arrow keys.(<q> to stop.)");
                ckiArg = System.Console.ReadKey();

                if (ckiArg.Key == ConsoleKey.DownArrow)
                {
                    Console.WriteLine("Down arrow.");
                    wrapA.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_X, iSpeed);
                }
                else if (ckiArg.Key == ConsoleKey.UpArrow)
                {
                    Console.WriteLine("Up arrow.");
                    wrapA.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_Y, iSpeed);
                }
                else if (ckiArg.Key == ConsoleKey.LeftArrow)
                {
                    Console.WriteLine("Left arrow.");
                    wrapA.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_Z, iSpeed);
                }
                else if (ckiArg.Key == ConsoleKey.RightArrow)
                {
                    Console.WriteLine("Right arrow.");
                    wrapA.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_PITCH, iSpeed);
                }
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

        // Events
        static void initSuccess(IntPtr iptrArg)
        {
            System.Console.WriteLine("Initialized successfully.");
        }
        static void initError(IntPtr iptrArg)
        {
            System.Console.WriteLine("Initialize error.");
        }
        static void homeEvent(ref byte _bArg)
        {
            System.Console.WriteLine("Home Event: " + _bArg);
        }

        // Main
        static void Main(string[] args)
        {
            // Members
            wrapA = Wrapper.getInstance();
            Wrapper.DgateCallBack dgateEventHandlerSuccess = initSuccess;
            Wrapper.DgateCallBack dgateEventHandlerError = initError;
            Wrapper.DgateCallBackByteRefArg dgateEventHandlerHoming = homeEvent;
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
            status = wrapA.initializationWrapped(Wrapper.enumSystemModes.MODE_ONLINE, Wrapper.enumSystemTypes.SYSTEM_TYPE_DEFAULT, dgateEventHandlerSuccess, dgateEventHandlerError);
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
                //System.Console.WriteLine("\t(E)Manual movement.(Unfunctional)");
                System.Console.WriteLine("\t(S)Set manual movement speed.(At the moment {0}.)", iSpeed);
                System.Console.WriteLine("\t(C)Close gripper.");
                System.Console.WriteLine("\t(O)Open gripper.");
                System.Console.WriteLine("\t(B)Move conveyerbelt.");
                System.Console.WriteLine("\t(P)Define point in new vector.");
                System.Console.WriteLine("\t(M)Move to point.");
                ckiArg = System.Console.ReadKey();
                System.Console.Clear();
                // --Check
                if(ckiArg.Key == ConsoleKey.H)
                {
                    System.Console.WriteLine("Homing.(Press <Enter> when successfully done.)");
                    status = wrapA.homeWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, dgateEventHandlerHoming);
                    System.Console.WriteLine("Status: {0}", status);
                    System.Console.ReadLine();
                }
                /*else if(ckiArg.Key == ConsoleKey.E)
                {
                    System.Console.WriteLine("Enabling manual movement[By axes].(Press <Enter> when successfully done.)");
                    status = wrapA.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_COORD);
                    System.Console.WriteLine("Status: {0}", status);
                    System.Console.ReadLine();
                    manualMovement();
                }*/
                else if(ckiArg.Key == ConsoleKey.S)
                {
                    System.Console.WriteLine("Enter new speed value: ");
                    iSpeed = int.Parse(System.Console.ReadLine());
                }
                else if(ckiArg.Key == ConsoleKey.C)
                {
                    System.Console.WriteLine("Closing gripper.(Press <Enter> afterwards.)");
                    status = wrapA.closeGripperWrapped();
                    System.Console.WriteLine("Status: {0}", status);
                }
                else if(ckiArg.Key == ConsoleKey.O)
                {
                    System.Console.WriteLine("Opening gripper.(Press <Enter> afterwards.)");
                    status = wrapA.openGripperWrapped();
                    System.Console.WriteLine("Status: {0}", status);
                }
                else if(ckiArg.Key == ConsoleKey.B)
                {
                    System.Console.WriteLine("Moving belt.(Press <Enter> to to other things.)");
                    wrapA.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES);
                    System.Console.WriteLine("Enabling status: {0}", status);
                    status = wrapA.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_CONVEYERBELT, iSpeed);
                    System.Console.WriteLine("Moving status: {0}", status);
                    System.Console.ReadLine();
                    wrapA.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_CONVEYERBELT, 0);
                }
                else if(ckiArg.Key == ConsoleKey.P)
                {
                    string sVecName;
                    SIRVector vVector;
                    VecPoint vpPoint;
                    int x, y, z, roll, pitch;

                    System.Console.Write("SIRVector name: ");
                    sVecName = System.Console.ReadLine();
                    status = wrapA.defineVectorWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, sVecName, 1);
                    System.Console.WriteLine("Status: {0}", status);
                    System.Console.WriteLine("(R)elative or (A)bsolute positioning:");
                    ckiArg = System.Console.ReadKey();
                    if(ckiArg.Key == ConsoleKey.R)
                    {
                        vVector = new RelCoordSirVector(sVecName);
                    }
                    else
                    {
                        vVector = new AbsCoordSirVector(sVecName);
                    }
                    System.Console.WriteLine();
                    System.Console.WriteLine("Making point in vector:");
                    System.Console.Write("X: ");
                    x = int.Parse(System.Console.ReadLine());
                    System.Console.Write("Y: ");
                    y = int.Parse(System.Console.ReadLine());
                    System.Console.Write("Z: ");
                    z = int.Parse(System.Console.ReadLine());
                    System.Console.Write("Pitch: ");
                    pitch =  int.Parse(System.Console.ReadLine());
                    System.Console.Write("Roll: ");
                    roll = int.Parse(System.Console.ReadLine());
                    vpPoint = new VecPoint(x, y, z, pitch, roll);

                    System.Console.WriteLine("Adding point:");
                    vVector.addPoint(vpPoint);
                    wrapA.teachWrapped(vVector);
                    System.Console.WriteLine("Status: {0} (<Enter>)", status);
                    System.Console.ReadLine();
                }
                else if(ckiArg.Key == ConsoleKey.M)
                {
                    string sVecName;

                    System.Console.WriteLine("Name of vector: ");
                    sVecName = System.Console.ReadLine();
                    System.Console.WriteLine("Moving to point 1 in vector.");
                    status = wrapA.moveLinearWrapped(sVecName, 0);
                    System.Console.WriteLine("Status: {0} (<Enter>)", status);
                    System.Console.ReadLine();
                }
                else
                {
                    System.Console.WriteLine("Invalid.");
                }
            }
        }
    }
}
