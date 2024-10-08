import axios from 'axios';

const apiUrl = 'http://localhost:5011/api/Paciente';

// Função para buscar todos os pacientes
export const fetchPacientes = async () => {
  const response = await axios.get(apiUrl);
  return response.data.result; // Ajuste conforme a estrutura da resposta da API
};

// Função para salvar um paciente (criação ou atualização)
export const savePaciente = async (paciente) => {
  const payload = {
    id: paciente.id || 0, // 0 para novo paciente
    nome: paciente.nome,
    telefone: paciente.telefone,
    sexo: paciente.sexo,
    email: paciente.email,
  };
  await axios.post(apiUrl, payload);
};

// Função para excluir um paciente
export const deletePaciente = async (id) => {
  await axios.delete(`${apiUrl}/${id}`);
};
