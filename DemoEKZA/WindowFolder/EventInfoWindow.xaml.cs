﻿using DemoEKZA.DataFolder;
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
using System.Windows.Shapes;

namespace DemoEKZA.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для EventInfoWindow.xaml
    /// </summary>
    public partial class EventInfoWindow : Window
    {
        Event ev = new Event();

        public EventInfoWindow(Event ev)
        {
            InitializeComponent();
            DataContext = ev;
            this.ev = ev;       
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
