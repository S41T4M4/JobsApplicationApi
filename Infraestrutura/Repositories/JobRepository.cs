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
     
        public Empresas GetEmpresaByCnpj(string cnpj)
        {
            return _connectionContext.Empresa
            .SingleOrDefault(e => e.cnpj == cnpj);
        }
        public IQueryable<Empresas> GetAllEmpresas()
        {
            return _connectionContext.Empresa;
        }


        public void AddCandidatura(Candidaturas candidatura)
        {
            _connectionContext.Candidatura.Add(candidatura);
            _connectionContext.SaveChanges();
        }

        public void AddEmpresa(Empresas empresa)
        {
            _connectionContext.Empresa.Add(empresa);
            _connectionContext.SaveChanges();
        }

       
        public void AddUsuarios(Usuarios usuario )
        {
           
                var usuarioExistente = GetUsuariosByEmail(usuario.email);
                if (usuarioExistente != null)
                {
                    throw new Exception("Usuário já existe.");
                }

                _connectionContext.Usuario.Add(usuario);
                _connectionContext.SaveChanges();
         
        }

        public void AddVagas(Vagas vaga)
        {
            _connectionContext.Vaga.Add(vaga);
            _connectionContext.SaveChanges();
        }
        public Empresas GetEmpresaById(int id)
        {
            return _connectionContext.Empresa
            .SingleOrDefault(e => e.id == id);
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
        public void DeleteEmpresas(int id)
        {
            var empresa = _connectionContext.Empresa.Find(id);
            if(empresa != null)
            {
                _connectionContext.Empresa.Remove(empresa);
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
            return _connectionContext.Usuario
            .FirstOrDefault(u => u.id == id);
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

           
            existingUser.nome = usuario.nome;
            existingUser.email = usuario.email;
            existingUser.senha = usuario.senha;
            existingUser.perfil = usuario.perfil;
            existingUser.data_criacao = DateTime.UtcNow;
            

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


        public Usuarios GetUsuariosByEmail(string email)
        {
            return _connectionContext.Usuario
                .Include(u => u.Empresa)  // Inclui a entidade Empresa
                .SingleOrDefault(u => u.email == email);
        }
        public List<Vagas> GetVagasByEmpresa(string nome)
        {
            return _connectionContext.Vaga
            .Include(v => v.Empresa)
            .Where(v => v.Empresa.nome.ToLower()
            .Contains(nome.ToLower()))
            .ToList();
        }



        public List<Vagas> GetVagasByStatus(string status)
        {
            return _connectionContext.Vaga.Include(v => v.Empresa)
            .Where(v => v.status == status)
            .ToList();
        }


        public List<Vagas> GetVagasBySalario(decimal salario)
        {
            return _connectionContext.Vaga.Where(v => v.salario == salario).ToList();
        }

    
        public List<Vagas> GetVagasByIdRecrutador(int id_recrutador)
        {
            return _connectionContext.Vaga.Where(v => v.id_recrutador == id_recrutador).ToList();
        }


        public List<Candidaturas> GetAllCandidaturasById(int id_vaga)
        {
            return _connectionContext.Candidatura.Include(c => c.candidato).Include(c => c.vaga).Where(c => c.id_vaga == id_vaga).ToList();

        }
        public List<Vagas> GetVagasByEmpresaCnpj(string cnpj)
        {
            return _connectionContext.Vaga
                .Include(v => v.Empresa) // Inclui a entidade Empresas
                .Where(v => v.Empresa.cnpj == cnpj)
                .ToList();
        }


    
        public List<Candidaturas> GetCandidaturasByIdRecrutador(int idRecrutador)
        {
            return _connectionContext.Candidatura
                .Include(c => c.candidato)  // Inclui a entidade relacionada candidato
                .Include(c => c.vaga)       // Inclui a entidade relacionada vaga
                .Where(c => c.vaga.id_recrutador == idRecrutador)
                .ToList();
        }

       
        public List<Candidaturas> GetAllCandidaturasByIdCandidato(int id_candidato)
        {
            return _connectionContext.Candidatura
                .Include(c => c.vaga)
                .Include(c =>c.vaga.Empresa)
                .Where(c => c.id_candidato == id_candidato)
                .ToList();
        }

 
        public bool CandidaturaExistente(int idVaga, int idCandidato)
        {
            return _connectionContext.Candidatura.Any(c => c.id_vaga == idVaga && c.id_candidato == idCandidato);
        }


        public List<Candidaturas> GetCandidaturasByIdVaga(int id_vaga)
        {
            return _connectionContext.Candidatura.Include(c => c.candidato).Include(c => c.vaga).Where(c => c.id_vaga == id_vaga).ToList();
        }

   
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

       
        public bool CandidaturasExistemParaVaga(int idVaga)
        {
            return _connectionContext.Candidatura.Any(c => c.id_vaga == idVaga);
        }

     
    }
}

