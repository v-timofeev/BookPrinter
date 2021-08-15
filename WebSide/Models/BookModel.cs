using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSide.Models
{
    public class BookModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FileName {  get; set; }
        public string OriginBookPath { get; set; }
        public string ConvertedBookPath { get; set; }
        public int BlankPageIndex { get; set; } = 0;
        public int SheetsOfNotebook { get; set; } = 4;

    }
}
