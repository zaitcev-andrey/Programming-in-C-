using System;
using System.Collections.Generic;
using System.Text;

namespace Task3_OOP1_WPF
{
    internal interface IConsultantMethods
    {
        void SetClientTelephoneNumber(Client client);
        void SetClientTelephoneNumber(Client client, string telephoneNumber);
    }
}
