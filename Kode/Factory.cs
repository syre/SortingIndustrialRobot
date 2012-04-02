/**
        \brief Class to handle global classes by using a factory and singleton patterns
        
        \author RoboGO
        \date 27-03-2012
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Threading;
using DSL_component;

namespace Styresystem
{
    /// <summary>
    /// Manages all global class's as a singleton and factory
    /// </summary>
    public static class Factory
    {
        // Static, VOLATILE variable to store single instance
        private static volatile ThreadHandling threadhandlingInstance;
        private static volatile Robot robotInstance;
        private static volatile Simulator simulatorInstance;
        private static volatile IRobot iRobotInstance;

        // Static synchronization root object, for locking
        private static object objectThreadSync = new object();
        private static object objectRobotSync = new object();
        private static object objectSimulatorSync = new object();
        private static object objectIRobotSync = new object();
        


       
        /// <summary>
        /// Returns the instance of ThreadHandler or creates it if its not already
        /// </summary>
        public static ThreadHandling getThreadHandlingInstance
        {
            get
            {
                // Check that the instance is null
                if (threadhandlingInstance == null)
                {
                    // Lock the object
                    lock (objectThreadSync)
                    {
                        // Check to make sure its null
                        if (threadhandlingInstance == null)
                        {
                            threadhandlingInstance = new ThreadHandling();
                        }
                    }
                }
            
                // Return the non-null instance of Singleton
                return threadhandlingInstance;
            }
        }

        /// <summary>
        /// Returns the instance of Robot or creates it if its not already
        /// </summary>
        public static Robot getRobotInstance
        {
            get
            {
                // Check that the instance is null
                if (robotInstance == null)
                {
                    // Lock the object
                    lock (objectRobotSync)
                    {
                        // Check to make sure its null
                        if (robotInstance == null)
                        {
                            robotInstance = new Robot();
                        }
                    }
                }

                // Return the non-null instance of Singleton
                return robotInstance;
            }
        }

        /// <summary>
        /// Returns the instance of Simulator or creates it if its not already
        /// </summary>
        public static Simulator getSimulatorInstance
        {
            get
            {
                // Check that the instance is null
                if (simulatorInstance == null)
                {
                    // Lock the object
                    lock (objectSimulatorSync)
                    {
                        // Check to make sure its null
                        if (simulatorInstance == null)
                        {
                            simulatorInstance = new Simulator();
                        }
                    }
                }

                // Return the non-null instance of Singleton
                return simulatorInstance;
            }
        }

        /// <summary>
        /// Returns the current set used method of executing scripts
        /// </summary>
        public static IRobot currentIRobotInstance
        {
            get
            {
                return iRobotInstance;
            }
            set
            {
                lock (objectIRobotSync)
                {
                    iRobotInstance = value;
                }
            }
        }


    }
}
