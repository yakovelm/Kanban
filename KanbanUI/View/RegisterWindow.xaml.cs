﻿using System;
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
using System.Windows.Shapes;
using KanbanUI.ViewModel;

namespace KanbanUI.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private RegisterViewModel RVM;

        public RegisterWindow(BackendController controller)
        {
            InitializeComponent();
            RVM = new RegisterViewModel(controller);
            DataContext = RVM;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RVM.register();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
