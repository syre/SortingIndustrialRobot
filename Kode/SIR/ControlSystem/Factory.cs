/**
        \brief Class to handle global classes by using a factory and singleton patterns
        
        \author Robotic Global Organization(RoboGO)
        \date 27-03-2012
*/

using System.Data.SqlClient;
using DSL;
using SqlInteraction;

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
        private static volatile ISQLHandler sqlHandlerInstance;

        // Static synchronization root object, for locking
        private static object objectThreadSync = new object();
        private static object objectRobotSync = new object();
        private static object objectSimulatorSync = new object();
        private static object objectIRobotSync = new object();
        private static object objectScriptRunnerSync = new object();
        private static object objectSQLHandlerSync = new object();


       
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
        /// Returns the instance of SQLHandler or creates it if its not already
        /// </summary>
        public static ISQLHandler getSQLHandlerInstance
        {
            get
            {
                // Check that the instance is null
                if (sqlHandlerInstance == null)
                {
                    // Lock the object
                    lock (objectSQLHandlerSync)
                    {
                        // Check to make sure its null
                        if (sqlHandlerInstance == null)
                        {
                            sqlHandlerInstance = new SQLHandler();
                        }
                    }
                }

                // Return the non-null instance of Singleton
                return sqlHandlerInstance;
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
                if (iRobotInstance == null)
                    iRobotInstance = getSimulatorInstance;
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
        /// <summary>
        /// Returns the instance of ScriptRunner or creates it if its not already
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
                        }
                    }
                }

                // Return the non-null instance of Singleton
                return scriptRunnerInstance;
            }
        }
    }
}
