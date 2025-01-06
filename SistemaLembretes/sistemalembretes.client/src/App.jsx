import { useState, useEffect } from 'react';
import axios from 'axios';
import CriarLembrete from './components/CriarLembrete';
import ListarLembretes from './components/ListarLembretes';
import styles from './App.module.scss';

const App = () => {
    const [lembretes, setLembretes] = useState([]);

    useEffect(() => {
        axios.get('/api/lembrete/todos')
            .then((response) => {
                const lembretesOrdenados = response.data.sort((a, b) => new Date(a.dataLembrete) - new Date(b.dataLembrete));
                setLembretes(lembretesOrdenados);
            })
            .catch((error) => console.error('Erro ao carregar lembretes: ', error));
    }, []);

    const handleLembreteCriado = (novoLembrete) => {
        setLembretes((prev) => [...prev, novoLembrete].sort((a, b) => new Date(a.dataLembrete) - new Date(b.dataLembrete)));
    };

    const handleExcluirLembrete = (id) => {
        axios.delete(`/api/lembrete/${id}/apagarLembrete`)
            .then(() => {
                setLembretes((prev) => prev.filter((lembrete) => lembrete.id !== id));
            })
            .catch((error) => console.error('Erro ao excluir lembrete:', error));
    };

    return (
        <div className={styles.App}>
            <h1 className={styles.titulo}>Gerenciador de Lembretes</h1>
            <CriarLembrete onLembreteCriado={handleLembreteCriado} />
            <ListarLembretes lembretes={lembretes} onExcluirLembrete={handleExcluirLembrete} />
        </div>
    );
};

export default App;
