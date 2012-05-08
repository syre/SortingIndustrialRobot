/**
        \brief Class to handle all threads in system, everything handled by a unique description tag that stays with a thread from moment it gets added till it gets removed.
        
        \author Robotic Global Organization(RoboGO)
        \date 18-03-2012

        \note Thread functions are to be defined as following - With start parameter: void functionname(object o) {} - Without start parameter: void functionname()
*/


using System;
using System.Collections.Generic;
using System.Threading;


namespace ControlSystem 
{
    public class ThreadHandling
    {
        private List<ThreadHolder> threadList;
 
        /// <summary>
        /// Constructor for class : Makes a new list for usage  
        /// </summary>
        public ThreadHandling()
        {
            threadList = new List<ThreadHolder>();
        }

        /// <summary>
        /// addThread function, adds a function as a thread to a list, which needs its own thread for running. This specific function handles ThreadStart parameter, which means it can only take functions with no parameters as argument
        /// </summary>
        /// <param name="_threadStart">Parameter is name of function that needs its own thread, as its Threadstart, it means it have to be a function with no parameters: (functionname(){})</param>
        /// <param name="_description">Description which the thread needs to be saved as (unique)</param>
        public void addThread(ThreadStart _threadStart, string _description)
        {
            ThreadHolder tempThreadHolder = find(_description);

            if(tempThreadHolder == null)
            {
                Thread tempThread = new Thread(_threadStart);
                ThreadHolder tempHolder = new ThreadHolder();

                tempHolder.stringDescription = _description;
                tempHolder.threadPlaceHolder = tempThread;
            
                threadList.Add(tempHolder);
            }

            if(tempThreadHolder != null)
                throw new ArgumentException("Thread already exists");

        }

        /// <summary>
        /// Overloaded addThread function, so that it can add threads with a parameterizedThreadStart (object) to a thread list by a description
        /// </summary>
        /// <param name="_parameterizedThreadStart">Parameter is name of function that needs its own thread, as its paramterizedThreadStart, it means it have to be a function with parameter object: (functionname(object example){})</param>
        /// <param name="_description">Description which the thread needs to be saved as (unique)</param>
        public void addThread(ParameterizedThreadStart _parameterizedThreadStart, string _description)
        {
            ThreadHolder tempThreadHolder = find(_description);

            if (tempThreadHolder == null)
            {
                Thread tempThread = new Thread(_parameterizedThreadStart);
                ThreadHolder tempHolder = new ThreadHolder();

                tempHolder.stringDescription = _description;
                tempHolder.threadPlaceHolder = tempThread;

                threadList.Add(tempHolder);

            }

            if (tempThreadHolder != null)
                throw new ArgumentException("Thread already exists");
        }

        /// <summary>
        /// Removes the specified thread from the thread list (Stops and waits for it to finish if it was running).
        /// </summary>
        /// <param name="_description">Description which the function need to find the thread that should be deleted</param>
        public void removeThread(string _description)
        {
            ThreadHolder tempThreadHolder = find(_description);

            if(tempThreadHolder != null)
            {
                if (tempThreadHolder.threadPlaceHolder.IsAlive)
                {
                    tempThreadHolder.threadPlaceHolder.Abort("Terminating Thread, Request from Program.");
                    tempThreadHolder.threadPlaceHolder.Join();
                }

                threadList.Remove(tempThreadHolder);

            }
            
            if(tempThreadHolder == null)
                throw new ArgumentException("No thread with that description found");
        }


        /// <summary>
        /// Terminates thread with supplied description, then waits for it to finish terminating
        /// </summary>
        /// <param name="_description">Description which the function needs to terminate thread for and wait for</param>
        public void abortAndWait(string _description)
        {
            ThreadHolder tempThreadHolder = find(_description);

            if (tempThreadHolder != null)
            {
                if (tempThreadHolder.threadPlaceHolder.IsAlive)
                {
                    tempThreadHolder.threadPlaceHolder.Abort("Terminating Thread, Request from Program.");
                    tempThreadHolder.threadPlaceHolder.Join();
                }

                throw new ArgumentException("Thread not running");
            }

            if (tempThreadHolder == null)
                throw new ArgumentException("No thread with that description found");
        }

        /// <summary>
        /// Functions aborts all threads currently running and waits for them to finish
        /// </summary>
        public void abortAllAndWait()
        {
            if (threadList.Count != 0)
            {
                foreach (var threadHolder in threadList)
                {
                    if (threadHolder.threadPlaceHolder.IsAlive)
                    {
                        threadHolder.threadPlaceHolder.Abort();
                        threadHolder.threadPlaceHolder.Join();
                    }
                }

            }

            if(threadList.Count == 0)
                throw new Exception("No running threads");
        }

        /// <summary>
        /// Starts the thread from supplied description if found.
        /// </summary>
        /// <param name="_description">Description is a string which the function needs to search and find a thread for in a list</param>
        public void start(string _description)
        {
            ThreadHolder tempThreadHolder = find(_description);

            if (tempThreadHolder != null)
            {
                if (!tempThreadHolder.threadPlaceHolder.IsAlive)
                {
                    tempThreadHolder.threadPlaceHolder.Start();
                }

                //throw new ArgumentException("Thread already started");
            }

            if(tempThreadHolder == null)
                throw new ArgumentException("No thread found");
        }


        /// <summary>
        /// Starts the thread from supplied description if found but with a given parameter object
        /// </summary>
        /// <param name="_description">Description is a string which the function needs to search and find a thread for in a list</param>
        /// <param name="_obj">An object that needs to be passed on to the thread as start parameter</param>
        public void start(string _description , object _obj)
        {
            ThreadHolder tempThreadHolder = find(_description);

            if (tempThreadHolder != null)
            {
                if (!tempThreadHolder.threadPlaceHolder.IsAlive)
                {
                    tempThreadHolder.threadPlaceHolder.Start(_obj);                 
                }

                throw new ArgumentException("Thread already started");
            }

            if(tempThreadHolder == null)
                throw new ArgumentException("No thread found");
        }


        /// <summary>
        /// Function that looks up saved threads by description
        /// </summary>
        /// <param name="_description">String that descripes the thread looking for</param>
        /// <returns>Returns Threadholder object if one found with description else null</returns>
        public ThreadHolder find(string _description)
        {
            foreach (ThreadHolder holder in threadList)
            {
                if (holder.stringDescription == _description)
                {
                    return holder;
                }
            }

            return null;
        }

        /// <summary>
        /// Class that holds the description of the thread and the thread itself
        /// </summary>
        public class ThreadHolder
        {
            private string description;
            private Thread thread;

            public string stringDescription
            {
                get { return description; }
                set { description = value; }
            }

            public Thread threadPlaceHolder
            {
                get { return thread; }
                set { thread = value; }
            }
        }
    }
}
