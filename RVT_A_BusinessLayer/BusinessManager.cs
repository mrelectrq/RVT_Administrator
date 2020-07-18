using RVT_A_BusinessLayer.Interfaces;
using RVT_A_BusinessLayer.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RVT_A_BusinessLayer
{
    public class BusinessManager
    {
        public ITerminal GetTerminal()
        {
            return new UserLvl();
        }
        public IAdmin GetAdmin()
        {
            return new AdminLvl();
        }
    }
}
