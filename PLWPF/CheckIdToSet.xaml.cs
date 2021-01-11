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
using BL;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for CheckSetTester.xaml
    /// </summary>
    public partial class CheckIdToSet : Window
    {
        public CheckIdToSet()
        {
            InitializeComponent();
        }
        public void setTesterDet(Object sender, EventArgs e)
        {
            BL.IBL bl = BL.FactoryBL.GetBL();
            try
            {
                string id = this.setId.Text;
                int newId = int.Parse(id);
                
                Tester isExistTester = bl.getTesterBL(newId);
                if (isExistTester != null)
                {
                    Window helpSetTester = new SetTester();
                    helpSetTester.Show();
                }
                else
                {
                    Trainee isExistTrainee = bl.getTraineeBL(newId);
                    if (isExistTrainee!=null)
                    {
                        Window helpSetTrainee = new SetTrainee();
                        helpSetTrainee.Show();
                    }
                    else
                        throw new Exception("The ID that was entered was not found.");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

    }
}