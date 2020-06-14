using DAL.IRepositories;
using DAL.Repositories;
using DAL.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private NurseNovaEntities _nnContext;
        private IMedicalProfileRepository _medicalProfileRepository;

        public UnitOfWork(NurseNovaEntities _nnEntities)
        {
            _nnContext = _nnEntities;
        }

        public IMedicalProfileRepository MedicalProfileRepository
        {
            get { return _medicalProfileRepository ?? (_medicalProfileRepository = new MedicalProfileRepository()); }
        }
    }
}
