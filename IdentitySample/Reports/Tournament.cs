using IdentitySample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentitySample.Reports
{
    public class TeamR
    {
        public string Name { get; set; }
        public string Coach { get; set; }
        public string Ranking { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool IsConfirmed { get; set; }
        public List<ParticipantR> Participants { get; set; }
    }

    public class ParticipantR
    {
        public string FullName { get; set; }
        public string Ranking { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Age { get; set; }
        public double Weight { get; set; }
    }
}