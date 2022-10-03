﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace Task1_WPF_TelegramBot
{
    class TelegramMessageClient
    {
        #region Поля

        private MainWindow w;
        private TelegramBotClient bot;

        // Список для хранения сообщений именно такого типа, чтобы происходили автоматические
        // обновления в UI при добавлении сообщения (для List явно нужен refresh)
        public ObservableCollection<MessageLog> ListMessageLog { get; set; }

        #endregion

        #region Методы и конструктор

        /// <summary>
        /// Конструктор по созданию телеграм клиента, который будет связан с UI, сделанным через WPF
        /// </summary>
        /// <param name="w"></param>
        /// <param name="token"></param>
        public TelegramMessageClient(MainWindow w, string token)
        {
            this.w = w;
            bot = new TelegramBotClient(token);
            ListMessageLog = new ObservableCollection<MessageLog>();

            bot.StartReceiving(updateHandler, Error);
        }

        /// <summary>
        /// Метод по отправке сообщений боту
        /// </summary>
        /// <param name="text"></param>
        /// <param name="Id"></param>
        public async void SendMessage(string text, string Id)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(Id))
                return;
            long id = Convert.ToInt64(Id);
            await bot.SendTextMessageAsync(id, text);
        }

        private string GetAllFiles(string pathToDirectory = "../")
        {
            string[] allfiles = Directory.GetFiles(pathToDirectory);
            if (allfiles.Length == 0)
                return "Нет файлов";
            StringBuilder sb = new StringBuilder();
            foreach (string filename in allfiles)
            {
                // Вместо выбора подстроки с 3 индекса, чтобы не выводить ../
                // используем Path.GetFileName(filename)
                sb.Append($"{Path.GetFileName(filename)}\n");
            }
            return sb.ToString();
        }

        /// <summary>
        /// метод, в котором происходит получение данных от бота и загрузка данных боту
        /// </summary>
        /// <param name="botClient">объект, через который мы отправляем данные боту и загружаем их от бота</param>
        /// <param name="update">в этом объекте будет храниться сообщение, которое мы получим от бота</param>
        /// <param name="token"></param>
        /// <returns></returns>
        // сразу заменяем private на async, чтобы ничего не зависало в потоках
        private async Task updateHandler(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            // Вот тут будем заниматься обработкой
            // Мы обращаемся к переменной update, именно она получает от 
            // StartReceiving все события
            var message = update.Message; // тут будет храниться само сообщение для удобства

            // Тут обрабатываем текстовые сообщения
            if (message.Text != null)
            {
                // Здесь происходит автоматическое добавление записей в UI через метод у w
                w.Dispatcher.Invoke(() =>
                {
                    ListMessageLog.Add(
                        new MessageLog(DateTime.Now.ToLongTimeString(),
                        message.Chat.Id, message.Chat.FirstName, message.Text));
                });

                if (message.Text.ToLower() == @"/start")
                {
                    // Отправляем таким образом сообщение нам от бота
                    // обязательно указываем id, куда бот будет отправлять сообщение
                    // и вторым обязательным параметром само сообщение
                    // также пишем в начале await, так как того требует Task
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Привет, я твой бот, и я буду отвечать на твои сообщения.\n" +
                        "Ты можешь отправлять мне любые файлы, а я их буду сохранять.\n" +
                        "Отправь мне 'получить файлы на компьютере' и я выведу весь список файлов, которые сейчас есть в твоей папке для файлов на компьютере'\n" +
                        "Отправь мне 'скачать файл + имя файла' (например скачать файл picture.jpg), если хочешь, чтобы я загрузил какой-нибудь файл в переписку");
                    return;
                }
                else if (message.Text.ToLower() == "получить файлы на компьютере")
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, GetAllFiles());

                    return;
                }
                else if (message.Text.ToLower().Contains("скачать файл"))
                {
                    string fileName = message.Text[13..]; // пропускаем скачать файл и пробел после него
                    string destinationFilePath = $"../{fileName}";

                    // обязательно проверка на существование файла
                    if (System.IO.File.Exists(destinationFilePath))
                    {
                        await using (Stream stream = System.IO.File.OpenRead(destinationFilePath))
                        {
                            // следующий метод взял из 3.4.2 в документации
                            await botClient.SendDocumentAsync(message.Chat.Id, new InputOnlineFile(stream, fileName));
                        }

                        return;
                    }

                    await botClient.SendTextMessageAsync(message.Chat.Id, "Вы некорректно ввели команду, возможно ошиблись в названии файла " +
                        "или поставили лишний пробел, попробуйте снова");
                    return;
                }
                else if (message.Text.ToLower() == "как дела")
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Дела отлично!");
                    return;
                }
            }

            if (message.Photo != null)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Отличное фото, " +
                    "но отправь его в виде документа, чтобы я мог сохранить его на компьютер.");

                return;
            }

            // через Document сохраняются фотографии
            if (message.Document != null)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Сохраняю на компьютер");

                var fileId = message.Document.FileId;
                var fileInfo = await botClient.GetFileAsync(fileId);
                var filePath = fileInfo.FilePath;

                // на Framework.net уже не работает, нужно переносить на Core.net
                // в качестве пути указываем папку на путь назад, то есть debug, 
                // куда и сохранится наш файл под тем же именем (в данном случае картинка)
                string destinationFilePath = $"../{message.Document.FileName}";
                await using (FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath))
                {
                    await botClient.DownloadFileAsync(filePath, fileStream);
                }

                return;
            }

            // Тут сохраняем аудиофайлы
            if (message.Audio != null)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Получил аудиофайл, сейчас сохраню его");

                var fileId = message.Audio.FileId;
                var fileInfo = await botClient.GetFileAsync(fileId);
                var filePath = fileInfo.FilePath;

                string destinationFilePath = $"../{message.Audio.FileName}";
                await using (FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath))
                {
                    await botClient.DownloadFileAsync(filePath, fileStream);
                }

                return;
            }

            // Тут сохраняем аудиосообщения
            if (message.Voice != null)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Получил аудиосообщение, сейчас сохраню его");

                var fileId = message.Voice.FileId;
                var fileInfo = await botClient.GetFileAsync(fileId);
                var filePath = fileInfo.FilePath;

                // обязательно в названии файла заменяем : на .
                // так как в названиях файлов не должно быть :
                string destinationFilePath = $"../Audio message from {DateTime.Now.ToString().Replace(':', '.')}.ogg";
                await using (FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath))
                {
                    await botClient.DownloadFileAsync(filePath, fileStream);
                }

                return;
            }

            // Тут сохраняем видео
            if (message.Video != null)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Получил видео, сейчас сохраню его");

                var fileId = message.Video.FileId;
                var fileInfo = await botClient.GetFileAsync(fileId);
                var filePath = fileInfo.FilePath;

                string destinationFilePath = $"../{message.Video.FileName}";
                await using (FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath))
                {
                    await botClient.DownloadFileAsync(filePath, fileStream);
                }

                return;
            }
        }

        private async Task Error(ITelegramBotClient botClient, Exception exc, CancellationToken token)
        {
            //throw new NotImplementedException();
            Console.WriteLine($"Error: {exc.Message}");
        }

        #endregion

    }
}
