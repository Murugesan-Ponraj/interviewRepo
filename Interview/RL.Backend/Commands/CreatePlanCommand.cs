using MediatR; 
using RL.BackEnd.Common.Model;
using RL.Data.DataModels;

namespace RL.Backend.Commands;

public class CreatePlanCommand : IRequest<ApiResponse<Plan>>
{

}