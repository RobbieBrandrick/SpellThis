using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spellthis.Models
{
    public class Word
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime AddDate { get; set; }
        public string AudioFileLocation { get; set; }
        public string AudioFileWebUri { get; set; }

    }
}
