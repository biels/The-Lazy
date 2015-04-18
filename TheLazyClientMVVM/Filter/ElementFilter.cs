using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLazyClientMVVM.Entities;

namespace TheLazyClientMVVM.Filter
{
    public class ElementFilter
    {               
        //Filter criteria        
        public string search_text { get; set; }
        public string username { get; set; } //Ignorat si == null
        public AcademicLevelEntity academic_level { get; set; } //Nomès es té en compte si subject == null
        public SubjectEntity subject { get; set; } //Més prioritat que academic_level
        public ElementOrderCriteriaEnum order_criteria { get; set; }
        public DraftShowCriteria draft_show_criteria { get; set; } //Mostra els esborranys

        //Info pool
        public List<AcademicLevelEntity> _AcademicLevels { get; set; }
        public List<SubjectEntity> _Subjects { get; set; }

        //Results
        List<int> id_list = new List<int>();
        List<ElementEntity> current_element_list = new List<ElementEntity>();
        bool busy = false;

        //Events
        public event RequestStartedEventHandler RequestStarted;
        public delegate void RequestStartedEventHandler();
        public event RequestDefinedEventHandler RequestDefined;
        public delegate void RequestDefinedEventHandler(List<int> id_list);
        public event ElementRecievedEventHandler ElementRecieved;
        public delegate void ElementRecievedEventHandler(ElementEntity e, int progress, int total);
        public event RequestCompleteEventHandler RequestComplete;
        public delegate void RequestCompleteEventHandler(List<ElementEntity> current_element_list);
        public void init()
        {
            draft_show_criteria = DraftShowCriteria.All;
            _AcademicLevels = Com.main.cache.academic_level_cache.getAcademicLevelFullList();
        }
        public void updateSubjectList(AcademicLevelEntity academic_level)
        {
            if (academic_level == null) { return; }
            _Subjects = Com.main.cache.subject_cache.getSubjectFullList(academic_level.id);
        }
        public async Task getFilteredElementsAsync()
        {
            if (busy) return; busy = true;
            RequestStarted();
            id_list = await getFilteredElementIDs();
            RequestDefined(id_list);
            current_element_list.Clear();
            int i = 0;
            int count = id_list.Count();
            foreach (int id in id_list)
            {
                ElementEntity e = await Task.Run(() => Com.main.cache.element_cache.getElement(id));
                current_element_list.Add(e);
                i++;
                ElementRecieved(e, i, count);                
            }
            RequestComplete(current_element_list);
            busy = false;
        }
        public async Task<List<int>> getFilteredElementIDs()
        {
            return await Task.Run(() => DbClient.DbElementClient.getFilteredElementIDList(getWhereClause(), getOrderByClause()));
        }
        public string getWhereClause()
        {
            string where_clause = "WHERE";
            if (academic_level != null && subject == null) { where_clause += String.Format(" academic_level_id={0}", academic_level.id); }
            if (subject != null) {where_clause += String.Format(" subject_id={0}", subject.id); }
            if (draft_show_criteria != DraftShowCriteria.All) { where_clause += String.Format("{0} (draft={1}{2})", (where_clause == "WHERE" ? "" : " AND"), 0, (draft_show_criteria == DraftShowCriteria.LocalUserOnly ? String.Format(" OR user_id={0}", Com.main.localUser.id) : "")); }            
            if (search_text != "" && search_text != null)
            {
                List<String> keywords = search_text.Split(new Char[] { ' ', ',', '.', ':', '\'', '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                string[] meaningless_keywords = new string[] {"de", "del", "dels", "els", "les", "la", "d", "l", "el", "la", "per", "pel" };
                if (keywords.Count > 1)keywords.RemoveAll((k) => meaningless_keywords.Contains(k));
                where_clause += (where_clause == "WHERE" ? "" : " AND");
                where_clause += " (";
                keywords.ForEach((e) => where_clause += String.Format(" {1}name LIKE '%{0}%'", e, (keywords.IndexOf(e) == 0 ? "" : "OR ")));
                keywords.ForEach((e) => where_clause += String.Format(" {1}description LIKE '%{0}%'", e, "OR "));
                where_clause += ")";
            }
            if (where_clause == "WHERE") where_clause = "";
            return where_clause;
        }
        public string getOrderByClause()
        {
            //if (order_criteria == null) return "";
            switch (order_criteria)
            {
                case ElementOrderCriteriaEnum.MostRecent:
                    return String.Format("ORDER BY create_time DESC");
                case ElementOrderCriteriaEnum.BestAvgRating:
                    return String.Format("ORDER BY avg_rating DESC");
                case ElementOrderCriteriaEnum.MostBought:
                    return String.Format("ORDER BY purchase_amount DESC");
                case ElementOrderCriteriaEnum.MostExpensive:
                    return String.Format("ORDER BY price DESC");
                case ElementOrderCriteriaEnum.Cheapest:
                    return String.Format("ORDER BY price ASC");
            }
            return "";
        }
        public enum ElementOrderCriteriaEnum
        {
            [Description("Més recents")]
            MostRecent,
            [Description("Més comprats")]
            MostBought,
            [Description("Més ben valorats")]
            BestAvgRating,
            [Description("Més cars")]
            MostExpensive,
            [Description("Més barats")]
            Cheapest
        };
        public enum DraftShowCriteria
        {
            None,
            LocalUserOnly,
            All
        };
    }
}
