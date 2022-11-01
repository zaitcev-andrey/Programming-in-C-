using System;
using System.Collections.Generic;
using System.Text;

namespace Task1_OOP2_WPF
{
    internal interface IConsultantMethods
    {
        void SetClientTelephoneNumber(Client client);
        void SetClientTelephoneNumber(Client client, string telephoneNumber);
    }
}
