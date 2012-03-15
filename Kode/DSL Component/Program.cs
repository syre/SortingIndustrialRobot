using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace DSL_component
{
    class Program
    {
        static void Main(string[] args)
        {
            Robot r = new Robot();
            ScriptRunner.RunRobotFunction("Hello()",r);
            string s = "def printHelloTwice():"+"\n"+
                       "    print 'hello'" +"\n"+
                       "    print 'hello'" +"\n"+
                       "printHelloTwice()";
            //ScriptRunner.setScriptFromString(s);
            ScriptRunner.ExecuteScript();
        }
    }
}
