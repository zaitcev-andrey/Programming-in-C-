using System;
using System.Collections.Generic;
using System.Text;

namespace Task1_OOP2_WPF
{
    internal interface IManagerMethods
    {
        void SetClientFio(Client client);
        void SetClientFio(Client client, string lastName, string firstName, string middleName);
        void SetClientFirstName(Client client);
        void SetClientLastName(Client client);
        void SetClientMiddleName(Client client);

        void SetClientTelephoneNumber(Client client);
        void SetClientPasportData(Client client);
        bool SetClientPasportData(Client client, string pasportSeries, string pasportNumber);

        Client AddNewNoteAboutClient();
        void AddTimeAndLogNoteAboutClient(Client client);
    }
}
