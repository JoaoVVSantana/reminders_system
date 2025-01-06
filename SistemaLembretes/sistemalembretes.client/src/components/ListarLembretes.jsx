import styles from '../App.module.scss';


const ListarLembretes = ({ lembretes, onExcluirLembrete }) => {
    const lembretesAgrupados = lembretes.reduce((agrupados, lembrete) => {
        const data = new Date(lembrete.dataLembrete).toLocaleDateString();
        agrupados[data] = agrupados[data] || [];
        agrupados[data].push(lembrete);
        return agrupados;
    }, {});

    return (
        <div className={styles.listaLembretes}>
            {Object.keys(lembretesAgrupados).map((data) => (
                <div key={data} className={styles.colunaLembrete}>
                    <h3>{data}</h3>
                    <ul>
                        {lembretesAgrupados[data].map((lembrete) => (
                            <li key={lembrete.id} className={styles.colunaLembrete}>
                                <div className={styles.tooltip}>
                                    <strong>{lembrete.titulo}</strong>
                                    <span className={styles.tooltipText}>{lembrete.descricao}</span>
                                </div>
                                <button
                                    className={styles.deleteButton}
                                    onClick={() => onExcluirLembrete(lembrete.id)}
                                >
                                    X
                                </button>
                            </li>
                        ))}
                    </ul>
                </div>
            ))}
        </div>
    );
};

export default ListarLembretes;
