/**
        \brief Class to handle styresystem functions
        
        \author RoboGO
        \date 27-03-2012
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Styresystem;
using Threading;
using DSL_component;

namespace Styresystem
{
    /// <summary>
    /// Class to handle all of styresystems useability
    /// </summary>
    public class Styresystem
    {
        /// <summary>
        /// Initialises the robot and homing it ready to use
        /// </summary>
        public void start()
        {
            IRobot tempIRobot = Factory.currentIRobotInstance;

            if (tempIRobot == null)
                throw new Exception("No chosen running method found");
            if (!tempIRobot.Initialization())
                throw new Exception("Could not initialize");
            if (!tempIRobot.homeRobot())
                throw new Exception("Could not home");
        }

        /// <summary>
        /// Function aborts all threads and stops current robot instruction.
        /// </summary>
        public void stop()
        {
            ThreadHandling tempThreadHandler = Factory.getThreadHandlingInstance;
           
            if(!tempThreadHandler.abortAllAndWait())
                throw new Exception("Could not abort threads");

            IRobot tempIRobot = Factory.currentIRobotInstance;

            if (tempIRobot == null)
                throw new Exception("No chosen running method found");
            if(!tempIRobot.stopAllMovement())
                throw new Exception("Could not stop robot");
        }
    }
}
