import React, { useState, useEffect } from 'react';
import Modal from 'react-modal';
import { fetchAtendimentos, saveAtendimento, deleteAtendimento } from '../api/atendimentoApi';
import { fetchPacientes } from '../api/pacienteApi';

const Atendimento = () => {
    const [atendimentos, setAtendimentos] = useState([]);
    const [pacientes, setPacientes] = useState([]);
    const [modalIsOpen, setModalIsOpen] = useState(false);
    const [isEdit, setIsEdit] = useState(false);
    const [atendimentoSelecionado, setAtendimentoSelecionado] = useState({
        numeroSequencial: 0,
        dataHoraChegada: '',
        status: 'Recepção',
        pacienteId: '', // Inicializado como string vazia
    });
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        Modal.setAppElement('#root');
        loadAtendimentos();
        loadPacientes();
    }, []);

    const loadAtendimentos = async () => {
        setLoading(true);
        try {
            const data = await fetchAtendimentos();
            setAtendimentos(Array.isArray(data) ? data : []);
        } catch (error) {
            console.error('Erro ao carregar atendimentos:', error);
        } finally {
            setLoading(false);
        }
    };

    const loadPacientes = async () => {
        setLoading(true);
        try {
            const data = await fetchPacientes();
            setPacientes(Array.isArray(data) ? data : []);
        } catch (error) {
            console.error('Erro ao carregar pacientes:', error);
        } finally {
            setLoading(false);
        }
    };

    const openModal = (atendimento = {}) => { // Aceita um objeto vazio se não for fornecido
        setIsEdit(!!atendimento.id);
        setAtendimentoSelecionado({
            numeroSequencial: atendimento.numeroSequencial || 0,
            dataHoraChegada: atendimento.dataHoraChegada || '',
            status: atendimento.status || 'Recepção',
            pacienteId: atendimento.pacienteId || '', // Garante que seja string vazia
        });
        setModalIsOpen(true);
    };

    const closeModal = () => {
        setModalIsOpen(false);
        setAtendimentoSelecionado({
            numeroSequencial: 0,
            dataHoraChegada: '',
            status: 'Recepção',
            pacienteId: '', // valor inicial como string vazia
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await saveAtendimento(atendimentoSelecionado);
            loadAtendimentos();
            closeModal();
        } catch (error) {
            console.error('Erro ao salvar o atendimento:', error);
        }
    };

    const handleDelete = async (id) => {
        if (window.confirm('Tem certeza que deseja excluir este atendimento?')) {
            try {
                await deleteAtendimento(id);
                loadAtendimentos();
            } catch (error) {
                console.error('Erro ao excluir atendimento:', error);
            }
        }
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setAtendimentoSelecionado({ ...atendimentoSelecionado, [name]: value });
    };

    return (
        <div className="headerpage">
            <div className="header">
                <h1>Gestão de Atendimentos</h1>
                <button onClick={() => openModal()} className="btn btn-primary">
                    Adicionar Novo Atendimento
                </button>
            </div>

            {loading ? (
                <p>Carregando...</p>
            ) : (
                <table className="table table-striped">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Número Sequencial</th>
                            <th>Data e Hora de Chegada</th>
                            <th>Status</th>
                            <th>ID do Paciente</th>
                            <th>Ações</th>
                        </tr>
                    </thead>
                    <tbody>
                        {Array.isArray(atendimentos) && atendimentos.length > 0 ? (
                            atendimentos.map((atendimento) => (
                                <tr key={atendimento.id}>
                                    <td>{atendimento.id}</td>
                                    <td>{atendimento.numeroSequencial}</td>
                                    <td>{new Date(atendimento.dataHoraChegada).toLocaleString()}</td>
                                    <td>{atendimento.status}</td>
                                    <td>{atendimento.pacienteId}</td>
                                    <td>
                                        <div className="actions">
                                            <i
                                                className="bi bi-pencil icon"
                                                onClick={() => openModal(atendimento)}
                                                style={{ cursor: 'pointer', marginRight: '10px' }}
                                                title="Editar"
                                            ></i>
                                            <i
                                                className="bi bi-trash icon"
                                                onClick={() => handleDelete(atendimento.id)}
                                                style={{ cursor: 'pointer' }}
                                                title="Excluir"
                                            ></i>
                                        </div>
                                    </td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan="6">Nenhum atendimento encontrado.</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            )}

            <Modal
                isOpen={modalIsOpen}
                onRequestClose={closeModal}
                contentLabel="Atendimento Modal"
                className="ReactModal__Content"
                overlayClassName="ReactModal__Overlay"
            >
                <h2>{isEdit ? 'Editar Atendimento' : 'Adicionar Novo Atendimento'}</h2>
                <form onSubmit={handleSubmit}>
                    <input
                        type="number"
                        name="numeroSequencial"
                        className="modal-input"
                        placeholder="Número Sequencial"
                        value={atendimentoSelecionado.numeroSequencial}
                        onChange={handleChange}
                        required
                    />
                    <input
                        type="datetime-local"
                        name="dataHoraChegada"
                        className="modal-input"
                        value={atendimentoSelecionado.dataHoraChegada}
                        onChange={handleChange}
                        required
                    />
                    <select
                        name="status"
                        className="modal-input"
                        value={atendimentoSelecionado.status}
                        onChange={handleChange}
                    >
                        <option value="Recepção">Recepção</option>
                        <option value="Triagem">Triagem</option>
                        <option value="Consultório">Consultório</option>
                    </select>
                    <select
                        name="pacienteId"
                        className="modal-input"
                        value={atendimentoSelecionado.pacienteId}
                        onChange={handleChange}
                        required
                    >
                        <option value="">Selecione um Paciente</option>
                        {pacientes.map((paciente) => (
                            <option key={paciente.id} value={paciente.id}>
                                {paciente.nome}
                            </option>
                        ))}
                    </select>
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

export default Atendimento;
