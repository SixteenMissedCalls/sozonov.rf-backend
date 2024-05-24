using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.entities
{
    public class Proposal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Количество символов должно быть больше 1")]
        [MaxLength(256, ErrorMessage = "Количество символов должно быть меньше 256")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Почта должна быть заполнена")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Поле не соответсвует формату почты")]
        [MaxLength(255, ErrorMessage = "Количество символов должно быть меньше 256")]
        public string Mail { get; set; } = string.Empty;
        [Required]
        [MinLength(1, ErrorMessage = "Количество символов должно быть больше 1")]
        [MaxLength(2000, ErrorMessage = "Количество символов должно быть меньше 2000")]
        public string Answer { get; set; } = string.Empty;
        [Required]
        [MinLength(1, ErrorMessage = "Количество символов должно быть больше 1")]
        [MaxLength(2000, ErrorMessage = "Количество символов должно быть меньше 2000")]
        public string Text { get; set; } = string.Empty;
        [Required]
        public ProposalStatus StatusCode { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
    }

    public enum ProposalStatus
    {
        Done = 200,
        Error = 500
    }
}
