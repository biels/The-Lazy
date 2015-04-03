using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLazyClientMVVM.Entities;

namespace TheLazyClientMVVM.Cache
{
    public class EducationCenterCache
    {
        private bool filled = false; //S'ha omplert la llista? Sinó el resultat podria ser incomplet
        private List<EducationCenterEntity> education_centers { get; set; }
        public EducationCenterCache()
        {
            education_centers = new List<EducationCenterEntity>();
        }
        public EducationCenterEntity getEducationCenter(int id, bool direct = false)
        {
            Predicate<EducationCenterEntity> match = new Predicate<EducationCenterEntity>(e => id == e.id);
            EducationCenterEntity cached = education_centers.FirstOrDefault(e => match(e) && e.isValid());
            if (cached != null && !direct) 
            {
                Com.main.cached_query_count++;
                return cached;
            }
            else
            {
                EducationCenterEntity tocache = DbClient.DbUserClient.getEducationCenterInfo(id);
                education_centers.RemoveAll(match);
                education_centers.Add(tocache);
                return tocache;
            }
        }
        public List<EducationCenterEntity> getEducationCenterFullList(bool direct = false) //Cal inicialitzar
        {
            bool allValid = education_centers.TrueForAll(e => e.isValid());
            if (allValid && !direct && filled)
            {
                Com.main.cached_query_count++;
            }
            else
            {
                fill();
            }
            return education_centers;
        }
        public void fill()
        {
            education_centers = DbClient.DbGroupEditor.getEducationCenterList();
            filled = true;
        }
    }
}
