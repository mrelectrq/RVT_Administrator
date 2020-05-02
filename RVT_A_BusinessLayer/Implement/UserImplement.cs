using Microsoft.EntityFrameworkCore;
using RVT_A_BusinessLayer.Responses;
using RVT_A_DataLayer.Entities;
using RVT_Block_lib;
using RVT_Block_lib.Algoritms;
using RVT_Block_lib.Models;
using System;
using System.Linq;
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
                );
                
                if (fisc==null)
                {
                    return new RegistrationResponse { Status = false, Message = "Datele introduse sunt gresite" };
                }
                else
                {
                    var crc = new Crc32();
                    var array = Encoding.ASCII.GetBytes(fisc.Name + fisc.Surname + fisc.Idnp);
                    var account = new IdvnAccounts();
                    account.VnPassword = crc.Get(array).ToString();
                    account.Idvn = IDVN_Gen.HashGen(account.VnPassword+fiscData.Idnp);
                    account.Email = data.Email;
                    account.PhoneNumber = data.Phone_Number;

                   var check = db.IdvnAccounts.FirstOrDefault(x => x.Email == account.Email || x.PhoneNumber == account.PhoneNumber
                    || x.Idvn == account.Idvn);

                    if (check!=null)
                    {
                        return new RegistrationResponse { Status = false, Message = "Unele din date sunt deja introduse" };
                    }

                        

                    account.IpAddress = data.Ip_address;
                    account.RegisterDate = data.RegisterDate;
                    account.StatusNumber = "Non Confirmed";

                    db.IdvnAccounts.Add(account);
                    var confirm = new ConfirmAcc();
                    confirm.Idvn = account.Idvn;
                    Random random = new Random();
                    confirm.ConfirmKey = random.Next(12452, 87620).ToString();
                    confirm.DateExpirationKey = DateTime.Now.AddMinutes(15);
                    db.ConfirmAcc.Add(confirm);

                    var rel = new IdRelations();
                    rel.Idvn = account.Idvn;
                    rel.Idbd = Cipher.Encrypt(data.IDNP,rel.Idvn);
                    db.IdRelations.Add(rel);

                    db.SaveChanges();
                    return new RegistrationResponse { Message = "Registered", Status = true,
                    ConfirmKey=confirm.ConfirmKey,
                    Email=account.Email,
                    Password = account.VnPassword,
                    IDVN=account.Idvn};
                }
            }       
        }

      /// De implementat regenerarea cheiei------------------------------
     
        internal async Task<AuthenticationResponse> Auth(AuthenticationMessage message)
        {
            var account = new IdvnAccounts();
            account.Idvn = IDVN_Gen.HashGen(message.VnPassword + message.IDNP);
            account.VnPassword = message.VnPassword;
            using (var context = new SFBD_AccountsContext())
            {
                var data = await context.IdvnAccounts.FirstOrDefaultAsync(m =>
                m.VnPassword == account.VnPassword && m.Idvn == account.Idvn);

                if(data==null)
                {
                    return new AuthenticationResponse { Status = false, Message = "User cu asa date nu exista" };
                }
                else
                {

                    return new AuthenticationResponse { Status = true, Message = "Autorizat", IDVN=account.Idvn };
                }
            }
               
        }

        internal async Task<ChooserResponse> VoteAsync(VoteMessage message)
        {
            using (var db = new SFBD_AccountsContext())
            {

                var data = await db.IdvnAccounts.FirstOrDefaultAsync(m => m.Idvn == message.IDVN);

                if (data == null)
                {
                    return new ChooserResponse { Status = false, Message = "Erorr state: This user doesn't exist", ProcessedTime = DateTime.Now };
                }

                var state = await db.VoteStatus.FirstOrDefaultAsync(m => m.Idvn == message.IDVN);

                if (state != null)
                {
                    return new ChooserResponse
                    {
                        Status = false,
                        Message = "Erorr state: Votul nu poate fi dublat. Votul este deja procesat",
                        ProcessedTime = DateTime.Now
                    };
                }

                var relation = await db.IdRelations.FirstOrDefaultAsync(m => m.Idvn == data.Idvn);
                if (relation == null)
                {
                    return new ChooserResponse { Status = false, Message = "Erorr state: This user IDBD doesn't exist", ProcessedTime = DateTime.Now };
                }

                string idnp = Cipher.Decrypt(relation.Idbd, relation.Idvn);

                var fiscData = await db.FiscData.FirstOrDefaultAsync(m => m.Idnp == idnp);

                if (fiscData == null)
                {
                    return new ChooserResponse { Status = false, Message = "Erorr state: This user Cicle3 IDNP doesn't exist", ProcessedTime = DateTime.Now };
                }

                var account = await db.IdvnAccounts.FirstOrDefaultAsync(m => m.Idvn == relation.Idvn);

                if (account == null)
                {
                    return new ChooserResponse { Status = false, Message = "Error state: Account didnt found", ProcessedTime = DateTime.Now };
                }

                var region_related = await db.RegionRelated.FirstOrDefaultAsync(m => m.RegionName == fiscData.Region);


               var chooser = new Chooser();
                chooser.Name = fiscData.Name;
                chooser.Surname = fiscData.Surname;
                chooser.IDNP = fiscData.Idnp;
                chooser.Gender = fiscData.Gender;
                chooser.Birth_date = fiscData.BirthDate;
                chooser.Vote_date = DateTime.Now;
                chooser.Region = region_related.IntValue.Value;
                chooser.Phone_Number = account.PhoneNumber;
                chooser.Email = account.Email;


                
                
            }

            return null;
        }


    }
}
