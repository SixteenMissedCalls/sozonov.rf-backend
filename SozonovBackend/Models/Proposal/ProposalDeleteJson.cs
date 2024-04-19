using System.ComponentModel.DataAnnotations;

namespace SozonovBackend.Models.Proposal
{
    public class ProposalDeleteJson
    {
        [Required(ErrorMessage = "Список id не может быть пустым")]
        [MinLength(1)]
        public List<int> Id { get; set; } = new List<int>();
    }
}
