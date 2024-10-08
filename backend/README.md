# Clínica API - Documentação

## Visão Geral
A **Clínica API** é um serviço RESTful que permite a interação com vários recursos relacionados ao sistema de uma clínica de saúde. Ela fornece endpoints para gerenciar **Atendimentos**, **Especialidades** e **Pacientes**. A API é construída usando OpenAPI 3.0.1 e segue códigos de resposta HTTP padrão para indicar o sucesso ou falha das chamadas de API.

---

## URL Base

http://localhost:[porta]/api

---

## Autenticação
Esta API não especifica um mecanismo de autenticação. Certifique-se de que ela seja protegida em ambientes de produção utilizando métodos como OAuth, chaves de API ou outras técnicas de autenticação seguras.

---

## Endpoints

### Atendimento

#### 1. Listar Atendimentos
- **Endpoint:** `/api/Atendimento`
- **Método:** `GET`
- **Descrição:** Recupera uma lista de todos os `Atendimentos` disponíveis. Você pode filtrar pelo `id` de um atendimento.
- **Parâmetros:**
  - `id` (opcional, `query`): Filtra pelo ID do Atendimento (integer)
- **Respostas:**
  - `200 OK`: Retorna uma lista de entidades `Atendimento`.
  
##### Exemplo de Resposta:
```json
{
  "data": [
    {
      "id": 1,
      "numeroSequencial": 101,
      "dataHoraChegada": "2024-10-07T16:50:00",
      "status": "Recepção",
      "pacienteId": 6
    }
  ]
}
```

#### 2. Criar Atendimento
- **Endpoint:** `/api/Atendimento`
- **Método:** `POST`
- **Descrição:** Cria/Altera um novo registro de `Atendimento`.
- **Corpo da Requisição:**
  - **Content-Type:** `application/json`
  - **Exemplo:**
    ```json
    {
      "numeroSequencial": 102,
      "dataHoraChegada": "2024-10-07T17:00:00",
      "status": "Recepção",
      "pacienteId": 2
    }
    ```
- **Respostas:**
  - `200 OK`: Retorna a entidade `Atendimento` recém-criada.
  
**Exemplo de Resposta:**
```json
{
  "data": {
    "id": 2,
    "numeroSequencial": 102,
    "dataHoraChegada": "2024-10-07T17:00:00",
    "status": "Recepção",
    "pacienteId": 2
  }
}
```

#### 3. Excluir Atendimento
- **Endpoint:** `/api/Atendimento/{id}`
- **Método:** `DELETE`
- **Descrição:** Exclui um `Atendimento` pelo seu ID.
- **Parâmetros:**
  - `id` (obrigatório, `path`): ID do Atendimento a ser excluído (integer)
- **Respostas:**
  - `200 OK`: Retorna uma mensagem de sucesso se a exclusão for bem-sucedida.

---

### Especialidade

#### 1. Listar Especialidades
- **Endpoint:** `/api/Especialidade`
- **Método:** `GET`
- **Descrição:** Recupera uma lista de `Especialidades`. Você pode filtrar pelo `id` de uma especialidade.
- **Parâmetros:**
  - `id` (opcional, `query`): Filtra pelo ID da Especialidade (integer)
- **Respostas:**
  - `200 OK`: Retorna uma lista de entidades `Especialidade`.

**Exemplo de Resposta:**
```json
{
  "data": [
    {
      "id": 1,
      "nome": "Cardiologia"
    }
  ]
}
```

#### 2. Criar Especialidade
- **Endpoint:** `/api/Especialidade`
- **Método:** `POST`
- **Descrição:** Cria/Altera uma nova `Especialidade`.
- **Corpo da Requisição:**
  - **Content-Type:** `application/json`
  - **Exemplo:**
    ```json
    {
      "nome": "Dermatologia"
    }
    ```
- **Respostas:**
  - `200 OK`: Retorna a nova `Especialidade` criada.

---

#### 3. Excluir Especialidade
- **Endpoint:** `/api/Especialidade/{id}`
- **Método:** `DELETE`
- **Descrição:** Exclui uma `Especialidade` pelo seu ID.
- **Parâmetros:**
  - `id` (obrigatório, `path`): ID da Especialidade a ser excluída (integer)
- **Respostas:**
  - `200 OK`: Retorna uma mensagem de sucesso se a exclusão for bem-sucedida.

---

### Paciente

#### 1. Listar Pacientes
- **Endpoint:** `/api/Paciente`
- **Método:** `GET`
- **Descrição:** Recupera uma lista de `Pacientes`. Você pode filtrar pelo `id` de um paciente.
- **Parâmetros:**
  - `id` (opcional, `query`): Filtra pelo ID do Paciente (integer)
- **Respostas:**
  - `200 OK`: Retorna uma lista de entidades `Paciente`.

**Exemplo de Resposta:**
```json
{
  "data": [
    {
      "id": 1,
      "nome": "João Silva",
      "idade": 30
    }
  ]
}
```

#### 2. Criar Paciente
- **Endpoint:** `/api/Paciente`
- **Método:** `POST`
- **Descrição:** Cria/Altera um novo `Paciente`.
- **Corpo da Requisição:**
  - **Content-Type:** `application/json`
  - **Exemplo:**
    ```json
    {
      "nome": "Maria Oliveira",
      "idade": 28,
      "cpf": "123.456.789-00"
    }
    ```
- **Respostas:**
  - `200 OK`: Retorna o novo `Paciente` criado.

---

#### 3. Excluir Paciente
- **Endpoint:** `/api/Paciente/{id}`
- **Método:** `DELETE`
- **Descrição:** Exclui um `Paciente` pelo seu ID.
- **Parâmetros:**
  - `id` (obrigatório, `path`): ID do Paciente a ser excluído (integer)
- **Respostas:**
  - `200 OK`: Retorna uma mensagem de sucesso se a exclusão for bem-sucedida.

---

## Segurança
Para garantir a segurança da API, recomenda-se o uso de autenticação via tokens JWT (JSON Web Tokens). O token deve ser enviado em cada requisição nos cabeçalhos, usando o seguinte formato:



---

## Considerações Finais
A API foi desenvolvida para ser robusta e fácil de usar. Para uma melhor experiência, utilize um cliente REST para testar os endpoints e suas funcionalidades. Para dúvidas ou sugestões, entre em contato com a equipe de desenvolvimento.

---

## Licença
Esta API está licenciada sob a Licença MIT. Consulte o arquivo `LICENSE` para mais detalhes.