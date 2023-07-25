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

namespace Behaviors
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MouseDragElementBehavior_DragBegun(object sender, MouseEventArgs e)
        {
            Lbl_Drag.Content = "ON THE MOVE";
        }

        private void MouseDragElementBehavior_DragFinished(object sender, MouseEventArgs e)
        {
            Lbl_Drag.Content = "DROPPED";
        }
    }
}
