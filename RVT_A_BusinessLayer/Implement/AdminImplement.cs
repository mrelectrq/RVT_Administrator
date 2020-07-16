using RVT_A_BusinessLayer.Responses;
using RVT_A_DataLayer.Entities;
using RVT_Block_lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
