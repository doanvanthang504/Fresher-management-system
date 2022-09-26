using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Fresher : BaseEntity
    {
        public string? AccountName { get; set; }

        public Guid ClassFresherId { get; set; }

        public string? ClassCode { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? RECer { get; set; }

        public StatusFresherEnum Status { get; set; }

        public SkillFresherEnum Skill { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public DateOnly DOB { get; set; }

        public DateOnly DropOutDate { get; set; }

        public int Graduation { get; set; }

        public double GPA { get; set; }

        public string? University { get; set; }

        public string? English { get; set; }

        public DateOnly OnBoard { get; set; }

        public int Salary { get; set; }

        public string? Major { get; set; }

        public string? RRCode { get; set; }

        public string? ContactType { get; set; }

        public string? Note { get; set; }
        
        public string? JobRank { get; set; }

        public ClassFresher? ClassFresher { get; set; }

        public ICollection<ModuleResult> ModuleResults { get; set; }

        public ICollection<Attendance> Attendances { get; set; }
    }
}
