/** \file infoViewModel.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
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

        private DataGrid tkt;
        // Constructor
        public InfoViewModel(DataGrid moo)
        {
            sqlDATableValues = new SqlDataAdapter();
            sqlDATables = new SqlDataAdapter();

            Tables = new DataTable();
            TableValues = new DataTable();

            tableValuesCommandBuilder = new SqlCommandBuilder(sqlDATableValues);

            tkt = moo;
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
                UIService.showMessageBox(exc.Message, "Getting table values.", MessageBoxButton.OK, MessageBoxImage.Information);
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
                UIService.showMessageBox(exc.Message, "Getting table values.", MessageBoxButton.OK, MessageBoxImage.Information);
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
                UIService.showMessageBox(exc.Message, "Getting table values.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void tablePrint()
        {
            PrintDialog printDlg = new PrintDialog();
            if (printDlg.ShowDialog() == true)
            {
                System.Printing.PrintCapabilities capabilities = printDlg.PrintQueue.GetPrintCapabilities(printDlg.PrintTicket);

                double height = tkt.ActualHeight;
                double width = tkt.ActualWidth;
                Transform tempTrans = tkt.LayoutTransform;

                double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth/tkt.ActualWidth,
                                        capabilities.PageImageableArea.ExtentHeight/tkt.ActualHeight);

                tkt.LayoutTransform = new ScaleTransform(scale, scale);

                Size sz = new Size(capabilities.PageImageableArea.ExtentWidth,
                                   capabilities.PageImageableArea.ExtentHeight);

                tkt.Measure(sz);
                tkt.Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));
                
                printDlg.PrintVisual(tkt, "DatabasePrint");
                
                tkt.LayoutTransform = tempTrans;
            }
        }
    }
}
