/** \file Factory.cs */
/** \author Robotic Global Organization(RoboGO) */


namespace ControlSystem
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
        private static volatile ScriptRunner scriptRunnerInstance;
        private static volatile IUser ICurrentUser;
        // Static synchronization root object, for locking
        private static object objectThreadSync = new object();
        private static object objectRobotSync = new object();
        private static object objectSimulatorSync = new object();
        private static object objectIRobotSync = new object();
        private static object objectScriptRunnerSync = new object();
        private static object objectUserSync = new object();
       
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
        /// Returns the instance of Robot or creates it if it's not already created
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
        /// Returns the instance of Simulator or creates it if its not already created
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
                if (iRobotInstance == null)
                    iRobotInstance = getSimulatorInstance;
                return iRobotInstance;
            }
            set
            {
                lock (objectIRobotSync)
                {
                    iRobotInstance = value;
                    getScriptRunnerInstance.setRobotInstance(value);
                }
            }
        }
        /// <summary>
        /// Returns the instance of ScriptRunner or creates it if its not already created
        /// </summary>
        public static ScriptRunner getScriptRunnerInstance
        {
            get
            {
                // Check that the instance is null
                if (scriptRunnerInstance == null)
                {
                    // Lock the object
                    lock (objectScriptRunnerSync)
                    {
                        // Check to make sure its null
                        if (scriptRunnerInstance == null)
                        {
                            scriptRunnerInstance = ScriptRunner.getInstance();
                            scriptRunnerInstance.setRobotInstance(currentIRobotInstance);
                        }
                    }
                }

                // Return the non-null instance of Singleton
                return scriptRunnerInstance;
            }
        }

        /// <summary>
        /// Returns the current logged in user
        /// </summary>
        public static IUser currentIUserInstance
        {
            get
            {
                return ICurrentUser;
            }
            set
            {
                lock (objectUserSync)
                {
                    ICurrentUser = value;
                }
            }
        }
    }
}
