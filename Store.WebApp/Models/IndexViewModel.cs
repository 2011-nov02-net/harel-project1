using System;

namespace Store.WebApp.Models
{
    public class IndexViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
