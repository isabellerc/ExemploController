using ExemploController.Models.CustomValidations;

namespace ExemploController.Models
{
    public class AlunoViewModel
    {
        public int Codigo { get; set; } // não tem codigo o exercicio que o Humberto pediu
        public string Nome { get; set; }
        public string RA { get; set; }
        [CpfValidationAttribute]
        public string CPF { get; set; }
        public bool Ativo { get; set; }
        public string Email { get; set; }
    }
}
