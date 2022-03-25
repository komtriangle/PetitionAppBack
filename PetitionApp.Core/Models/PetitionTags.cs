using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetitionApp.Core.Models
{

    /// <summary>
    /// Модель, связывающая петиции и теги
    /// </summary>
    public class PetitionTags
    {
        /// <summary>
        /// Идентификатор петиции
        /// </summary>
        public int PetitionId { get; set; }

        /// <summary>
        /// Петиция
        /// </summary>
        public virtual Petition Petition { get; set; }

        /// <summary>
        /// Идентификатор тега
        /// </summary>
        public int TagId { get; set; }

        /// <summary>
        /// Тег
        /// </summary>
        public virtual Tag Tag { get; set; }

        public PetitionTags() { }

        public PetitionTags(int petitionId, int tagId)
        {
            PetitionId = petitionId;
            TagId = tagId;
        }
    }
}
