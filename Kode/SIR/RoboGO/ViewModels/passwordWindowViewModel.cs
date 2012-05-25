/** \file passwordWIndowViewModel.cs */
/** \author Robotic Global Organization(RoboGO) */
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ControlSystem;
using SqlInteraction;

namespace RoboGO.ViewModels
{
    /// <summary>
    /// ViewModel for password window.
    /// </summary>
    public class passwordWindowViewModel
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public passwordWindowViewModel()
        {
            
        }

        /// <summary>
        /// Checks the user.
        /// </summary>
        /// <param name="_loginName">User name.</param>
        /// <param name="_loginPassword">Password for user.</param>
        /// <returns>Returns true if matching user and password.</returns>
        public bool authenticate(string _loginName, string _loginPassword)
        {
           SqlCommand tempCmd = SQLHandler.GetInstance.makeCommand("SELECT Users.Name, Users.Password, Permissions.Name FROM Users INNER JOIN Permissions ON Users.Permission=Permissions.ID WHERE Users.Name ='"+_loginName+"' AND Users.Password ='"+_loginPassword+"'");
           ISQLReader tempRead = SQLHandler.GetInstance.runQuery(tempCmd, "read");

            List<object> lstRowsItems = tempRead.readRow();
            if(lstRowsItems.Count > 0)
            {
                if(((string)lstRowsItems[2]) == "Admin")
                {
                   Factory.currentIUserInstance = new Admin();
                }
                else if (((string)lstRowsItems[2]) == "User")
                {
                    Factory.currentIUserInstance = new User();
                }

                tempRead.close();
                return true;
            }
            else
            {
                tempRead.close();
                return false;
            }
        }
    }
}
