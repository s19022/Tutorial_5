using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutorial_5.DTOs.Requests;

namespace Tutorial_5.Services
{
    public interface IStudentServiceDb
    {
        void EnrollStudent(EnrollStudentRequest req);
        void PromoteStudents(int semester, string studies);
    }
}
