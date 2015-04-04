using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.Entities
{
    public class LocalElementDataEntity : Entity
    {
        public int id { get; set; }
        public bool favourite { get; set; }
        public int rating { get; set; }
        public ElementPurchaseEntity purchase { get; set; }

        public bool isUnlocked()
        {
            return purchase != null;
        }
        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(favourite);
            sb.Append(rating);

            return sb.ToString().GetHashCode();
        }
    }
}
