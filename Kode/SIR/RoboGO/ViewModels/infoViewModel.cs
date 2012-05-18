/** \file infoViewModel.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
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

        private DataGrid guiDatabaseValues;

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
        public InfoViewModel(DataGrid _databaseValues)
        {
            sqlDATableValues = new SqlDataAdapter();
            sqlDATables = new SqlDataAdapter();

            Tables = new DataTable();
            TableValues = new DataTable();

            tableValuesCommandBuilder = new SqlCommandBuilder(sqlDATableValues);

            guiDatabaseValues = _databaseValues;
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

                if(Factory.currentIUserInstance.permissionDictionary[_objTableName])
                {
                    guiDatabaseValues.IsReadOnly = false;
                    guiDatabaseValues.IsEnabled = true;
                }
                else
                {
                    guiDatabaseValues.IsReadOnly = true;
                    guiDatabaseValues.IsEnabled = false;
                    if(_objTableName == "Users")
                        tempTable.Clear();
                }

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
        /// <summary>
        /// Exports the table to CSV string.
        /// 
        /// Source: http://dotnetpulse.blogspot.com/2006/11/systemdata-export-table-to-csv-file.html
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="printHeaders">
        /// if set to <c>true</c> print headers.</param>
        /// <returns>CSV formated string</returns>
        private static string ExportTableToCsvString(DataTable table, bool printHeaders)
        {
            StringBuilder sb = new StringBuilder();

            if (printHeaders)
            {
                //write the headers.
                for (int colCount = 0;
                     colCount < table.Columns.Count; colCount++)
                {
                    sb.Append(table.Columns[colCount].ColumnName);
                    if (colCount != table.Columns.Count - 1)
                    {
                        sb.Append(",");
                    }
                    else
                    {
                        sb.AppendLine();
                    }
                }
            }

            // Write all the rows.
            for (int rowCount = 0;
                 rowCount < table.Rows.Count; rowCount++)
            {
                for (int colCount = 0;
                     colCount < table.Columns.Count; colCount++)
                {
                    sb.Append(table.Rows[rowCount][colCount]);
                    if (colCount != table.Columns.Count - 1)
                    {
                        sb.Append(",");
                    }
                }
                if (rowCount != table.Rows.Count - 1)
                {
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }
        public void tablePrint()
        {
            /*PrintDialog printDlg = new PrintDialog();
            if (printDlg.ShowDialog() == true)
            {
                /*
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
                
                
            }*/
            if (TableValues.Rows.Count > 0)
            {
                SaveFileDialog sfdDialog = new SaveFileDialog(); /// \warning Using UI dialog.
                sfdDialog.DefaultExt = ".csv";
                sfdDialog.Filter = "CSV Files(*.csv)|*.csv";
                if (sfdDialog.ShowDialog() == true)
                {
                    string cvsData = ExportTableToCsvString(TableValues, true);
                    StreamWriter sw = new StreamWriter(sfdDialog.FileName);
                    sw.Write(cvsData);
                    sw.Close();
                }
            }
        }
    }
}
