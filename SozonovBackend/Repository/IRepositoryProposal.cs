using Domain.entities;
using SozonovBackend.Models.Proposal;

namespace SozonovBackend.Repository
{
    public interface IRepositoryProposal : IRepository<Proposal>
    {
        Task<IEnumerable<Proposal>> GetAll();
        Task<Proposal> Get(int id);
        Task Delete(IEnumerable<int> id);
    }
}
