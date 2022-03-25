using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetitionApp.API.DTO
{
    public class CreatePetitionDTO
    {
        public string Title { get; set; }

        public string Text { get; set; }

        /// <summary>
        /// Цель (голоса)
        /// </summary>
        public int Goal { get; set; }
        public List<string> Tags { get; set; }

    }
}
