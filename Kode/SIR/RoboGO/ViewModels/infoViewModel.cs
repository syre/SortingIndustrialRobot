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
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private SqlCommandBuilder tableValuesCommandBuilder;

        public SqlDataAdapter sqlDATables;
        public SqlDataAdapter sqlDATableValues;

        private DataTable dtTables;
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
        public DataTable TableValues
        {
            get { return dttableValues; }
            set
            {
                dttableValues = value;
                NotifyPropertyChanged("TableValues");
            }
        }

        //Needs to be deleted.
        private SQLHandler tempSQL;
        

        public InfoViewModel()
        {
            //Needs to be deleted.
            tempSQL = new SQLHandler();

            sqlDATableValues = new SqlDataAdapter();
            sqlDATables = new SqlDataAdapter();

            Tables = new DataTable();
            TableValues = new DataTable();

            tableValuesCommandBuilder = new SqlCommandBuilder(sqlDATableValues);

        }

        public void loadAllTables()
        {
            SqlCommand tempCommandTables = Factory.getSQLHandlerInstance.makeCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", CommandType.Text);
            sqlDATables.SelectCommand = tempCommandTables;

            DataTable tempTable = new DataTable();
            sqlDATables.Fill(tempTable);
            
            Tables = tempTable;
        }

        public void tableSelectionChanged(string tableName)
        {
            SqlCommand tempCommandTableValues = Factory.getSQLHandlerInstance.makeCommand("SELECT * FROM " + tableName, CommandType.Text);
            sqlDATableValues.SelectCommand = tempCommandTableValues;

            DataTable tempTable = new DataTable();
            sqlDATableValues.Fill(tempTable);
            TableValues = tempTable;
        }

        public void tableSave()
        {
            tableValuesCommandBuilder.RefreshSchema();
            sqlDATableValues.Update(TableValues);
        }
    }
}
