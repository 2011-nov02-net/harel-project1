using System;
using Store;
using Store.DataModel;

namespace Store.WebApp.Models
{
    public class SessionViewModel
    {
        public Session Session { get; set; }
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}