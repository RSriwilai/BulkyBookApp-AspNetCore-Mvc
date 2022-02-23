using BulkyBook.DataAccess.DatabaseModel;
using BulkyBook.Models.CoverTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Interfaces
{
    public interface ICoverTypeRepository
    {
        Task<CoverTypeDto> CreateCoverType(CoverTypeDto model);
        Task<List<CoverType>> GetAll();
        Task<CoverType> GetById(int? coverTypeId);
        Task Delete(int coverTypeId);
        void Update(CoverType model);
    }
}
