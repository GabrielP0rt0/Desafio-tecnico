# Transferencia API

## Tabelas utilizadas

<img src="https://github.com/GabrielP0rt0/GabrielP0rt0/blob/main/imagens_readme/desafio_tecnico.png"/>

>Obs: as tabelas não possuem Foreing Keys, foram adicionadas a este diagrama lógico apenas para simbolizar a sua conexão 

#### criando o DB

```
create database desafiotecnico;
```

### Usuario
A primeira tabela utilizada, esta utilizada para cadastro e login dos usuários, é nomeada user, com:
 * id - esse, uma chave primária, gerado automaticamente para indentificação do registro;
 * name - Nome do usuário, esse item pode ser repetir;
 * email - Esse item é único, porém isso é definido via API;
 * password - Senha atrelada a cada usuário.

```
CREATE TABLE desafiotecnico.user (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(60) NOT NULL,
  `email` varchar(60) NOT NULL,
  `password` varchar(60) NOT NULL,
  `created` datetime NOT NULL,
  `updated` datetime NOT NULL,
  PRIMARY KEY (`id`)
);

```

---
### Conta
A segunda tabela utilizada, esta para login e transferências, é a tabela conta que por sua vez possuí:
 * id - chave primária gerada automaticamente para identificar o usuário dentro desta tabela;
 * id_user - id utilizado para localizar o usuário na tabela user;
 * transfer_key - Guid utilizada como chave de transferencia para o usuário fazer transações;
 * amount - o saldo disponível na conta do usuário.
 
```
CREATE TABLE desafiotecnico.account (
  `id` int NOT NULL AUTO_INCREMENT,
  `id_user` int NOT NULL,
  `transfer_key` varchar(45) NOT NULL,
  `amount` decimal(10,2) NOT NULL,
  `created` datetime NOT NULL,
  `updated` datetime NOT NULL,
  PRIMARY KEY (`id`)
);
```

---
### Extrato
A terceira tabela utilizada, esta para consulta de extrato, é a tabela extrato que por sua vez possuí:
 * id - a identificação de cada transação feita, chave primária gerada automaticamente;
 * key_source_account - identificação da chave de transferencia pertencente a conta origem;
 * key_destination_account - identificação da chave de transferencia pertencente a conta destino;
 * amount - valor o qual foi transferido da origem para o destino;
 
```
CREATE TABLE desafiotecnico.statement (
  `id` int NOT NULL AUTO_INCREMENT,
  `key_source_account` varchar(100) NOT NULL,
  `key_destination_account` varchar(100) NOT NULL,
  `amount` decimal(10,2) NOT NULL,
  `created` datetime NOT NULL,
  `updated` datetime NOT NULL,
  PRIMARY KEY (`id`)
);

```
 
 
