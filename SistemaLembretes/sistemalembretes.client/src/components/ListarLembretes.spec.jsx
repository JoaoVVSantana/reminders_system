import { render, screen, fireEvent } from '@testing-library/react';
import ListarLembretes from './ListarLembretes';

describe('ListarLembretes Component', () => {
  const lembretesMock = [
    {
      id: 1,
      titulo: 'Lembrete 1',
      dataLembrete: '2025-01-10',
      descricao: 'Descrição do lembrete 1',
    },
    {
      id: 2,
      titulo: 'Lembrete 2',
      dataLembrete: '2025-01-10',
      descricao: 'Descrição do lembrete 2',
    },
    {
      id: 3,
      titulo: 'Lembrete 3',
      dataLembrete: '2025-01-11',
      descricao: 'Descrição do lembrete 3',
    },
  ];

  it('deve agrupar lembretes por data e exibir corretamente', () => {
    render(<ListarLembretes lembretes={lembretesMock} onExcluirLembrete={jest.fn()} />);

    expect(screen.getByText('Lembrete 1')).toBeInTheDocument();
    expect(screen.getByText('Lembrete 2')).toBeInTheDocument();
    expect(screen.getByText('Lembrete 3')).toBeInTheDocument();
  });

  it('deve exibir descrição ao passar o mouse sobre um lembrete', () => {
    render(<ListarLembretes lembretes={lembretesMock} onExcluirLembrete={jest.fn()} />);

    const lembrete = screen.getByText('Lembrete 1');
    fireEvent.mouseOver(lembrete);

    expect(screen.getByText('Descrição do lembrete 1')).toBeInTheDocument();
  });

  it('deve chamar onExcluirLembrete ao clicar no botão de exclusão', () => {
    const mockExcluir = jest.fn();
    render(<ListarLembretes lembretes={lembretesMock} onExcluirLembrete={mockExcluir} />);

    const excluirButton = screen.getAllByRole('button', { name: /x/i })[0];
    fireEvent.click(excluirButton);

    expect(mockExcluir).toHaveBeenCalledWith(1);
  });

  it('não deve quebrar se a lista de lembretes estiver vazia', () => {
    render(<ListarLembretes lembretes={[]} onExcluirLembrete={jest.fn()} />);

    expect(screen.queryByRole('list')).not.toBeInTheDocument();
    expect(screen.queryByText(/10\/01\/2025/i)).not.toBeInTheDocument();
  });
});
