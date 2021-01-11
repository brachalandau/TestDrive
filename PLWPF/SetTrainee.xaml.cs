using System;
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
    /// Interaction logic for AddNewTrainee.xaml
    /// </summary>
    public partial class SetTrainee : Window
    {
        BE.Trainee trainee;
        BL.IBL bl;

        public SetTrainee()
        {
            InitializeComponent();
            trainee = new BE.Trainee();
            this.grid2.DataContext = trainee;
            bl = BL.FactoryBL.GetBL();

            this.TraineeGenderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.TypeOfVehicle.ItemsSource = Enum.GetValues(typeof(BE.TypeOfVehicle));
            this.TypeOfGearControl.ItemsSource = Enum.GetValues(typeof(BE.TypeOfGearControl));
            this.town1.Text = trainee.StudentAddress.town;
            this.buildingNumber1.Text = trainee.StudentAddress.ToString();
            this.street1.Text = trainee.StudentAddress.street;
           
        }
       
        private void endProsses_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                trainee.StudentAddress = new BE.Address { street = this.street1.Text, buildingNumber = int.Parse(this.buildingNumber1.Text), town = town1.Text };
                bl.SetTraineeBL(trainee);
                trainee = new BE.Trainee();
                this.grid2.DataContext = trainee;
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
