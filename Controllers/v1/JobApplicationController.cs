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
   
        // Adicionar usuário do tipo Candidato
        [HttpPost("cadastro/usuarios")]
        public IActionResult AddCandidato([FromBody] UsuariosViewModel usuariosView)
        {
           

            var usuarioExistente = _jobRepository.GetUsuariosByEmail(usuariosView.Email);

            if (usuarioExistente != null)
            {
                return BadRequest("Usuário já existe.");
            }

            //Empresa id é null pois o tipo de usuario é Candidato
            usuariosView.EmpresaId = null;

            var usuario = new Usuarios
            {
                nome = usuariosView.Nome,
                email = usuariosView.Email,
                senha = usuariosView.Senha,
                perfil = "Candidato",  //Perfil do usuario é Candidato
                empresa_id = null 
            };
            if (string.IsNullOrEmpty(usuariosView.Nome))
            {
                return BadRequest("O campo Nome tem que ser preenchido");
            }
            if (string.IsNullOrEmpty(usuariosView.Email))
            {
                return BadRequest("O campo Email tem que ser preenchido");
            }
            if (string.IsNullOrEmpty(usuariosView.Senha))
            {
                return BadRequest("O campo Senha tem que ser preenchido");
            }
            try
            {
                _jobRepository.AddUsuarios(usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar o candidato: {ex.Message}");
            }
        }
        
        [HttpPost("cadastro/recrutador")]
        public IActionResult AddRecrutador([FromBody] RecrutadorViewModel recrutadorView)
        {
            if (recrutadorView == null)
            {
                return BadRequest("Os dados do recrutador não podem ser nulos.");
            }

            var usuarioExistente = _jobRepository.GetUsuariosByEmail(recrutadorView.Email);

            if (usuarioExistente != null)
            {
                return BadRequest("Usuário já existe.");
            }

            if (string.IsNullOrEmpty(recrutadorView.Cnpj))
            {
                return BadRequest("Recrutadores devem fornecer o CNPJ da empresa.");
            }
            if (string.IsNullOrEmpty(recrutadorView.Nome))
            {
                return BadRequest("O campo Nome tem que ser preenchido");
            }
            if (string.IsNullOrEmpty(recrutadorView.Email))
            {
                return BadRequest("O campo Email tem que ser preenchido");
            }
            if (string.IsNullOrEmpty(recrutadorView.Senha))
            {
                return BadRequest("O campo Senha tem que ser preenchido");
            }

            //Verificar se o CNPJ existe
            var empresaExistente = _jobRepository.GetEmpresaByCnpj(recrutadorView.Cnpj);
            if (empresaExistente == null)
            {
                return BadRequest("A empresa não existe.");
            }

            var usuario = new Usuarios
            {
                nome = recrutadorView.Nome,
                email = recrutadorView.Email,
                senha = recrutadorView.Senha,
                perfil = "Recrutador",
                empresa_id = empresaExistente.id 
            };

            try
            {
                _jobRepository.AddUsuarios(usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar o recrutador: {ex.Message}");
            }
        }


        
        [HttpPost("cadastro/empresas")]
        public IActionResult AddEmpresa([FromForm] EmpresasViewModel empresasView)
        {
            if (empresasView == null)
            {
                return BadRequest("O formato não pode ser nulo.");
            }

            var empresaExistente = _jobRepository.GetEmpresaByCnpj(empresasView.Cnpj);

            if (empresaExistente != null)
            {
                return BadRequest("Já existe uma empresa com este CNPJ.");
            }
           
            var empresa = new Empresas
            {
                nome = empresasView.Name,
                cnpj = empresasView.Cnpj,
                
            };
            if (string.IsNullOrWhiteSpace(empresasView.Name))
            {
                return BadRequest("O nome da empresa não pode ser vazio");
            }
            if (string.IsNullOrWhiteSpace(empresasView.Cnpj))
            {
                return BadRequest("O cnpj não pode ser vazio");
            }


            try
            {
                _jobRepository.AddEmpresa(empresa);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar a empresa: {ex.Message}");
            }
        }



        [HttpGet("usuarios")]
        public IActionResult GetUsers([FromQuery] string filtro , [FromQuery] string ordem = "email")
        {
            IQueryable<Usuarios> query = _jobRepository.GetAllUsuarios();

            if (!string.IsNullOrEmpty(filtro))
            {
                query = query.Where(u => u.email.Contains(filtro));
            }
            switch (ordem.ToLower())
            {
                case "email":
                    query = query.OrderBy(u => u.email);
                    break;
                case "nome":
                    query = query.OrderBy(u => u.nome);
                    break;

            }


            var users = query.ToList();
            return Ok(users);
        }
        [HttpGet("empresas")]
        public IActionResult GetAllEmpresas([FromQuery] string filtro, [FromQuery] string ordem = "nome")
        {
       
            IQueryable<Empresas> query = _jobRepository.GetAllEmpresas();


            if (!string.IsNullOrEmpty(filtro))
            {
                query = query.Where(e => e.nome.Contains(filtro));
            }

            switch (ordem.ToLower())
            {
                case "nome":
                    query = query.OrderBy(e => e.nome);
                    break;
                case "cnpj":
                    query = query.OrderBy(e => e.cnpj);
                    break;
                default:
                    query = query.OrderBy(e => e.nome);
                    break;
            }


            int pageNumber = 1;
            int pageSize = 10;
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            
            var empresas = query.ToList();
            return Ok(empresas);
        }
        [HttpPut("usuarios/{id}")]
        public IActionResult UpdateUser(int id, UsuariosViewModel usuariosView)
        {
            
            var existingUser = _jobRepository.GetUsuariosById(id);
            
            if (existingUser == null)
            {
                
                return NotFound(new { Message = "Usuário não encontrado" });
            }

            
            existingUser.nome = usuariosView.Nome;
            existingUser.email = usuariosView.Email;
            existingUser.senha = usuariosView.Senha;
            existingUser.perfil = usuariosView.Perfil;

            if (string.IsNullOrEmpty(usuariosView.Nome))
            {
                return BadRequest("O campo Nome tem que ser preenchido");
            }
            if (string.IsNullOrEmpty(usuariosView.Email))
            {
                return BadRequest("O campo Email tem que ser preenchido");
            }
            if (string.IsNullOrEmpty(usuariosView.Senha))
            {
                return BadRequest("O campo Senha tem que ser preenchido");
            }


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

            var recrutador = _jobRepository.GetUsuariosById(vagasView.IdRecrutador);
            if (recrutador == null || recrutador.perfil != "Recrutador")
            {
                return BadRequest("O ID do recrutador é inválido ou o usuário não é um recrutador.");
            }

         
            var vaga = new Vagas
            {
                titulo = vagasView.Titulo,
                descricao = vagasView.Descricao,
                requisitos = vagasView.Requisitos,
                salario = vagasView.Salario,
                localizacao = vagasView.Localizacao,
                status = vagasView.Status,
                id_recrutador = vagasView.IdRecrutador,
                empresa_id = vagasView.IdEmpresa
                
            };

            if (string.IsNullOrWhiteSpace(vagasView.Titulo))
            {
                return BadRequest("O título não pode ser vazio!");
            }

            if (string.IsNullOrWhiteSpace(vagasView.Descricao))
            {
                return BadRequest("A descrição não pode ser vazia!");
            }

            if (string.IsNullOrWhiteSpace(vagasView.Requisitos))
            {
                return BadRequest("Os requisitos não podem ser vazios!");
            }

            if (string.IsNullOrWhiteSpace(vagasView.Localizacao))
            {
                return BadRequest("A localização não pode ser vazia.");
            }

            if (vagasView.Salario > 99999 || vagasView.Salario == 0 || vagasView.Salario < 499)
            {
                return BadRequest("O salário deve estar entre 500 e 99999.");
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

        [HttpGet("vagas/status/fechada")]
        public IActionResult GetVagasByStatusFechada()
        {

            var vagasFechadas = _jobRepository.GetVagasByStatus("Fechada");
            return Ok(vagasFechadas);
        }
        [HttpGet("vagas/nomeEmpresa")]
        public IActionResult GetVagasByEmpresa(string nome)
        {
            var nomeEmpresa = _jobRepository.GetVagasByEmpresa(nome);
            if (string.IsNullOrEmpty(nome))
            {
                return BadRequest("O nome da empresa não pode ser vazio ");
            }    
                return Ok(nomeEmpresa);
    
        }

        [HttpPut("vagas/{id}")]
        public IActionResult UpdateVagas(int id, [FromBody] VagasViewModel vagasViewModel)
        {


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

           
            if (string.IsNullOrWhiteSpace(vagasViewModel.Titulo))
            {
                return BadRequest("O titulo não pode ser vazio !");
            }

            if (string.IsNullOrWhiteSpace(vagasViewModel.Descricao))
            {
                return BadRequest("A descrição não pode ser vazia !");
            }

            if (string.IsNullOrWhiteSpace(vagasViewModel.Requisitos))
            {
                return BadRequest("Os requisitos não podem ser vazios !");
            }

            if (string.IsNullOrWhiteSpace(vagasViewModel.Localizacao))
            {
                return BadRequest("A localização não pode ser vazia");
            }

            if (vagasViewModel.Salario > 99999)
            {
                return BadRequest("Não é possivel adicionar um salário maior do que R$99.999");
            }
      
            else if(vagasViewModel.Salario < 499)
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
        [HttpDelete("empresa/{id}")]
        public IActionResult DeleteEmpresa(int id)
        {
            var existingEmpresa = _jobRepository.GetEmpresaById(id);
            if (existingEmpresa == null)
            {
                return NotFound(new { Message = "A empresa não foi encontrada" });
            }
            _jobRepository.DeleteEmpresas(id);
            return Ok();
        }

        [HttpGet("vagas/{id_recrutador}")]
        public IActionResult GetVagaByID(int id_recrutador)
        {
            var jobs = _jobRepository.GetVagasByIdRecrutador(id_recrutador);
            return Ok(jobs);
        }
        [HttpGet("vagas/empresa")]
        public IActionResult GetVagasByCnpj([FromQuery] string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
            {
                return BadRequest("O CNPJ não pode ser nulo ou vazio.");
            }

            var vagas = _jobRepository.GetVagasByEmpresaCnpj(cnpj);

            if (vagas == null || !vagas.Any())
            {
                return NotFound("Nenhuma vaga encontrada para o CNPJ fornecido.");
            }

            return Ok(vagas);
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
                NomeCandidato = c.candidato.nome,          // Usando a propriedade 'nome' do modelo Usuarios
                EmailCandidato = c.candidato.email,
                TituloVaga = c.vaga.titulo,
                Status = c.status,
                DataCandidatura = c.data_candidatura
            }).ToList();

            return Ok(viewModels);
        }
        [HttpGet("vagas/status/aberta")]
        public IActionResult GetVagasByStatusAberta()
        {
            //Retorna apenas vagas abertas
            var vagasAbertas = _jobRepository.GetVagasByStatus("Aberta");

            var viewModels = vagasAbertas.Select(c => new VagasViewModel
            {
                IdEmpresa = c.empresa_id,
                Titulo = c.titulo,
                Descricao = c.descricao,
                Localizacao = c.localizacao,
                Requisitos = c.requisitos,
                Salario = c.salario,
                Status = c.status,
                Nome = c.Empresa.nome,


            }).ToList();
            
            return Ok(vagasAbertas);
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
