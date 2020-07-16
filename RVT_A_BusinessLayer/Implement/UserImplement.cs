using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RVT_A_BusinessLayer.Responses;
using RVT_A_DataLayer.Entities;
using RVT_Block_lib.Algoritms;
using RVT_Block_lib.Models;
using RVT_Block_lib.Requests;
using RVTLibrary.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.EntityFrameworkCore.Query;
using RVT_A_BusinessLayer.BusinessModels;
using RVT_A_BusinessLayer.Helpers;

namespace RVT_A_BusinessLayer.Implement
{
    public class UserImplement
    {
        internal async Task<RegistrationResponse> registerAction(RegistrationMessage data)
        {
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
               x.BirthDate == fiscData.BirthDate
               );
                    if (fisc == null)
                    {
                        return new RegistrationResponse { Status = false, Message = "Datele introduse sunt gresite" };
                    }
                }
                catch (AggregateException)
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
                return new RegistrationResponse { Status = false, Message = "Eroare de conectare la server LB.1.0.1" + e.Message };
            }

            if (regLbResponse.Status == true)
            {
                ///-------SMTP SEND EMAIL WITH PASSWORD------
                // Who send?
                MailAddress From = new MailAddress("rvtvote@gmail.com", "RVT Vote");
                string utilizator = "rvtvote@gmail.com";
                string password = "Ialoveni1";
                // where to send?
                MailAddress To = new MailAddress(data.Email);
                MailMessage msg = new MailMessage(From, To);
                msg.Subject = "Registration - RVT";
                msg.Body = "Registration Code - " + regLbResponse.VnPassword;
                msg.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(utilizator, password);
                smtp.EnableSsl = true;
                smtp.Send(msg);
                using (var db = new SFBD_AccountsContext())
                {
                    var account = new IdvnAccounts();
                    account.Idvn = regLbResponse.IDVN;
                    account.VnPassword = LoginHelper.HashGen(regLbResponse.VnPassword);
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
        internal async Task<AuthenticationResponse> AuthAction(AuthenticationMessage data)
        {
            var pass = LoginHelper.HashGen(data.VnPassword);
            var idvn = IDVN_Gen.HashGen(data.VnPassword + data.IDNP);
            using (var db = new SFBD_AccountsContext())
            {
                var verif = db.IdvnAccounts.FirstOrDefault(x =>
               x.Idvn == idvn &&
               x.VnPassword == pass);

                if (verif == null)
                {
                    return new AuthenticationResponse { Status = false, Message = "IDNP or password are not correct." };
                }
                else
                {

                }
            }

            return new AuthenticationResponse { Status = true, IDVN = idvn, Message = "Authenticated." };

        }
        internal async Task<VoteAdminResponse> VoteAction(VoteAdminMessage message)
        {

            var idvn = IDVN_Gen.HashGen(message.VnPassword + message.IDNP);


            using (var bd = new SFBD_AccountsContext())
            {
                var account = bd.IdvnAccounts.FirstOrDefault(m => m.Idvn == idvn);
                //if (account == null)
                //{
                //    return new VoteAdminResponse
                //    {
                //        VoteStatus = false,
                //        Message = "Credentialele sunt introduse incorect",
                //        ProcessedTime = DateTime.Now
                //    };
                //}
                //else if (account.StatusNumber == "Non Confirmed")
                //{
                //    return new VoteAdminResponse
                //    {
                //        VoteStatus = false,
                //        Message = "Accountul nu este activat, va rugam sa il activati",
                //        ProcessedTime = DateTime.Now
                //    };
                //}

                var vote_state = bd.VoteStatus.FirstOrDefault(m => m.Idvn == idvn);
                if (vote_state != null)
                    return new VoteAdminResponse
                    {
                        VoteStatus = false,
                        Message = "Procesul de votare este deja efectuat, nu puteti vota dublu",
                        ProcessedTime = DateTime.Now
                    };
                var fiscData = bd.FiscData.FirstOrDefault(m => m.Idnp == message.IDNP);
                if (fiscData==null)
                    return new VoteAdminResponse
                    {
                        VoteStatus = false,
                        Message = "Eroare de identificare identitatii. Contactati suportul tehnic",
                        ProcessedTime = DateTime.Now
                    };

                var party = bd.Parties.FirstOrDefault(m => m.Idpart.ToString() == message.Party);
                var region = bd.Regions.FirstOrDefault(m => m.Region == fiscData.Region);

                var chooser = new ChooserLbMessage();
                chooser.Email = account.Email;
                chooser.Gender = fiscData.Gender;
                chooser.Name = fiscData.Name;
                chooser.Surname = fiscData.Surname;
                chooser.Region = region.Idreg;
                chooser.Vote_date = DateTime.Now;
                chooser.PartyChoosed = party.Idpart;
                chooser.IDVN = idvn;
                chooser.Phone_Number = account.PhoneNumber;
                chooser.Birth_date = fiscData.BirthDate;
                chooser.IDNP = fiscData.Idnp;

                var content = new StringContent(JsonConvert.SerializeObject(chooser), Encoding.UTF8, "application/json");
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                var client = new HttpClient(handler);
                client.BaseAddress = new Uri("https://localhost:44322/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsync("api/Vote", content);

                var regLbResponse = new VoteLbResponse();
                try
                {
                    var data_resp = await response.Result.Content.ReadAsStringAsync();
                    regLbResponse = JsonConvert.DeserializeObject<VoteLbResponse>(data_resp);
                }
                catch (AggregateException e)
                {
                    return new VoteAdminResponse { VoteStatus = false, Message = "Eroare de conectare la server LB.1.0.1" + e.Message };
                }

                if(regLbResponse.Status==false)
                {
                    return new VoteAdminResponse { VoteStatus = false, Message = regLbResponse.Message, ProcessedTime = regLbResponse.ProcessedTime };
                }

                var block = regLbResponse.block;
                var datablock = new Blocks();
               // datablock.BlockId = block.BlockId;
                datablock.CreatedOn = block.CreatedOn;
                datablock.Gender = block.Gender;
                datablock.Hash = block.Hash;
                datablock.PartyChoosed = block.PartyChoosed;
                datablock.PreviousHash = block.PreviousHash;
                datablock.YearBirth = block.YearToBirth;
                datablock.RegionChoosed = block.RegionChoosed;
                bd.Add(datablock);



                var vote_status = new RVT_A_DataLayer.Entities.VoteStatus();
                vote_status.Idvn = idvn;
                vote_status.VoteState = "Confirmed";
                bd.Add(vote_status);
                bd.SaveChanges();
                return new VoteAdminResponse { VoteStatus = true, ProcessedTime = DateTime.Now, Message = "Voted" };

            }
        }

        internal async Task<VoteStatusResponse> VoteStatusAction(VoteStatusMessage vote)
        {
            List<VoteStatistics> parties = new List<VoteStatistics>();
            int votants = 0;
            int population = 0;
            using (var context = new SFBD_AccountsContext())
            {
                votants = (from st in context.Blocks
                               select st.BlockId).Count();
                //-----------------Number of parties to count------------------
                for (int i = 1; i <=5; i++)
                {
                    var party = new VoteStatistics();
                    party.IDParty = i;
                    party.Votes = (from st in context.Blocks
                        where st.PartyChoosed == party.IDParty
                        select st).Count();
                    parties.Add(party);
                    population = (from st in context.FiscData
                                  select st.Idnp).Count();

                }

            }

            return new VoteStatusResponse
            {
                Time = DateTime.Now, TotalVotes = parties, Votants = votants,Population=population            };
        }

    }
}

