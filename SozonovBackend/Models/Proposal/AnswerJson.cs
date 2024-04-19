using System.ComponentModel.DataAnnotations;

namespace SozonovBackend.Models.Proposal
{
    public class AnswerJson
    {
        [MaxLength(2000)]
        [Required(ErrorMessage = "Поле ответа не может быть пустым")]
        public string Answer {  get; set; } = string.Empty;
    }
}
