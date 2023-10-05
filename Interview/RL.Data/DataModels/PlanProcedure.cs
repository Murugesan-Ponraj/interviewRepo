using RL.Data.DataModels.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RL.Data.DataModels;

public class PlanProcedure : IChangeTrackable
{
    public PlanProcedure() => UserPlanProcedureRelations = new List<UserPlanProcedureRelation>();

    [Key] 
    public int PlanProcedureId {get;set;}
               
    public int ProcedureId { get; set; }
    public int PlanId { get; set; }
    public virtual Procedure Procedure { get; set; }
    public virtual Plan Plan { get; set; } 
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public virtual ICollection<UserPlanProcedureRelation> UserPlanProcedureRelations { get; set; }
}
