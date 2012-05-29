using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlSystem;

namespace WrapperTester
{
    class Program
    {
        // Functions
        public static void callbackSuccess(IntPtr arg)
        {
            Console.WriteLine("Success");
        }

        public static void callbackError(IntPtr arg)
        {
            Console.WriteLine("Error");
        }

        public static void callbackMotionStart(ref byte arg)
        {

        }

        public static void callbackMotionEnd(ref byte arg)
        {
            Console.WriteLine("End");
        }

        public static void callbackDigitalSig(long arg)
        {
            Console.WriteLine("Dig");
        }

        public static void callbackHoming(ref byte arg)
        {

        }

        static void Main(string[] args)
        {
            // Variables
            int returnValue = 0;
            DLL.DgateCallBack dgateSuccess = callbackSuccess;
            DLL.DgateCallBack dgateError = callbackError;
            DLL.DgateCallBackByteRefArg dgateMotionEnd = callbackMotionEnd;
            DLL.DgateCallBackByteRefArg dgateMotionStart = callbackMotionStart;
            DLL.DgateCallBackLongArg dgateDig = callbackDigitalSig;
            DLL.DgateCallBackByteRefArg dgateHome = callbackHoming;

            // Test
            returnValue = DLLImport.initialization(1, 0, dgateSuccess, dgateError);
            Console.WriteLine("Status: {0}", returnValue);
            Console.ReadKey();
            DLLImport.WatchMotion(dgateMotionEnd, dgateMotionStart);
            returnValue = DLLImport.DefineVector((byte)'A', "TestVec", 1);
            Console.WriteLine("Status: {0}", returnValue);
            Console.ReadKey();
            //int[] iArray = new int[]{269030, 0, 504328, -63548, 0}; //IT WORKS!!!!!!!!!!
            int[] iArray = new int[] { 200000, 200000, 100000, 100000, 1000000 };
            returnValue = DLLImport.Teach("TestVec", 1, iArray, 5, -32767); // !!!!!!!!!!! 1 !!!!!!!!
            Console.WriteLine("Status: {0}", returnValue);
            Console.ReadKey();

            /* Kan godt køre uden Control*/

            //returnValue = DLLImport.Control((byte)'&', true);
            //Console.WriteLine("Status: {0}", returnValue);
            //Console.ReadKey();

            /* Home before absolute coordinates and relative*/

            DLLImport.Home((byte)'A', dgateHome);
            Console.ReadKey();
            returnValue = DLLImport.MoveLinear("TestVec", 1, null, 0);
            Console.WriteLine("Status: {0}", returnValue);
            Console.ReadKey();
            int[] array1 = new int[8];
            int[] array2 = new int[8];
            int[] array3 = new int[8];
            array1[0] = 2;
            returnValue = DLLImport.GetCurrentPosition(array1, array2, array3);
            Console.WriteLine("Status: {0}", returnValue);
            Console.WriteLine("X: {0}, Y: {1}. Z: {2}, Pitch: {3}, Roll: {4}.", array3[0], array3[1], array3[2], array3[3], array3[4]);
            Console.ReadKey();
            while (true)
            { }
        }
    }
}