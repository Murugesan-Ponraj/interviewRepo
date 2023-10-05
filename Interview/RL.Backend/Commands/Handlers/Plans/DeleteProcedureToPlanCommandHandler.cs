using MediatR;
using Microsoft.EntityFrameworkCore;
using RL.Backend.Exceptions;
using RL.BackEnd.Common.Model;
using RL.Data;
using RL.Data.DataModels;

namespace RL.Backend.Commands.Handlers.Plans;

public class DeleteProcedureFromPlanCommandHandler : IRequestHandler<DeleteProcedureFromPlanCommand, ApiResponse<Unit>>
{
    private readonly RLContext _context;

    public DeleteProcedureFromPlanCommandHandler(RLContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<Unit>> Handle(DeleteProcedureFromPlanCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //Validate request
            if (request.PlanId < 1)
                return ApiResponse<Unit>.Fail(new BadRequestException("Invalid PlanId"));
            if (request.ProcedureId < 1)
                return ApiResponse<Unit>.Fail(new BadRequestException("Invalid ProcedureId"));

            var plan = await _context.Plans
                .Include(p => p.PlanProcedures)
                .FirstOrDefaultAsync(p => p.PlanId == request.PlanId);
            var procedure = await _context.Procedures.FirstOrDefaultAsync(p => p.ProcedureId == request.ProcedureId);

            if (plan is null)
                return ApiResponse<Unit>.Fail(new NotFoundException($"PlanId: {request.PlanId} not found"));
            if (procedure is null)
                return ApiResponse<Unit>.Fail(new NotFoundException($"ProcedureId: {request.ProcedureId} not found"));

            PlanProcedure? planProcedure = _context.PlanProcedures.Where(a => a.PlanId == request.PlanId && a.ProcedureId == request.ProcedureId).FirstOrDefault();
            if (planProcedure == null)
                return ApiResponse<Unit>.Fail(new NotFoundException("Plan Procedure not found"));
            //Already has the procedure, so just succeed
            IQueryable<UserPlanProcedureRelation> userPlanProcedureRelations = _context.UserPlanProcedureRelations.
               Where(a => a.UserPlanProcedureRelationId == planProcedure.PlanProcedureId);
            if(userPlanProcedureRelations.Any())
                _context.UserPlanProcedureRelations.RemoveRange(userPlanProcedureRelations); 
            _context.PlanProcedures.Remove(planProcedure);
            await _context.SaveChangesAsync();

            return ApiResponse<Unit>.Succeed(new Unit());
        }
        catch (Exception e)
        {
            return ApiResponse<Unit>.Fail(e);
        }
    }
}