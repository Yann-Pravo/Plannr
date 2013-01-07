using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace Plannr.DAL
{
    public class ResponsableUERepository : IResponsableUe
    {
         private PlannrContext context;

        public ResponsableUERepository(PlannrContext context)
        {
            this.context = context;
        }

        public ResponsableUE Get(int id) {
                
    
            return this.context.ResponsablesUE.Find(id);
        }

        public ResponsableUE GetEns(int id)
        {
            Enseignant ens = this.context.Enseignants.Find(id);
            ResponsableUE resp = new ResponsableUE();
            //resp.UserId=ens.UserId;
            resp.UserName=ens.UserName;
             resp.Name=ens.Name;
             resp.FirstName=ens.FirstName;
             resp.Tel=ens.Tel;
            resp.ResponsableDepuis= DateTime.Parse("10/01/2009");
            resp.Enseignements = ens.Enseignements;
            
            this.context.Enseignants.Remove(ens);
            
            //WebSecurity.CreateAccount(resp.UserName, resp.UserName);

             return resp;
        }

        public Enseignant GetResp(int id)
        {
            ResponsableUE resp = this.context.ResponsablesUE.Find(id);
            Enseignant ens = new Enseignant();
            ens.UserId = resp.UserId;
            ens.UserName = resp.UserName;
            ens.Name = resp.Name;
            ens.FirstName = resp.FirstName;
            ens.Tel = resp.Tel;
            ens.Enseignements = resp.Enseignements;
            this.context.ResponsablesUE.Remove(resp);

            return ens;
        }

        public void Insert(Models.ResponsableUE e)
        {
            this.context.ResponsablesUE.Add(e);
        }

        public void Edit(ResponsableUE e)
        {
           this.context.Entry(e).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var e = this.context.ResponsablesUE.Find(id);
            this.context.ResponsablesUE.Remove(e);
        }

        public void Entry(ResponsableUE e)
        {
            this.context.Entry(e).State = System.Data.EntityState.Modified;
        }

        public IEnumerable<ResponsableUE> GetAll()
        {
            return this.context.ResponsablesUE.ToList();
        }
        public IEnumerable<ResponsableUE> GetList()
        {

            return this.context.ResponsablesUE.ToList();
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

        // DIspose
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Count()
        {
            return context.ResponsablesUE.Count();
        }
    
    }
}