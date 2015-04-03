using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLazyClientMVVM.Entities;

namespace TheLazyClientMVVM.Cache
{
    public class AcademicLevelCache
    {
        private bool filled = false; //S'ha omplert la llista? Sinó el resultat podria ser incomplet
        private List<AcademicLevelEntity> academic_levels { get; set; }
        public AcademicLevelCache()
        {
            academic_levels = new List<AcademicLevelEntity>();
        }
        public AcademicLevelEntity getAcademicLevel(int id, bool direct = false)
        {
            Predicate<AcademicLevelEntity> match = new Predicate<AcademicLevelEntity>(e => id == e.id);
            AcademicLevelEntity cached = academic_levels.FirstOrDefault(e => match(e) && e.isValid());
            if (cached != null && !direct) 
            {
                Com.main.cached_query_count++;
                return cached;
            }
            else
            {
                AcademicLevelEntity tocache = DbClient.DbUserClient.getAcademicLevelInfo(id);
                academic_levels.RemoveAll(match);
                academic_levels.Add(tocache);
                return tocache;
            }
        }
        public List<AcademicLevelEntity> getAcademicLevelFullList(bool direct = false) //Cal inicialitzar
        {         
            bool allValid = academic_levels.TrueForAll(e => e.isValid());
            if (allValid && !direct && filled)
            {
                Com.main.cached_query_count++;
            }
            else
            {
                fill();
            }           
            return academic_levels;
        }
        public void fill()
        {
            academic_levels = DbClient.DbGroupEditor.getAcademicLevelList();
            filled = true;
            System.Console.WriteLine("Filled!");
        }
    }
}
