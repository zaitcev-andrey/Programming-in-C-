using System;
using System.Collections.Generic;
using System.Text;

namespace Task3_OOP1_Console
{
    internal interface IManagerMethods
    {
        void SetClientFio(Client client);
        void SetClientFirstName(Client client);
        void SetClientLastName(Client client);
        void SetClientMiddleName(Client client);

        void SetClientTelephoneNumber(Client client);
        void SetClientPasportData(Client client);

        Client AddNewNoteAboutClient();
    }
}
