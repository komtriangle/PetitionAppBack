using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetitionApp.Core.Models
{
    public class Tag
    {
        public int Id { get; set; }

        /// <summary>
        /// Название тега
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Петиции, у которых есть этот тег
        /// </summary>
        public virtual IEnumerable<PetitionTags> PetitionTags { get; set; }
    }
}
