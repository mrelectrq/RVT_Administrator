using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using RVT_A_BusinessLayer.BusinessModels;
using RVT_A_BusinessLayer.Responses;
using RVT_A_DataLayer.Entities;
using RVT_Block_lib;
using RVT_Block_lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RVT_A_BusinessLayer.Implement
{
    public class AdminImplement
    {
        internal async Task<AdminAuthResp> AdminAuthAction(AdminAuthMessage data)
        {
            using (var db = new SFBD_AccountsContext())
            {
                ///--------------Admin Registration--------------
                //var account = new AdminSession();
                //account.Ip = data.IP;
                //account.LastTime = data.date;
                //account.Token = data.Token;
                //db.Add(account);
                //db.SaveChanges();
                var verif = db.AdminSession.FirstOrDefault(x => x.Token == data.Token);

                if (verif == null)
                {
                    return new AdminAuthResp { Status = false, Message = "IDNP or password are not correct." };
                }
                else
                {
                    verif.LastTime = data.date;
                    verif.Ip = data.IP;
                    db.SaveChanges();
                }
            }

            return new AdminAuthResp { Status = true, Message = "Authenticated.", Token = data.Token };

        }
        internal async Task<VoteStatusResponse> VoteStatusAction()
        {
            List<VoteStatistics> parties = new List<VoteStatistics>();
            int votants = 0;
            int population = 0;
            int pending = 0;
            var gender = new GenderStatistic();
            using (var context = new SFBD_AccountsContext())
            {
                votants = (from st in context.Blocks
                           select st.BlockId).Count();
                //-----------------Number of parties to count------------------
                for (int i = 1; i <= 5; i++)
                {
                    var party = new VoteStatistics();
                    party.IDParty = i;
                    party.Votes = (from st in context.Blocks
                                   where st.PartyChoosed == party.IDParty
                                   select st).Count();
                    parties.Add(party);
                    population = (from st in context.FiscData
                                  select st.Idnp).Count();
                    pending = (from st in context.IdvnAccounts
                               select st.Idvn).Count();
                    gender.Male = (from st in context.Blocks
                                   where st.Gender == "Male"
                                   select st).Count();
                    gender.Female = (from st in context.Blocks
                                     where st.Gender == "Female"
                                     select st).Count();

                }

            }

            return new VoteStatusResponse
            {
                Time = DateTime.Now,
                TotalVotes = parties,
                Votants = votants,
                Population = population,
                Pending = pending,
                GenderStatistics = gender
            };
        }
        internal async Task<List<Blocks>> BlocksAction(BlocksMessage blockmess)
        {
            if (blockmess.Status = true)
            {
                var blocks = new List<Blocks>();
                using (var context = new SFBD_AccountsContext())
                {
                    var query = (from st in context.Blocks
                                 select st);
                    foreach (var block in query.ToList())
                    {
                        blocks.Add(block);
                    }
                }
                return blocks;
            }
            return null;
        }
    }
}
