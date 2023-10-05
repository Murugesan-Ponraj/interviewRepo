using RL.Data.DataModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL.Data.DataModels
{
    public class UserPlanProcedureRelation :  IChangeTrackable
    { 
       
        [Key]
        public int UserPlanProcedureRelationId { get; set; }
        public virtual PlanProcedure PlanProcedure  { get;set;}
        public int PlanProcedureId { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set;  }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; } 
    }
}
