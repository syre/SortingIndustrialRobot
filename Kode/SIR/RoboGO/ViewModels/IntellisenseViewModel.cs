﻿using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ControlSystem;

namespace RoboGO.ViewModels
{
    public class IntellisenseViewModel
    {
        bool isBig = false;
        private string know=string.Empty;
        private int i;
        private MethodList methodList;
        private ObservableCollection<string> _updatedUpdatedCollection;

        public IntellisenseViewModel()
        {
            MethodList = new MethodList();
            UpdatedCollection = new ObservableCollection<string>();
        }

        public ObservableCollection<string> UpdatedCollection
        {
            get { return _updatedUpdatedCollection; }
            set { _updatedUpdatedCollection = value; }
        }

        public MethodList MethodList
        {
            get { return methodList; }
            set { methodList = value; }
        }

        //Shiftproblems to be solved
        public void showMethodsPopUP(Rect rect, TextBox txtbox, Popup popup, ListBox list,KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.LeftShift:
                case Key.RightShift:
                    isBig = true;
                    break;
                case Key.Right:
                case Key.Tab:
                case Key.Up:
                case Key.Left:
                break;
                case Key.Space:
                case Key.Enter:
                    popup.IsOpen = false;
                    know = string.Empty;
                    break;
                case Key.Down:
                    if (popup.IsOpen) list.Focus();
                    break;
                case Key.Back:
                    if(know.Length > 0)
                    know = know.Substring(0, know.Length - 1);
                    know = know.ToLower();

                    if (RearrangeList())
                        ShowPopUpInRightPlace(rect, txtbox, popup);

                    break;
                default:
                    if (isBig)
                    {
                        know += e.Key.ToString();
                        isBig = false;
                    }
                    else know += e.Key.ToString().ToLower();
                
 
                    if(RearrangeList())
                        ShowPopUpInRightPlace(rect, txtbox, popup);
                    else popup.IsOpen = false;
                    break;
            }

            if (know == string.Empty) popup.IsOpen = false;
        }

        private static void ShowPopUpInRightPlace(Rect rect, TextBox txtbox, Popup popup)
        {
            popup.PlacementTarget = txtbox;
            popup.PlacementRectangle = rect;
            popup.IsOpen = true;
        }

        private bool RearrangeList()
        {
            UpdatedCollection.Clear();

            foreach (var s in MethodList.Where(s=> s.StartsWith(know)))
            {
                UpdatedCollection.Add(s);
            }

            return UpdatedCollection.Count >= 1;
        }

        public void list_KeyDown(ListBox list, KeyEventArgs e, TextBox txtbox, Popup popup)
        {
            i = txtbox.CaretIndex;

            switch (e.Key)
            {
                case Key.Enter:
                    txtbox.Text = Regex.Replace(txtbox.Text, @"\b" + know + @"\b", UpdatedCollection[list.SelectedIndex]);
                    txtbox.CaretIndex = i + UpdatedCollection[list.SelectedIndex].Length;
                    know = string.Empty;
                    popup.IsOpen = false;
                    e.Handled = true;
                    txtbox.Focus();
                    break;
                case Key.Left:
                    popup.IsOpen = false;
                    txtbox.CaretIndex = i;
                    e.Handled = true;
                    txtbox.Focus();
                    break;
            }
        }
    }
}
