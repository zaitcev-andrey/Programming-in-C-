using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
    /// Логика взаимодействия для ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        Repository rep;
        Manager manager = new Manager();
        int lastChangeIndex = -1;
        public ManagerWindow()
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
            string clientIndex = textBoxClientNumber.Text;
            int departmentId = (comboBoxDepartments.SelectedItem as Department).DepartmentId;
            if (!int.TryParse(clientIndex, out int ind) || (ind < 1 || ind > rep.GetMaxIdFromDepartmentsArray(departmentId)))
            {
                labelTelephoneNumber.Content = "Ошибка в индексе клиента";
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
                        if (client.Id == ind && client.DepartmentId == departmentId)
                        {
                            manager.SetClientTelephoneNumber(client, telephoneNumber);
                            lastChangeIndex = ind;
                            labelTelephoneNumber.Content = "Номер успешно сохранён";
                            break;
                        }
                    }

                }
                else
                    labelTelephoneNumber.Content = "Ошибка в номере телефона";
            }
            else
                labelTelephoneNumber.Content = "Вы не ввели телефон";
        }

        private void buttonGetTelephoneNumber_Click(object sender, RoutedEventArgs e)
        {
            string clientIndex = textBoxClientNumber2.Text;
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
                    textBlockGettingTelephone.Text = manager.GetClientNumber(client);
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

        private void buttonChangePasport_Click(object sender, RoutedEventArgs e)
        {
            string clientIndex = textBoxClientNumber_Copy.Text;
            int departmentId = (comboBoxDepartments.SelectedItem as Department).DepartmentId;
            if (!int.TryParse(clientIndex, out int ind) || (ind < 1 || ind > rep.GetMaxIdFromDepartmentsArray(departmentId)))
            {
                labelPasport.Content = "Вы неверно ввели индекс клиента";
                return;
            }

            string pasportSeries = textBoxPasportSeries.Text;
            string pasportNumber = textBoxPasportNumber.Text;

            foreach (var client in rep.Clients)
            {
                if(client.Id == ind && client.DepartmentId == departmentId)
                {
                    if(manager.SetClientPasportData(client, pasportSeries, pasportNumber))
                    {
                        lastChangeIndex = ind;
                        labelPasport.Content = "Паспорт успешно изменён";
                    }
                    else
                        labelPasport.Content = "Вы ввели некорректные данные";
                    break;
                }               
            }
        }

        private void buttonAddNote_Click(object sender, RoutedEventArgs e)
        {
            string lastName = textBoxLastNameForNote.Text;
            string firstName = textBoxFirstNameForNote.Text;
            string middleName = textBoxMiddleNameForNote.Text;

            if (string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(middleName))
            {
                labelNewNote.Content = "Вы заполнили не все поля ФИО";
                return;
            }

            if(!int.TryParse(textBoxAgeForNote.Text, out int age))
            {
                labelNewNote.Content = "Вы неверно ввели возраст";
                return;
            }

            string pasportSeries = textBoxPasportSeriesForNote.Text;
            string pasportNumber = textBoxPasportNumberForNote.Text;

            if (!manager.CheckPasportSeries(pasportSeries))
            {
                labelNewNote.Content = "Вы ошиблись в серии паспорта";
                return;
            }

            if (!manager.CheckPasportNumber(pasportNumber))
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

            int departmentId = (comboBoxDepartments.SelectedItem as Department).DepartmentId;
            rep.IncreaseIdInDepartment(departmentId);

            Client client = new Client(firstName, lastName, middleName, age, telephoneNumber,
                allPasport, departmentId, rep.GetMaxIdFromDepartmentsArray(departmentId));
            manager.AddTimeAndLogNoteAboutClient(client);
            rep.Clients.Add(client);
            labelNewNote.Content = "Запись успешно добавлена";
            lastChangeIndex = rep.GetMaxIdFromDepartmentsArray(departmentId); // для последних изменений

            //string json = JsonConvert.SerializeObject(rep.Clients);
            //File.WriteAllText("clients.json", json);

            //listViewClients.Items.Refresh(); // не сработала, поэтому делаем по-другому
            ICollectionView view = CollectionViewSource.GetDefaultView(listViewClients.ItemsSource);
            view.Refresh();
        }

        private void buttonSaveFIO_Click(object sender, RoutedEventArgs e)
        {
            string clientIndex = textBoxClientNumberForFio.Text;
            int departmentId = (comboBoxDepartments.SelectedItem as Department).DepartmentId;
            if (!int.TryParse(clientIndex, out int ind) || (ind < 1 || ind > rep.GetMaxIdFromDepartmentsArray(departmentId)))
            {
                textBlockNewFIO.Text = "Вы неверно ввели индекс клиента";
                return;
            }

            string lastName = textBoxLastName.Text;
            string firstName = textBoxFirstName.Text;
            string middleName = textBoxMiddleName.Text;

            if (!string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(middleName))
            {
                foreach (var client in rep.Clients)
                {
                    if (client.Id == ind && client.DepartmentId == departmentId)
                    {
                        manager.SetClientFio(client, lastName, firstName, middleName);
                        break;
                    }
                }
                lastChangeIndex = ind;
                textBlockNewFIO.Text = "ФИО успешно изменены";
                // нужно явно прописать Refresh, иначе запись не обновится в поле загрузки
                listViewClients.Items.Refresh();
            }
            else
                textBlockNewFIO.Text = "Вы заполнили не все поля!";
        }

        private void buttonGetPasport_Click(object sender, RoutedEventArgs e)
        {
            string clientIndex = textBoxClientForPasport.Text;
            int departmentId = (comboBoxDepartments.SelectedItem as Department).DepartmentId;
            if (!int.TryParse(clientIndex, out int ind) || (ind < 1 || ind > rep.GetMaxIdFromDepartmentsArray(departmentId)))
            {
                textBlockGettingTelephone.Text = "";
                return;
            }

            foreach (var client in rep.Clients)
            {
                if(client.Id == ind && client.DepartmentId == departmentId)
                {
                    textBlockGettingPasport.Text = manager.GetClientPasportData(client);
                    break;
                }                
            }
        }
    }
}
