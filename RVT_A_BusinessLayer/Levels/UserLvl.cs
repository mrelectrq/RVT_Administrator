using RVT_A_BusinessLayer.Implement;
using RVT_A_BusinessLayer.Interfaces;
using RVT_A_BusinessLayer.Responses;
using RVT_Block_lib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RVT_A_BusinessLayer.Levels
{
    public class UserLvl : UserImplement, ITerminal
    {

        public Task<RegistrationResponse> Registration(RegistrationMessage message)
        {
            return registerAction(message);
        }

    }
}
