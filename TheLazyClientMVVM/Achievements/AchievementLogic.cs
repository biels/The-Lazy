using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.Achievements
{
    public abstract class AchievementLogic
    {
        public abstract String getName();
        public abstract String getDescription();

        public abstract int getValue();
        public abstract int getMaxValue();
        public double getPercent()
        {
            return getValue() / getMaxValue() * 100;
        }
        public void check()
        {

        }
    }
}
