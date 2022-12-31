# Transferencia API
---
## Tabelas utilizadas

### Usuario
A primeira tabela utilizada, esta utilizada para cadastro e login dos usuários, é nomeada usuario, com:
 * id - esse, uma chave primária, gerado automaticamente;
 * nome - Nome do usuário, esse item pode ser repetir;
 * email - Esse item é único, porém isso é definido via API;
 * senha - Senha atrelada a cada usuário.

<img src="https://github.com/GabrielP0rt0/GabrielP0rt0/blob/109c30b09fcd57e80b434a5203de5a5a68a6adde/imagens_readme/usuario.png"/>

---
### Conta
A segunda tabela utilizada, esta para login e transferências, é a tabela conta que por sua vez possuí:
 * id - chave primária gerada automaticamente para identificar o usuário dentro desta tabela;
 * id_usuario_private - id que o usuário receberá para utilização do método de transferências;
 * id_usuario_public - id que deverá chegar ao usuário final através do frontend e para onde deve ser direcionado as transações;
 * saldo - o saldo disponível na conta do usuário.
 
<img src="https://github.com/GabrielP0rt0/GabrielP0rt0/blob/109c30b09fcd57e80b434a5203de5a5a68a6adde/imagens_readme/conta.png"/>

---
### Extrato
A terceira tabela utilizada, esta para consulta de extrato, é a tabela extrato que por sua vez possuí:
 * id - a identificação de cada transação feita, chave primária gerada automaticamente;
 * id_public_origem - identificação pública da conta a qual fez a transferêcia;
 * id_public_destino - identificação pública da conta a qual recebeu a transfêrencia;
 * valor - valor o qual foi transferido da origem para o destino;
 * data_hora - data e a hora na qual a transferência ocorreu.
 
 <img src="https://github.com/GabrielP0rt0/GabrielP0rt0/blob/109c30b09fcd57e80b434a5203de5a5a68a6adde/imagens_readme/extrato.png"/>
 
 
