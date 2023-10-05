using MediatR;
using RL.BackEnd.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL.BackEnd.BusinessService.Commands
{
    public class ProcedureCommand : IRequest<ApiResponse<Unit>>
    {
        public int ProcedureId { get; set; }
        public string ProcedureTitle { get; set; }
    } 
}
