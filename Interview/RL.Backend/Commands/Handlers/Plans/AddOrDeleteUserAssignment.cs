using MediatR;
using RL.Backend.Exceptions;
using RL.BackEnd.Common.Model;
using RL.Data;
using Microsoft.EntityFrameworkCore;
using RL.Data.DataModels;

namespace RL.Backend.Commands.Handlers.Plans
{
    public class AddOrDeleteUserAssignmentCommonHandler : IRequestHandler<AddUserToProcedurePlanCommand, ApiResponse<Unit>>
    {
        private readonly RLContext _context;

        public AddOrDeleteUserAssignmentCommonHandler(RLContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<Unit>> Handle(AddUserToProcedurePlanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //Validate request
                if (request.UserId < 1)
                    return ApiResponse<Unit>.Fail(new BadRequestException("Invalid UserId"));
                if (request.PlanId < 1)
                    return ApiResponse<Unit>.Fail(new BadRequestException("Invalid PlanId"));
                if (request.ProcedureId < 1)
                    return ApiResponse<Unit>.Fail(new BadRequestException("Invalid ProcedureId"));
                PlanProcedure? planProcedure = _context.PlanProcedures.Where(a => a.PlanId == request.PlanId && a.ProcedureId == request.ProcedureId).FirstOrDefault();
                if(planProcedure == null)
                    return ApiResponse<Unit>.Fail(new NotFoundException("Plan Procedure not found"));

                UserPlanProcedureRelation? userPlanProcedureRelations = _context.UserPlanProcedureRelations
                    .Where(a => a.PlanProcedureId == planProcedure.PlanProcedureId && a.UserId == request.UserId).FirstOrDefault();

                if (userPlanProcedureRelations == null)
                {  
                    UserPlanProcedureRelation userPlanProcedureRelation = new UserPlanProcedureRelation()
                    {
                        PlanProcedureId = planProcedure.PlanProcedureId,
                        UserId = request.UserId
                    };
                    _context.UserPlanProcedureRelations.Add(userPlanProcedureRelation); 
                    await _context.SaveChangesAsync();
                }
                else 
                { 
                    _context.UserPlanProcedureRelations.Remove(userPlanProcedureRelations);
                    await _context.SaveChangesAsync();
                } 
               
                return ApiResponse<Unit>.Succeed(new Unit());
            }
            catch (Exception e)
            {
                return ApiResponse<Unit>.Fail(e);
            }
        }
    }
}
