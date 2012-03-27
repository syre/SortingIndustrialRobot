/**
        \brief Class to handle all threads in system, everything handled by a unique description tag that stays with a thread from moment it gets added till it gets removed.
        
        \author RoboGO
        \date 18-03-2012

        \note Thread functions are to be defined as following - With start parameter: void functionname(object o) {} - Without start parameter: void functionname()
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace Styresystem 
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
        /// <returns>Returns 0 if successful : Returns -1 if thread by same description is already existing</returns>
        public int addThread(ThreadStart _threadStart, string _description)
        {
            if(find(_description) == null)
            {
                Thread tempThread = new Thread(_threadStart);
                ThreadHolder tempHolder = new ThreadHolder();

                tempHolder.stringDescription = _description;
                tempHolder.threadPlaceHolder = tempThread;
            
                threadList.Add(tempHolder);

                return 0;
            }

            return -1;
        }

        /// <summary>
        /// Overloaded addThread function, so that it can add threads with a parameterizedThreadStart (object) to a thread list by a description
        /// </summary>
        /// <param name="_parameterizedThreadStart">Parameter is name of function that needs its own thread, as its paramterizedThreadStart, it means it have to be a function with parameter object: (functionname(object example){})</param>
        /// <param name="_description">Description which the thread needs to be saved as (unique)</param>
        /// <returns>Returns 0 if successful : Returns -1 if thread by same description is already existing</returns>
        public int addThread(ParameterizedThreadStart _parameterizedThreadStart, string _description)
        {
            if (find(_description) == null)
            {
                Thread tempThread = new Thread(_parameterizedThreadStart);
                ThreadHolder tempHolder = new ThreadHolder();

                tempHolder.stringDescription = _description;
                tempHolder.threadPlaceHolder = tempThread;

                threadList.Add(tempHolder);

                return 0;
            }

            return -1;
        }

        /// <summary>
        /// Removes the specified thread from the thread list (Stops and waits for it to finish if it was running).
        /// </summary>
        /// <param name="_description">Description which the function need to find the thread that should be deleted</param>
        /// <returns>Returns 0 if successful : Returns -1 if nothing was found with given description</returns>
        public int removeThread(string _description)
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

                return 0;
            }
            return -1;
        }


        /// <summary>
        /// Terminates thread with supplied description, then waits for it to finish terminating
        /// </summary>
        /// <param name="_description">Description which the function needs to terminate thread for and wait for</param>
        /// <returns>Returns 0 if successful : Returns -1 if no thread was found with supplied description : Returns -2 if supplied thread was not running</returns>
        public int abortAndWait(string _description)
        {
            ThreadHolder tempThreadHolder = find(_description);

            if (tempThreadHolder != null)
            {
                if (tempThreadHolder.threadPlaceHolder.IsAlive)
                {
                    tempThreadHolder.threadPlaceHolder.Abort("Terminating Thread, Request from Program.");
                    tempThreadHolder.threadPlaceHolder.Join();
                    return 0;
                }

                return -2;
            }

            return -1;
        }

        /// <summary>
        /// Functions aborts all threads currently running and waits for them to finish
        /// </summary>
        /// <returns>Returns 0 if successful : Returns -1 if there is no threads</returns>
        public int abortAllAndWait()
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

                return 0;
            }

            return -1;
        }

        /// <summary>
        /// Starts the thread from supplied description if found.
        /// </summary>
        /// <param name="_description">Description is a string which the function needs to search and find a thread for in a list</param>
        /// <returns>Returns 0 if successful : Returns -1 if no thread was found by supplied description : Returns -2 if wanted thread is already started</returns>
        public int start(string _description)
        {
            ThreadHolder tempThreadHolder = find(_description);

            if (tempThreadHolder != null)
            {
                if (!tempThreadHolder.threadPlaceHolder.IsAlive)
                {
                    tempThreadHolder.threadPlaceHolder.Start();
                    return 0;
                }

                return -2;
            }

            return -1;
        }


        /// <summary>
        /// Starts the thread from supplied description if found but with a given parameter object
        /// </summary>
        /// <param name="_description">Description is a string which the function needs to search and find a thread for in a list</param>
        /// <param name="_obj">An object that needs to be passed on to the thread as start parameter</param>
        /// <returns>Returns 0 if successful : Returns -1 if no thread was found by supplied description : Returns -2 if wanted thread is already started</returns>
        public int start(string _description , object _obj)
        {
            ThreadHolder tempThreadHolder = find(_description);

            if (tempThreadHolder != null)
            {
                if (!tempThreadHolder.threadPlaceHolder.IsAlive)
                {
                    tempThreadHolder.threadPlaceHolder.Start(_obj);
                    return 0;
                }

                return -2;
            }

            return -1;   
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
