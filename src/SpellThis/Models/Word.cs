using System;

namespace SpellThis.Models
{
    public class Word
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? LastAttempted { get; set; }
        public DateTime? NextAttempt { get; set; }

    }
}
