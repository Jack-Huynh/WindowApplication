using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;  //add Threading

using System.IO;  // add File IO
using System.ComponentModel;
using System.Windows.Threading;  //add background worker

namespace kin4
{
    /// <summary>
    /// Demonstrate threads mixing data when not synched
    /// </summary>
    public partial class MainWindow : Window
    {

        TextBox value;

        public MainWindow()
        {
            InitializeComponent();

            // Create and call either the synched version
            // or the not saynched version

            value = Text_Output ;

            //synched.Main m1 = new synched.Main();
            //notsynched.Main m1 = new notsynched.Main();

            //m1.Main1(value);


            // invoke GUI update using GUI thread...to output on textbox
            Dispatcher.Invoke(new Action(() =>
            {
                //outBox.Text = outBox.Text + "\rMAIN=>" + dd.getThreadperson();
                Text_Output.AppendText("Startup");
            }));
        }





        // Clear the textbox...BackgroundWorker keeps running       
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Text_Output.Clear();  // we're on the GUI thread here
        }

        // Send abort to thread 1.  //cancel to BackgroundWorker
        private void Synch_Button_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher Dis = Dispatcher;
            synched.Main m1 = new synched.Main(Dis);
            m1.Main1(value);
        }

        // Restart the existing Thread.
        private void NotSynch_button_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher Dis = Dispatcher;
            notsynched.Main m1 = new notsynched.Main(Dis);
            m1.Main1(value);
        }


    }
}
