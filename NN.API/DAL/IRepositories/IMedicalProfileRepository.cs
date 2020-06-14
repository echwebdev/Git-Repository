using DAL.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IMedicalProfileRepository
    {
        long CreateMedicalProfile(MedicalProfile entry);
        MedicalProfile GetMedicalProfileById(long id);
        IQueryable<MedicalProfile> GetAllMedicalProfile();
        long UpdateMedicalProfile(MedicalProfile entity);
        long DeleteMedicalProfile(long id);
    }
}
