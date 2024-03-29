using System;
using Pumox.Core.Types.Enums;

namespace Pumox.Core.Domain.Entities
{
    public class Employe : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public JobTitle JobTitle { get; set; }

        public ulong CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}