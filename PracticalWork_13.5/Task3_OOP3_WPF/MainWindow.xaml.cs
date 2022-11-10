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

namespace Task3_OOP3_WPF
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
            List<BankDepositAccount> depositAccounts1 = new List<BankDepositAccount>();
            depositAccounts1.Add(new BankDepositAccount(1, BankDepositAccountNames.NewTimeAccount, 15500.5));
            depositAccounts1.Add(new BankDepositAccount(2, BankDepositAccountNames.FirstAccount, 20000));
            depositAccounts1.Add(new BankDepositAccount(3, BankDepositAccountNames.StableAccount, 30000));
            // 2 строчки ниже позволяют сделать ковариантность через IEnumerable
            IEnumerable<BankAccount> ba1 = depositAccounts1;
            List<BankAccount> bankDepositAccounts1 = ba1.ToList();

            List<BankNotDepositAccount> notDepositAccounts1 = new List<BankNotDepositAccount>();
            notDepositAccounts1.Add(new BankNotDepositAccount(1, BankNotDepositAccountNames.BeneficialAccount, 14000));
            notDepositAccounts1.Add(new BankNotDepositAccount(2, BankNotDepositAccountNames.SafeAccount, 18000));
            IEnumerable<BankAccount> ba1_1 = notDepositAccounts1;
            List<BankAccount> bankNotDepositAccounts1 = ba1_1.ToList();

            clients.Add(new Client("Иван", "Иванов", "Иванович", 34, "89998762315", "7718 999888", bankDepositAccounts1, bankNotDepositAccounts1));

            // А теперь сделаем ковариантность через собственный интерфейс
            List<BankAccount> bankDepositAccounts2 = new List<BankAccount>();
            // можно добавить элемент по полиморфизму
            bankDepositAccounts2.Add(new BankDepositAccount(1, BankDepositAccountNames.NewTimeAccount, 10000));
            // а можно добавить через ковариантный интерфейс
            IBankDepositAccount<BankAccount> concreteDepositAccount2 = new Realize_IBankDepositAccount(); // такое приведение доступно только из-за out
            bankDepositAccounts2.Add(concreteDepositAccount2.GetBankDepositAccount(2, BankDepositAccountNames.FirstAccount, 13000.5));
            bankDepositAccounts2.Add(concreteDepositAccount2.GetBankDepositAccount(3, BankDepositAccountNames.StableAccount, 7000));
            bankDepositAccounts2.Add(concreteDepositAccount2.GetBankDepositAccount(4, BankDepositAccountNames.NewTimeAccount, 50000));

            List<BankAccount> bankNotDepositAccounts2 = new List<BankAccount>();
            IBankNotDepositAccount<BankAccount> concreteNotDepositAccount2 = new Realize_IBankNotDepositAccount(); // такое приведение доступно только из-за out
            bankNotDepositAccounts2.Add(concreteNotDepositAccount2.GetBankNotDepositAccount(1, BankNotDepositAccountNames.BeneficialAccount, 33000));
            bankNotDepositAccounts2.Add(concreteNotDepositAccount2.GetBankNotDepositAccount(2, BankNotDepositAccountNames.SafeAccount, 60000));
            bankNotDepositAccounts2.Add(concreteNotDepositAccount2.GetBankNotDepositAccount(3, BankNotDepositAccountNames.NewLifeAccount, 42000));

            clients.Add(new Client("Сергей", "Сергеев", "Сергеевич", 19, "87778762315", "3466 675026", bankDepositAccounts2, bankNotDepositAccounts2));

            // Сначала заполняем депозитные счета
            List<BankAccount> bankDepositAccounts3 = new List<BankAccount>();
            IBankDepositAccount<BankAccount> concreteDepositAccount3 = new Realize_IBankDepositAccount(); // такое приведение доступно только из-за out
            bankDepositAccounts3.Add(concreteDepositAccount3.GetBankDepositAccount(1, BankDepositAccountNames.NewTimeAccount, 14000));
            bankDepositAccounts3.Add(concreteDepositAccount3.GetBankDepositAccount(2, BankDepositAccountNames.FirstAccount, 16000));
            bankDepositAccounts3.Add(concreteDepositAccount3.GetBankDepositAccount(3, BankDepositAccountNames.StableAccount, 25000));
            bankDepositAccounts3.Add(concreteDepositAccount3.GetBankDepositAccount(4, BankDepositAccountNames.NewTimeAccount, 40000));

            // Затем заполняем недепозитные счета
            List<BankAccount> bankNotDepositAccounts3 = new List<BankAccount>();
            IBankNotDepositAccount<BankAccount> concreteNotDepositAccount3 = new Realize_IBankNotDepositAccount(); // такое приведение доступно только из-за out
            bankNotDepositAccounts3.Add(concreteNotDepositAccount3.GetBankNotDepositAccount(1, BankNotDepositAccountNames.BeneficialAccount, 32000));
            bankNotDepositAccounts3.Add(concreteNotDepositAccount3.GetBankNotDepositAccount(2, BankNotDepositAccountNames.SafeAccount, 10000.5));
            bankNotDepositAccounts3.Add(concreteNotDepositAccount3.GetBankNotDepositAccount(3, BankNotDepositAccountNames.NewLifeAccount, 80000));

            clients.Add(new Client("Александра", "Сидорова", "Артёмовна", 44, "83338762315", "9813 257652", bankDepositAccounts3, bankNotDepositAccounts3));
            #endregion

            bank = new Bank<Client>(clients);
            // Связываем список на форме с клиентами у банка
            listViewClients.ItemsSource = bank.Members;

            // связываем элементы комбобокса с типами счетов, но
            // предварительно типы счетов сохраняем в массив строк,
            // так как комбобокс не умеет работать с enum 
            string[] bankAccountTypes = Enum.GetNames(typeof(EnumBankAccountTypes));
            comboBoxAccountType1.ItemsSource = comboBoxAccountType2.ItemsSource =
                comboBoxAccountType3.ItemsSource = comboBoxAccountType4.ItemsSource = 
                comboBoxAccountType5.ItemsSource = bankAccountTypes;
        }

        // событие, которое происходит после выбора счёта
        private void comboBoxAccountType1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] bankAccountNames;
            Enum.TryParse(comboBoxAccountType1.SelectedItem as String, out EnumBankAccountTypes type);
            if (type == EnumBankAccountTypes.Deposit)
                bankAccountNames = Enum.GetNames(typeof(BankDepositAccountNames));
            else
                bankAccountNames = Enum.GetNames(typeof(BankNotDepositAccountNames));
            comboBoxAccountNames.ItemsSource = bankAccountNames;
        }

        private bool CheckCorrectClientId(in Bank<Client> bank, int id)
        {
            if (id < 1 || bank.Members.Count < id)
                return false;
            return true;
        }

        private void listViewClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Поскольку SelectedItem возвращает object, то его надо явно привести к типу клиента
            listViewDepositAccounts.ItemsSource = (listViewClients.SelectedItem as Client).BankDepositAccounts;
            listViewNotDepositAccounts.ItemsSource = (listViewClients.SelectedItem as Client).BankNotDepositAccounts;
        }

        private void listViewDepositAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // обязательно делаем проверку на null, т.к. при смене клиента при выбранном банковском счёте
            // без проверки будет ошибка, поскольку в этот момент listViewDepositAccounts.SelectedItem будет null
            if (listViewDepositAccounts.SelectedItem is null)
                return;
            textBoxAccountId.Text = textBoxAccountIdFrom.Text = textBoxAccountId2.Text =
                (listViewDepositAccounts.SelectedItem as BankDepositAccount).Id.ToString();
        }
        private void listViewNotDepositAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewNotDepositAccounts.SelectedItem is null)
                return;
            textBoxAccountId.Text = textBoxAccountIdFrom.Text = textBoxAccountId2.Text =
                (listViewNotDepositAccounts.SelectedItem as BankNotDepositAccount).Id.ToString();
        }

        private void buttonOpenAccount_Click(object sender, RoutedEventArgs e)
        {
            // Забираем Id клиента и проверяем его на корректность
            if (!int.TryParse(textBoxChooseClient1.Text, out int clientId) || !CheckCorrectClientId(bank, clientId))
            {
                textBlockInfo1.Text = "Вы неверно ввели id клиента";
                return;
            }
            // Забираем тип счёта
            if (!Enum.TryParse((comboBoxAccountType1.SelectedItem as String), out EnumBankAccountTypes type))
            {
                textBlockInfo1.Text = "Вы не указали тип банковского счёта";
                return;
            }
            // Забираем имя счёта из комбобокса
            string bankAccountName;
            if (type == EnumBankAccountTypes.Deposit)
            {
                if (!Enum.TryParse((comboBoxAccountNames.SelectedItem as String), out BankDepositAccountNames name))
                {
                    textBlockInfo1.Text = "Вы неверно указали имя депозитного банковского счёта";
                    return;
                }
                bankAccountName = name.ToString();
            }
            else
            {
                if (!Enum.TryParse((comboBoxAccountNames.SelectedItem as String), out BankNotDepositAccountNames name))
                {
                    textBlockInfo1.Text = "Вы неверно указали имя недепозитного банковского счёта";
                    return;
                }
                bankAccountName = name.ToString();
            }

            // Забираем сумму денег из текстбокса
            if (!double.TryParse(textBoxSumOfMoney.Text, out double money))
            {
                textBlockInfo1.Text = "Вы неверно ввели сумму";
                return;
            }
            // Если все проверки пройдены, то здесь мы добавляем счёт
            bank.OpenBankAccount(type, clientId, bankAccountName, money);
            textBlockInfo1.Text = "Счёт успешно добавлен";
            if (type == EnumBankAccountTypes.Deposit)
                listViewDepositAccounts.Items.Refresh();
            else
                listViewNotDepositAccounts.Items.Refresh();
        }

        private void buttonCloseAccount_Click(object sender, RoutedEventArgs e)
        {
            // Забираем Id клиента и проверяем его на корректность
            if (!int.TryParse(textBoxChooseClient2.Text, out int clientId) || !CheckCorrectClientId(bank, clientId))
            {
                textBlockInfo2.Text = "Вы неверно ввели id клиента";
                return;
            }
            // Забираем тип счёта
            if (!Enum.TryParse((comboBoxAccountType2.SelectedItem as String), out EnumBankAccountTypes type))
            {
                textBlockInfo2.Text = "Вы не указали тип банковского счёта";
                return;
            }
            // Забираем Id счёта, который хотим закрыть
            if (!int.TryParse(textBoxAccountId.Text, out int accountId))
            {
                textBlockInfo2.Text = "Вы неверно ввели id счёта";
                return;
            }
            // Закрываем счёт у клиента
            if (!bank.CloseBankAccount(type, clientId, accountId))
            {
                textBlockInfo2.Text = "Не удалось закрыть счёт, так как по данному id его нет";
            }
            else
            {
                textBlockInfo2.Text = "Счёт успешно закрыт";
                if (type == EnumBankAccountTypes.Deposit)
                    listViewDepositAccounts.Items.Refresh();
                else
                    listViewNotDepositAccounts.Items.Refresh();
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
            // Забираем тип счёта
            if (!Enum.TryParse((comboBoxAccountType3.SelectedItem as String), out EnumBankAccountTypes type))
            {
                textBlockInfo3.Text = "Вы не указали тип банковского счёта";
                return;
            }
            // Забираем Id того счёта, с которого будем переводить деньги
            if (!int.TryParse(textBoxAccountIdFrom.Text, out int accountIdFrom) || accountIdFrom < 1)
            {
                textBlockInfo3.Text = "Вы неверно ввели id счёта откуда будут списываться деньги";
                return;
            }
            if((type == EnumBankAccountTypes.Deposit && bank.Members[clientId - 1].MaxBankDepositAccountsId < accountIdFrom)
                || (type == EnumBankAccountTypes.NotDeposit && bank.Members[clientId - 1].MaxBankNotDepositAccountsId < accountIdFrom))
            {
                textBlockInfo3.Text = "Вы неверно ввели id счёта откуда будут списываться деньги";
                return;
            }

            // Забираем Id счёта, на который будем переводить деньги
            if (!int.TryParse(textBoxAccountIdTo.Text, out int accountIdTo) || accountIdTo < 1)
            {
                textBlockInfo3.Text = "Вы неверно ввели id счёта куда будут начисляться деньги";
                return;
            }
            if ((type == EnumBankAccountTypes.Deposit && bank.Members[clientId - 1].MaxBankDepositAccountsId < accountIdTo)
                || (type == EnumBankAccountTypes.NotDeposit && bank.Members[clientId - 1].MaxBankNotDepositAccountsId < accountIdTo))
            {
                textBlockInfo3.Text = "Вы неверно ввели id счёта куда будут начисляться деньги";
                return;
            }
            // Получаем деньги, которые нужно перевести
            if (!double.TryParse(textBoxMoneySum.Text, out double money))
            {
                textBlockInfo3.Text = "Вы неверно ввели денежную сумму";
                return;
            }
            // Переводим деньги
            if (bank.TransferMoneyFromOneAccountToAnother(type, clientId, accountIdFrom, accountIdTo, money))
            {
                textBlockInfo3.Text = "Перевод успешно выполнен";
                if (type == EnumBankAccountTypes.Deposit)
                    listViewDepositAccounts.Items.Refresh();
                else
                    listViewNotDepositAccounts.Items.Refresh();
            }
            else
                textBlockInfo3.Text = "Ошибка: перепроверьте id счетов и сумму, она либо меньше 0, либо превышает максимум по счёту";
        }

        private void buttonAddMoneyToAccount_Click(object sender, RoutedEventArgs e)
        {
            // Забираем Id клиента и проверяем его на корректность
            if (!int.TryParse(textBoxChooseClient4.Text, out int clientId) || !CheckCorrectClientId(bank, clientId))
            {
                textBlockInfo4.Text = "Вы неверно ввели id клиента";
                return;
            }
            // Забираем тип счёта
            if (!Enum.TryParse((comboBoxAccountType4.SelectedItem as String), out EnumBankAccountTypes type))
            {
                textBlockInfo4.Text = "Вы не указали тип банковского счёта";
                return;
            }
            // Забираем Id счёта, баланс которого хотим пополнить
            if (!int.TryParse(textBoxAccountId2.Text, out int accountId))
            {
                textBlockInfo4.Text = "Вы неверно ввели id счёта";
                return;
            }
            // Получаем деньги
            if (!double.TryParse(textBoxSumOfMoney2.Text, out double money))
            {
                textBlockInfo4.Text = "Вы неверно ввели денежную сумму";
                return;
            }

            if (bank.AddMoneyToBankAccount(type, clientId, accountId, money))
            {
                textBlockInfo4.Text = "Пополнение средств успешно выполнено";
                if (type == EnumBankAccountTypes.Deposit)
                    listViewDepositAccounts.Items.Refresh();
                else
                    listViewNotDepositAccounts.Items.Refresh();
            }
            else
                textBlockInfo4.Text = "Ошибка в пополении средств, проверьте данные";
        }

        private void buttonMoneyTransferBetweenClients_Click(object sender, RoutedEventArgs e)
        {
            // Забираем Id клиентов и проверяем их на корректность
            if (!int.TryParse(textBoxChooseClient5From.Text, out int clientIdFrom) || !CheckCorrectClientId(bank, clientIdFrom))
            {
                textBlockInfo5.Text = "Вы неверно ввели id клиента, от которого будет перевод";
                return;
            }
            if (!int.TryParse(textBoxChooseClient5To.Text, out int clientIdTo) || !CheckCorrectClientId(bank, clientIdTo))
            {
                textBlockInfo5.Text = "Вы неверно ввели id клиента, кому будет перевод";
                return;
            }
            // Забираем тип счёта
            if (!Enum.TryParse((comboBoxAccountType5.SelectedItem as String), out EnumBankAccountTypes type))
            {
                textBlockInfo5.Text = "Вы не указали тип банковского счёта";
                return;
            }
            // Забираем Id того счёта, с которого будем переводить деньги
            if (!int.TryParse(textBoxAccountId5From.Text, out int accountIdFrom) || accountIdFrom < 1)
            {
                textBlockInfo5.Text = "Вы неверно ввели id счёта откуда будут списываться деньги";
                return;
            }
            if ((type == EnumBankAccountTypes.Deposit && bank.Members[clientIdFrom - 1].MaxBankDepositAccountsId < accountIdFrom)
                || (type == EnumBankAccountTypes.NotDeposit && bank.Members[clientIdFrom - 1].MaxBankNotDepositAccountsId < accountIdFrom))
            {
                textBlockInfo5.Text = "Вы неверно ввели id счёта откуда будут списываться деньги";
                return;
            }

            // Забираем Id счёта, на который будем переводить деньги
            if (!int.TryParse(textBoxAccountId5To.Text, out int accountIdTo) || accountIdTo < 1)
            {
                textBlockInfo5.Text = "Вы неверно ввели id счёта куда будут начисляться деньги";
                return;
            }
            if ((type == EnumBankAccountTypes.Deposit && bank.Members[clientIdTo - 1].MaxBankDepositAccountsId < accountIdTo)
                || (type == EnumBankAccountTypes.NotDeposit && bank.Members[clientIdTo - 1].MaxBankNotDepositAccountsId < accountIdTo))
            {
                textBlockInfo5.Text = "Вы неверно ввели id счёта куда будут начисляться деньги";
                return;
            }
            // Получаем деньги
            if (!double.TryParse(textBoxMoneySum5.Text, out double money))
            {
                textBlockInfo5.Text = "Вы неверно ввели денежную сумму";
                return;
            }

            if (bank.TransferMoneyFromOneMemberToAnother(clientIdFrom, clientIdTo, type, accountIdFrom, accountIdTo, money))
            {
                textBlockInfo5.Text = "Перевод успешно выполнен";
                if (type == EnumBankAccountTypes.Deposit)
                    listViewDepositAccounts.Items.Refresh();
                else
                    listViewNotDepositAccounts.Items.Refresh();
            }
            else
                textBlockInfo5.Text = "Ошибка в переводе средств, проверьте данные";
        }
    }
}
