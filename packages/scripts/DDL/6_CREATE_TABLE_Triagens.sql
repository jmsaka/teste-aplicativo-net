USE [ClinicaDB]
GO

CREATE TABLE [dbo].[Triagens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Sintoma] [nvarchar](max) NOT NULL,
	[PressaoArterial] [nvarchar](max) NOT NULL,
	[Peso] [real] NOT NULL,
	[Altura] [real] NOT NULL,
	[EspecialidadeId] [int] NOT NULL,
	[AtendimentoId] [int] NOT NULL,
 CONSTRAINT [PK_Triagens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Triagens]  WITH CHECK ADD  CONSTRAINT [FK_Triagens_Atendimentos_AtendimentoId] FOREIGN KEY([AtendimentoId])
REFERENCES [dbo].[Atendimentos] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Triagens] CHECK CONSTRAINT [FK_Triagens_Atendimentos_AtendimentoId]
GO

ALTER TABLE [dbo].[Triagens]  WITH CHECK ADD  CONSTRAINT [FK_Triagens_Especialidades_EspecialidadeId] FOREIGN KEY([EspecialidadeId])
REFERENCES [dbo].[Especialidades] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Triagens] CHECK CONSTRAINT [FK_Triagens_Especialidades_EspecialidadeId]
GO


