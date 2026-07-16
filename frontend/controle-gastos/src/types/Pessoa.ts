export interface Pessoa {
    id: number;
    nome: string;
    idade: number;
}

export interface PessoaRequest {
    nome: string;
    dataNascimento: string;
}
