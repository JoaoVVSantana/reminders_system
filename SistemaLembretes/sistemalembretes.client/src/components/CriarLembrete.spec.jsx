import { render, fireEvent, screen } from '@testing-library/react';
import axios from 'axios';
import CriarLembrete from './CriarLembrete';

jest.mock('axios');

describe('CriarLembrete Component', () => {
  it('deve renderizar os campos de input corretamente', () => {
    render(<CriarLembrete onLembreteCriado={jest.fn()} />);
    expect(screen.getByPlaceholderText('Título')).toBeInTheDocument();
    expect(screen.getByPlaceholderText('Descrição (opcional)')).toBeInTheDocument();
    expect(screen.getByRole('button', { name: /criar/i })).toBeInTheDocument();
  });

  it('deve exibir erro quando campos obrigatórios não forem preenchidos', () => {
    render(<CriarLembrete onLembreteCriado={jest.fn()} />);
    fireEvent.click(screen.getByRole('button', { name: /criar/i }));
    expect(screen.getByText('Os campos de Titulo e Data sao obrigatorios.')).toBeInTheDocument();
  });

  it('deve enviar requisição ao servidor e limpar os campos após criação', async () => {
    const mockOnLembreteCriado = jest.fn();
    axios.post.mockResolvedValueOnce({ data: { id: 1, titulo: 'Teste', dataLembrete: '2025-01-01', descricao: '' } });

    render(<CriarLembrete onLembreteCriado={mockOnLembreteCriado} />);

    fireEvent.change(screen.getByPlaceholderText('Título'), { target: { value: 'Teste' } });
    fireEvent.change(screen.getByPlaceholderText('Descrição (opcional)'), { target: { value: 'Descrição' } });
    fireEvent.change(screen.getByPlaceholderText('Data'), { target: { value: '2025-01-01' } });

    fireEvent.click(screen.getByRole('button', { name: /criar/i }));

    expect(axios.post).toHaveBeenCalledWith('/api/lembrete/criarLembrete', {
      titulo: 'Teste',
      dataLembrete: '2025-01-01',
      descricao: 'Descrição',
    });

    expect(await screen.findByText('Lembrete criado com sucesso!')).toBeInTheDocument();
    expect(screen.getByPlaceholderText('Título').value).toBe('');
    expect(screen.getByPlaceholderText('Descrição (opcional)').value).toBe('');
    expect(screen.getByPlaceholderText('Data').value).toBe('');
  });

  it('deve exibir mensagem de erro ao receber erro do servidor', async () => {
    axios.post.mockRejectedValueOnce({ response: { data: { message: 'Erro no servidor', details: '' } } });

    render(<CriarLembrete onLembreteCriado={jest.fn()} />);

    fireEvent.change(screen.getByPlaceholderText('Título'), { target: { value: 'Teste' } });
    fireEvent.change(screen.getByPlaceholderText('Data'), { target: { value: '2025-01-01' } });

    fireEvent.click(screen.getByRole('button', { name: /criar/i }));

    expect(await screen.findByText('Erro no servidor')).toBeInTheDocument();
  });
});
