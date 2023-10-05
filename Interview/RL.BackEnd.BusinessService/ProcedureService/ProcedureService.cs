using MediatR;
using RL.Backend.Exceptions;
using RL.BackEnd.BusinessService.Commands;
using RL.BackEnd.Common.Model;
using RL.BackEnd.Data;
using RL.Data;
using RL.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL.BackEnd.BusinessService.ProcedureService
{
    public class ProcedureServiceCommonHandler : IRequestHandler<ProcedureCommand, ApiResponse<Unit>>, IProcedureService
    {
      
        private readonly RLContext _context;
        private readonly IRepository<Procedure> _procedureRepo;

        public ProcedureServiceCommonHandler(RLContext context, IRepository<Procedure> repository)
        {
            _context = context;
            _procedureRepo = repository;
        }

        public ApiResponse<ProcedureModel> GetById(int id)
        {
            Procedure procedure = _procedureRepo.GetById(id);
            if(procedure is null)
                return ApiResponse<ProcedureModel>.Fail(new NotFoundException());
            ProcedureModel procedureModel = new ProcedureModel() { ProcedureId = procedure.ProcedureId, ProcedureTitle = procedure.ProcedureTitle };
            return ApiResponse<ProcedureModel>.Succeed(procedureModel); 
        }

        public ApiResponse<ProcedureModel> GetAll()
        {
            IEnumerable<Procedure> procedures = _procedureRepo.GetAll();
            if (procedures is null && procedures.Count() < 1 )
                return ApiResponse<ProcedureModel>.Fail(new NotFoundException());

            IEnumerable<ProcedureModel> procedureModels = procedures.Select( a => new ProcedureModel() { ProcedureId = a.ProcedureId, ProcedureTitle = a.ProcedureTitle });
            return ApiResponse<ProcedureModel>.Succeed(procedureModels);
        }

        public async Task<ApiResponse<Unit>> Handle(ProcedureCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //Validate request
                if (request.ProcedureId < 1)
                    return ApiResponse<Unit>.Fail(new Exception("Invalid ProcedureId"));


                var procedure = _context.Procedures.FirstOrDefault(p => p.ProcedureId == request.ProcedureId);


                if (procedure != null)
                {
                    procedure = new Procedure() { ProcedureTitle = request.ProcedureTitle };
                    _procedureRepo.Add(procedure);

                }
                else
                {
                    procedure.ProcedureTitle = request.ProcedureTitle;
                    _procedureRepo.Update(procedure);

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
