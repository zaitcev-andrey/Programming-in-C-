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
        Consultant consultant = new Consultant();
        bool isFirstLoading = true;
        int lastChangeIndex = -1;

        public ConsultantWindow()
        {
            InitializeComponent();            
        }

        private void buttonClickLoadData(object sender, RoutedEventArgs e)
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

        private void buttonSaveNumber_Click(object sender, RoutedEventArgs e)
        {
            // Получаем индекс из строки и убеждаемся в его корректности
            string clientIndex = textBoxClientNumber.Text;
            if (!int.TryParse(clientIndex, out int ind) || (ind < 0 || ind >= clients.Count))
                return;

            string telephoneNumber = textBoxTelephoneNumber.Text;

            bool flag = true;
            if (!string.IsNullOrEmpty(telephoneNumber))
            {
                foreach (char c in telephoneNumber)
                {
                    if (c < '0' || c > '9')
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    consultant.SetClientTelephoneNumber(clients[ind], telephoneNumber);
                    lastChangeIndex = ind;
                    labelTelephoneNumber.Content = "Номер успешно сохранён";
                }
            }
        }

        //private void listBox_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    textBoxClientNumber.Text = listBox.ItemsSource.GetEnumerator().ToString();
        //}

        //private void listBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    textBoxClientNumber.Text = listBox.ItemsSource.GetEnumerator().ToString();
        //}

        // Сработал именно Preview метод
        private void listBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            textBoxClientNumber.Text = listBox.ItemsSource.ToString();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (lastChangeIndex == -1)
                textBlockLastChanges.Text = "Изменений пока что нет";
            else
                textBlockLastChanges.Text = clients[lastChangeIndex].GetLastChanges();
        }

        private void buttonGetTelephoneNumber_Click(object sender, RoutedEventArgs e)
        {
            string clientIndex = textBoxClientNumber2.Text;
            if (!int.TryParse(clientIndex, out int ind) || (ind < 0 || ind >= clients.Count))
            {
                textBlockGettingTelephone.Text = "";
                return;
            }

            textBlockGettingTelephone.Text = consultant.GetClientNumber(clients[ind]);
        }
    }
}
