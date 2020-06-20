﻿using KanbanUI.Model;
using KanbanUI.ViewModel;
using System.Windows;

namespace KanbanUI.View
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        TaskViewModel TVM;
        public TaskWindow(UserModel um)
        {
            InitializeComponent();
            TVM = new TaskViewModel(um);
            DataContext = TVM;
        }
        public TaskWindow(TaskModel tm, UserModel um)
        {
            InitializeComponent();
            TVM = new TaskViewModel(tm, um);
            DataContext = TVM;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (TVM.apply())
            {
                Close();
            }
        }
    }
}
