using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace FaceMaker
{
    public partial class MainWindow : Window
    {
        string[] hairArr = { "/Images/hair1.png", "/Images/hair2.png"};
        string[] eyesArr = { "/Images/eyes1.png", "/Images/eyes2.png"};
        string[] noseArr = { "/Images/nose1.png", "/Images/nose2.png"};
        string[] mouthArr = { "/Images/mouth1.png", "/Images/mouth2.png"};

        int hairIndex = 0;
        int eyesIndex = 0;
        int noseIndex = 0;
        int mouthIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnSelectHairStyle1(object sender, RoutedEventArgs e)
        {
            DisplayImage(Hair, "hair1.png");
        }

        private void OnSelectHairStyle2(object sender, RoutedEventArgs e)
        {
            DisplayImage(Hair, "hair2.png");
        }

        private void OnSelectEyeStyle1(object sender, RoutedEventArgs e)
        {
            DisplayImage(Eyes, "eyes1.png");
        }

        private void OnSelectEyeStyle2(object sender, RoutedEventArgs e)
        {
            DisplayImage(Eyes, "eyes2.png");
        }

        private void OnSelectNoseStyle1(object sender, RoutedEventArgs e)
        {
            DisplayImage(Nose, "nose1.png");
        }

        private void OnSelectNoseStyle2(object sender, RoutedEventArgs e)
        {
            DisplayImage(Nose,"nose2.png");
        }

        private void OnSelectMouthStyle1(object sender, RoutedEventArgs e)
        {
            DisplayImage(Mouth, "mouth1.png");
        }

        private void OnSelectMouthStyle2(object sender, RoutedEventArgs e)
        {
            DisplayImage(Mouth, "mouth2.png");
        }

        private void OnRandomFace(object sender, RoutedEventArgs e)
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

        private void DisplayImage(Image img, string imageName)
        {
            img.Source = new BitmapImage(new Uri($"Images/{imageName}", UriKind.Relative));
        }
    }
}