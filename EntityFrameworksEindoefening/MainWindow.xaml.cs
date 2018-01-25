using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EntityFrameworksEindoefening
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LandenStedenTalenEntities entities = new LandenStedenTalenEntities();
        public MainWindow()
        {
            InitializeComponent();
            VulList_landen();
        }
        private void VulList_landen()
        {
            List_Landen.ItemsSource = (from land in entities.Landen
                                       orderby land.Naam
                                       select land).ToList();
        }
        private void VulList_Steden()
        {
            if (List_Landen.SelectedItem!=null)
                List_Steden.ItemsSource = ((Land)List_Landen.SelectedItem).Steden;
        }
        private void VulList_Talen()
        {
            if (List_Landen.SelectedItem != null)
                List_Talen.ItemsSource = ((Land)List_Landen.SelectedItem).Talen;
        }

        private void List_Landen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VulList_Steden();
            VulList_Talen();
        }
        
        private int LaatstGekozen;
        private void Button_bijvoegen_Click(object sender, RoutedEventArgs e)
        {
            if(Textbox_Stad.Text!=string.Empty&&Textbox_Stad.Text!=null)
            {
                if(List_Landen.SelectedItem!=null)
                {
                    LaatstGekozen = List_Landen.SelectedIndex;
                    entities.Steden.Add(new Stad { Naam = Textbox_Stad.Text, Land = (Land)List_Landen.SelectedItem });
                    entities.SaveChanges();
                    Textbox_Stad.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("Er is geen land geselecteerd in de listbox");
                }
            }
            else
            {
                MessageBox.Show("De textbox is leeg");
            }
        }
    }
}
