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
            int iReturnValue;
            wrapA.initializationWrapped(Wrapper.MODE_SIMULAT, Wrapper.SYSTEM_TYPE_DEFAULT);
            iReturnValue = wrapA.setParameterFolderWrapped(".");
            System.Console.WriteLine("Value: {0}", iReturnValue);
            System.Console.ReadLine();
        }
    }
}
