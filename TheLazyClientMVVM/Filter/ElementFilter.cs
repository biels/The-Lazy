﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLazyClientMVVM.Entities;

namespace TheLazyClientMVVM.Filter
{
    public class ElementFilter
    {               
        public string search_text { get; set; } //Falta
        public SubjectEntity subject { get; set; }
        public ElementOrderCriteriaEnum order_criteria { get; set; }
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
            _AcademicLevels = Com.main.cache.academic_level_cache.getAcademicLevelFullList();
        }
        public void updateSubjectList(AcademicLevelEntity academic_level)
        {
            if (academic_level == null) { return; }
            _Subjects = Com.main.cache.subject_cache.getSubjectFullList(academic_level.id);
        }
        public  List<ElementEntity> getFilteredElements()
        {
            
            string sql = "";
            if (subject != null) {sql = String.Format("WHERE subject_id={0}", subject.id); }

            return DbClient.DbElementClient.getFilteredElementList(sql);
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

            string where_clause = "";
            if (subject != null) {where_clause = String.Format("WHERE subject_id={0}", subject.id); }

            return await Task.Run(() => DbClient.DbElementClient.getFilteredElementIDList(where_clause));
        }

    }
}