using JobApplication.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using JobApplication.ViewModels;
using JobApplication.Infraestrutura.Repositories;
using JobApplication.Domain.Models;

namespace JobApplication.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/jobApplication")]
    [ApiVersion("1.0")]
    public class JobApplicationController : ControllerBase
    {
        private readonly IJobRepository _jobRepository;

        public JobApplicationController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        [HttpPost("usuarios")]
        public IActionResult AddUser([FromBody] UsuariosViewModel usuariosView)
        {
            if (usuariosView == null)
            {
                return BadRequest("O usuário não pode ser nulo.");
            }

            // Converte o ViewModel para a entidade Usuario
            var usuario = new Usuarios
            {
                nome = usuariosView.Nome,
                email = usuariosView.Email,
                senha = usuariosView.Senha,
                perfil = usuariosView.Perfil
            };

            // Adiciona o usuário usando o repositório
            _jobRepository.AddUsuarios(usuario);

            // Retorna um status 201 Created com o usuário criado
            return Ok();
        }
        [HttpGet("usuarios")]
        public IActionResult GetUsers()
        {
            var users = _jobRepository.GetAllUsuarios();
            return Ok(users);
        }
        [HttpPut("usuarios/{id}")]
        public IActionResult UpdateUser(int id, UsuariosViewModel usuariosView)
        {
            // Recuperar o usuário existente pelo Id
            var existingUser = _jobRepository.GetUsuariosById(id);
            if (existingUser == null)
            {
                return NotFound(new { Message = "Usuário não encontrado" });
            }

            // Atualizar as propriedades do usuário com as informações do ViewModel
            existingUser.nome = usuariosView.Nome;
            existingUser.email = usuariosView.Email;
            existingUser.senha = usuariosView.Senha;
            existingUser.perfil = usuariosView.Perfil;

            // Salvar as alterações
            _jobRepository.UpdateUsuarios(existingUser);

            return Ok(new { Message = "Usuário atualizado com sucesso" });
        }
        [HttpDelete("usuarios/{id}")]
        public IActionResult DeleteUser(int id)
        {
            _jobRepository.DeleteUsuarios(id);
            return Ok();
        }
        [HttpPost("vagas")]
        public IActionResult AddVaga([FromBody] VagasViewModel vagasView)
        {
            if (vagasView == null)
            {
                return BadRequest("A vaga não pode ser nula.");
            }

            // Converte o ViewModel para a entidade Vagas
            var vaga = new Vagas
            {
                titulo = vagasView.Titulo,
                descricao = vagasView.Descricao,
                requisitos = vagasView.Requisitos,
                salario = vagasView.Salario,
                localizacao = vagasView.Localizacao,
                status = vagasView.Status,
                id_recrutador = vagasView.IdRecrutador,
                // data_criacao será definida no modelo
            };

            // Adiciona a vaga usando o repositório
            _jobRepository.AddVagas(vaga);

            // Retorna um status 201 Created com a vaga criada
            return Ok();
        }
        [HttpGet("vagas")]
        public IActionResult GetAllVagas()
        {
            var vagas = _jobRepository.GetAllVagas();
            return Ok(vagas);
        }
        [HttpPut("vagas/{id}")]
        public IActionResult UpdateVagas(int id, [FromBody] VagasViewModel vagasViewModel)
        {
            // Verificar se o ViewModel é nulo
            if (vagasViewModel == null)
            {
                return BadRequest("Os dados da vaga não podem ser nulos.");
            }

            // Recuperar a vaga existente pelo Id
            var existingVaga = _jobRepository.GetVagasById(id);
            if (existingVaga == null)
            {
                return NotFound(new { Message = "Vaga não encontrada" });
            }

            // Atualizar as propriedades da vaga com as informações do ViewModel
            existingVaga.titulo = vagasViewModel.Titulo;
            existingVaga.descricao = vagasViewModel.Descricao;
            existingVaga.requisitos = vagasViewModel.Requisitos;
            existingVaga.salario = vagasViewModel.Salario;
            existingVaga.localizacao = vagasViewModel.Localizacao;
            existingVaga.status = vagasViewModel.Status;
            existingVaga.id_recrutador = vagasViewModel.IdRecrutador;
            // Não atualiza a data_criacao

            // Salvar as alterações
            _jobRepository.UpdateVagas(existingVaga);

            return Ok(new { Message = "Vaga atualizada com sucesso" });
        }

        [HttpDelete("vagas/{id}")]
        public IActionResult DeleteVagas(int id)
        {
            _jobRepository.DeleteVagas(id);
            return Ok();
        }
        // Método para candidatar-se a uma vaga
        [HttpPost("candidaturas")]
        public IActionResult ApplyForJob([FromBody] CandidaturasViewModel candidaturasViewModel)
        {
            if (candidaturasViewModel == null)
            {
                return BadRequest("A candidatura não pode ser nula.");
            }

            // Verificar se a vaga e o candidato existem
            var vaga = _jobRepository.GetVagasById(candidaturasViewModel.IdVaga);
            var candidato = _jobRepository.GetUsuariosById(candidaturasViewModel.IdCandidato);

            if (vaga == null)
            {
                return NotFound(new { Message = "Vaga não encontrada." });
            }

            if (candidato == null)
            {
                return NotFound(new { Message = "Candidato não encontrado." });
            }

            // Converte o ViewModel para a entidade Candidaturas
            var candidatura = new Candidaturas
            {
                id_vaga = candidaturasViewModel.IdVaga,
                id_candidato = candidaturasViewModel.IdCandidato,
                status = candidaturasViewModel.Status ?? "Pendente", // Define um status padrão se não fornecido
                data_candidatura = candidaturasViewModel.DataCandidatura ?? DateTime.UtcNow // Define a data atual se não fornecida
            };

            // Adiciona a candidatura usando o repositório
            _jobRepository.AddCandidatura(candidatura);

            // Retorna um status 201 Created com a candidatura criada
            return Ok();
        }
        // Método para obter uma candidatura pelo ID
        [HttpGet("candidaturas/{id}")]
        public IActionResult GetCandidaturaById(int id)
        {
            var candidatura = _jobRepository.GetCandidaturasById(id);
            if (candidatura == null)
            {
                return NotFound(new { Message = "Candidatura não encontrada." });
            }

            return Ok(candidatura);
        }
        [HttpGet("candidaturas")]
        public IActionResult GetAllCandidaturas()
        {
            var candidaturas = _jobRepository.GetAllCandidaturas();
            return Ok(candidaturas);
        }
        [HttpPut("candidaturas/{id}")]
        public IActionResult UpdateCandidatura(int id, [FromBody] CandidaturasViewModel candidaturasViewModel)
        {
            if (candidaturasViewModel == null)
            {
                return BadRequest("A candidatura não pode ser nula.");
            }

            var existingCandidatura = _jobRepository.GetCandidaturasById(id);
            if (existingCandidatura == null)
            {
                return NotFound(new { Message = "Candidatura não encontrada." });
            }

            // Atualizar as propriedades da candidatura
            existingCandidatura.id_vaga = candidaturasViewModel.IdVaga;
            existingCandidatura.id_candidato = candidaturasViewModel.IdCandidato;
            existingCandidatura.status = candidaturasViewModel.Status;
            existingCandidatura.data_candidatura = candidaturasViewModel.DataCandidatura ?? existingCandidatura.data_candidatura;

            // Atualiza a candidatura no repositório

            _jobRepository.UpdateCandidaturas(existingCandidatura);

            return Ok(new { Message = "Candidatura atualizada com sucesso." });
        }
        [HttpDelete("candidaturas/{id}")]
        public IActionResult DeleteCandidatura(int id)
        {
            var existingCandidatura = _jobRepository.GetCandidaturasById(id);
            if (existingCandidatura == null)
            {
                return NotFound(new { Message = "Candidatura não encontrada." });
            }

            _jobRepository.DeleteCandidaturas(id);
            return Ok(new { Message = "Candidatura excluída com sucesso." });
        }

    }
}
