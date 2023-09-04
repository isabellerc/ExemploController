using ExemploController.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ExemploController.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : PrincipalController
    {
        AlunoViewModel aluno = new AlunoViewModel();
        private readonly string _alunoCaminhoArquivo;

        public AlunoController()
        {
            _alunoCaminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "Data", "aluno.json");
        }

        #region Métodos arquivo
        private List<AlunoViewModel> LerAlunosDoArquivo()
        {
            if (!System.IO.File.Exists(_alunoCaminhoArquivo))
            {
                return new List<AlunoViewModel>();
            }

            string json = System.IO.File.ReadAllText(_alunoCaminhoArquivo);
            return JsonConvert.DeserializeObject<List<AlunoViewModel>>(json);
        }

        private int ObterProximoCodigoDisponivel()
        {
            List<AlunoViewModel> aluno = LerAlunosDoArquivo();
            if (aluno.Any())
            {
                return aluno.Max(p => p.Codigo) + 1;
            }
            else
            {
                return 1;
            }
        }

        private void EscreverAlunosNoArquivo(List<AlunoViewModel> alunos)
        {
            string json = JsonConvert.SerializeObject(aluno);
            System.IO.File.WriteAllText(_alunoCaminhoArquivo, json);
        }

        #endregion

        #region Operações do crud


        [HttpGet]
        public IActionResult Get()
        {
            List<AlunoViewModel> aluno = LerAlunosDoArquivo();
            return Ok(aluno);
        }

        [HttpGet("{codigo}")]
        public IActionResult Get(int codigo)
        {
            List<AlunoViewModel> alunos = LerAlunosDoArquivo();
            AlunoViewModel aluno = alunos.Find(p => p.Codigo == codigo);
            if (aluno == null)
            {
                return NotFound();
            }
            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post([FromBody] NovoAlunoViewModel aluno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<AlunoViewModel> alunos = LerAlunosDoArquivo();
            int proximoCodigo = ObterProximoCodigoDisponivel();
           

            AlunoViewModel novoAluno = new AlunoViewModel
            {
                Codigo = proximoCodigo,
                Nome = aluno.Nome,
                RA = aluno.RA,
                CPF = aluno.CPF,
                Ativo = aluno.Ativo,
                Email = aluno.Email

            };

            alunos.Add(novoAluno);
            EscreverAlunosNoArquivo(alunos);

            return ApiResponse(novoAluno, "Aluno criado com sucesso");
            //return CreatedAtAction(nameof(Get), new { codigo = novoAluno.Codigo }, novoAluno);

        }

        [HttpPut("{codigo}")]
        public IActionResult Put(int Codigo, [FromBody] EditaAlunoViewModel aluno, int codigo)
        {
            if (aluno == null)
            {
                return BadRequest();
            }


            List<AlunoViewModel> alunos = LerAlunosDoArquivo();
            int index = alunos.FindIndex(p => p.Codigo == codigo);
            if (index == -1)
                return NotFound();

            AlunoViewModel alunoEditado = new AlunoViewModel()
            {
                Codigo = Codigo,
                Nome = aluno.Nome,
                RA = aluno.RA,
                CPF = aluno.CPF,
                Ativo = aluno.Ativo,
                Email = aluno.Email
            };

            alunos[index] = alunoEditado;
            EscreverAlunosNoArquivo(alunos);

            return NoContent();
        }


        [HttpDelete("{codigo}")]
        public IActionResult Delete(int codigo)
        {
            List<AlunoViewModel> alunos = LerAlunosDoArquivo();
            AlunoViewModel alunoParaRemover = alunos.Find(p => p.Codigo == codigo);
            if (alunoParaRemover == null)
            {
                return NoContent();
            }


            alunos.Remove(alunoParaRemover);
            EscreverAlunosNoArquivo(alunos);

            return NoContent();
        }
        #endregion
    }
}
