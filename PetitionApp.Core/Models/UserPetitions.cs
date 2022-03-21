using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetitionApp.Core.Models
{
    /// <summary>
    /// Модель, показывающая за какие петиции
    /// проголосовал пользователь
    /// </summary>
    public class UserPetitions
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Пользователя
        /// </summary>
        public virtual  User User { get; set; }
        /// <summary>
        /// Идентификатор петиции
        /// </summary>
        public int PetitionId { get; set; }

        /// <summary>
        /// Петиция
        /// </summary>
        public virtual  Petition Petition { get; set; }
    }
}
