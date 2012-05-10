using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ControlSystem;
using SqlInteraction;

namespace RoboGO.ViewModels
{
    public class passwordWindowViewModel
    {
        public passwordWindowViewModel()
        {
            
        }

        public bool authenticate(string _loginName, string _loginPassword)
        {
           SqlCommand tempCmd = Factory.getSQLHandlerInstance.makeCommand("SELECT Users.Name, Users.Password, Permissions.Name FROM Users INNER JOIN Permissions ON Users.Permission=Permissions.ID WHERE Users.Name ='"+_loginName+"' AND Users.Password ='"+_loginPassword+"'", CommandType.Text);
            ISQLReader tempRead = Factory.getSQLHandlerInstance.runQuery(tempCmd, "read");

            //int tempint = tempRead.readRow().Count;
            //List<object> tempObj = new List<object>(tempRead.readRow());
            List<object> lstRowsItems = tempRead.readRow();
            if(lstRowsItems.Count > 0)
            {
                if(((string)lstRowsItems[2]) == "Admin")
                {
                   
                }
                else if (((string)lstRowsItems[2]) == "User")
                {
                    
                }
                else if (((string)lstRowsItems[2]) == "Technician")
                {
                    
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
