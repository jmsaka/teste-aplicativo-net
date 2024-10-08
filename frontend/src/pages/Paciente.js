import React, { useState, useEffect } from 'react';
import Modal from 'react-modal';
import { fetchPacientes, savePaciente, deletePaciente } from '../api/pacienteApi'; // Importando a API

const Paciente = () => {
  const [pacientes, setPacientes] = useState([]);
  const [modalIsOpen, setModalIsOpen] = useState(false);
  const [isEdit, setIsEdit] = useState(false);
  const [pacienteSelecionado, setPacienteSelecionado] = useState({ nome: '', telefone: '', sexo: '', email: '' });
  const [loading, setLoading] = useState(false);

  // Definindo o elemento principal da aplicação para acessibilidade
  useEffect(() => {
    Modal.setAppElement('#root');  // Define o elemento principal da aplicação
    loadPacientes();
  }, []);

  const loadPacientes = async () => {
    setLoading(true);
    try {
      const data = await fetchPacientes();
      setPacientes(data);
    } catch (error) {
      console.error('Erro ao carregar pacientes:', error);
    } finally {
      setLoading(false);
    }
  };

  const openModal = (paciente = { nome: '', telefone: '', sexo: '', email: '' }) => {
    setIsEdit(!!paciente.id);
    setPacienteSelecionado(paciente);
    setModalIsOpen(true);
  };

  const closeModal = () => {
    setModalIsOpen(false);
    setPacienteSelecionado({ nome: '', telefone: '', sexo: '', email: '' });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await savePaciente(pacienteSelecionado);
      loadPacientes();
      closeModal();
    } catch (error) {
      console.error('Erro ao salvar o paciente:', error);
    }
  };

  const handleDelete = async (id) => {
    if (window.confirm('Tem certeza que deseja excluir este paciente?')) {
      try {
        await deletePaciente(id);
        loadPacientes();
      } catch (error) {
        console.error('Erro ao excluir paciente:', error);
      }
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setPacienteSelecionado({ ...pacienteSelecionado, [name]: value });
  };

  return (
    <div className="headerpage">
      <div className="header">
        <h1>Gestão de Pacientes</h1>
        <button onClick={() => openModal()} className="btn btn-primary">
          Adicionar Novo Paciente
        </button>
      </div>

      {loading ? (
        <p>Carregando...</p>
      ) : (
        <table className="table table-striped">
          <thead>
            <tr>
              <th>ID</th>
              <th>Nome</th>
              <th>Telefone</th>
              <th>Sexo</th>
              <th>Email</th>
              <th>Ações</th>
            </tr>
          </thead>
          <tbody>
            {pacientes.map((paciente) => (
              <tr key={paciente.id}>
                <td>{paciente.id}</td>
                <td>{paciente.nome}</td>
                <td>{paciente.telefone}</td>
                <td>{paciente.sexo}</td>
                <td>{paciente.email}</td>
                <td role="cell">
                  <div className="actions">
                    <i
                      className="bi bi-pencil icon"
                      onClick={() => openModal(paciente)}
                      style={{ cursor: 'pointer', marginRight: '10px' }}
                      title="Editar"
                    ></i>
                    <i
                      className="bi bi-trash icon"
                      onClick={() => handleDelete(paciente.id)}
                      style={{ cursor: 'pointer' }}
                      title="Excluir"
                    ></i>
                  </div>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}

      <Modal
        isOpen={modalIsOpen}
        onRequestClose={closeModal}
        contentLabel="Paciente Modal"
        className="ReactModal__Content"
        overlayClassName="ReactModal__Overlay"
        appElement={document.getElementById('root')}
      >
        <h2>{isEdit ? 'Editar Paciente' : 'Adicionar Novo Paciente'}</h2>
        <form onSubmit={handleSubmit}>
          <input
            type="text"
            name="nome"
            className="modal-input"
            placeholder="Nome"
            value={pacienteSelecionado.nome}
            onChange={handleChange}
            required
          />
          <input
            type="text"
            name="telefone"
            className="modal-input"
            placeholder="Telefone"
            value={pacienteSelecionado.telefone}
            onChange={handleChange}
            required
          />
          <input
            type="text"
            name="sexo"
            className="modal-input"
            placeholder="Sexo"
            value={pacienteSelecionado.sexo}
            onChange={handleChange}
            required
          />
          <input
            type="email"
            name="email"
            className="modal-input"
            placeholder="Email"
            value={pacienteSelecionado.email}
            onChange={handleChange}
            required
          />
          <div className="modal-buttons">
            <button type="button" onClick={closeModal} className="btn btn-secondary">
              Cancelar
            </button>
            <button type="submit" className="btn btn-primary">
              Salvar
            </button>
          </div>
        </form>
      </Modal>
    </div>
  );
};

export default Paciente;