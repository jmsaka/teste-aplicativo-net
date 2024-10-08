import React, { useState, useEffect } from 'react';
import Modal from 'react-modal';
import { fetchTriagens, saveTriagem, fetchEspecialidades, fetchAtendimentos } from '../api/triagemApi';

const Triagem = () => {
  const [triagens, setTriagens] = useState([]);
  const [especialidades, setEspecialidades] = useState([]);
  const [atendimentos, setAtendimentos] = useState([]);
  const [modalIsOpen, setModalIsOpen] = useState(false);
  const [isEdit, setIsEdit] = useState(false);
  const [triagemSelecionada, setTriagemSelecionada] = useState({
    id: 0,
    sintoma: '',
    pressaoArterial: '',
    peso: 0,
    altura: 0,
    especialidadeId: 0,
    atendimentoId: 0,
  });
  const [loading, setLoading] = useState(false);

  // Definindo o elemento principal da aplicação para acessibilidade
  useEffect(() => {
    Modal.setAppElement('#root');  // Define o elemento principal da aplicação
    loadTriagens();
    loadEspecialidades();
    loadAtendimentos();
  }, []);

  const loadTriagens = async () => {
    setLoading(true);
    try {
      const data = await fetchTriagens();
      setTriagens(data);
    } catch (error) {
      console.error('Erro ao carregar triagens:', error);
    } finally {
      setLoading(false);
    }
  };

  const loadEspecialidades = async () => {
    try {
      const data = await fetchEspecialidades();
      setEspecialidades(data);
    } catch (error) {
      console.error('Erro ao carregar especialidades:', error);
    }
  };

  const loadAtendimentos = async () => {
    try {
      const data = await fetchAtendimentos();
      setAtendimentos(data);
    } catch (error) {
      console.error('Erro ao carregar atendimentos:', error);
    }
  };

  const openModal = (triagem = { id: 0, sintoma: '', pressaoArterial: '', peso: 0, altura: 0, especialidadeId: 0, atendimentoId: 0 }) => {
    setIsEdit(!!triagem.id);
    setTriagemSelecionada(triagem);
    setModalIsOpen(true);
  };

  const closeModal = () => {
    setModalIsOpen(false);
    setTriagemSelecionada({ id: 0, sintoma: '', pressaoArterial: '', peso: 0, altura: 0, especialidadeId: 0, atendimentoId: 0 });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await saveTriagem(triagemSelecionada);
      loadTriagens();
      closeModal();
    } catch (error) {
      console.error('Erro ao salvar a triagem:', error);
    }
  };

  // const handleDelete = async (id) => {
  //   if (window.confirm('Tem certeza que deseja excluir esta triagem?')) {
  //     try {
  //       await deleteTriagem(id);
  //       loadTriagens();
  //     } catch (error) {
  //       console.error('Erro ao excluir triagem:', error);
  //     }
  //   }
  // };

  const handleChange = (e) => {
    const { name, value } = e.target;

    // Converte para float se o campo for 'peso' ou 'altura'
    const newValue = (name === 'peso' || name === 'altura') ? parseFloat(value) : value;

    setTriagemSelecionada({ ...triagemSelecionada, [name]: value });
  };

  return (
    <div className="headerpage">
      <div className="header">
        <h1>Gestão de Triagens</h1>
        <button onClick={() => openModal()} className="btn btn-primary">Adicionar Nova Triagem</button>
      </div>
      {loading ? (
        <p>Carregando...</p>
      ) : (
        <table className="table table-striped">
          <thead>
            <tr>
              <th>ID</th>
              <th>Sintoma</th>
              <th>Pressão Arterial</th>
              <th>Peso</th>
              <th>Altura</th>
              <th>Especialidade</th>
              <th>Atendimento</th>
              <th>Ações</th>
            </tr>
          </thead>
          <tbody>
            {triagens.map((triagem) => (
              <tr key={triagem.id}>
                <td>{triagem.id}</td>
                <td>{triagem.sintoma}</td>
                <td>{triagem.pressaoArterial}</td>
                <td>{triagem.peso}</td>
                <td>{triagem.altura}</td>
                <td>{triagem.especialidadeId}</td>
                <td>{triagem.atendimentoId}</td>
                <td role="cell">
                  <div className="actions">
                    <i
                      className="bi bi-pencil icon"
                      onClick={() => openModal(triagem)}
                      style={{ cursor: 'pointer', marginRight: '10px' }}
                      title="Editar"
                    ></i>
                    {/* <i
                      className="bi bi-trash icon"
                      onClick={() => handleDelete(triagem.id)}
                      style={{ cursor: 'pointer' }}
                      title="Excluir"
                    ></i> */}
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
        contentLabel="Triagem Modal"
        className="ReactModal__Content"
        overlayClassName="ReactModal__Overlay"
      >
        <h2>{isEdit ? 'Editar Triagem' : 'Adicionar Nova Triagem'}</h2>
        <form onSubmit={handleSubmit}>
          <input type="text" name="sintoma" className="modal-input" placeholder="Sintoma" value={triagemSelecionada.sintoma} onChange={handleChange} required />
          <input type="text" name="pressaoArterial" className="modal-input" placeholder="Pressão Arterial" value={triagemSelecionada.pressaoArterial} onChange={handleChange} required />
          <input type="text" name="peso" className="modal-input" placeholder="Peso" value={triagemSelecionada.peso} onChange={handleChange} required />
          <input type="text" name="altura" className="modal-input" placeholder="Altura" value={triagemSelecionada.altura} onChange={handleChange} required />
          <select name="especialidadeId" className="modal-input" value={triagemSelecionada.especialidadeId} onChange={handleChange} required>
            <option value="">Selecione Especialidade</option>
            {especialidades.map((especialidade) => (
              <option key={especialidade.id} value={especialidade.id}>{especialidade.nome}</option>
            ))}
          </select>
          <select name="atendimentoId" className="modal-input" value={triagemSelecionada.atendimentoId} onChange={handleChange} required>
            <option value="">Selecione Atendimento</option>
            {atendimentos.map((atendimento) => (
              <option key={atendimento.id} value={atendimento.id}>{atendimento.numeroSequencial}-{atendimento.data}-{atendimento.id}</option>
            ))}
          </select>
          <div className="modal-buttons">
            <button type="button" onClick={closeModal} className="btn btn-secondary">Cancelar</button>
            <button type="submit" className="btn btn-primary">Salvar</button>
          </div>
        </form>
      </Modal>
    </div>
  );
};

export default Triagem;
