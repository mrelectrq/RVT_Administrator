using RVT_A_BusinessLayer.Implement;
using RVT_A_BusinessLayer.Interfaces;
using RVT_A_BusinessLayer.Responses;
using RVT_A_DataLayer.Entities;
using RVT_Block_lib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RVT_A_BusinessLayer.Levels
{
    public class AdminLvl : AdminImplement, IAdmin
    {
        public Task<AdminAuthResp> AdminAuth(AdminAuthMessage message)
        {
            return AdminAuthAction(message);
        }

        public Task<VoteStatusResponse> VoteStatus()
        {
            return VoteStatusAction();
        }
        public Task<List<Blocks>> Blocks(BlocksMessage blockmess)
        {
            return BlocksAction(blockmess);
        }
        public Task<RegionResponse> RegionStatus(string id)
        {
            return RegionAction(id);
        }
    }
    
}
