using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Spellthis.Models
{
    public class Word
    {

        public int Id { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$"), Required, StringLength(60)]
        public string Name { get; set; }

        [Display(Name = "Add Date")]
        public DateTime AddDate { get; set; }
        public string AudioFileLocation { get; set; }
        public string AudioFileWebUri { get; set; }

    }
}
