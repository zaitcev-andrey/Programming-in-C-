using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Task3_OOP1_WPF
{
    /// <summary>
    /// Логика взаимодействия для Consultant.xaml
    /// </summary>
    public partial class ConsultantWindow : Window
    {
        ObservableCollection<Client> clients = new ObservableCollection<Client>();
        bool isFirstLoading = true;
        public ConsultantWindow()
        {
            InitializeComponent();            
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
            if(isFirstLoading)
            {
                if (File.Exists("clients.json"))
                {
                    string json = File.ReadAllText("clients.json");
                    clients = JsonConvert.DeserializeObject<ObservableCollection<Client>>(json);
                }
                else
                {
                    Client cl1 = new Client("Иван", "Иванов", "Иванович", "89998762315", "7718 999888");
                    Client cl2 = new Client("Сергей", "Сергеев", "Сергеевич", "87778762315", "3466 999888");
                    Client cl3 = new Client("Пётр", "Петров", "Петрович", "85558762315", "1922 999888");
                    Client cl4 = new Client("Александра", "Сидорова", "Артёмовна", "83338762315", "9813 999888");
                    clients.Add(cl1);
                    clients.Add(cl2);
                    clients.Add(cl3);
                    clients.Add(cl4);
                    string json = JsonConvert.SerializeObject(clients);
                    File.WriteAllText("clients.json", json);
                }
                isFirstLoading = false;

                // Данные загрузили и теперь отображаем их в ListBox
                // В xaml для этого листбокса прописываем каждое поле через Binding, иначе
                // целиком объект не сможет отобразиться
                listBox.ItemsSource = clients;
            }            
        }
    }
}
