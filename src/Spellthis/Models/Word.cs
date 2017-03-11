using System;
using System.ComponentModel.DataAnnotations;
namespace Spellthis.Models
{
    public class Word
    {

        public int Id { get; set; }

        [RegularExpression(@"^([A-Z]|[a-z])+", ErrorMessage = "Please enter a single word"), Required, StringLength(60)]
        public string Name { get; set; }

        [Display(Name = "Add Date")]
        public DateTime AddDate { get; set; }
        public string AudioFileLocation { get; set; }
        public string AudioFileWebUri { get; set; }

    }
}
