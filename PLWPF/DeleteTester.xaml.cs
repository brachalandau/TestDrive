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
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for delete.xaml
    /// </summary>
    public partial class DeleteTester : Window
    {
        
        public DeleteTester()
        {
            InitializeComponent();

        }
        public void deleteTester(Object sender, EventArgs e)
        {
            try
            {
                BL.IBL bl = BL.FactoryBL.GetBL();
                string id = this.checkId.Text;
                int newid = int.Parse(id);

                Tester isExistTester = bl.getTesterBL(newid);                    
                    if (isExistTester != null)
                    {
                        bl.deleteTesterBL(newid);
                        throw new Exception("נמחקת מהמערכת בהצלחה");
                    }
                    else
                    {
                        throw new Exception("הת.ז שהוכנס לא קיים במערכת");
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