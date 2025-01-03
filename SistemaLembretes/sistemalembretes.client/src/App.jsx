import { useState, useEffect } from 'react';
import axios from 'axios';
import styles from './App.module.css';

const App = () => {
    const [lembretes, setLembretes] = useState([]);
    const [novoLembrete, setNovoLembrete] = useState({ titulo: '', data: '', descricao: '' });
    const [erro, setErro] = useState('');
    const [mensagemSucesso, setMensagemSucesso] = useState('');


    useEffect(() => {
        axios.get('/api/lembrete/todos')
            .then(response => {
                const lembretesOrdenados = response.data.sort((a, b) => new Date(a.dataLembrete) - new Date(b.dataLembrete));
                setLembretes(lembretesOrdenados);
            })
            .catch(error => console.error('Erro ao carregar lembretes:', error));
    }, []);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setNovoLembrete({ ...novoLembrete, [name]: value });
    };

    const handleCriarLembrete = () => {
        if (!novoLembrete.titulo || !novoLembrete.data) {
            setErro('Preencha todos os campos!');
            return;
        }
        const dataAtual = new Date();
        if (novoLembrete.data < dataAtual) {
            setErro('A data não pode ser no passado!');
            return;
        }

        const lembrete = {
            titulo: novoLembrete.titulo,
            dataLembrete: novoLembrete.data,
            descricao: novoLembrete.descricao || null
        };

        axios.post('/api/lembrete/criarLembrete', lembrete)
            .then(response => {
                setLembretes(prev => [...prev, response.data].sort((a, b) => new Date(a.dataLembrete) - new Date(b.dataLembrete)));
                setNovoLembrete({ titulo: '', data: '', descricao: '' });
                setErro('');
                setMensagemSucesso('Lembrete criado com sucesso!');
                setTimeout(() => setMensagemSucesso(''), 3000);
            })
            .catch(error => {
                
                    const backendMessage = error.response.data.message || 'Erro inesperado no servidor.';
                    const backendDetails = error.response.data.details || '';
                    setErro(`${backendMessage}${backendDetails ? `: ${backendDetails}` : ''}`);
                 

                    
                
                console.error('Erro ao criar lembrete:', error);
            });
    }
        const handleExcluirLembrete = (id) => {
            axios.delete(`/api/lembrete/${id}/apagarLembrete`)
                .then(() => {
                    setLembretes(prev => prev.filter(lembrete => lembrete.id !== id));
                })
                .catch(error => {
                    if (error.response && error.response.data) {
                        setErro(error.response.data.message || 'Erro ao excluir lembrete.');
                    } else {
                        setErro('Erro de conexão com o servidor.');
                    }
                    console.error('Erro ao excluir lembrete:', error);
                });
        };

    const lembretesAgrupados = lembretes.reduce((agrupados, lembrete) => {
        const data = new Date(lembrete.dataLembrete).toLocaleDateString();
        agrupados[data] = agrupados[data] || [];
        agrupados[data].push(lembrete);
        return agrupados;
    }, {});

    return (
        <div className={styles.App}>
            <h1 className={styles.titulo}>Gerenciador de Lembretes</h1>
            <header className={styles.novoLembrete}>
                <input 
                    type="text"
                    name="titulo"
                    placeholder="Titulo"
                    value={novoLembrete.titulo}
                    onChange={handleInputChange}
                />
                <input 
                    type="date"
                    name="data"
                    value={novoLembrete.data}
                    onChange={handleInputChange}
                />
                <textarea 
                    name="descricao"
                    placeholder="Descricao (opcional)"
                    value={novoLembrete.descricao}
                    onChange={handleInputChange}
                ></textarea>
                <button onClick={handleCriarLembrete}>Criar</button>
                {erro && <p className={styles.error}>{erro}</p>}
                {mensagemSucesso && (
                    <div className={styles.sucess}>
                        {mensagemSucesso}
                    </div>
                )}
            </header>
            <div className={styles.listaLembretes}>
                {Object.keys(lembretesAgrupados).map(data => (
                    <div key={data} className={styles.colunaLembrete}>
                        <h3>{data}</h3>
                        <ul>
                            {lembretesAgrupados[data].map(lembrete => (
                                <li key={lembrete.id} className={styles.colunaLembrete}>
                                    <div className={styles.tooltip}>
                                        <strong>{lembrete.titulo}</strong>
                                        <span className={styles.tooltipText}>{lembrete.descricao}</span>
                                    </div>
                                    <button
                                        className={styles.deleteButton}
                                        onClick={(e) => {
                                            e.stopPropagation();
                                            handleExcluirLembrete(lembrete.id);
                                        }}
                                    >
                                        X
                                    </button>
                                </li>
                            ))}
                        </ul>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default App;
