/** \file ui.cs */
/** \author Robotic Global Organization(RoboGO) */
/** \brief Contains interfaces and classes for simple communication with the UI. */
using System.Text;
using System.ComponentModel;

namespace ControlSystem
{
    /// <summary>
    /// Simple interface for UI interaction.
    /// </summary>
    public interface IUI
    {
        /// <summary>
        /// Writes the string with arguments to the UI.
        /// 
        /// Newline character appended to end of string.
        /// </summary>
        /// <param name="sMsg">The string with the message and argument placement.(Like normal WriteLine())</param>
        /// <param name="paramobjArgument">Arguments to be placed in the string.</param>
        void writeLine(string sMsg, params object[] paramobjArgument);
    }

    /// <summary>
    /// Console UI for writing to the console output.
    /// </summary>
    public class ConsoleUI : IUI
    {
        
        public void writeLine(string _sMsg, params object[] _paramobjArgument)
        {
            System.Console.WriteLine(_sMsg, _paramobjArgument);
        }
    }
    
    /// <summary>
    /// String UI for writing to a string variable.
    /// </summary>
    public class StringUI : IUI, INotifyPropertyChanged
    {
        // Members and properties
        private StringBuilder sbBuffer;
        /// <summary>
        /// Buffer used for output.
        /// </summary>
        public string Buffer
        {
            get{return(sbBuffer.ToString());}
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        
        // Functions
        /// <summary>
        /// Default constructor that sets up string buffer.
        /// </summary>
        public StringUI()
        {
            sbBuffer = new StringBuilder();
        }
        
        public void writeLine(string _sMsg, params object[] _paramobjArgument)
        {
            sbBuffer.AppendLine("* " + string.Format(_sMsg, _paramobjArgument));
            NotifyPropertyChanged("Buffer");
        }
        
        /// <summary>
        /// Clears the buffer.
        /// </summary>
        public void clearString()
        {
            sbBuffer.Clear();
        }
    }
}
