using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetitionApp.Core.Models
{
    public class Petition
    {
        public int Id { get; set; }


        /// <summary>
        /// Идентификатор автора статьи
        /// </summary>
        public Guid AuthorId { get; set; }
        /// <summary>
        /// Автор петиции
        /// </summary>
        public virtual User Author { get; set; }

        /// <summary>
        /// Заголовок петиции
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// Текст петиции
        /// </summary>
        public string Text { get; set; }


        /// <summary>
        /// Количетсво проголосовавших
        /// </summary>
        public int CountVoices { get; set; }

        /// <summary>
        /// Цель (количество подписавшихся)
        /// </summary>
        public int Goal { get; set; }

        /// <summary>
        /// Изображение, относящиеся к петиции
        /// </summary>
        public virtual IEnumerable<Image> Images { get; set; }

        /// <summary>
        /// Дата создания петиции
        /// </summary>
        public DateOnly CreationDate { get; set; }


        /// <summary>
        /// Теги
        /// </summary>
        public virtual ICollection<PetitionTags> PetitionTags { get; set; }

        /// <summary>
        /// Пользователи, проголосовавший за эту петицию
        /// </summary>
        public virtual ICollection<UserPetitions> UserPetitions { get; set; }
    }
}
