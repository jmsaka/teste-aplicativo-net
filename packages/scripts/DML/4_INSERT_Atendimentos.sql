USE [ClinicaDB]
GO

INSERT INTO [dbo].[Atendimentos]
           ([NumeroSequencial], [DataHoraChegada], [Status], [PacienteId])
     VALUES
           (1, '2024-10-07 16:50:00.0000000', 'Recep��o', 6),
           (2, '2024-10-07 17:00:00.0000000', 'Recep��o', 2),
           (3, '2024-10-09 19:51:00.0000000', 'Recep��o', 5),
           (4, '2024-10-07 19:54:00.0000000', 'Recep��o', 10)
GO
