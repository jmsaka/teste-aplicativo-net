import axios from 'axios';

const apiUrl = 'http://localhost:5011/api/Atendimento';

export const fetchAtendimentos = async () => {
    const response = await axios.get(apiUrl);
    return response.data.result;
};

export const saveAtendimento = async (atendimento) => {
    const response = await axios.post(apiUrl, atendimento);
    return response.data.result;
};

export const deleteAtendimento = async (id) => {
    await axios.delete(`${apiUrl}/${id}`);
};
