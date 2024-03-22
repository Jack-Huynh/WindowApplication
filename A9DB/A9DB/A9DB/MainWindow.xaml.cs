using A9DB;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Data;
namespace FaceMaker
{
    public partial class MainWindow : Window
    {

        string connstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\PROG2500\mygit\WindowApplication\A9DB\A9DB\A9DB\DB_Bind.mdf;Integrated Security=True";

        // Declare DataTable class-wide, so delete button can access
        DataTable dt;

        // update needs current primary key
        int current_primary_key = 0;

        bool isRunning = false;

        string[] hairArr = { "/Images/hair1.png", "/Images/hair2.png" };
        string[] eyesArr = { "/Images/eyes1.png", "/Images/eyes2.png" };
        string[] mouthArr = { "/Images/mouth1.png", "/Images/mouth2.png" };

        int hairIndex = 0;
        int eyesIndex = 0;
        int mouthIndex = 0;

        KeyBind H1 = new KeyBind(Hair1, true);
        KeyBind H2 = new KeyBind(Hair2, true);
        KeyBind E1 = new KeyBind(Eyes1, true);
        KeyBind E2 = new KeyBind(Eyes2, true);
        KeyBind M1 = new KeyBind(Mouth1, true);
        KeyBind M2 = new KeyBind(Mouth2, true);
        KeyBind R = new KeyBind(RandomFace, true);
        KeyBind K = new KeyBind(HotKey, true);
        KeyBind H = new KeyBind(Home, true);

        Function func = new Function();

        public MainWindow()
        {

            InitializeComponent();

            FillDataGrid();

            isRunning = true;

            DataContext = new
            {
                H1 = H1,
                H2 = H2,
                E1 = E1,
                E2 = E2,
                M1 = M1,
                M2 = M2,
                R = R,
                K = K,
                H = H
            };

            InputBindings.Add(new KeyBinding(H1, new KeyGesture(Key.F1, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(H2, new KeyGesture(Key.F2, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(E1, new KeyGesture(Key.F3, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(E2, new KeyGesture(Key.F4, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(M1, new KeyGesture(Key.F5, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(M2, new KeyGesture(Key.F6, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(R, new KeyGesture(Key.R, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(K, new KeyGesture(Key.K, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(H, new KeyGesture(Key.H, ModifierKeys.Control)));
        }

        public static void Hair1()
        {
            MainWindow thisWindow = ((MainWindow)System.Windows.Application.Current.MainWindow);

            Function thisFunc = thisWindow.func;
            Image Hair = thisWindow.Hair;
            thisFunc.Display_Image(Hair, "hair1.png");
        }

        public static void Hair2()
        {
            MainWindow thisWindow = ((MainWindow)System.Windows.Application.Current.MainWindow);

            Function thisFunc = thisWindow.func;
            Image Hair = thisWindow.Hair;
            thisFunc.Display_Image(Hair, "hair2.png");
        }

        public static void Eyes1()
        {
            MainWindow thisWindow = ((MainWindow)System.Windows.Application.Current.MainWindow);
            Function thisFunc = thisWindow.func;
            Image Eyes = thisWindow.Eyes;
            thisFunc.Display_Image(Eyes, "eyes1.png");
        }

        public static void Eyes2()
        {
            MainWindow thisWindow = ((MainWindow)System.Windows.Application.Current.MainWindow);
            Function thisFunc = thisWindow.func;
            Image Eyes = thisWindow.Eyes;
            thisFunc.Display_Image(Eyes, "eyes2.png");
        }

        public static void Mouth1()
        {
            MainWindow thisWindow = ((MainWindow)System.Windows.Application.Current.MainWindow);
            Function thisFunc = thisWindow.func;
            Image Mouth = thisWindow.Mouth;
            thisFunc.Display_Image(Mouth, "mouth1.png");
        }

        public static void Mouth2()
        {
            MainWindow thisWindow = ((MainWindow)System.Windows.Application.Current.MainWindow);
            Function thisFunc = thisWindow.func;
            Image Mouth = thisWindow.Mouth;
            thisFunc.Display_Image(Mouth, "mouth2.png");
        }

        public static void RandomFace()
        {
            MainWindow thisWindow = ((MainWindow)System.Windows.Application.Current.MainWindow);
            Function thisFunc = thisWindow.func;
            thisFunc.RandomPart(thisWindow.Hair, thisWindow.hairArr, ref thisWindow.hairIndex);
            thisFunc.RandomPart(thisWindow.Eyes, thisWindow.eyesArr, ref thisWindow.eyesIndex);
            thisFunc.RandomPart(thisWindow.Mouth, thisWindow.mouthArr, ref thisWindow.mouthIndex);
        }
        public static void HotKey()
        {
            HelpNavigator Nav_by_Topic = HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "FaceHelp.chm", Nav_by_Topic, "HotKey.htm");
        }

        public static void Home()
        {
            HelpNavigator Nav_by_Topic = HelpNavigator.Topic;
            System.Windows.Forms.Help.ShowHelp(null, "FaceHelp.chm", Nav_by_Topic, "Home.htm");
        }

        private void FillDataGrid()
        {

            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(connstr))
            {
                // pull whatever columns we want on the grid...get all
                CmdString = "SELECT Id, fname, lname, city FROM Person";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);             // adapter to connect both sides
                dt = new DataTable("gridPerson");
                sda.Fill(dt);
                gridPerson.ItemsSource = dt.DefaultView;  // Fill grid with DataTable
            }
        }

        // Add a person record with these attributes...SQL INSERT command
        private void addPerson(String fn, String ln, String city)
        {
            // Old school connection
            SqlConnection conn = new SqlConnection(connstr);

            // old school insert statement...note Trace output should show format of SQL Insert command
            String cmd_Text = "INSERT INTO person(fname, lname, city)  VALUES('" + fn + "', '" + ln + "', '" + city + "');";
            Trace.Write(cmd_Text);

            // DB insert in try-catch
            try
            {
                // Example of C# named parameters...a good idea for important library calls
                SqlCommand command = new SqlCommand(cmdText: cmd_Text, connection: conn);
                command.Connection.Open();
                command.ExecuteNonQuery();  //does the actual insert statement

            }
            catch { System.Windows.MessageBox.Show("DB Add Exception"); }
            finally { conn.Close(); }
        }

        // Add a person record with these attributes...SQL INSERT command
        private void delPerson(int pkey)
        {
            // Old school connection
            SqlConnection conn = new SqlConnection(connstr);

            // old school insert statement...note Trace output should show format of SQL Insert command
            String cmd_Text = "DELETE FROM Person WHERE Id = " + pkey + ";";
            Trace.Write(cmd_Text);

            // DB insert in try-catch
            try
            {
                // Example of C# named parameters...a good idea for important library calls
                SqlCommand command = new SqlCommand(cmdText: cmd_Text, connection: conn);
                command.Connection.Open();
                command.ExecuteNonQuery();  //does the actual insert statement
            }
            catch { System.Windows.MessageBox.Show("DB Delete Exception"); }
            finally { conn.Close(); }

        }

        // Add a person record with these attributes...SQL INSERT command
        private void upPerson(int pkey, String fn, String ln, String city)
        {
            // Old school connection
            SqlConnection conn = new SqlConnection(connstr);

            // old school insert statement...note Trace output should show format of SQL Insert command
            String cmd_Text =
                "UPDATE Person SET fname = '" + fn +
                "', lname = '" + ln +
                "', city = '" + city +
                "'  WHERE Id = " + pkey + ";";
            Trace.Write(cmd_Text);

            // DB insert in try-catch
            try
            {
                // Example of C# named parameters...a good idea for important library calls
                SqlCommand command = new SqlCommand(cmdText: cmd_Text, connection: conn);
                command.Connection.Open();
                command.ExecuteNonQuery();  //does the actual insert statement
            }
            catch { System.Windows.MessageBox.Show("DB Delete Exception"); }
            finally { conn.Close(); }

        }

        // Gets called when grid is selected
        private void gridPerson_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // When we get here after deleting a row, we can't get the current row
            if (gridPerson.SelectedItem != null && isRunning && (gridPerson.SelectedItem as DataRowView).Row["Id"] != null)
            {
                try
                {
                    // fetch the columns from the selected row
                    current_primary_key = (int)(gridPerson.SelectedItem as DataRowView).Row.ItemArray[0];
                    tb_first.Text = (string)(gridPerson.SelectedItem as DataRowView).Row.ItemArray[1];
                    tb_last.Text = (string)(gridPerson.SelectedItem as DataRowView).Row.ItemArray[2];
                    tb_city.Text = (string)(gridPerson.SelectedItem as DataRowView).Row.ItemArray[3];

                    Trace.WriteLine("Selected = " + current_primary_key + tb_first.Text + tb_last.Text);
                }
                catch
                {
                    // If deleting row, get exception trying to get it's data
                    Trace.WriteLine("No Row (deleted?)...default record used");
                    current_primary_key = -1;
                    tb_first.Text = ":(";
                    tb_last.Text = ":(";
                    tb_city.Text = ":(";
                }
            }
                
        }

        private void searchDB(string query)
        {
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(connstr))
            {
                CmdString = "SELECT " +
                            "Id, " +
                            "fname, " +
                            "lname, " +
                            "city " +
                            "FROM Person " +
                            "WHERE lname = @query"; // Using parameterized query to prevent SQL injection
                SqlCommand cmd = new SqlCommand(CmdString, con);
                cmd.Parameters.AddWithValue("@query", query);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                dt = new DataTable("gridPerson");
                sda.Fill(dt);
                gridPerson.ItemsSource = dt.DefaultView;
            }
        }

        private int currentIndex = 0;

        // Event handler for the Next button click
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex < dt.Rows.Count - 1)
            {
                currentIndex++;
                gridPerson.SelectedIndex = currentIndex;
                gridPerson.ScrollIntoView(gridPerson.SelectedItem);
            }
        }

        // Event handler for the Previous button click
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                gridPerson.SelectedIndex = currentIndex;
                gridPerson.ScrollIntoView(gridPerson.SelectedItem);
            }
        }

        private void b_Add_Click(object sender, RoutedEventArgs e)
        {
            String fn = tb_first.Text;
            String ln = tb_last.Text;
            String city = tb_city.Text;

            // Add this record if values not empty
            if (fn != "" && ln != "" && city != "")
            {
                this.addPerson(fn, ln, city);  // old school SQL-over-the-connection
                //this.addPerson1(fn, ln, city);  // Stored procedure
            }
            else
            {
                System.Windows.MessageBox.Show("A field is empty...enter all fields");
            }

            // Update changes to the grid
            FillDataGrid();
        }

        private void b_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (gridPerson.SelectedItem != null)
            {
                int key = (int)(gridPerson.SelectedItem as DataRowView).Row.ItemArray[0];
                delPerson(key);

                // These lines update the grid, but not the database
                dt.Rows.Remove((gridPerson.SelectedItem as DataRowView).Row);
                dt.AcceptChanges();
            }
        }

        private void b_Update_Click(object sender, RoutedEventArgs e)
        {
            if (current_primary_key > -1)
            {
                upPerson(current_primary_key, tb_first.Text, tb_last.Text, tb_city.Text);

                // Update changes to the grid
                FillDataGrid();

                // These lines update the grid, but not the database
                //dt.Rows.Remove((gridPerson.SelectedItem as DataRowView).Row);
                //dt.AcceptChanges();
            }
        }

        private void b_Search_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = tb_SearchBar.Text;
            searchDB(searchQuery);
        }

    }
    public class KeyBind : ICommand
    {
        private Action _action;
        private bool _canExecute;
        private Action<object, RoutedEventArgs> onSelectHairStyle1;
        private bool v;

        public KeyBind(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            _action();
        }

    }
    internal class Function
    {
        public void Display_Image(Image image, string imageName)
        {
            SetImageSource(image, $"Images/{imageName}");
        }

        private void SetImageSource(Image image, string imagePath)
        {
            image.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
        }

        public void RandomPart(Image image, string[] array, ref int i)
        {
            Random rand = new Random();
            i = rand.Next(array.Length);
            image.Source = new BitmapImage(new Uri(array[i], UriKind.Relative));
        }
    }
}