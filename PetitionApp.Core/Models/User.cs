using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetitionApp.Core.Models
{
    public class User :IdentityUser<Guid>
    {
        /// <summary>
        /// Список петиций, за которые проголосовал пользователь
        /// </summary>
        public virtual IEnumerable<UserPetitions> UserPetitions { get; set; }
    }
}
