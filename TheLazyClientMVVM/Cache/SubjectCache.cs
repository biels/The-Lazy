using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLazyClientMVVM.Entities;

namespace TheLazyClientMVVM.Cache
{
    public class SubjectCache
    {
        //private bool filled = false; //S'ha omplert la llista? Sinó el resultat podria ser incomplet
        private List<int> filled_academic_level_ids; //S'ha omplert la llista per aquest determinat ID? Sinó el resultat podria ser incomplet
        private List<SubjectEntity> academic_levels { get; set; }
        public SubjectCache()
        {
            academic_levels = new List<SubjectEntity>();
            filled_academic_level_ids = new List<int>();
        }
        public SubjectEntity getSubject(int id, bool direct = false)
        {
            Predicate<SubjectEntity> match = new Predicate<SubjectEntity>(e => id == e.id);
            SubjectEntity cached = academic_levels.FirstOrDefault(e => match(e) && e.isValid());
            if (cached != null && !direct) 
            {
                Com.main.cached_query_count++;
                return cached;
            }
            else
            {
                SubjectEntity tocache = DbClient.DbSubjectEditorClient.getSubjectInfo(id);
                academic_levels.RemoveAll(match);
                academic_levels.Add(tocache);
                return tocache;
            }
        }
        public List<SubjectEntity> getSubjectFullList(int academic_level_id, bool direct = false) //Cal inicialitzar
        {
            List<SubjectEntity> filtered = academic_levels.Where(e => e.academic_level.id == academic_level_id).ToList<SubjectEntity>();
            Predicate<SubjectEntity> isvalid = new Predicate<SubjectEntity>(e => e.isValid());
            bool allValid = filtered.TrueForAll(isvalid);
            bool idfilled = filled_academic_level_ids.Contains(academic_level_id);
            if (allValid && !direct && idfilled)
            {
                Com.main.cached_query_count++;
                return filtered;
            }
            else
            {
                fill(academic_level_id);
                return (getSubjectFullList(academic_level_id));
            }           
            
        }
        public void fill(int academic_level_id)
        {
            academic_levels.RemoveAll(e => e.academic_level.id == academic_level_id);
            academic_levels.AddRange(DbClient.DbSubjectEditorClient.getSubjectList(academic_level_id));
            filled_academic_level_ids.Add(academic_level_id);
        }
        public void fill()
        {           
            throw new NotImplementedException();
            //filled = true;
        }
    }
}
