import React, { useState, useEffect } from 'react';
import Modal from 'react-modal';
import { fetchEspecialidades, saveEspecialidade, deleteEspecialidade } from '../api/especialidadeApi';  // Importando a API

const Especialidade = () => {
  const [especialidades, setEspecialidades] = useState([]);
  const [modalIsOpen, setModalIsOpen] = useState(false);
  const [isEdit, setIsEdit] = useState(false);
  const [especialidadeSelecionada, setEspecialidadeSelecionada] = useState({ nome: '' });
  const [loading, setLoading] = useState(false);

  // Definindo o elemento principal da aplicação para acessibilidade
  useEffect(() => {
    Modal.setAppElement('#root');  // Define o elemento principal da aplicação
    loadEspecialidades();
  }, []);

  const loadEspecialidades = async () => {
    setLoading(true);
    try {
      const data = await fetchEspecialidades();
      setEspecialidades(data);
    } catch (error) {
      console.error('Erro ao carregar especialidades:', error);
    } finally {
      setLoading(false);
    }
  };

  const openModal = (especialidade = { nome: '' }) => {
    setIsEdit(!!especialidade.id);
    setEspecialidadeSelecionada(especialidade);
    setModalIsOpen(true);
  };

  const closeModal = () => {
    setModalIsOpen(false);
    setEspecialidadeSelecionada({ nome: '' });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await saveEspecialidade(especialidadeSelecionada);
      loadEspecialidades();
      closeModal();
    } catch (error) {
      console.error('Erro ao salvar a especialidade:', error);
    }
  };

  const handleDelete = async (id) => {
    if (window.confirm('Tem certeza que deseja excluir esta especialidade?')) {
      try {
        await deleteEspecialidade(id);
        loadEspecialidades();
      } catch (error) {
        console.error('Erro ao excluir especialidade:', error);
      }
    }
  };

  const handleChange = (e) => {
    setEspecialidadeSelecionada({ ...especialidadeSelecionada, nome: e.target.value });
  };

  return (
    <div className="headerpage">
      <div className="header">
        <h1>Gestão de Especialidades</h1>
        <button onClick={() => openModal()} className="btn btn-primary">
          Adicionar Nova Especialidade
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
              <th>Ações</th>
            </tr>
          </thead>
          <tbody>
            {especialidades.map((especialidade) => (
              <tr key={especialidade.id}>
                <td>{especialidade.id}</td>
                <td>{especialidade.nome}</td>
                <td role="cell">
                  <div className="actions">
                    <i
                      className="bi bi-pencil icon"
                      onClick={() => openModal(especialidade)}
                      style={{ cursor: 'pointer', marginRight: '10px' }}
                      title="Editar"
                    ></i>
                    <i
                      className="bi bi-trash icon"
                      onClick={() => handleDelete(especialidade.id)}
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
        contentLabel="Especialidade Modal"
        className="ReactModal__Content"
        overlayClassName="ReactModal__Overlay"
      >
        <h2>{isEdit ? 'Editar Especialidade' : 'Adicionar Nova Especialidade'}</h2>
        <form onSubmit={handleSubmit}>
          <input
            type="text"
            className="modal-input"
            placeholder="Nome da especialidade"
            value={especialidadeSelecionada.nome}
            onChange={handleChange}
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

export default Especialidade;
