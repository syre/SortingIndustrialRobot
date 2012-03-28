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
            IRobot wr = new Robot();
            ScriptRunner.setRobotInstance(wr);

            ScriptRunner.setScriptFromString(@"testHelloName('Soeren')");
            ScriptRunner.ExecuteScript();
        }
    }
}
