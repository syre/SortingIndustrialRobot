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
    public class InfoViewModel
    {

        private SqlCommandBuilder tableValuesCommandBuilder;

        public SqlDataAdapter sqlDATables;
        public SqlDataAdapter sqlDATableValues;

        private DataTable Tables;
        public DataTable tables
        {
            get { return Tables; }
            set 
            { 
                Tables = value;
            }
        }

        private DataTable TableValues;
        public DataTable tableValues
        {
            get { return TableValues; }
            set
            {
                TableValues = value;
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

            tables = new DataTable();
            tableValues = new DataTable();

            tableValuesCommandBuilder = new SqlCommandBuilder(sqlDATableValues);

        }

        public void loadAllTables()
        {
            SqlCommand tempCommandTables = Factory.getSQLHandlerInstance.makeCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", CommandType.Text);
            sqlDATables.SelectCommand = tempCommandTables;

            DataTable tempTable = new DataTable();
            sqlDATables.Fill(tempTable);
            tables = tempTable;
        }

        public void tableSelectionChanged(string tableName)
        {
            SqlCommand tempCommandTableValues = Factory.getSQLHandlerInstance.makeCommand("SELECT * FROM " + tableName, CommandType.Text);
            sqlDATableValues.SelectCommand = tempCommandTableValues;

            DataTable tempTable = new DataTable();
            sqlDATableValues.Fill(tempTable);
            tableValues = tempTable;
        }

        public void tableSave()
        {
            tableValuesCommandBuilder.RefreshSchema();
            sqlDATableValues.Update(tableValues);
        }
    }
}
