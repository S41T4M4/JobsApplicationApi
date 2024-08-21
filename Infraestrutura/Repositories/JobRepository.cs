using JobApplication.Infraestrutura; 
using JobApplication.Domain.Models; 
using Microsoft.EntityFrameworkCore; 
using Microsoft.AspNetCore.Http.HttpResults; 

namespace JobApplication.Infraestrutura.Repositories
{
   
    public class JobRepository : IJobRepository
    {
        private readonly ConnectionContext _connectionContext;

        // Construtor que injeta a dependência do contexto de conexão com o banco de dados
        public JobRepository(ConnectionContext connectionContext)
        {
            _connectionContext = connectionContext;
        }

        // Método para adicionar uma nova candidatura ao banco de dados
        public void AddCandidatura(Candidaturas candidatura)
        {
            _connectionContext.Candidatura.Add(candidatura);
            _connectionContext.SaveChanges();
        }

        // Método para adicionar um novo usuário ao banco de dados
        public void AddUsuarios(Usuarios usuario)
        {
            _connectionContext.Usuario.Add(usuario);
            _connectionContext.SaveChanges();
        }

        // Método para adicionar uma nova vaga ao banco de dados
        public void AddVagas(Vagas vaga)
        {
            _connectionContext.Vaga.Add(vaga);
            _connectionContext.SaveChanges();
        }

        // Método para excluir uma candidatura  pelo ID
        public void DeleteCandidaturas(int id)
        {
            var candidatura = _connectionContext.Candidatura.Find(id);
            if (candidatura != null)
            {
                _connectionContext.Candidatura.Remove(candidatura);
                _connectionContext.SaveChanges();
            }
        }

        // Método para excluir um usuario  pelo ID
        public void DeleteUsuarios(int id)
        {
            var usuario = _connectionContext.Usuario.Find(id);
            if (usuario != null)
            {
                _connectionContext.Usuario.Remove(usuario);
                _connectionContext.SaveChanges();
            }
        }

        // Método para excluir uma vaga  pelo ID
        public void DeleteVagas(int id)
        {
            var vaga = _connectionContext.Vaga.Find(id);
            if (vaga != null)
            {
                _connectionContext.Vaga.Remove(vaga);
                _connectionContext.SaveChanges();
            }
        }

        // Método para retornar todas as candidaturas
        public List<Candidaturas> GetAllCandidaturas()
        {
            return _connectionContext.Candidatura.ToList();
        }

        // Método para retornar todos os usuários
        public List<Usuarios> GetAllUsuarios()
        {
            return _connectionContext.Usuario.ToList();
        }

        // Método para retornar todas as vagas
        public List<Vagas> GetAllVagas()
        {
            return _connectionContext.Vaga.ToList();
        }

        // Método para retornar uma candidatura  pelo ID
        public Candidaturas GetCandidaturasById(int id)
        {
            return _connectionContext.Candidatura.Find(id);
        }

        // Método para retornar uma vaga  pelo ID
        public Vagas GetVagasById(int id)
        {
            return _connectionContext.Vaga.Find(id);
        }

        // Método para retornar um usuário específico pelo ID
        public Usuarios GetUsuariosById(int id)
        {
            return _connectionContext.Usuario.Find(id);
        }

        // Método para atualizar uma candidatura existente
        public void UpdateCandidaturas(Candidaturas candidatura)
        {
            _connectionContext.Candidatura.Update(candidatura);
            _connectionContext.SaveChanges();
        }

        // Método para atualizar um usuário existente
        public void UpdateUsuarios(Usuarios usuario)
        {
            // Carregar o usuário existente do banco de dados
            var existingUser = _connectionContext.Usuario.Find(usuario.id);
            //Verifica se o usuario existe
            if (existingUser == null)
            {
                throw new ArgumentException("Usuário não encontrado");
            }

           
            existingUser.nome = usuario.nome;
            existingUser.email = usuario.email;
            existingUser.senha = usuario.senha;
            existingUser.perfil = usuario.perfil;
            existingUser.data_criacao = DateTime.UtcNow;  

            _connectionContext.Usuario.Update(existingUser);
            _connectionContext.SaveChanges();
        }

        // Método para atualizar uma vaga existente
        public void UpdateVagas(Vagas vaga)
        {
            // Carregar a vaga existente do banco de dados
            var existingVaga = _connectionContext.Vaga.Find(vaga.id);
            if (existingVaga == null)
            {
                throw new ArgumentException("Vaga não encontrada");
            }

            
            existingVaga.titulo = vaga.titulo;
            existingVaga.descricao = vaga.descricao;
            existingVaga.requisitos = vaga.requisitos;
            existingVaga.salario = vaga.salario;
            existingVaga.localizacao = vaga.localizacao;
            existingVaga.status = vaga.status;
            existingVaga.id_recrutador = vaga.id_recrutador;
            existingVaga.data_criacao = existingVaga.data_criacao.ToUniversalTime();


         

            _connectionContext.Vaga.Update(existingVaga);
            _connectionContext.SaveChanges();
        }

        // Método para retornar um usuário específico pelo email
        public Usuarios GetUsuariosByEmail(string email)
        {
            return _connectionContext.Usuario.SingleOrDefault(u => u.email == email);
        }

        // Método para retornar vagas com um status especifico
        public List<Vagas> GetVagasByStatus(string status)
        {
            return _connectionContext.Vaga.Where(v => v.status == status).ToList();
        }

        // Método para retornar vagas com um salario especifico
        public List<Vagas> GetVagasBySalario(double salario)
        {
            return _connectionContext.Vaga.Where(v => v.salario == salario).ToList();
        }

        // Método para retornar vagas por ID do recrutador
        public List<Vagas> GetVagasByIdRecrutador(int id_recrutador)
        {
            return _connectionContext.Vaga.Where(v => v.id_recrutador == id_recrutador).ToList();
        }


        public List<Candidaturas> GetAllCandidaturasById(int id_vaga)
        {
            return _connectionContext.Candidatura.Include(c => c.candidato).Include(c => c.vaga).Where(c => c.id_vaga == id_vaga).ToList();



        }

        // Método para retornar candidaturas por ID de recrutador, incluindo dados de candidato e vaga
        public List<Candidaturas> GetCandidaturasByIdRecrutador(int idRecrutador)
        {
            return _connectionContext.Candidatura
                .Include(c => c.candidato)  // Inclui a entidade relacionada candidato
                .Include(c => c.vaga)       // Inclui a entidade relacionada vaga
                .Where(c => c.vaga.id_recrutador == idRecrutador)
                .ToList();
        }

        // Método para retornar todas as candidaturas por ID de candidato, incluindo dados da vaga
        public List<Candidaturas> GetAllCandidaturasByIdCandidato(int id_candidato)
        {
            return _connectionContext.Candidatura
                .Include(c => c.vaga)
                .Where(c => c.id_candidato == id_candidato)
                .ToList();
        }

        // Método para verificar se uma candidatura já existe para uma determinada vaga e candidato
        public bool CandidaturaExistente(int idVaga, int idCandidato)
        {
            return _connectionContext.Candidatura.Any(c => c.id_vaga == idVaga && c.id_candidato == idCandidato);
        }

        // Método para retornar candidaturas por ID da vaga
        public List<Candidaturas> GetCandidaturasByIdVaga(int id_vaga)
        {
            return _connectionContext.Candidatura.Include(c => c.candidato).Include(c => c.vaga).Where(c => c.id_vaga == id_vaga).ToList();
        }

        // Método para atualizar o status de uma candidatura
        public void UpdateStatusCandidaturas(Candidaturas candidaturas)
        {
            var existingCandidatura = _connectionContext.Candidatura.Find(candidaturas.id);
            if (existingCandidatura != null)
            {
                existingCandidatura.status = candidaturas.status;
                existingCandidatura.data_candidatura = candidaturas.data_candidatura.ToUniversalTime();
                _connectionContext.Candidatura.Update(existingCandidatura);
                _connectionContext.SaveChanges();
            }
            else
            {
                throw new Exception("Candidatura não existe");
            }
        }

        // Método para verificar se existem candidaturas associadas a uma vaga específica
        public bool CandidaturasExistemParaVaga(int idVaga)
        {
            return _connectionContext.Candidatura.Any(c => c.id_vaga == idVaga);
        }
    }
}

