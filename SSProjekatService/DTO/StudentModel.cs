using SSProjekatService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSProjekatService.DTO
{
    public class StudentModel : IAggregateRoot
    {
        public long BrojIndeksa { get; set; }
        public string ImeStudenta { get; set; }
        public string PrezimeStudenta { get; set; }
        public float BrojBodovaPredmet { get; set; }
        public float Prosjek { get; set; }
        public int SifraPredmeta { get; set; }
    }
}
