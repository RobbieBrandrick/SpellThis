using Spellthis.Models.Account;

namespace Spellthis.Models
{
    public class UserWord
    {

        public int Id { get; set; }
        public string UserId { get; set; }
        public int WordId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Word Word { get; set; }

    }
}
