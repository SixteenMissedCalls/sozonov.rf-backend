using Domain.entities;
using MailKit;
using Microsoft.EntityFrameworkCore;
using SozonovBackend.Exceptions.ProposalsEx;
using SozonovBackend.Models.Proposal;
using System;

namespace SozonovBackend.Repository
{
    public class RepositoryProposal : IRepositoryProposal
    {
        private readonly DatabaseContext _db;
        private ILogger _logger;

        public RepositoryProposal(DatabaseContext db, ILogger<MailService> logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task Add(Proposal entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(IEnumerable<int> id)
        {
            var entitiesToDelete = _db.Proposals.Where(e => id.Contains(e.Id));

            _db.Proposals.RemoveRange(entitiesToDelete);
            await _db.SaveChangesAsync();
        }

        public async Task<Proposal> Get(int id) => await _db.Proposals.FindAsync(id) ?? 
            throw new DbResultNullException();

        public async Task<IEnumerable<Proposal>> GetAll()
        {
            return await _db.Proposals.ToListAsync() ??
                throw new DbResultNullException();
        }

        public async Task Update(Proposal entity)
        {
            var existingProposal = await _db.Proposals.FindAsync(entity.Id) ??
                throw new DbResultNullException();

            existingProposal = entity;
            await _db.SaveChangesAsync();
        }
    }
}
