using A5CreateHelp;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Forms;

namespace FaceMaker
{
    public partial class MainWindow : Window
    {

        string[] hairArr = { "/hair1.png", "/hair2.png" };
        string[] eyesArr = { "/eyes1.png", "/eyes2.png" };
        string[] mouthArr = { "/mouth1.png", "/mouth2.png" };

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
            SetImageSource(image, $"/{imageName}");
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