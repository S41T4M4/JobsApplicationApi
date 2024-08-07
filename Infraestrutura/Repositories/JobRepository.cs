using JobApplication.Infraestrutura;
using JobApplication.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace JobApplication.Infraestrutura.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly ConnectionContext _connectionContext;

        public JobRepository(ConnectionContext connectionContext)
        {
            _connectionContext = connectionContext;
        }

        public void AddCandidatura(Candidaturas candidatura)
        {
            _connectionContext.Candidatura.Add(candidatura);
            _connectionContext.SaveChanges();
        }

        public void AddUsuarios(Usuarios usuario)
        {
            _connectionContext.Usuario.Add(usuario);
            _connectionContext.SaveChanges();
        }

        public void AddVagas(Vagas vaga)
        {
            _connectionContext.Vaga.Add(vaga);
            _connectionContext.SaveChanges();
        }

        public void DeleteCandidaturas(int id)
        {
            var candidatura = _connectionContext.Candidatura.Find(id);
            if (candidatura != null)
            {
                _connectionContext.Candidatura.Remove(candidatura);
                _connectionContext.SaveChanges();
            }
        }

        public void DeleteUsuarios(int id)
        {
            var usuario = _connectionContext.Usuario.Find(id);
            if (usuario != null)
            {
                _connectionContext.Usuario.Remove(usuario);
                _connectionContext.SaveChanges();
            }
        }

        public void DeleteVagas(int id)
        {
            var vaga = _connectionContext.Vaga.Find(id);
            if (vaga != null)
            {
                _connectionContext.Vaga.Remove(vaga);
                _connectionContext.SaveChanges();
            }
        }

        public List<Candidaturas> GetAllCandidaturas()
        {
            return _connectionContext.Candidatura.ToList();
        }

        public List<Usuarios> GetAllUsuarios()
        {
            return _connectionContext.Usuario.ToList();
        }

        public List<Vagas> GetAllVagas()
        {
            return _connectionContext.Vaga.ToList();
        }

        public Candidaturas GetCandidaturasById(int id)
        {
            return _connectionContext.Candidatura.Find(id);
        }

        public Vagas GetVagasById(int id)
        {
            return _connectionContext.Vaga.Find(id);
        }

        public Usuarios GetUsuariosById(int id)
        {
            return _connectionContext.Usuario.Find(id);
        }

        public void UpdateCandidaturas(Candidaturas candidatura)
        {
            _connectionContext.Candidatura.Update(candidatura);
            _connectionContext.SaveChanges();
        }

        public void UpdateUsuarios(Usuarios usuario)
        {
            // Carregar o usuário existente do banco de dados
            var existingUser = _connectionContext.Usuario.Find(usuario.id);
            if (existingUser == null)
            {
                throw new ArgumentException("Usuário não encontrado");
            }

            // Manter a data de criação e atualizar apenas os outros campos
            existingUser.nome = usuario.nome;
            existingUser.email = usuario.email;
            existingUser.senha = usuario.senha;
            existingUser.perfil = usuario.perfil;
            existingUser.data_criacao = DateTime.UtcNow;  // Atualiza a data de atualização

            _connectionContext.Usuario.Update(existingUser);
            _connectionContext.SaveChanges();
        }


        public void UpdateVagas(Vagas vaga)
        {
            // Carregar a vaga existente do banco de dados
            var existingVaga = _connectionContext.Vaga.Find(vaga.id);
            if (existingVaga == null)
            {
                throw new ArgumentException("Vaga não encontrada");
            }

            // Manter a data de criação e atualizar apenas os outros campos
            existingVaga.titulo = vaga.titulo;
            existingVaga.descricao = vaga.descricao;
            existingVaga.requisitos = vaga.requisitos;
            existingVaga.salario = vaga.salario;
            existingVaga.localizacao = vaga.localizacao;
            existingVaga.status = vaga.status;
            existingVaga.id_recrutador = vaga.id_recrutador;
            // A data de criação deve ser mantida
            existingVaga.data_criacao = existingVaga.data_criacao.ToUniversalTime();  // Garantir que está no formato UTC

            _connectionContext.Vaga.Update(existingVaga);
            _connectionContext.SaveChanges();
        }
        public Usuarios GetUsuariosByEmail(string email)
        {
            return _connectionContext.Usuario.SingleOrDefault(u => u.email == email);
        }

        public List<Vagas> GetVagasByStatus(string status)
        {
            return _connectionContext.Vaga.Where(v => v.status == status).ToList();
        }

        public List<Vagas> GetVagasBySalario(int salario)
        {
            return _connectionContext.Vaga.Where(v => v.salario == salario).ToList();
        }

        public List<Vagas> GetVagasByIdRecrutador(int id_recrutador)
        {
            return _connectionContext.Vaga.Where(v => v.id_recrutador == id_recrutador).ToList();
        }
    }
}
