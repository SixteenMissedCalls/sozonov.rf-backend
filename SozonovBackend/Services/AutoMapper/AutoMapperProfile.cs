using AutoMapper;
using Domain.entities;
using SozonovBackend.Models.Proposal;

namespace SozonovBackend.Services.AutoMapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Proposal, ProposalRequest>().ReverseMap();
        }
    }
}
