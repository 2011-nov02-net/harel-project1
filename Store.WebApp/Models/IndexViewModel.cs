using System;
using Store;
using Store.DataModel;

namespace Store.WebApp.Models
{
    public class IndexViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private Session session;

        public IndexViewModel(Session session)
        {
            this.session = session;
        }
    }
}
