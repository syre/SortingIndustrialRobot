using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlSystem
{
    public class MethodList : List<string>
    {
        public MethodList()
        {
            FileStream fs = null;
            StreamReader sr = null;

            try
            {
                fs = new FileStream(@"DSLFiler\IntelliSenseKnows.txt", FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs, Encoding.Default);
                char[] separators = { ':' };

                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    if (str != "")
                    {
                        string[] tokens = str.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var token in tokens)
                        {
                            Add(token);    
                        }
                
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Intellisense is not working");
            }
            finally
            {
                sr.Close();
                fs.Close();
            }
        }
    }
}
