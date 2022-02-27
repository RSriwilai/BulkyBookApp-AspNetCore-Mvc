using BulkyBook.Models.CoverTypes;
using BulkyBook.Models.DatabaseModel;

namespace BulkyBook.DataAccess.Interfaces
{
    public interface ICoverTypeRepository
    {
        Task<CoverTypeDto> CreateCoverType(CoverTypeDto model);
        IEnumerable<CoverType> GetAll();
        Task<CoverType> GetById(int? coverTypeId);
        Task Delete(int coverTypeId);
        void Update(CoverType model);
    }
}
