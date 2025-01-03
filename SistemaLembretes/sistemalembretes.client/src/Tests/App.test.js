import React from 'react';
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import App from '../App';
import axios from 'axios';

jest.mock('axios');

beforeEach(() => {
    jest.clearAllMocks();
    axios.get.mockResolvedValue({
        data: [
            { id: 1, titulo: 'Lembrete Atual', dataLembrete: '2025-01-03', descricao: 'Descrição atual' },
            { id: 2, titulo: 'Lembrete Futuro', dataLembrete: '2025-01-04', descricao: 'Descrição futura' },
        ],
    });
});

describe('App Component - Functional Tests', () => {
    test('should create a lembrete with valid inputs', async () => {
        axios.post.mockResolvedValueOnce({
            data: { id: 3, titulo: 'Novo Lembrete', dataLembrete: '2025-01-05', descricao: 'Descrição do novo lembrete' },
        });

        render(<App />);

        fireEvent.change(screen.getByPlaceholderText(/Titulo/i), { target: { value: 'Novo Lembrete' } });
        fireEvent.change(screen.getByPlaceholderText(/Data/i), { target: { value: '2025-01-05' } });
        fireEvent.click(screen.getByText(/Criar/i));

        await waitFor(() => {
            expect(screen.getByText(/Novo Lembrete/i)).toBeInTheDocument();
        });
    });

    test('should not create a lembrete with past date', async () => {
        render(<App />);

        fireEvent.change(screen.getByPlaceholderText(/Titulo/i), { target: { value: 'Lembrete Inválido' } });
        fireEvent.change(screen.getByPlaceholderText(/Data/i), { target: { value: '2020-01-01' } });
        fireEvent.click(screen.getByText(/Criar/i));

        expect(screen.getByText(/Data não pode ser no passado/i)).toBeInTheDocument();
    });

    test('should not create a lembrete without a date', async () => {
        render(<App />);

        fireEvent.change(screen.getByPlaceholderText(/Titulo/i), { target: { value: 'Sem Data' } });
        fireEvent.click(screen.getByText(/Criar/i));

        expect(screen.getByText(/Data é obrigatória/i)).toBeInTheDocument();
    });

    test('should not create a lembrete without a title', async () => {
        render(<App />);

        fireEvent.change(screen.getByPlaceholderText(/Data/i), { target: { value: '2025-01-05' } });
        fireEvent.click(screen.getByText(/Criar/i));

        expect(screen.getByText(/Título é obrigatório/i)).toBeInTheDocument();
    });

    test('should delete a lembrete', async () => {
        axios.delete.mockResolvedValueOnce({});

        render(<App />);

        const deleteButton = await screen.findAllByText(/X/i);
        fireEvent.click(deleteButton[0]);

        await waitFor(() => {
            expect(screen.queryByText(/Lembrete Atual/i)).not.toBeInTheDocument();
        });
    });
});
