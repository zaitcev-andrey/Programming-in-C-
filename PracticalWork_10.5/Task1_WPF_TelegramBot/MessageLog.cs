using System;
using System.Collections.Generic;
using System.Text;

namespace Task1_WPF_TelegramBot
{
    struct MessageLog
    {
        public string Time { get; set; }
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string Msg { get; set; }

        public MessageLog(string Time, long Id, string FirstName, string Msg)
        {
            this.Time = Time;
            this.Id = Id;
            this.FirstName = FirstName;
            this.Msg = Msg;
        }
    }
}
