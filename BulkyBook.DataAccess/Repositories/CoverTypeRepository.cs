using BulkyBook.DataAccess.DBContext;
using BulkyBook.DataAccess.Interfaces;
using BulkyBook.Models.CoverTypes;
using BulkyBook.Models.DatabaseModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repositories
{
    public class CoverTypeRepository : ICoverTypeRepository
    {
        private readonly BulkyBookDbContext _db;

        public CoverTypeRepository(BulkyBookDbContext db)
        {
            _db = db;
        }

        public async Task<CoverTypeDto> CreateCoverType(CoverTypeDto model)
        {
            var newCoverType = new CoverType()
            {
                Name = model.Name,
            };

            await _db.CoverTypes.AddAsync(newCoverType);
            await _db.SaveChangesAsync();   
            return model;
        }

        public async Task Delete(int coverTypeId)
        {
            var coverType = await _db.CoverTypes.FindAsync(coverTypeId);
            _db.CoverTypes.Remove(coverType);

            await _db.SaveChangesAsync();
        }

        public IEnumerable<CoverType> GetAll()
        {
            var coverTypes = _db.CoverTypes.ToList();

            return coverTypes;
        }

        public async Task<CoverType> GetById(int? coverTypeId)
        {
            var coverType = await _db.CoverTypes.FindAsync(coverTypeId);

            return coverType;
        }

        public void Update(CoverType model)
        {
            _db.CoverTypes.Update(model);
            _db.SaveChanges();
        }
    }
}
