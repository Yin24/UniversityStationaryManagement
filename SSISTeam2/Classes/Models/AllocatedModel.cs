using System;
using System.Collections.Generic;

namespace SSISTeam2.Classes.Models
{
    public class AllocatedModel : RecordModel
    {
        public AllocatedModel()
        {
        }
        public AllocatedModel(UserModel userModel, DateTime date, Department department, Dictionary<ItemModel, int> items) : base(userModel, date, department, items)
        {
        }
        public AllocatedModel(Request efRequest, Dictionary<ItemModel, int> items) : base(efRequest, items)
        {
        }


        public override int RequestId
        {
            get
            {
                return base.RequestId;
            }
        }
    }
}