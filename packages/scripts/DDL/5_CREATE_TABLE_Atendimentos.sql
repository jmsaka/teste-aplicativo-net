USE [ClinicaDB]
GO

CREATE TABLE [dbo].[Atendimentos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NumeroSequencial] [int] NOT NULL,
	[DataHoraChegada] [datetime2](7) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[PacienteId] [int] NOT NULL,
 CONSTRAINT [PK_Atendimentos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Atendimentos]  WITH CHECK ADD  CONSTRAINT [FK_Atendimentos_Pacientes_PacienteId] FOREIGN KEY([PacienteId])
REFERENCES [dbo].[Pacientes] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Atendimentos] CHECK CONSTRAINT [FK_Atendimentos_Pacientes_PacienteId]
GO