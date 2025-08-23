using DataAccessLayer.UnitOfWork;
using Entities;
using System.Diagnostics.Metrics;

namespace BusinessLogicalLayer.Apis
{
    public class ApiConsume
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApiConsume(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
