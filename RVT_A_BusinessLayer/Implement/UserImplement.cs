using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RVT_A_BusinessLayer.Responses;
using RVT_A_DataLayer.Entities;
using RVT_Block_lib;
using RVT_Block_lib.Algoritms;
using RVT_Block_lib.Models;
using RVTLibrary.Models;
using RVTLibrary.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RVT_A_BusinessLayer.Implement
{
    public class UserImplement
    {
        internal async Task<RegistrationResponse> registerAction(RegistrationMessage data)
        {
            //fnv1a32
            var fiscData = new FiscData();
            fiscData.Idnp = data.IDNP;
            fiscData.Name = data.Name;
            fiscData.Region = data.Region;
            fiscData.Surname = data.Surname;
            fiscData.Gender = data.Gender;
            fiscData.BirthDate = data.Birth_date.Date;

            using (var db = new SFBD_AccountsContext())
            {
                try
                {
                    var fisc = await db.FiscData.FirstOrDefaultAsync(x =>
               x.Idnp == fiscData.Idnp &&
               x.Gender == fiscData.Gender &&
               x.Region.Contains(fiscData.Region) &&
               x.Surname == fiscData.Surname &&
               x.Name == fiscData.Name &&
               //x.BirthDate.Value.Year == fiscData.BirthDate.Value.Year &&
               //x.BirthDate.Value.Month == fiscData.BirthDate.Value.Month &&
               //x.BirthDate.Value.Day == fiscData.BirthDate.Value.Day
               x.BirthDate == fiscData.BirthDate
               );// o gasit in baza de date fiscala, si transmite spre loadbalancer
                    if (fisc == null)
                    {// o gasit in baza de date fiscala asa persoana si deja se transmite la LB 
                        return new RegistrationResponse { Status = false, Message = "Datele introduse sunt gresite" };
                    }
                }
                catch(AggregateException)
                {

                }



            }


            // To LOADBALANCER
            var content = new StringContent(data.Serialize(), Encoding.UTF8, "application/json");
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:44322/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsync("api/Regist", content);

            var regLbResponse = new RegLbResponse();
            try
            {
                var data_resp = await response.Content.ReadAsStringAsync();
                regLbResponse = JsonConvert.DeserializeObject<RegLbResponse>(data_resp);
            }
            catch (AggregateException e)
            {
                return new RegistrationResponse { Status = false, Message = "Eroare de conectare la server LB.1.0.1"+e.Message };
            }

            if (regLbResponse.Status == true)
            {
                using (var db = new SFBD_AccountsContext())
                {
                    var account = new IdvnAccounts();
                    account.Idvn = regLbResponse.IDVN;
                    account.VnPassword = regLbResponse.VnPassword;
                    account.StatusNumber = "Non confirmed";
                    account.IpAddress = data.Ip_address;
                    account.PhoneNumber = data.Phone_Number;
                    account.Email = data.Email;

                    db.Add(account);
                    db.SaveChanges();

                }
                var random = new Random();
                return new RegistrationResponse { Status = true, ConfirmKey = random.Next(12452, 87620).ToString(), Message = "Registered", IDVN = regLbResponse.IDVN, Email = data.Email };
            }
            else return new RegistrationResponse { Status = false, Message = "Eroare de inregistrare" };
        }
    }
}

