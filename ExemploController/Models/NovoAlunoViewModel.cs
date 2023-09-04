using ExemploController.Models.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace ExemploController.Models
{
    public class NovoAlunoViewModel
    {
        [Required(ErrorMessage = " O nome é obrigatório.")]
        [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 letras.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = " O RA é obrigatório.")]
        [RegularExpression(@"^RA\d{6}$", ErrorMessage = "O campo RA deve começar com as letras 'RA' seguidas de 6 dígitos.")]
        public string RA { get; set; }
        [CpfValidationAttribute]
        public string CPF { get; set; }
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo de e-mail não é um endereço de e-mail válido.")]
        public string Email { get; set; }
    }
}
