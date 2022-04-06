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
    public class PetitionRepository : Repository<Petition>, IPetitionRepository
    {

        public PetitionRepository(PetitionAppDbContext context) : base(context) {
        }

        public PetitionAppDbContext PetitionContext
        {
            get
            {
                return Context as PetitionAppDbContext;
            }
        }
        public async Task<Image> AddImageToPetition(Petition petition, Image image)
        {
            image.PetitionId = petition.Id;
            await PetitionContext.Images.AddAsync(image);
            return image;
        }

        public Task<Image> AddImageToPetitionAsync(Petition petition, Image image)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Tag>> AddTagsToPetitionAsync(Petition petition, IEnumerable<Tag> tags)
        {
            List<PetitionTags> tagsList = new List<PetitionTags>();
            foreach(Tag tag in tags)
            {
                PetitionTags pt = new PetitionTags() { PetitionId = petition.Id, TagId = tag.Id };
                tagsList.Add(pt);
                
            }
            await PetitionContext.PetitionTags.AddRangeAsync(tagsList);
            return tags;
        }


        public async Task AddVoiceAsync(Petition petition, User user)
        {
            UserPetitions up = new UserPetitions() { PetitionId = petition.Id, UserId = user.Id };
            await PetitionContext.UserPetitions.AddAsync(up);
        }

        public async Task<Petition> CreateAsync(Petition entity)
        {
            await PetitionContext.Petitions.AddAsync(entity);
            return entity;

        }

        public void Delete(Petition entity)
        {
            PetitionContext.Petitions.Remove(entity);
        }

        public IEnumerable<Petition> Find(Expression<Func<Petition, bool>> predicate)
        {
            return PetitionContext.Petitions.Where(predicate);
        }

        public Petition GetById(int Id)
        {
            return PetitionContext.Petitions.SingleOrDefault(pet => pet.Id == Id);
        }

        public IEnumerable<Petition> GetPetitionsByAuthor(User user)
        {
            return PetitionContext.Petitions.Where(pet => pet.Author.Id == user.Id);
        }

        public IEnumerable<Petition> GetVoicesForUser(User user)
        {
            return PetitionContext.UserPetitions.Where(up => up.UserId == user.Id).Select(up => up.Petition);
        }

        public IEnumerable<Petition> GetTopPetitions(int count)
        {
            return PetitionContext.Petitions
                .Include(p => p.Author)
                .Include(p => p.PetitionTags)
                .ThenInclude(p => p.Tag)
                .OrderByDescending(p => p.CountVoices).Take(count);
        }

        public IEnumerable<Petition> GetPetitionsByTags(IEnumerable<Tag> tags)
        {
            //Список Id тегов
            var tagIds = tags.Select(tag => tag.Id).ToList();
            return PetitionContext.PetitionTags.Where(pt => tagIds.Contains(pt.TagId)).Select(pt => pt.Petition);
        }
    }
}
