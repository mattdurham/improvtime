using System.Collections.Generic;

namespace ImprovTime
{
    public class Attr
    {
        public AttrKey Key { get; set; }
        public List<Datum> Datums { get; set; } = new List<Datum>();
    }
}