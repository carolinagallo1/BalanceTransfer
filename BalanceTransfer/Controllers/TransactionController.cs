using BalanceTransfer.Interfaces;
using BalanceTransfer.Models;
using BalanceTransfer.Request;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace BalanceTransfer.Controllers
{
    [ApiController]
    [Route("/Transacciones")]
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IWalletRepository _walletRepository;
        public TransactionController(ITransactionRepository transactionRepository, IWalletRepository walletRepository) {
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            try {
                return Ok(await _transactionRepository.GetAllAsync());
            } catch (Exception) {
                return StatusCode(500, "Error al procesar la transacción.");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            try {
                if (id <= 0)
                    return BadRequest(new { message = "El ID debe ser mayor a 0." });
                var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
                if (transaction == null)
                    return NotFound(new { message = "Billetera no encontrada." });
                return Ok(transaction);
            } catch (Exception ex) {
                return StatusCode(500, "Error al procesar la transacción.");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create(TransferRequest request) {
            try {
                if(request.WalletIdTo.Equals(request.WalletIdFrom))
                    return BadRequest("La billeteras no pueden ser iguales");
                if (request.Amount <= 0)
                    return BadRequest("La cantidad debe ser mayor a cero");
                var walletFrom = await _walletRepository.GetWalletByIdAsync(request.WalletIdFrom);
                if (walletFrom == null)
                    return BadRequest(String.Format("La billetera con Id: {0} no existe", request.WalletIdFrom));

                var walletTo = await _walletRepository.GetWalletByIdAsync(request.WalletIdTo);
                if (walletTo == null)
                    return BadRequest(String.Format("La billetera con Id: {0} no existe", request.WalletIdTo));

                if (request.Amount > walletFrom.Balance)
                    return BadRequest("No tiene el saldo suficiente");

                var debitTransaction = new Transactions() {
                    WalletId = walletFrom.Id,
                    Amount = request.Amount,
                    Type = "Debit"
                };

                var CreditTransaction = new Transactions() {
                    WalletId = walletTo.Id,
                    Amount = request.Amount,
                    Type = "Credit"
                };

                walletFrom.Balance -= request.Amount;
                walletTo.Balance += request.Amount;
                walletTo.UpdatedAt = walletFrom.UpdatedAt = DateTime.UtcNow;

                await _transactionRepository.CreateTransactionAsync(debitTransaction);
                await _transactionRepository.CreateTransactionAsync(CreditTransaction);
                await _walletRepository.UpdateWalletAsync(walletTo);
                await _walletRepository.UpdateWalletAsync(walletFrom);
                return Ok("Transacción exitosa");
            } catch (Exception ex) {
                return StatusCode(500, "Error al procesar la transacción.");
            }

        }
    }
}
