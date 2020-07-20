using RVT_A_BusinessLayer.Responses;
using RVT_A_DataLayer.Entities;
using RVT_Block_lib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RVT_A_BusinessLayer.Interfaces
{
    public interface IAdmin
    {
        public Task<AdminAuthResp> AdminAuth(AdminAuthMessage message);

        public Task<VoteStatusResponse> VoteStatus(VoteStatusMessage vote);
        public Task<List<Blocks>> Blocks(BlocksMessage blockmess);
    }
}
