using A10DB2;
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

        string connstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\PROG2500\mygit\WindowApplication\A10DB2\A10DB2\A10DB2\Database1.mdf;Integrated Security=True";

        DataTable dt;

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

            FillComboBoxes();

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

        private void FillComboBoxes()
        {
            using (SqlConnection connection = new SqlConnection(connstr))
            {
                List<Occupation> occupations = new List<Occupation>();
                string queryOccupation = "SELECT Id, Name FROM Occupation";
                SqlCommand commandOccupation = new SqlCommand(queryOccupation, connection);
                connection.Open();
                SqlDataReader readerOccupation = commandOccupation.ExecuteReader();
                while (readerOccupation.Read())
                {
                    occupations.Add(new Occupation { Id = (int)readerOccupation["Id"], Name = (string)readerOccupation["Name"] });
                }
                readerOccupation.Close();
                cmbOccupation.ItemsSource = occupations;
                cmbOccupation.DisplayMemberPath = "Name";

                List<Hobby> hobbies = new List<Hobby>();
                string queryHobbies = "SELECT Id, Name FROM Hobbies";
                SqlCommand commandHobbies = new SqlCommand(queryHobbies, connection);
                SqlDataReader readerHobbies = commandHobbies.ExecuteReader();
                while (readerHobbies.Read())
                {
                    hobbies.Add(new Hobby { Id = (int)readerHobbies["Id"], Name = (string)readerHobbies["Name"] });
                }
                readerHobbies.Close();
                cmbHobbies.ItemsSource = hobbies;
                cmbHobbies.DisplayMemberPath = "Name";
            }
        }
        private void FillDataGrid()
        {

            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(connstr))
            {
                // pull whatever columns we want on the grid...get all
                CmdString = "SELECT p.Id, p.fname, p.lname, p.city, o.Name AS Occupation, h.Name AS Hobbies " +
                       "FROM Person p " +
                       "LEFT JOIN Occupation o ON p.occupationID = o.Id " +
                       "LEFT JOIN Hobbies h ON p.hobbiesID = h.Id";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                dt = new DataTable("gridPerson");
                sda.Fill(dt);
                gridPerson.ItemsSource = dt.DefaultView;  // Fill grid with DataTable
            }
        }

        // Add a person record with these attributes...SQL INSERT command
        private void AddPerson(string firstName, string lastName, string city, int occupationId, int hobbiesId)
        {
            using (SqlConnection connection = new SqlConnection(connstr))
            {
                string query = "INSERT INTO Person (fname, lname, city, occupationID, hobbiesID) " +
                               "VALUES (@FirstName, @LastName, @City, @OccupationId, @HobbiesId)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@City", city);
                command.Parameters.AddWithValue("@OccupationId", occupationId);
                command.Parameters.AddWithValue("@HobbiesId", hobbiesId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void DeletePerson(int id)
        {
            using (SqlConnection connection = new SqlConnection(connstr))
            {
                string query = "DELETE FROM Person WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void UpdatePerson(int id, string firstName, string lastName, string city, int occupationId, int hobbiesId)
        {
            using (SqlConnection connection = new SqlConnection(connstr))
            {
                string query = "UPDATE Person SET fname = @FirstName, lname = @LastName, city = @City, " +
                               "occupationID = @OccupationId, hobbiesID = @HobbiesId WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@City", city);
                command.Parameters.AddWithValue("@OccupationId", occupationId);
                command.Parameters.AddWithValue("@HobbiesId", hobbiesId);
                connection.Open();
                command.ExecuteNonQuery();
            }
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
            string firstName = tb_first.Text;
            string lastName = tb_last.Text;
            string city = tb_city.Text;
            int occupationId = ((Occupation)cmbOccupation.SelectedItem).Id;
            int hobbiesId = ((Hobby)cmbHobbies.SelectedItem).Id;

            AddPerson(firstName, lastName, city, occupationId, hobbiesId);

            FillDataGrid();
        }

        private void b_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (gridPerson.SelectedItem != null)
            {
                int id = (int)(gridPerson.SelectedItem as DataRowView).Row["Id"];
                DeletePerson(id);
                FillDataGrid();
            }
        }

        private void b_Update_Click(object sender, RoutedEventArgs e)
        {
            if (gridPerson.SelectedItem != null)
            {
                int id = (int)(gridPerson.SelectedItem as DataRowView).Row["Id"];
                string firstName = tb_first.Text;
                string lastName = tb_last.Text;
                string city = tb_city.Text;
                int occupationId = ((Occupation)cmbOccupation.SelectedItem).Id;
                int hobbiesId = ((Hobby)cmbHobbies.SelectedItem).Id;

                UpdatePerson(id, firstName, lastName, city, occupationId, hobbiesId);

                FillDataGrid();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes();
        }

        private void ClearTextBoxes()
        {
            tb_first.Text = string.Empty;
            tb_last.Text = string.Empty;
            tb_city.Text = string.Empty;
        }

        private void tb_first_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_first.Text = string.Empty;
        }

        private void tb_last_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_last.Text = string.Empty;
        }

        private void tb_city_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_city.Text = string.Empty;
        }


        private void b_Search_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = tb_SearchBar.Text;
            searchDB(searchQuery);
        }

        public class Occupation
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class Hobby
        {
            public int Id { get; set; }
            public string Name { get; set; }
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