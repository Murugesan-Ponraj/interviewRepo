using MediatR;
using RL.BackEnd.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL.BackEnd.BusinessService.Commands
{
    public class UserCommand :  IRequest<ApiResponse<Unit>>
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
