using System;
<<<<<<< HEAD
using Store;
using Store.DataModel;
=======
>>>>>>> c572094160eb0c12ca7d68e55f6d9d3d6e2ea0e7

namespace Store.WebApp.Models
{
    public class IndexViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
<<<<<<< HEAD

        private Session session;

        public IndexViewModel(Session session)
        {
            this.session = session;
        }
=======
>>>>>>> c572094160eb0c12ca7d68e55f6d9d3d6e2ea0e7
    }
}
