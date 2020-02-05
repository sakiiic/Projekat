using SSProjekatService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSProjekatService.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public StudentModel Add(StudentModel newEntity)
        {
            throw new NotImplementedException();
        }

        public void Remove(StudentModel entity)
        {
            throw new NotImplementedException();
        }

        public void Update(StudentModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
