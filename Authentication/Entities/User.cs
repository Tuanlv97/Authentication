using System;

namespace BookStore.Data.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string PassWord { get; set; }
        public string UserName { get; set; }
        public string FavoriteColor { get; set; }
        public string Role { get; set; }
        public string GoogleId { get; set; }

    }
}
