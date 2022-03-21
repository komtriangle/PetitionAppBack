using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PetitionApp.Core.Models
{
    public class Image
    {
        public int Id { get; set; }
        public int PetitionId { get; set; }
        [JsonIgnore]
        public Petition Petition { get; set; }

    }
}
