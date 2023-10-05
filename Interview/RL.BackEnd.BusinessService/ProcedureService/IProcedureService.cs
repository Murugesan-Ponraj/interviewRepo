using RL.BackEnd.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL.BackEnd.BusinessService.ProcedureService
{
    public interface IProcedureService
    {
        public ApiResponse<ProcedureModel> GetById(int id);
        public ApiResponse<ProcedureModel> GetAll();
    }
}
