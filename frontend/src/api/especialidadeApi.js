import axios from 'axios';

const apiUrl = 'http://localhost:5011/api/Especialidade';

// Função para buscar todas as especialidades
export const fetchEspecialidades = async () => {
  try {
    const response = await axios.get(apiUrl);
    return response.data.result;  // Acessa a chave 'result' no JSON
  } catch (error) {
    console.error('Erro ao buscar especialidades:', error);
    throw error;
  }
};

// Função para criar ou editar uma especialidade
export const saveEspecialidade = async (especialidade) => {
  try {
    const payload = {
      id: especialidade.id || 0,  // Se não houver id, assume 0
      nome: especialidade.nome,
    };
    await axios.post(apiUrl, payload);
  } catch (error) {
    console.error('Erro ao salvar a especialidade:', error);
    throw error;
  }
};

// Função para deletar uma especialidade
export const deleteEspecialidade = async (id) => {
  try {
    await axios.delete(`${apiUrl}/${id}`);
  } catch (error) {
    console.error('Erro ao excluir especialidade:', error);
    throw error;
  }
};
