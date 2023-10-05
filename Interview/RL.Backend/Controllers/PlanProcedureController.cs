using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RL.Data;
using RL.Data.DataModels;
using System.Data.Entity;

namespace RL.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class PlanProcedureController : ControllerBase
{
    private readonly ILogger<PlanProcedureController> _logger;
    private readonly RLContext _context;

    public PlanProcedureController(ILogger<PlanProcedureController> logger, RLContext context)
    {
        _logger = logger;
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet]
    [EnableQuery]
    public IEnumerable<PlanProcedure> Get()
    {
        var procedures = _context.Procedures.ToList();
        var plans = _context.Plans.ToList();
        var userPlanProcedureRelations = _context.UserPlanProcedureRelations.ToList();
        var planProcedures = _context.PlanProcedures;
        
        //planProcedures.ToList().ForEach((a) => {
        //   // a.Plan = plans?.FirstOrDefault(d => d.PlanId == a.PlanId);
        //    a.Procedure = procedures?.FirstOrDefault(b => b.ProcedureId == a.ProcedureId);
        //    a.UserPlanProcedureRelations = userPlanProcedureRelations.Where(c => c.PlanProcedureId == a.PlanProcedureId).ToList();
        //});
        return planProcedures;

            //.ToList().Select( new PlanProcedure


        //a => a.Procedure = _context.Procedures.FirstOrDefault(b => b.ProcedureId == a.ProcedureId),
        //a => a.UserPlanProcedureRelations = _context.UserPlanProcedureRelations.Where(c => c.PlanProcedureId == a.PlanProcedureId))
    } 

}
