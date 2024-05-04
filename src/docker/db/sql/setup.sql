USE 'openfinan';

CREATE TABLE Cliente (
    cpf int not null,
    nome varchar(250) not null,
    uf varchar(2) not null,
    celular varchar(11) not null
)
