import axios from 'axios';

const apiUrlTriagem = 'http://localhost:5011/api/Triagem';
const apiUrlEspecialidade = 'http://localhost:5011/api/Especialidade';
const apiUrlAtendimento = 'http://localhost:5011/api/Atendimento';

export const fetchTriagens = async () => {
  const response = await axios.get(apiUrlTriagem);
  return response.data.result;
};

export const fetchEspecialidades = async () => {
  const response = await axios.get(apiUrlEspecialidade);
  return response.data.result;
};

export const fetchAtendimentos = async () => {
  const response = await axios.get(apiUrlAtendimento);
  return response.data.result;
};

export const saveTriagem = async (triagem) => {
  const response = await axios.post(apiUrlTriagem, triagem);
  return response.data.result;
};

export const deleteTriagem = async (id) => {
  await axios.delete(`${apiUrlTriagem}/${id}`);
};
