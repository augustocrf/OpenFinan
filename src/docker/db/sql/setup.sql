USE 'openfinan';

CREATE TABLE Cliente (
    cpf int not null,
    nome varchar(250) not null,
    uf varchar(2) not null,
    celular varchar(11) not null,
    PRIMARY KEY(cpf)
) ENGINE=INNODB;

CREATE TABLE TipoFinanciamento (
    idTipoFinanciamento INT NOT NULL,
    descricao varchar(255) NOT NULL,
    taxa DECIMAL NOT NULL,
    PRIMARY KEY(idTipoFinanciamento)
) ENGINE=INNODB;

CREATE TABLE Financiamento (
    idfinanciamento INT NOT NULL AUTO_INCREMENT,
    cpf INT NOT NULL,
    valortotal DECIMAL NOT NULL,
    dataultimovencimento DATETIME NOT NULL,
    idtipofinanciamento INT NOT NULL,
    PRIMARY KEY(idfinanciamento),
    FOREIGN KEY (cpf)
        REFERENCES Cliente(cpf),
    FOREIGN KEY (idtipofinanciamento)
        REFERENCES TipoFinanciamento(idTipoFinanciamento)
) ENGINE=INNODB;

INSERT INTO TipoFinanciamento (id, descricao, taxa) VALUES (1, 'Crédito Direto', 2)
INSERT INTO TipoFinanciamento (id, descricao, taxa) VALUES (2, 'Crédito Consignado', 1)
INSERT INTO TipoFinanciamento (id, descricao, taxa) VALUES (3, 'Crédito Pessoa Jurídica', 5)
INSERT INTO TipoFinanciamento (id, descricao, taxa) VALUES (4, 'Crédito Pessoa Física', 3)
INSERT INTO TipoFinanciamento (id, descricao, taxa) VALUES (1, 'Crédito Imobiliário', 9)