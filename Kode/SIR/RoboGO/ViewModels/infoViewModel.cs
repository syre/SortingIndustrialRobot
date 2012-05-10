/** \file infoViewModel.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using SqlInteraction;
using ControlSystem;

namespace RoboGO.ViewModels
{
    public class InfoViewModel : INotifyPropertyChanged
    {
        // Members and properties
        private SqlCommandBuilder tableValuesCommandBuilder;
        public SqlDataAdapter sqlDATables;
        public SqlDataAdapter sqlDATableValues;

        private DataTable dtTables;
        /// <summary>
        /// Table list.
        /// </summary>
        public DataTable Tables
        {
            get { return dtTables; }
            set 
            { 
                dtTables = value;
                NotifyPropertyChanged("Tables");
            }
        }

        private DataTable dttableValues;
        /// <summary>
        /// Table information.
        /// </summary>
        public DataTable TableValues
        {
            get { return dttableValues; }
            set
            {
                dttableValues = value;
                NotifyPropertyChanged("TableValues");
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        
        // Constructor
        public InfoViewModel()
        {
            sqlDATableValues = new SqlDataAdapter();
            sqlDATables = new SqlDataAdapter();

            Tables = new DataTable();
            TableValues = new DataTable();

            tableValuesCommandBuilder = new SqlCommandBuilder(sqlDATableValues);
        }

        /// <summary>
        /// Loads information about all tables in database.(Loads in other thread.)
        /// </summary>
        private bool firstTimeGettingTables = true;
        public void loadAllTables()
        {
            if (firstTimeGettingTables == false)
                ControlSystem.Factory.getThreadHandlingInstance.removeThread("Tables");
            if(firstTimeGettingTables == true)
                firstTimeGettingTables = false;

            ControlSystem.Factory.getThreadHandlingInstance.addThread(_loadAllTables, "Tables");
            ControlSystem.Factory.getThreadHandlingInstance.start("Tables");
        }

        // Real implementation
        private void _loadAllTables()
        {
            try
            {
                SqlCommand tempCommandTables = SQLHandler.GetInstance.makeCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", CommandType.Text);
                sqlDATables.SelectCommand = tempCommandTables;

                DataTable tempTable = new DataTable();
                sqlDATables.Fill(tempTable);

                tempTable.Columns[0].ColumnName = "Tables:"; // Better than TABLE_NAME
                Tables = tempTable;
            }
            catch (SqlException exc)
            {
                // Handle error
                UIService.showMessageBox(exc.Message, "Getting table values.");
            }
        }

        /// <summary>
        /// Gets information from table.(Saved in TableValues.)
        /// </summary>
        /// <param name="tableName">Name of table.</param>
        public void getTableInfo(string _objTableName)
        {
            try
            {
                string tableName = (string) _objTableName;
                SqlCommand tempCommandTableValues = SQLHandler.GetInstance.makeCommand("SELECT * FROM " + tableName, CommandType.Text);
                sqlDATableValues.SelectCommand = tempCommandTableValues;

                DataTable tempTable = new DataTable();

                sqlDATableValues.Fill(tempTable);

                TableValues = tempTable;
            }
            catch (SqlException exc)
            {
                // Handle error
                UIService.showMessageBox(exc.Message, "Getting table values.");
            }
        }

        /// <summary>
        /// Saves table edits.
        /// </summary>
        public void tableSave()
        {
            try
            {
                tableValuesCommandBuilder.RefreshSchema();
                sqlDATableValues.Update(TableValues);
            }
            catch (SqlException exc)
            {
                // Handle error
                UIService.showMessageBox(exc.Message, "Getting table values.");
            }
        }
    }
}
