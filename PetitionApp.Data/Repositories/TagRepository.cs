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

        public async Task<IEnumerable<Tag>> FindOrCreateTags(IEnumerable<Tag> tags)
        {
            foreach (var tag in tags)
            {
                var tagInDb = PetitionContext.Tags.FirstOrDefault(t => t.Name == tag.Name);
                if(tagInDb == null)
                {
                    await PetitionContext.Tags.AddAsync(tag);
                    PetitionContext.SaveChanges();
                }
                else
                {
                    tag.Id = tagInDb.Id;
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
