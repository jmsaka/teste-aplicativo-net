USE [ClinicaDB]
GO

INSERT INTO [dbo].[Triagens]
           ([Sintoma], [PressaoArterial], [Peso], [Altura], [EspecialidadeId], [AtendimentoId])
     VALUES
           ('Febre', '14/8', 4.0, 3.0, 5, 14)
GO