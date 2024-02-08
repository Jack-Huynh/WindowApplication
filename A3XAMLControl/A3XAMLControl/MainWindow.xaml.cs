using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace A3XAMLControl
{
    public partial class MainWindow : Window
    {
        string[] hairArr = { "/Images/hair1.png", "/Images/hair2.png" };
        string[] eyesArr = { "/Images/eyes1.png", "/Images/eyes2.png" };
        string[] noseArr = { "/Images/nose1.png", "/Images/nose2.png" };
        string[] mouthArr = { "/Images/mouth1.png", "/Images/mouth2.png" };

        int hairIndex = 0;
        int eyesIndex = 0;
        int noseIndex = 0;
        int mouthIndex = 0;

        public MainWindow()
        {
            InitializeComponent();

            hairCheck.Checked += (sender, e) => UpdateHair();
            hairCheck.Unchecked += (sender, e) => UpdateHair();

            UpdateHair();

            EyesSelection.Items.Add("Eyes 1");
            EyesSelection.Items.Add("Eyes 2");
        }

        private void UpdateHair()
        {
            if (hairCheck.IsChecked == true)
            {
                Hair.Source = new BitmapImage(new Uri("/Images/hair1.png", UriKind.Relative));
            }
            else
            {
                Hair.Source = null;
            }
        }

        private void eyesCombo(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            int selectedIndex = comboBox.SelectedIndex;

            if (selectedIndex >= 0 && selectedIndex < eyesArr.Length)
            {
                Eyes.Source = new BitmapImage(new Uri(eyesArr[selectedIndex], UriKind.Relative));
            }
        }

        private void Slider(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Random rand = new Random();
            hairIndex = rand.Next(hairArr.Length);
            eyesIndex = rand.Next(eyesArr.Length);
            noseIndex = rand.Next(noseArr.Length);
            mouthIndex = rand.Next(mouthArr.Length);

            Hair.Source = new BitmapImage(new Uri(hairArr[hairIndex], UriKind.Relative));
            Eyes.Source = new BitmapImage(new Uri(eyesArr[eyesIndex], UriKind.Relative));
            Nose.Source = new BitmapImage(new Uri(noseArr[noseIndex], UriKind.Relative));
            Mouth.Source = new BitmapImage(new Uri(mouthArr[mouthIndex], UriKind.Relative));
        }

        private void Radio1(object sender, RoutedEventArgs e)
        {
            Mouth.Source = new BitmapImage(new Uri("/Images/mouth1.png", UriKind.Relative));
        }

        private void Radio2(object sender, RoutedEventArgs e)
        {
            Mouth.Source = new BitmapImage(new Uri("/Images/mouth2.png", UriKind.Relative));
        }
    }
}
