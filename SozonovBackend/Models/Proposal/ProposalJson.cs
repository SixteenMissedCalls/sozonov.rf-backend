using System.ComponentModel.DataAnnotations;

namespace SozonovBackend.Models.Proposal
{
    public class ProposalJson
    {
        [Required(ErrorMessage = "Имя должно быть заполнено")]
        [MaxLength(255)]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Почта должна быть заполнена")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", 
            ErrorMessage = "Поле не соответсвует формату почты")]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Имя должно быть заполнено")]
        [MaxLength(2000)]
        public string Text { get; set; } = string.Empty;
    }
}
