using Budget.json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Documents;

namespace Budget
{
    /// <summary>
    /// Логика взаимодействия для CreateTypeWindow.xaml
    /// </summary>
    public partial class CreateTypeWindow : Window
    {
        
        public CreateTypeWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.type.Add(Name_new_type.Text);
            FileManager.MySerialize("Type.json", MainWindow.type);
            Close();
            
          }
    }
}
