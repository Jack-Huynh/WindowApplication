using A4XAMLMenus;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace FaceMaker
{
    public partial class MainWindow : Window
    {

        string[] hairArr = { "/Images/hair1.png", "/Images/hair2.png" };
        string[] eyesArr = { "/Images/eyes1.png", "/Images/eyes2.png" };
        string[] mouthArr = { "/Images/mouth1.png", "/Images/mouth2.png" };

        int hairIndex = 0;
        int eyesIndex = 0;
        int mouthIndex = 0;

        KeyBind H1 = new(Hair1, true);
        KeyBind H2 = new(Hair2, true);
        KeyBind E1 = new(Eyes1, true);
        KeyBind E2 = new(Eyes2, true);
        KeyBind M1 = new(Mouth1, true);
        KeyBind M2 = new(Mouth2, true);
        KeyBind R = new(RandomFace, true);

        Function func = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new
            {
                H1 = H1,
                H2 = H2,
                E1 = E1,
                E2 = E2,
                M1 = M1,
                M2 = M2,
                R = R
            };

            InputBindings.Add(new KeyBinding(H1, new KeyGesture(Key.F1, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(H2, new KeyGesture(Key.F2, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(E1, new KeyGesture(Key.F3, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(E2, new KeyGesture(Key.F4, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(M1, new KeyGesture(Key.F5, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(M2, new KeyGesture(Key.F6, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(R, new KeyGesture(Key.R, ModifierKeys.Control)));
        }
        public static void Hair1()
        {
            MainWindow thisWindow = ((MainWindow)Application.Current.MainWindow);
            Function thisFunc = thisWindow.func;
            Image Hair = thisWindow.Hair;
            thisFunc.Display_Image(Hair, "hair1.png");
        }

        public static void Hair2()
        {
            MainWindow thisWindow = ((MainWindow)Application.Current.MainWindow);
            Function thisFunc = thisWindow.func;
            Image Hair = thisWindow.Hair;
            thisFunc.Display_Image(Hair, "hair2.png");
        }

        public static void Eyes1()
        {
            MainWindow thisWindow = ((MainWindow)Application.Current.MainWindow);
            Function thisFunc = thisWindow.func;
            Image Eyes = thisWindow.Eyes;
            thisFunc.Display_Image(Eyes, "eyes1.png");
        }

        public static void Eyes2()
        {
            MainWindow thisWindow = ((MainWindow)Application.Current.MainWindow);
            Function thisFunc = thisWindow.func;
            Image Eyes = thisWindow.Eyes;
            thisFunc.Display_Image(Eyes, "eyes2.png");
        }

        public static void Mouth1()
        {
            MainWindow thisWindow = ((MainWindow)Application.Current.MainWindow);
            Function thisFunc = thisWindow.func;
            Image Mouth = thisWindow.Mouth;
            thisFunc.Display_Image(Mouth, "mouth1.png");
        }

        public static void Mouth2()
        {
            MainWindow thisWindow = ((MainWindow)Application.Current.MainWindow);
            Function thisFunc = thisWindow.func;
            Image Mouth = thisWindow.Mouth;
            thisFunc.Display_Image(Mouth, "mouth2.png");
        }

        public static void RandomFace()
        {
            MainWindow thisWindow = ((MainWindow)Application.Current.MainWindow);
            Function thisFunc = thisWindow.func;
            thisFunc.RandomPart(thisWindow.Hair, thisWindow.hairArr, ref thisWindow.hairIndex);
            thisFunc.RandomPart(thisWindow.Eyes, thisWindow.eyesArr, ref thisWindow.eyesIndex);
            thisFunc.RandomPart(thisWindow.Mouth, thisWindow.mouthArr, ref thisWindow.mouthIndex);
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

        public event EventHandler? CanExecuteChanged;

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