using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.Achievements
{
    public class AchievementManager
    {
        List<AchievementLogic> achievements = new List<AchievementLogic>();
        public void init()
        {
            registerAchievements();
        }
        void registerAchievements()
        {
            var q = from t in System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && !t.IsAbstract && typeof(AchievementLogic).IsAssignableFrom(t) && t.Namespace == "TheLazyClientMVVM.Achievements.Logic"
                    select t;
            q.ToList().ForEach(t => achievements.Add((AchievementLogic)Activator.CreateInstance(t)));
        }
    }
}
