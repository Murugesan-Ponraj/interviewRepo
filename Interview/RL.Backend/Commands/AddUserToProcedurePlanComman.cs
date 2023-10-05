using MediatR;
using RL.BackEnd.Common.Model;

namespace RL.Backend.Commands
{
    public class AddUserToProcedurePlanCommand : IRequest<ApiResponse<Unit>>
    {
        public int UserId { get; set; }
        public int PlanId { get; set; }
        public int ProcedureId { get; set; }
    }
}
