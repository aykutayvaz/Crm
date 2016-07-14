using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Crm.Models
{
    public class ServiceFunctions
    {
        Repository<dt_Service> db;

        Repository<lu_Region> lu_RegionRepository;

        Repository<dt_Project> dt_ProjectRepository;

        JsonModel jsonmodel;

        public JsonModel Jsonmodel
        {
            get { return jsonmodel; }
            set { jsonmodel = value; }
        }

        public ServiceFunctions()
        {
             db =  new Repository<dt_Service>();

             jsonmodel = new JsonModel();
        }


        //public List<dt_Service> ServisleriGetir()
        //{
            //var servisler = from servis in db.Listele()
            //                select new
            //                {
            //                    ServisID = servis.ServiceId,
            //                    Bölgeİsmi = from b in lu_RegionRepository.Listele() where b.RegionId==servis.ServiceRegionId select b.RegionName,
            //                    Projeİsmi = from p in  dt_ProjectRepository.Listele() where p.ProjectId==servis.ProjectId select p.ProjectName,      
            //                };
            //var projectIds = from b in db.tx_ProjectPeople
            //                 where b.PersonId == UserId
            //                 select b.ProjectId;
            //var projects = from b in db.dt_Project
            //               where projectIds.Contains(b.ProjectId)
            //               select b;
            //return View(projects.ToList());
            //return servisler.ToList<dt_Service>();
        //}
    }
}