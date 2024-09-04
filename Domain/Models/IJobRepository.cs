using Microsoft.AspNetCore.SignalR;

namespace JobApplication.Domain.Models
{
    public interface IJobRepository
    {
        Empresas GetEmpresaById(int id);

        List<Vagas> GetVagasByEmpresaCnpj(string cnpj);
        Empresas GetEmpresaByCnpj(string cnpj);
        List<Empresas> GetAllEmpresas();
        void AddEmpresa(Empresas empresa);
        void DeleteEmpresas(int id);

        void AddUsuarios(Usuarios usuario);
        List<Usuarios> GetAllUsuarios();
        Usuarios GetUsuariosById(int id);
        void UpdateUsuarios(Usuarios usuario);
        void DeleteUsuarios(int id);

        void AddVagas(Vagas vaga);
        List<Vagas> GetAllVagas();
        Vagas GetVagasById(int id);
        void UpdateVagas(Vagas vaga);
        void DeleteVagas(int id);
        List<Vagas> GetVagasByStatus(string status);
        List<Vagas> GetVagasBySalario(decimal salario);
        List<Vagas> GetVagasByIdRecrutador(int id_recrutador);

        void AddCandidatura(Candidaturas candidatura);
        List<Candidaturas> GetAllCandidaturas();
        Candidaturas GetCandidaturasById(int id);
        void UpdateCandidaturas(Candidaturas candidatura);
        void DeleteCandidaturas(int id);
        List<Candidaturas> GetAllCandidaturasById(int id_vaga);
        List<Candidaturas> GetAllCandidaturasByIdCandidato(int id_candidato);
        bool CandidaturaExistente(int idVaga, int idCandidato);
        Usuarios GetUsuariosByEmail(string email);
        List<Candidaturas> GetCandidaturasByIdRecrutador(int id_recrutador);
        List<Candidaturas> GetCandidaturasByIdVaga(int id_vaga);
        void UpdateStatusCandidaturas(Candidaturas status);
        
        bool CandidaturasExistemParaVaga(int idVaga);



    }
}
