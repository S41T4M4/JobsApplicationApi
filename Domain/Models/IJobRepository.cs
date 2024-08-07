﻿namespace JobApplication.Domain.Models
{
    public interface IJobRepository
    {
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
        List<Vagas> GetVagasBySalario(int salario);
        List<Vagas> GetVagasByIdRecrutador(int id_recrutador);

        void AddCandidatura(Candidaturas candidatura);
        List<Candidaturas> GetAllCandidaturas();
        Candidaturas GetCandidaturasById(int id);
        void UpdateCandidaturas(Candidaturas candidatura);
        void DeleteCandidaturas(int id);

        Usuarios GetUsuariosByEmail(string email);

        



    }
}
