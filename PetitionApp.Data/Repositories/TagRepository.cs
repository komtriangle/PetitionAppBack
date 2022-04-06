using Microsoft.EntityFrameworkCore;
using PetitionApp.Core.Models;
using PetitionApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetitionApp.Data.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
       
        public TagRepository(PetitionAppDbContext context) : base(context)
        {
        }

        private PetitionAppDbContext PetitionContext
        {
            get
            {
                return Context as PetitionAppDbContext;
            }
        }


        public async Task<Tag> CreateAsync(Tag entity)
        {
            await PetitionContext.Tags.AddAsync(entity);
            return entity;
        }

        public void Delete(Tag entity)
        {
            PetitionContext.Tags.Remove(entity);
        }

        public  IEnumerable<Tag> Find(Expression<Func<Tag, bool>> predicate)
        {
            return  PetitionContext.Tags.Where(predicate);
        }

        public  IEnumerable<Tag> GetTagsByPetitionId(int id)
        {
            return PetitionContext.PetitionTags.Where(pt => pt.PetitionId == id).Select(pt => pt.Tag);
        }

        public async Task<IEnumerable<Tag>> FindOrCreateTags(IList<Tag> tags)
        {
            for(int i = 0; i < tags.Count(); i++)
            {
                var tagInDb = PetitionContext.Tags.FirstOrDefault(t => t.Name == tags[i].Name);
                if(tagInDb == null)
                {
                    await PetitionContext.Tags.AddAsync(tags[i]);
                    PetitionContext.SaveChanges();
                }
                else
                {
                    tags[i].Id = tagInDb.Id;
                }
            }
            return tags;
        }

        public async Task CreatePetitionTags(int petitionId, IEnumerable<Tag> tags)
        {
            var petitionTags = tags.Select(t => new PetitionTags(petitionId, t.Id));
            await PetitionContext.PetitionTags.AddRangeAsync(petitionTags);
        }

    }
}
