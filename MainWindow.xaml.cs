using labb3enSida.Model;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml.Linq;

// Labb tre BookingSystem 2.0
namespace labb3enSida
{
    public partial class MainWindow : Window
    {

        // En Lista (LIST)
        List<IBooking> TableBookList = new List<IBooking>();

      
        public MainWindow()
        {
            InitializeComponent();
            PermBok();
        }

        // innehåll comboBox bokningstider  (FOR / FOREACH )
        private void comboBoxTime_Loaded(object sender, RoutedEventArgs e)
        {
            string[] TableTime = { "20:00", "20:30", "21:00", "21:30", "22:00", "22:30", "23:00" };
            foreach (string tid in TableTime)
            {
                comboBoxTime.Items.Add(tid.ToString());

            }
        }
        // innehåll comboBox Bordsnummer
        private void ComboBoxTable_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i =  1; i < 8;  i++)
            {
                ComboBoxTable.Items.Add (i.ToString());
            }
        }
        
        // innehåll Listbox permanenta förbokningar 
        private void PermBok()
        {
            TableDate.SelectedDate = DateTime.Now;

            TableBookList.Add(new Booking("2022-11-02", "Adam ", "22:00", "5"));
            TableBookList.Add(new Booking("2022-11-03 ", "Kia ", "21:00", " 1"));
            AddToList();
        }

        // lägger till ovanstående bokningar i listan
        private void AddToList()
        {
            listboxTable.Items.Clear();

            TableDate.SelectedDate = DateTime.Now;

            foreach (var Booking in TableBookList)
            {
                listboxTable.Items.Add(
                      Booking.Name.ToString() + " - " 
                    + Booking.Day.ToString() + " - " 
                    + Booking.Time.ToString() + ", " 
                    + Booking.TableNum.ToString());
                    
            }
        }

        // Book knappen  (IF/ELSE) (QUERY) 
        private void btnBook_Click_1(object sender, RoutedEventArgs e)
        {
            DateTime? selectedDate = TableDate.SelectedDate;
            if (!string.IsNullOrWhiteSpace(txtName.Text) &&
                !listboxTable.Items.Contains(txtName.Text) &&
                selectedDate.HasValue &&
                comboBoxTime.SelectedItem != null &&
                ComboBoxTable.SelectedItem != null
                )
                // Query 
            {
                var BookingQuery =
                    (from selectedBooking in TableBookList
                     where selectedBooking.Time == comboBoxTime.SelectedItem.ToString()
                     && selectedBooking.Day == selectedDate.Value.ToString("yyyy-MM-dd")
                     && selectedBooking.TableNum == ComboBoxTable.SelectionBoxItem.ToString()
                     select selectedBooking).Any();


                // Meddelande vid dubbelbokning
                if (BookingQuery)
                {
                    MessageBox.Show("There is already a reservation\n" + "Choose another time\r\n");
                }
                
                else
                {
                    TableBookList.Add(
                        new Booking(selectedDate.Value.ToString("yyyy-MM-dd"),
                            txtName.Text.ToString(),
                            comboBoxTime.SelectedItem.ToString(),
                            ComboBoxTable.SelectedItem.ToString()
                        )
                    );

                    // Lägger till bokning om all ok
                    MessageBox.Show("Booking Confirmed");
                    AddToList();
                    txtName.Clear();
                }
                
            }
            // Om information saknas
            else
            {
                MessageBox.Show("Missing Info");
            }


        }

        // Ta bort bokning knapp
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listboxTable.SelectedItem != null)
            {
                TableBookList.RemoveAt(listboxTable.SelectedIndex);
                AddToList();
            }

        }

        // Show/Hide lista knapp 
        private void btnShowList_Click(object sender, RoutedEventArgs e)
        {
            if (this.listboxTable.Visibility == Visibility.Collapsed)
            {
                this.listboxTable.Visibility = Visibility.Visible;
                this.btnShowList.Content = "Hide List"; 
                this.btnDelete.Visibility = Visibility.Visible;
                this.ListInfo.Visibility = Visibility.Visible;
            }
            else 
            {
                this.listboxTable.Visibility = Visibility.Collapsed;
                this.btnShowList.Content = "Show List";
                this.btnDelete.Visibility = Visibility.Collapsed;
                this.ListInfo.Visibility = Visibility.Collapsed;
            }

        }

        // Textbox Name (Enbart bokstäver kan matas in)
        private void txtName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            {
                Regex regex = new Regex("[^A-Öa-ö]+");
                e.Handled = regex.IsMatch(e.Text);
            }
        }
    }

}
