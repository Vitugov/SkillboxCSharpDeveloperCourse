using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task01.Model.Accsess;
using Task01.Model.Data;

namespace Task01.ViewModel
{
    public class ClientViewModel
    {
        public List<string> DisplayableProperties { get; set; }
        public List<string> ChangableProperties { get; set; }
        public bool BeAbleToCreate { get; set; }

        public ClientViewModel(Client client, Role role)
        {
            var BaseProperties = new List<string>() {
                client.Surname,
                client.Name,
                client.Patronymic,
                client.TelephoneNumber};

            //if (role == Role.Consultant)
            //{
            //    DisplayableProperties = BaseProperties.Concat(new List<string>() { }).ToList();
            //    ChangableProperties = BaseProperties.Concat(new List<string>() { }).ToList();
            //    BeAbleToCreate = false;
            //}

            //else if (role == Role.Manager)
            //{
            //    DisplayableProperties = BaseProperties.Concat(new List<string>() { client.PassportSeriesNumber }).ToList();
            //    ChangableProperties = BaseProperties.Concat(new List<string>() { client.PassportSeriesNumber }).ToList();
            //    BeAbleToCreate = true;
            //}
            //else
            //{
            //    throw new ArgumentException($"Role '{role}' not supported.");
            //}
        }
    }
}
