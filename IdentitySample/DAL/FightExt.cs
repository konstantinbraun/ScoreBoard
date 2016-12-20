using IdentitySample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentitySample.DAL
{
    public enum FightStatus
    {
        NotDefined,
        Waiting,
        Running,
        Closed
    }
    public class FightExt
    {
        public Fight Fight { get; set; }
        public FightStatus FightStatus { get; set; }

    }
}