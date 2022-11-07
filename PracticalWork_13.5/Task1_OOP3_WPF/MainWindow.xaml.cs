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

namespace Task1_OOP3_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Bank<Client> bank;
        public MainWindow()
        {
            InitializeComponent();

            List<Client> clients = new List<Client>();
            #region Подготавливаем список клиентов
            List<BankAccount> accounts1 = new List<BankAccount>();
            accounts1.Add(new BankAccount(1, BankAccountNames.NewTimeAccount, 15500.5));
            accounts1.Add(new BankAccount(2, BankAccountNames.FirstAccount, 20000));
            accounts1.Add(new BankAccount(3, BankAccountNames.StableAccount, 30000));
            clients.Add(new Client("Иван", "Иванов", "Иванович", 34, "89998762315", "7718 999888", accounts1));

            List<BankAccount> accounts2 = new List<BankAccount>();
            accounts2.Add(new BankAccount(1, BankAccountNames.NewTimeAccount, 10000));
            accounts2.Add(new BankAccount(2, BankAccountNames.FirstAccount, 13000.5));
            accounts2.Add(new BankAccount(3, BankAccountNames.StableAccount, 7000));
            accounts2.Add(new BankAccount(4, BankAccountNames.NewTimeAccount, 50000));
            clients.Add(new Client("Сергей", "Сергеев", "Сергеевич", 19, "87778762315", "3466 675026", accounts2));

            List<BankAccount> accounts3 = new List<BankAccount>();
            accounts3.Add(new BankAccount(1, BankAccountNames.NewTimeAccount, 14000));
            accounts3.Add(new BankAccount(2, BankAccountNames.FirstAccount, 22000));
            clients.Add(new Client("Александра", "Сидорова", "Артёмовна", 44, "83338762315", "9813 257652", accounts3));

            List<BankAccount> accounts4 = new List<BankAccount>();
            accounts4.Add(new BankAccount(1, BankAccountNames.NewTimeAccount, 8000));
            accounts4.Add(new BankAccount(2, BankAccountNames.FirstAccount, 35000.3));
            accounts4.Add(new BankAccount(3, BankAccountNames.FirstAccount, 70000));
            accounts4.Add(new BankAccount(4, BankAccountNames.StableAccount, 15000));
            clients.Add(new Client("Михаил", "Смирнов", "Артёмович", 32, "88076452003", "9843 864390", accounts4));

            List<BankAccount> accounts5 = new List<BankAccount>();
            accounts5.Add(new BankAccount(1, BankAccountNames.NewTimeAccount, 18000));
            accounts5.Add(new BankAccount(2, BankAccountNames.StableAccount, 40000));
            accounts5.Add(new BankAccount(3, BankAccountNames.StableAccount, 20000));
            clients.Add(new Client("Никита", "Гришин", "Андреевич", 22, "81357845724", "7842 122941", accounts5));
            #endregion

            bank = new Bank<Client>(clients);
            // Связываем список на форме с клиентами у банка
            listViewClients.ItemsSource = bank.Members;

            // связываем элементы комбобокса с именами счетов, но
            // предварительно имена счетов сохраняем в массив строк,
            // так как комбобокс не умеет работать с enum 
            string[] bankAccountNames = Enum.GetNames(typeof(BankAccountNames));
            comboBoxAccountNames.ItemsSource = bankAccountNames;
        }

        private bool CheckCorrectClientId(in Bank<Client> bank, int id)
        {
            if(id < 1 || bank.Members.Count < id)
                return false;
            return true;
        }

        private void listViewClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Поскольку SelectedItem возвращает object, то его надо явно привести к типу клиента
            listViewAccounts.ItemsSource = (listViewClients.SelectedItem as Client).BankAccounts;
        }

        private void buttonOpenAccount_Click(object sender, RoutedEventArgs e)
        {
            // Забираем Id клиента и проверяем его на корректность
            if(!int.TryParse(textBoxChooseClient1.Text, out int clientId) || !CheckCorrectClientId(bank, clientId))
            {
                textBlockInfo1.Text = "Вы неверно ввели id клиента";
                return;
            }
            // Забираем имя счёта из комбобокса
            Enum.TryParse((comboBoxAccountNames.SelectedItem as String), out BankAccountNames bankAccountName);
            // Забираем сумму денег и текстбокса
            if(!double.TryParse(textBoxSumOfMoney.Text, out double money))
            {
                textBlockInfo1.Text = "Вы неверно ввели сумму";
                return;
            }
            // Если все проверки пройдены, то здесь мы добавляем счёт
            bank.OpenBankAccount(clientId, bankAccountName, money);
            textBlockInfo1.Text = "Счёт успешно добавлен";
            listViewAccounts.Items.Refresh();
        }

        private void buttonCloseAccount_Click(object sender, RoutedEventArgs e)
        {
            // Забираем Id клиента и проверяем его на корректность
            if (!int.TryParse(textBoxChooseClient2.Text, out int clientId) || !CheckCorrectClientId(bank, clientId))
            {
                textBlockInfo2.Text = "Вы неверно ввели id клиента";
                return;
            }
            // Забираем Id счёта, который хотим закрыть
            if (!int.TryParse(textBoxAccountId.Text, out int accountId))
            {
                textBlockInfo2.Text = "Вы неверно ввели id счёта";
                return;
            }
            // Закрываем счёт у клиента
            if(!bank.CloseBankAccount(clientId, accountId))
            {
                textBlockInfo2.Text = "Не удалось закрыть счёт, так как по данному id его нет";
            }
            else
            {
                textBlockInfo2.Text = "Счёт успешно закрыт";
                listViewAccounts.Items.Refresh();
            }                
        }

        private void buttonMoneyTransfer_Click(object sender, RoutedEventArgs e)
        {
            // Забираем Id клиента и проверяем его на корректность
            if (!int.TryParse(textBoxChooseClient3.Text, out int clientId) || !CheckCorrectClientId(bank, clientId))
            {
                textBlockInfo3.Text = "Вы неверно ввели id клиента";
                return;
            }
            // Забираем Id того счёта, с которого будем переводить деньги
            if (!int.TryParse(textBoxAccountIdFrom.Text, out int accountIdFrom) 
                || accountIdFrom < 1 || bank.Members[clientId-1].MaxBankAccountsId < accountIdFrom)
            {
                textBlockInfo3.Text = "Вы неверно ввели id счёта откуда будут списываться деньги";
                return;
            }
            // Забираем Id счёта, на который будем переводить деньги
            if (!int.TryParse(textBoxAccountIdTo.Text, out int accountIdTo)
                || accountIdTo < 1 || bank.Members[clientId - 1].MaxBankAccountsId < accountIdTo)
            {
                textBlockInfo3.Text = "Вы неверно ввели id счёта куда будут начисляться деньги";
                return;
            }
            // Получаем деньги, которые нужно перевести
            if(!double.TryParse(textBoxMoneySum.Text, out double money))
            {
                textBlockInfo3.Text = "Вы неверно ввели денежную сумму";
                return;
            }
            // Переводим деньги
            if(bank.TransferMoneyFromOneAccountToAnother(clientId,accountIdFrom,accountIdTo,money))
            {
                textBlockInfo3.Text = "Перевод успешно выполнен";
                listViewAccounts.Items.Refresh();
            }
            else
                textBlockInfo3.Text = "Ошибка: перепроверьте id счетов и сумму, она либо меньше 0, либо превышает максимум по счёту";
        }
    }
}
