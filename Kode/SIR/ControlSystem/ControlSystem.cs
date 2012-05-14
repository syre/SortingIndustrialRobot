<<<<<<< HEAD
﻿/** \file controlSystem.cs */

using System;
using DSL;

/**
        \brief Class for ControlSystem
        
        \author Robotic Global Organization(RoboGO)
        \date 27-03-2012
*/

=======
﻿/** \file ControlSystem.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using DSL;

>>>>>>> cfe803f93fee06ed002fd80ed83fd6695bc5bd07
namespace ControlSystem
{
    /// <summary>
    /// Class to handle all of styresystems useability
    /// </summary>
    public class ControlSystem
    {
        /// <summary>
        /// Initialises the robot and homing it ready to use
        /// </summary>
        public void start()
        {
            IRobot tempIRobot = Factory.currentIRobotInstance;

            if (tempIRobot == null)
                throw new Exception("No chosen running method found");
            if (!tempIRobot.initialization())
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
           
            tempThreadHandler.abortAllAndWait();

            IRobot tempIRobot = Factory.currentIRobotInstance;

            if (tempIRobot == null)
                throw new Exception("No chosen running method found");
            if(!tempIRobot.stopAllMovement())
                throw new Exception("Could not stop robot");
        }
    }
}
