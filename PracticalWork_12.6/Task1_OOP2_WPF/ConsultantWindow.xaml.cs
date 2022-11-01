using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq; // добавляет метод расширения Where к списку
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Task1_OOP2_WPF
{
    /// <summary>
    /// Логика взаимодействия для ConsultantWindow.xaml
    /// </summary>
    public partial class ConsultantWindow : Window
    {
        Repository rep;
        Consultant consultant = new Consultant();
        int lastChangeIndex = -1;
        public ConsultantWindow()
        {
            InitializeComponent();

            rep = new Repository();
            comboBoxDepartments.ItemsSource = rep.Departments;
        }

        private void comboBoxDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // у Where используется предикат - функция Find, которая возвращает bool
            listViewClients.ItemsSource = rep.Clients.Where(Find);
        }

        private bool Find(Client client)
        {
            return (client.DepartmentId == (comboBoxDepartments.SelectedItem as Department).DepartmentId);
        }

        private void buttonSaveNumber_Click(object sender, RoutedEventArgs e)
        {
            // Получаем индекс из строки и убеждаемся в его корректности
            string clientIndex = textBoxChooseClient.Text;
            int departmentId = (comboBoxDepartments.SelectedItem as Department).DepartmentId;
            if (!int.TryParse(clientIndex, out int ind) || (ind < 1 || ind > rep.GetMaxIdFromDepartmentsArray(departmentId)))
            {
                labelTelephoneNumber.Text = "Ошибка в индексе клиента";
                return;
            }                

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
                    foreach (var client in rep.Clients)
                    {
                        if(client.Id == ind && client.DepartmentId == departmentId)
                        {
                            consultant.SetClientTelephoneNumber(client, telephoneNumber);
                            lastChangeIndex = ind;
                            labelTelephoneNumber.Text = "Номер успешно сохранён";
                            break;
                        }
                    }
                    
                }
                else
                    labelTelephoneNumber.Text = "Ошибка в номере телефона";
            }
            else
                labelTelephoneNumber.Text = "Вы не ввели телефон";
        }

        private void buttonGetTelephoneNumber_Click(object sender, RoutedEventArgs e)
        {
            string clientIndex = textBoxChooseClient2.Text;
            int departmentId = (comboBoxDepartments.SelectedItem as Department).DepartmentId;
            if (!int.TryParse(clientIndex, out int ind) || (ind < 1 || ind > rep.GetMaxIdFromDepartmentsArray(departmentId)))
            {
                textBlockGettingTelephone.Text = "";
                return;
            }

            foreach (var client in rep.Clients)
            {
                if (client.Id == ind && client.DepartmentId == departmentId)
                {
                    textBlockGettingTelephone.Text = consultant.GetClientNumber(client);
                    break;
                }
            }            
        }

        private void buttonGetLastChanges_Click(object sender, RoutedEventArgs e)
        {
            if (lastChangeIndex == -1)
                textBlockLastChanges.Text = "Изменений пока что нет";
            else
            {
                int departmentId = (comboBoxDepartments.SelectedItem as Department).DepartmentId;
                foreach (var client in rep.Clients)
                {
                    if (client.Id == lastChangeIndex && client.DepartmentId == departmentId)
                    {
                        textBlockLastChanges.Text = client.GetLastChanges();
                        break;
                    }
                }
            }
        }

        private void buttonSortByFirstName_Click(object sender, RoutedEventArgs e)
        {
            rep.Clients.Sort(Client.SortBy(VariantsOfSorts.FirstName));
            //listViewClients.Items.Refresh(); // не сработала, поэтому делаем по-другому
            ICollectionView view = CollectionViewSource.GetDefaultView(listViewClients.ItemsSource);
            view.Refresh();
        }

        private void buttonSortByLastName_Click(object sender, RoutedEventArgs e)
        {
            rep.Clients.Sort(Client.SortBy(VariantsOfSorts.LastName));
            ICollectionView view = CollectionViewSource.GetDefaultView(listViewClients.ItemsSource);
            view.Refresh();
        }

        private void buttonSortByMiddleName_Click(object sender, RoutedEventArgs e)
        {
            rep.Clients.Sort(Client.SortBy(VariantsOfSorts.MiddleName));
            ICollectionView view = CollectionViewSource.GetDefaultView(listViewClients.ItemsSource);
            view.Refresh();
        }

        private void buttonSortByAge_Click(object sender, RoutedEventArgs e)
        {
            rep.Clients.Sort(Client.SortBy(VariantsOfSorts.Age));
            ICollectionView view = CollectionViewSource.GetDefaultView(listViewClients.ItemsSource);
            view.Refresh();
        }

        private void buttonSortById_Click(object sender, RoutedEventArgs e)
        {
            rep.Clients.Sort(Client.SortBy(VariantsOfSorts.Id));
            ICollectionView view = CollectionViewSource.GetDefaultView(listViewClients.ItemsSource);
            view.Refresh();
        }
    }
}
