import { useState } from 'react';
import axios from 'axios';
import styles from '../App.module.scss';

const CriarLembrete = ({ onLembreteCriado }) => {
    const [novoLembrete, setNovoLembrete] = useState({ titulo: '', data: '', descricao: '' });
    const [erro, setErro] = useState('');
    const [mensagemSucesso, setMensagemSucesso] = useState('');

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setNovoLembrete({ ...novoLembrete, [name]: value });
    };

    const handleCriarLembrete = () => {
        if (!novoLembrete.titulo || !novoLembrete.data) {
            setErro('Os campos de Titulo e Data sao obrigatorios.');
            return;
        }

        const lembrete = {
            titulo: novoLembrete.titulo,
            dataLembrete: novoLembrete.data,
            descricao: novoLembrete.descricao || null,
        };

        axios.post('/api/lembrete/criarLembrete', lembrete)
            .then((response) => {
                onLembreteCriado(response.data);
                setNovoLembrete({ titulo: '', data: '', descricao: '' });
                setErro('');
                setMensagemSucesso('Lembrete criado com sucesso! ');
                setTimeout(() => setMensagemSucesso(''), 3000);
            })
            .catch(error => {
                const backendMessage = error.response.data.message || 'Erro no servidor. ';
                const backendDetails = error.response.data.details || '';
                setErro(`${backendMessage}${backendDetails ? backendDetails : ''}`);
                console.error('Nao foi possivel criar o lembrete: ', error);
            });
    };

    return (
        <header className={styles.novoLembrete}>
            <input
                type="text"
                name="titulo"
                placeholder="Título"
                value={novoLembrete.titulo}
                onChange={handleInputChange}
            />
            <input
                type="date"
                name="data"
                placeholder="Data"
                value={novoLembrete.data}
                onChange={handleInputChange}
            />
            <textarea
                name="descricao"
                placeholder="Descrição (opcional)"
                value={novoLembrete.descricao}
                onChange={handleInputChange}
            />
            <button onClick={handleCriarLembrete}>Criar</button>
            {erro && <p className={styles.error}>{erro}</p>}
            {mensagemSucesso && <div className={styles.sucess}>{mensagemSucesso}</div>}
        </header>
    );
};

export default CriarLembrete;
