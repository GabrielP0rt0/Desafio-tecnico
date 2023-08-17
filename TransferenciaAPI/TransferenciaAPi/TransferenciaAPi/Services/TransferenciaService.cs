using System.Data;
using TransferenciaAPi.Models;
using TransferenciaAPi.Data;
using Microsoft.AspNetCore.Mvc;
using TransferenciaAPi.Core.ResponseBase;
using TransferenciaAPi.Data.Entidades;
using Microsoft.EntityFrameworkCore;
using TransferenciaAPi.Errors;

namespace TransferenciaAPi.Services
{
    public class TransferenciaService : SingleResponse
    {
        private DBContext _context;
        public TransferenciaService(DBContext dbContext)
        {
            _context = dbContext;
        }
        /// <summary>
        /// A função de consulta ao histórico busca os dados contendo todas as transações feitas a partir do ID de origem
        /// </summary>
        /// <param name="ConsultarExtrato"></param>
        /// <returns></returns>
        public async Task<IActionResult> ConsultStatement(int idUser)
        {
            if (idUser <= 0)
                return ErrorResponse(TransferenciaErrors.UserEmpty);

            Account? account = await _context.Account.Where(x => x.IdUser.Equals(idUser)).FirstOrDefaultAsync();
            if (account == null)
                return ErrorResponse(TransferenciaErrors.AccountNotFound);

            List<Statement> statement = await _context.Statement.Where(x => x.KeySourceAccount.Equals(account.TransferKey) || x.KeyDestinationAccount.Equals(account.TransferKey)).ToListAsync();
            return SuccessResponse(statement);
        }
        /// <summary>
        /// Esta função é utilizada para executar a transferência de saldo entre usuários. A função recebe as informações do requerimento:
        /// id de origem, id de destino e valor. Através desses itens é gerado uma operação a qual fica salva no banco de dados Extrato e
        /// as informações de saldo no banco de dados Conta dos usuários é atualizado
        /// </summary>
        /// <param name="Transferir"></param>
        /// <returns></returns>
        public async Task<IActionResult> Transfer(TransferRequestModel request)
        {
            (bool isValid, string err) = request.Validate();
            if (!isValid)
                return ErrorResponse(err);

            Account? sourceAccount = await _context.Account.Where(x => x.IdUser.Equals(request.IdUser)).FirstOrDefaultAsync();

            if (sourceAccount == null)
                return ErrorResponse(TransferenciaErrors.AccountNotFound);

            Account? destinationAccount = await _context.Account.Where(x => x.TransferKey.Equals(request.KeyDestiny)).FirstOrDefaultAsync();
            if (destinationAccount == null)
                return ErrorResponse(TransferenciaErrors.DestinyAccountNotFound);
            //verifica se é possivel fazer a transferência (se o usuário possui saldo o suficiente).
            decimal newBalance = sourceAccount.Amount - request.Amount;
            if (newBalance < 0)
                return ErrorResponse(TransferenciaErrors.InsufficientBalance);

            sourceAccount.Amount = newBalance;
            sourceAccount.Updated = DateTime.Now;
            destinationAccount.Amount += request.Amount;
            destinationAccount.Updated = DateTime.Now;

            await _context.Statement.AddAsync(new()
            {
                KeySourceAccount = sourceAccount.TransferKey,
                KeyDestinationAccount = request.KeyDestiny,
                Amount = request.Amount,
                Created = DateTime.Now,
                Updated = DateTime.Now
            });
            await _context.SaveChangesAsync();

            return SuccessResponse();

        }
    }
}
