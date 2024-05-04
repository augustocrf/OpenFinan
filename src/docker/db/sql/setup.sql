USE 'openfinan';

CREATE TABLE Cliente (
    cpf int not null,
    nome varchar(250) not null,
    uf varchar(2) not null,
    celular varchar(11) not null,
    PRIMARY KEY(cpf)
) ENGINE=INNODB;

CREATE TABLE TipoFinanciamento (
    idTipoFinanciamento INT NO NULL AUTO_INCREMENT,
    descricao varchar(255) NOT NULL,
    taxa DECIMAL NOT NULL,
    PRIMARY KEY(idTipoFinanciamento)
) ENGINE=INNODB;