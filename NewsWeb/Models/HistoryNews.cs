using System;

namespace NewsWeb.Models
{
    public class HistoryNews
    {
        public int id { get; set; }
        public int UserID { get; set; }
        public String Link { get; set; }
        public DateTime Time { get; set; }

        // Khóa ngoại cho UserID
        public virtual Users User { get; set; }

        // Khóa ngoại cho NewsID
        
    }
}
