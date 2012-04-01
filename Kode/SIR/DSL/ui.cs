/** \file iUI.cs */
/** \author Robotic Global Organization(RoboGO) */
/** \brief Contains interfaces and classes for simple communication with the UI. */

namespace DSL
{
    /** \brief Simple interface for UI interaction.(Text writing) */
    public interface IUI
    {
        /// <summary>
        /// Writes the string with arguments to the UI.
        /// 
        /// No newline character written.
        /// </summary>
        /// <param name="sMsg">The string with the message and argument placement.(Like normal Write())</param>
        /// <param name="paramobjArgument">Arguments to be placed in the string.</param>
        void write(string sMsg, params object[] paramobjArgument);

        /// <summary>
        /// Writes the string with arguments to the UI.
        /// 
        /// Newline character appended to end of string.
        /// </summary>
        /// <param name="sMsg">The string with the message and argument placement.(Like normal WriteLine())</param>
        /// <param name="paramobjArgument">Arguments to be placed in the string.</param>
        void writeLine(string sMsg, params object[] paramobjArgument);
    }

    /** \brief Console UI for writing to the console output. */
    public class ConsoleUI : IUI
    {
        public void write(string sMsg, params object[] paramobjArgument)
        {
            System.Console.Write(sMsg, paramobjArgument);
        }

        public void writeLine(string sMsg, params object[] paramobjArgument)
        {
            System.Console.WriteLine(sMsg, paramobjArgument);
        }
    }
}
