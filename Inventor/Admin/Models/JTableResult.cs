using System.Collections;

namespace Admin.Models
{
    public class JTableResult
    {
        public string Result { get; set; }
        public string Message { get; set; }
        public IList Records { get; set; }
        public int TotalRecordCount { get; set; }
    }
}