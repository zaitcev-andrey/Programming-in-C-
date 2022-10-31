using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using Newtonsoft.Json;

namespace Task3_OOP1_WPF
{
    /// <summary>
    /// Логика взаимодействия для ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        ObservableCollection<Client> clients = new ObservableCollection<Client>();
        Manager manager = new Manager();
        bool isFirstLoading = true;
        int lastChangeIndex = -1;

        public ManagerWindow()
        {
            InitializeComponent();

            Client.ResetId();
        }

        private void buttonClickLoadData(object sender, RoutedEventArgs e)
        {
            if (isFirstLoading)
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

        private void buttonGetTelephoneNumber_Click(object sender, RoutedEventArgs e)
        {
            string clientIndex = textBoxClientNumber2.Text;
            if (!int.TryParse(clientIndex, out int ind) || (ind < 1 || ind > clients.Count))
            {
                textBlockGettingTelephone.Text = "";
                return;
            }

            textBlockGettingTelephone.Text = manager.GetClientNumber(clients[ind - 1]);
        }

        private void buttonCheckLastChanges_Click(object sender, RoutedEventArgs e)
        {
            if (lastChangeIndex == -1)
                textBlockLastChanges.Text = "Изменений пока что нет";
            else
                textBlockLastChanges.Text = clients[lastChangeIndex].GetLastChanges();
        }

        // Этот вариант работает, но через
        // Binding и Path в TextBox удобнее
        private void listBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // в случае, если выбранный элемент в списке - это действительно экземпляр клиента,
            // мы возьмём его id и поместим в textbox
            //if (listBox.SelectedItem is Client client)
            //    textBoxClientNumber.Text = client.Id.ToString();
        }

        private void buttonSaveNumber_Click(object sender, RoutedEventArgs e)
        {
            // Получаем индекс из строки и убеждаемся в его корректности
            string clientIndex = textBoxClientNumber.Text;
            if (!int.TryParse(clientIndex, out int ind) || (ind < 1 || ind > clients.Count))
            {
                labelTelephoneNumber.Content = "Вы неверно ввели индекс клиента";
                return;
            }                

            string telephoneNumber = textBoxTelephoneNumber.Text;

            if(manager.CheckClientTelephoneNumber(telephoneNumber))
            {
                manager.SetClientTelephoneNumber(clients[ind - 1], telephoneNumber);
                lastChangeIndex = ind - 1;
                labelTelephoneNumber.Content = "Номер успешно изменён";
            }
        }

        private void buttonSavePasport_Click(object sender, RoutedEventArgs e)
        {
            string clientIndex = textBoxClientNumber_Copy.Text;
            if (!int.TryParse(clientIndex, out int ind) || (ind < 1 || ind > clients.Count))
            {
                labelPasport.Content = "Вы неверно ввели индекс клиента";
                return;
            }
                
            string pasportSeries = textBoxPasportSeries.Text;
            string pasportNumber = textBoxPasportNumber.Text;

            if (manager.SetClientPasportData(clients[ind - 1], pasportSeries, pasportNumber))
            {
                lastChangeIndex = ind - 1;
                labelPasport.Content = "Паспорт успешно изменён";
            }
            else
                labelPasport.Content = "Вы ввели некорректные данные";
        }

        private void buttonGetPasport_Click(object sender, RoutedEventArgs e)
        {
            string clientIndex = textBoxClientForPasport.Text;
            if (!int.TryParse(clientIndex, out int ind) || (ind < 1 || ind > clients.Count))
            {
                textBlockGettingTelephone.Text = "";
                return;
            }

            textBlockGettingPasport.Text = manager.GetClientPasportData(clients[ind - 1]);
        }

        private void buttonAddNote_Click(object sender, RoutedEventArgs e)
        {
            string lastName = textBoxLastNameForNote.Text;
            string firstName = textBoxFirstNameForNote.Text;
            string middleName = textBoxMiddleNameForNote.Text;

            if(string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(middleName))
            {
                labelNewNote.Content = "Вы заполнили не все поля ФИО";
                return;
            }

            string pasportSeries = textBoxPasportSeriesForNote.Text;
            string pasportNumber = textBoxPasportNumberForNote.Text;

            if (!manager.CheckPasportSeries(pasportSeries))
            {
                labelNewNote.Content = "Вы ошиблись в серии паспорта";
                return;
            }
                
            if(!manager.CheckPasportNumber(pasportNumber))
            {
                labelNewNote.Content = "Вы ошиблись в номере паспорта";
                return;
            }
            string allPasport = pasportSeries + " " + pasportNumber;

            string telephoneNumber = textBoxTelephoneForNote.Text;

            if (!manager.CheckClientTelephoneNumber(telephoneNumber))
            {
                labelNewNote.Content = "Вы ошиблись в номере телефона";
                return;
            }

            Client client = new Client(firstName, lastName, middleName, telephoneNumber, allPasport);
            manager.AddTimeAndLogNoteAboutClient(client);
            clients.Add(client);
            labelNewNote.Content = "Запись успешно добавлена";

            string json = JsonConvert.SerializeObject(clients);
            File.WriteAllText("clients.json", json);
            listBox.Items.Refresh();
        }

        private void buttonSaveFIO_Click(object sender, RoutedEventArgs e)
        {
            string clientIndex = textBoxClientNumberForFio.Text;
            if (!int.TryParse(clientIndex, out int ind) || (ind < 1 || ind > clients.Count))
            {
                textBlockNewFIO.Text = "Вы неверно ввели индекс клиента";
                return;
            }

            string lastName = textBoxLastName.Text;
            string firstName = textBoxFirstName.Text;
            string middleName = textBoxMiddleName.Text;

            if(!string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(middleName))
            {
                manager.SetClientFio(clients[ind - 1], lastName, firstName, middleName);
                lastChangeIndex = ind - 1;
                textBlockNewFIO.Text = "ФИО успешно изменены";
                // нужно явно прописать Refresh, иначе запись не обновится в поле загрузки
                listBox.Items.Refresh();
            }
            else
                textBlockNewFIO.Text = "Вы заполнили не все поля!";
        }
    }
}
