using System.Collections.Generic;

namespace BudgetOnline.Web.Areas.Admin.Models
{
    public class UserListViewModel
    {
        public IEnumerable<UserListItemsViewModel> Items { get; set; }
        public UserListSearchViewModel Search { get; set; }
    }
}