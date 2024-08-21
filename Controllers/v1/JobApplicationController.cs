using JobApplication.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using JobApplication.ViewModels;
using JobApplication.Infraestrutura.Repositories;


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
        [HttpPost("cadastro/usuarios")]
        public IActionResult AddUser([FromBody] UsuariosViewModel usuariosView)
        {
            if (usuariosView == null)
            {
                return BadRequest("O usuário não pode ser nulo.");
            }

            //Body requisitado para criação de usuario
            var usuario = new Usuarios
            {
                nome = usuariosView.Nome,
                email = usuariosView.Email,
                senha = usuariosView.Senha,
                perfil = usuariosView.Perfil
            };

            // Adiciona o usuário usando o repositório
            _jobRepository.AddUsuarios(usuario);

            
            return Ok();
        }
        [HttpGet("usuarios")]
        public IActionResult GetUsers()
        {
            //Retorna lista de usuarios
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

            //Body de requisição para a criação de vagas
            var vaga = new Vagas
            {
                titulo = vagasView.Titulo,
                descricao = vagasView.Descricao,
                requisitos = vagasView.Requisitos,
                salario = vagasView.Salario,
                localizacao = vagasView.Localizacao,
                status = vagasView.Status,
                id_recrutador = vagasView.IdRecrutador,
                // data_criacao = vagasView.DataCriacao
                //Data gerada automaticamente

            };

            if (vagasView.Salario > 99999)
            {
                return BadRequest("Não é possivel adicionar um salário nesse valor");
            }
            else if (vagasView.Salario == 0)
            {
                return BadRequest("Não é possivel adicionar um salário nesse valor");
            }
            else if (vagasView.Salario < 0)
            {
                return BadRequest("Não é possivel adicionar um salário nesse valor");
            }
            else
            {
                _jobRepository.AddVagas(vaga);
                return Ok();
            }
        }
        [HttpGet("vagas")]
        public IActionResult GetAllVagas()
        {
            var vagas = _jobRepository.GetAllVagas();
            return Ok(vagas);
        }
        [HttpGet("vagas/status/aberta")]
        public IActionResult GetVagasByStatusAberta()
        {
            //Retorna apenas vagas abertas
            var vagasAbertas = _jobRepository.GetVagasByStatus("Aberta");
            return Ok(vagasAbertas);
        }
        [HttpGet("vagas/status/fechada")]
        public IActionResult GetVagasByStatusFechada()
        {
            //Retorna apenas vagas fechadas
            var vagasFechadas = _jobRepository.GetVagasByStatus("Fechada");
            return Ok(vagasFechadas);
        }
        
        [HttpPut("vagas/{id}")]
        public IActionResult UpdateVagas(int id, [FromBody] VagasViewModel vagasViewModel)
        {
            
            if (vagasViewModel == null)
            {
                return BadRequest("Os dados da vaga não podem ser nulos.");
            }

            //Procurar a vaga pelo id da vaga
            var existingVaga = _jobRepository.GetVagasById(id);
            if (existingVaga == null)
            {
                return NotFound(new { Message = "Vaga não encontrada" });
            }

            //Body de atualização da vaga
            existingVaga.titulo = vagasViewModel.Titulo;
            existingVaga.descricao = vagasViewModel.Descricao;
            existingVaga.requisitos = vagasViewModel.Requisitos;
            existingVaga.salario = vagasViewModel.Salario;
            existingVaga.localizacao = vagasViewModel.Localizacao;
            existingVaga.status = vagasViewModel.Status;
            existingVaga.id_recrutador = vagasViewModel.IdRecrutador;

            if (vagasViewModel.Salario > 99999)
            {
                return BadRequest("Não é possivel adicionar um salário nesse valor");
            }
            else if (vagasViewModel.Salario == 0)
            {
                return BadRequest("Não é possivel adicionar um salário nesse valor");
            }
            else if(vagasViewModel.Salario < 0)
            {
                return BadRequest("Não é possivel adicionar um salário nesse valor");
            }
            else
            {
                _jobRepository.UpdateVagas(existingVaga);

                return Ok(new { Message = "Vaga atualizada com sucesso" });

            }
            //data_criacao
            // É mantido a data de criação da vaga

            
           
        }

        [HttpDelete("vagas/{id}")]
        public IActionResult DeleteVagas(int id)
        {
            // Verificar se existem candidaturas associadas à vaga
            if (_jobRepository.CandidaturasExistemParaVaga(id))
            {
                return BadRequest(new { Message = "Não é possível excluir a vaga, pois existem candidaturas associadas a ela." });
            }

            _jobRepository.DeleteVagas(id);
            return Ok(new { Message = "Vaga excluída com sucesso." });
        }

        [HttpGet("candidaturasPorCandidato/{id_candidato}")]
        public IActionResult GetCandidaturasByIdCandidato(int id_candidato)
        {
            var candidaturaId = _jobRepository.GetAllCandidaturasByIdCandidato(id_candidato);
            return Ok(candidaturaId);
        }
        // Método para candidatar-se a uma vaga
        [HttpPost("candidaturas")]
        public IActionResult ApplyForJob([FromBody] CandidaturasViewModel candidaturasViewModel)
        {
            if (candidaturasViewModel == null)
            {
                return BadRequest("A candidatura não pode ser nula.");
            }

            var vaga = _jobRepository.GetVagasById(candidaturasViewModel.IdVaga);
            var candidato = _jobRepository.GetUsuariosById(candidaturasViewModel.IdCandidato);

            if (vaga == null)
            {
                return NotFound(new { Message = "Vaga não encontrada." });
            }
            //Se o perfil do candidato for Recrutador ele não irá conseguir se candidatar a vaga
            if (candidaturasViewModel.IdRecrutador == candidaturasViewModel.IdCandidato)
            {
                return NotFound(new { Message = "Um recrutador não pode se candidatar na sua própria vaga." });
            }

            if (candidato == null)
            {
                return NotFound(new { Message = "Candidato não encontrado." });
            }

            // Verificar se o candidato já se candidatou a vaga 
            if (_jobRepository.CandidaturaExistente(candidaturasViewModel.IdVaga, candidaturasViewModel.IdCandidato))
            {
                return Conflict(new { Message = "Candidatura já existente para esta vaga." });
            }

            var candidatura = new Candidaturas
            {
                id_vaga = candidaturasViewModel.IdVaga,
                id_candidato = candidaturasViewModel.IdCandidato,
                //Ao se candidatar o Status Pendente é colocado
                status = "Pendente",
                data_candidatura = candidaturasViewModel.DataCandidatura ?? DateTime.UtcNow
            };

            _jobRepository.AddCandidatura(candidatura);

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
           // existingCandidatura.data_candidatura = candidaturasViewModel.DataCandidatura ?? existingCandidatura.data_candidatura;

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
        [HttpGet("vagas/{id_recrutador}")]
        public IActionResult GetVagaByID(int id_recrutador)
        {
            var jobs = _jobRepository.GetVagasByIdRecrutador(id_recrutador);
            return Ok(jobs);
        }
        [HttpGet("candidatos/{id_vaga}")]
        public IActionResult GetCandidaturasByVaga(int id_vaga)
        {
            var candidaturas = _jobRepository.GetCandidaturasByIdVaga(id_vaga);
            
            if (candidaturas == null || candidaturas.Count == 0)
            {
                return NotFound("Nenhuma candidatura encontrada para o recrutador especificado.");
            }

            var viewModels = candidaturas.Select(c => new CandidaturasViewModel
            {
                Id = c.id,
                IdVaga = c.id_vaga,
                IdCandidato = c.id_candidato,
                NomeCandidato = c.candidato.nome,  // Usando a propriedade 'nome' do modelo Usuarios
                EmailCandidato = c.candidato.email,
                TituloVaga = c.vaga.titulo,
                Status = c.status,
                DataCandidatura = c.data_candidatura
            }).ToList();

            return Ok(viewModels);
        }
       
        [HttpPut("candidaturas/status/{id}")]
        public IActionResult UpdateStatusCandidatura(int id, [FromBody] UpdateCandidaturaStatusViewModel statusViewModel)
        {
            if (statusViewModel == null || string.IsNullOrEmpty(statusViewModel.Status))
            {
                return BadRequest("O status da candidatura não pode ser nulo ou vazio.");
            }


            var existingCandidatura = _jobRepository.GetCandidaturasById(id);
            if (existingCandidatura == null)
            {
                return NotFound(new { Message = "Candidatura não encontrada." });
            }

            // Atualiza apenas o status da candidatura
            existingCandidatura.status = statusViewModel.Status;
            existingCandidatura.data_candidatura = statusViewModel.DataCandidatura ?? existingCandidatura.data_candidatura;

            _jobRepository.UpdateStatusCandidaturas(existingCandidatura);

            return Ok(new { Message = "Status da candidatura atualizado com sucesso." });
        }
      




    }
}
