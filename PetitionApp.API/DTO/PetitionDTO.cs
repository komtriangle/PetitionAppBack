using PetitionApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetitionApp.API.DTO
{
    public class PetitionDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public UserDTO? Author { get; set; }

        public string Text { get; set; }

        /// <summary>
        /// Цель (голоса)
        /// </summary>
        public int Goal { get; set; }
        public List<Tag> Tags { get; set; }

        public int CountVoices { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
