﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddNewTester.xaml
    /// </summary>
    public partial class SetTester : Window
    {
        BE.Tester tester;
        BL.IBL bl;
        public SetTester()
        {
            InitializeComponent();
            tester = new BE.Tester();
            this.grid1.DataContext = tester;

            bl = BL.FactoryBL.GetBL();

            this.TesterGenderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.TesterTypeOfVehicle.ItemsSource = Enum.GetValues(typeof(BE.TypeOfVehicle));
            this.TesterTypeOfGearControl.ItemsSource = Enum.GetValues(typeof(BE.TypeOfGearControl));
            this.town1.Text = tester.TesterAddress.town;
            this.buildingNumber1.Text = tester.TesterAddress.ToString();
            this.street1.Text = tester.TesterAddress.street;          
        }
   
   
        private void setHour(object sender, RoutedEventArgs e)
        {
            addButton.IsEnabled = true;
            //row1colum1
            string help = "rowColum";
            for (int i = 1; i < 5; i++)
            {
                for (int j = 1; j < 6; j++)
                {
                    help = "rowColum" + i + j;
                    CheckBox check = (CheckBox)matrix.FindName(help);
                    if (check.IsChecked == true)
                        tester.WorkingHours[i - 1, j - 1] = (bool)check.IsChecked;
                }
            }
        }

        private void endProsses_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tester.TesterAddress = new BE.Address { street = this.street1.Text, buildingNumber = int.Parse(this.buildingNumber1.Text), town = town1.Text };
                bl.SetTesterBL(tester);
                tester = new BE.Tester();
                this.grid1.DataContext = tester;
                throw new Exception("פרטיך עודכנו במערכת");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }
    }
}
