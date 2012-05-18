/** \file iUI.cs */
/** \author Robotic Global Organization(RoboGO) */
/** \brief Contains interfaces and classes for simple communication with the UI. */
using System.Text;
using System.ComponentModel;

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
        void write(string _sMsg, params object[] _paramobjArgument);

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
        public void write(string _sMsg, params object[] _paramobjArgument)
        {
            System.Console.Write(_sMsg, _paramobjArgument);
        }

        public void writeLine(string _sMsg, params object[] _paramobjArgument)
        {
            System.Console.WriteLine(_sMsg, _paramobjArgument);
        }
    }
    
    /** \brief String UI for writing to a string variable. */
    public class StringUI : IUI, INotifyPropertyChanged
    {
        // Members and properties
        private StringBuilder sbBuffer;
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
        public StringUI()
        {
            sbBuffer = new StringBuilder();
        }
        
        public void write(string _sMsg, params object[] _paramobjArgument)
        {
            sbBuffer.Append("* " + string.Format(_sMsg, _paramobjArgument));
            NotifyPropertyChanged("Buffer");
        }

        public void writeLine(string _sMsg, params object[] _paramobjArgument)
        {
            sbBuffer.Append("* " + string.Format(_sMsg, _paramobjArgument) + "\n");
            NotifyPropertyChanged("Buffer");
        }
    }
}
