# Clinica Entity Relationship Diagram

```mermaid
classDiagram
    class PacienteEntity {
        +int Id
        +string Nome
        +string Telefone
        +string Sexo
        +string? Email
    }

    class AtendimentoEntity {
        +int Id
        +int NumeroSequencial
        +DateTime DataHoraChegada
        +string Status
        +int PacienteId
    }

    class TriagemEntity {
        +int Id
        +string Sintoma
        +string PressaoArterial
        +float Peso
        +float Altura
        +int EspecialidadeId
        +int AtendimentoId
    }

    class EspecialidadeEntity {
        +int Id
        +string Nome
    }

    PacienteEntity "1" -- "*" AtendimentoEntity : has
    AtendimentoEntity "1" -- "1" TriagemEntity : has
    TriagemEntity "*" -- "1" EspecialidadeEntity : has
