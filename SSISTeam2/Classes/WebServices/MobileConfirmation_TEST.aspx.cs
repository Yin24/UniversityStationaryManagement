using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Classes.WebServices
{
    public partial class MobileConfirmation_TEST : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPushMe_Click(object sender, EventArgs e)
        {
            string user = "leo";

            Dictionary<string, int> items = new Dictionary<string, int>();

            //items.Add("P042", 9);
            //items.Add("P030", 15);
            items.Add("P032", 4);

            MobileConfirmation.ConfirmRetrievalFromWarehouse(user, items);
        }
    }
}