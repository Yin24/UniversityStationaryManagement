using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.Classes.Models
{
    public class RetrievalModel : AllocatedModel
    {
        public RetrievalModel()
        {

        }
        public RetrievalModel(Request efRequest, Dictionary<ItemModel, int> items) : base(efRequest, items)
        {
        }
    }
}
