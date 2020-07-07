using RVT_A_BusinessLayer.Responses;
using RVT_Block_lib.Models;
using RVT_Block_lib.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RVT_A_BusinessLayer.Interfaces
{
    public interface ITerminal
    {
        public  Task<RegistrationResponse> Registration(RegistrationMessage message);
        public  Task<AuthenticationResponse> Auth(AuthenticationMessage message);
        public VoteAdminResponse Vote(VoteAdminMessage message);
        public Task<VoteStatusResponse> VoteStatus(VoteStatusMessage vote);
    }
}
