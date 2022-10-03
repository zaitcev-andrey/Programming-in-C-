using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; // Task
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//using System.Threading; // CancellationToken
using Telegram.Bot;
using System.Collections.ObjectModel;
//using Telegram.Bot.Types; // Update
//using Telegram.Bot.Types.InputFiles;

namespace Task1_WPF_TelegramBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TelegramMessageClient client;
        public MainWindow()
        {
            // создаём токен - уникальный id бота (перед запуском программы добавьте token от своего бота)
            string token = "";
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Перед запуском программы вам нужно добавить токен от своего бота в переменную token");
                return;
            }

            // Метод ниже нужен для слияния пользовательского интерфейса,
            // определенного в разметке xaml, с классом кода программной части
            InitializeComponent();

            client = new TelegramMessageClient(this, token);

            // Тут мы связываем содеримое блока ListBox с нашим списком сообщений,
            // чтобы этот блок имел представление откуда брать FirstName, Msg, Time
            loglist.ItemsSource = client.ListMessageLog;
        }

        private void buttonMsgSend_Click(object sender, RoutedEventArgs e)
        {
            client.SendMessage(textBoxSendMsg.Text, targetSendId.Text);
        }

        private void buttonSaveMsg_Click(object sender, RoutedEventArgs e)
        {
            if(client.ListMessageLog.Count != 0)
            {
                string json = JsonConvert.SerializeObject(client.ListMessageLog);
                File.WriteAllText("MsgStory.json", json);
            }
        }

        private void buttonCloseApp_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void buttonGetMsgStory_Click(object sender, RoutedEventArgs e)
        {
            if(File.Exists("MsgStory.json"))
            {
                string json = File.ReadAllText("MsgStory.json");
                if (!string.IsNullOrEmpty(json))
                {
                    ObservableCollection<MessageLog> storyList = new ObservableCollection<MessageLog>();
                    storyList = JsonConvert.DeserializeObject<ObservableCollection<MessageLog>>(json);
                    storyListBox.ItemsSource = storyList;
                }
            }           
        }
    }
}
