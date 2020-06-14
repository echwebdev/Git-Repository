using DAL.IRepositories;
using DAL.SQL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DAL.Repositories
{
    public class MedicalProfileRepository : IMedicalProfileRepository
    {
        private NurseNovaEntities _ctx;
        
        public MedicalProfileRepository()
        {
            _ctx = new NurseNovaEntities();
        }

        #region CRUD
        public long CreateMedicalProfile(MedicalProfile entry)
        {
            try
            {
                _ctx.MedicalProfiles.Add(entry);
                _ctx.SaveChanges();
                return entry.Id;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MedicalProfile GetMedicalProfileById(long id)
        {
            try
            {
                MedicalProfile result = _ctx.MedicalProfiles.Find(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IQueryable<MedicalProfile> GetMedicalProfileByUserId(string userId)
        {
            try
            {
                var result = _ctx.MedicalProfiles.Where(a => a.UserID == userId);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IQueryable<MedicalProfile> GetAllMedicalProfile()
        {
            try
            {
                var result = _ctx.MedicalProfiles;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public long UpdateMedicalProfile(MedicalProfile entity)
        {
            try
            {
                entity.DateUpdated = DateTime.UtcNow;
                _ctx.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                _ctx.SaveChanges();

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public long DeleteMedicalProfile(long id)
        {
            try
            {
                MedicalProfile entry = _ctx.MedicalProfiles.Find(id);
                entry.DateUpdated = DateTime.UtcNow;
                _ctx.Entry(entry).State = System.Data.Entity.EntityState.Modified;
                _ctx.SaveChanges();

                return id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}