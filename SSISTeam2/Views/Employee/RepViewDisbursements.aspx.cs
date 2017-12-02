using SSISTeam2.Classes.EFFacades;
using SSISTeam2.Classes.Models;
using SSISTeam2.Views.StoreClerk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.Employee
{
    public partial class RepViewDisbursements : System.Web.UI.Page
    {
        private const string SESSION_GROUPED_DATES = "RepViewDisbursements_GroupedDates";
        private const string SESSION_GROUPED_BY_NAME = "RepViewDisbursements_GroupedByName";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

            using (SSISEntities context = new SSISEntities())
            {
                string currentUser = User.Identity.Name;
                string deptCode = (new UserModel(currentUser)).Department.dept_code;

                var disbursements = FacadeFactory.getDisbursementService(context).getAllThatWereDisbursed().fromDepartment(deptCode);

                if (disbursements.Count == 0)
                {
                    return;
                }

                IOrderedEnumerable<IGrouping<DateTime, DisbursementModel>> groupedByDate = disbursements.GroupBy(k => k.Date.Date).OrderByDescending(o => o.Key);

                // For recents
                var recents = groupedByDate.ToDictionary(k => k.Key.ToShortDateString(), v => v.Key.ToShortDateString());

                lboxRecents.DataSource = recents;
                lboxRecents.DataValueField = "Key";
                lboxRecents.DataTextField = "Value";
                lboxRecents.DataBind();

                Session[SESSION_GROUPED_DATES] = groupedByDate;

                IOrderedEnumerable<IGrouping<string, DisbursementModel>> datesGroupedByName = groupedByDate.First().GroupBy(k => k.Username).OrderBy(o => o.Key);

                Session[SESSION_GROUPED_BY_NAME] = datesGroupedByName;

                var names = datesGroupedByName.ToDictionary(k => k.Key, v => v.First().UserModel.Fullname);

                //lblDebug.Text = names.Count.ToString();

                lboxRequests.DataSource = names;
                lboxRequests.DataValueField = "Key";
                lboxRequests.DataTextField = "Value";
                lboxRequests.DataBind();

                var listOfRequests = datesGroupedByName.First();

                ItemViewModelDictionary items = new ItemViewModelDictionary();

                foreach (var request in listOfRequests)
                {
                    foreach (var item in request.Items)
                    {
                        items.AddOrIncrease(item.Key, item.Value);
                    }
                }   

                var displayItems = items.Select(s => new ItemViewModelKVP(s));

                lboxItems.DataSource = displayItems;
                lboxItems.DataValueField = "ItemCode";
                lboxItems.DataTextField = "ItemAndQty";
                lboxItems.DataBind();
            }
        }

        private void _refreshNames(int idx)
        {
            IOrderedEnumerable<IGrouping<DateTime, DisbursementModel>> groupedByDate = Session[SESSION_GROUPED_DATES] as IOrderedEnumerable<IGrouping<DateTime, DisbursementModel>>;

            var datesGroupedByName = groupedByDate.ElementAt(idx).GroupBy(k => k.Username).OrderBy(o => o.Key);

            var names = datesGroupedByName.ToDictionary(k => k.Key, v => v.First().UserModel.Fullname);

            lboxRequests.DataSource = names;
            lboxRequests.DataValueField = "Key";
            lboxRequests.DataTextField = "Value";
            lboxRequests.DataBind();

            Session[SESSION_GROUPED_BY_NAME] = datesGroupedByName;

            string username = datesGroupedByName.First().Key;

            _refreshItems(username);
        }

        private void _refreshItems(string username)
        {
            IOrderedEnumerable<IGrouping<string, DisbursementModel>> list = Session[SESSION_GROUPED_BY_NAME] as IOrderedEnumerable<IGrouping<string, DisbursementModel>>;

            var listOfRequests = list.Where(w => w.Key == username).First();

            ItemViewModelDictionary items = new ItemViewModelDictionary();

            foreach (var request in listOfRequests)
            {
                foreach (var item in request.Items)
                {
                    items.AddOrIncrease(item.Key, item.Value);
                }
            }

            var displayItems = items.Select(s => new ItemViewModelKVP(s));

            lboxItems.DataSource = displayItems;
            lboxItems.DataValueField = "ItemCode";
            lboxItems.DataTextField = "ItemAndQty";
            lboxItems.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void lboxRecents_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lbox = sender as ListBox;

            //string selectedDTStr = lbox.SelectedValue.ToString();
            //long selectedTicks = Convert.ToInt64(selectedTicksStr);


            //DateTime selectedDT = Convert.ToDateTime(selectedDTStr);

            //_refreshNames(selectedDT);

            int selIdx = lbox.SelectedIndex;
            //lblDebug.Text = selIdx.ToString();

            _refreshNames(selIdx);
        }

        protected void lboxRequests_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lbox = sender as ListBox;

            string selectedName = lbox.SelectedValue;

            _refreshItems(selectedName);
        }
    }

    class ItemViewModelKVP
    {
        private KeyValuePair<ItemModel, int> kvp;

        public ItemViewModelKVP(KeyValuePair<ItemModel, int> kvp)
        {
            this.kvp = kvp;
        }

        public string ItemAndQty
        {
            get
            {
                return kvp.Key.Description + " - " + kvp.Value;
            }
        }

        public string ItemCode
        {
            get
            {
                return kvp.Key.ItemCode;
            }
        }
    }


    public class ItemViewModelDictionary : Dictionary<ItemModel, int>
    {
        public void AddOrIncrease(ItemModel key, int value)
        {
            if (this.ContainsKey(key))
            {
                // Increase
                int orig = this[key];
                int newVal = orig + value;
                this[key] = newVal;
            }
            else
            {
                // Just add
                this.Add(key, value);
            }
        }
    }
}