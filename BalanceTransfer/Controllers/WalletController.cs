using BalanceTransfer.Interfaces;
using BalanceTransfer.Models;
using BalanceTransfer.Request;
using BalanceTransfer.Server.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BalanceTransfer.Controllers
{
    
    [ApiController]
    [Route("/Billetera")]
    public class WalletController : Controller {
        private readonly IWalletRepository _walletRepository;

        public WalletController(IWalletRepository walletRepository) {
                _walletRepository = walletRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            try {
                return Ok(await _walletRepository.GetAllAsync());
            }
            catch (Exception) {
                return StatusCode(500, "Error al procesar la billetera.");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            try {
                if (id <= 0)
                    return BadRequest("El ID debe ser mayor a 0.");
                var wallet = await _walletRepository.GetWalletByIdAsync(id);
                if (wallet == null)
                    return NotFound("Billetera no encontrada.");
                return Ok(wallet);
            }
            catch (Exception ex) {
                return StatusCode(500, "Error al procesar la billetera.");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Create(WalletRequest request) {
            try {
                if(string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.DocumentId))
                    return BadRequest("Debe llenar todos los datos");
                var wallet = new Wallet() {
                    Name = request.Name,
                    DocumentId = request.DocumentId,
                    Balance = request.Balance
                };
                await _walletRepository.CreateWalletAsync(wallet);
                return Ok("Billetera creada exitosamente");
            }
            catch (Exception ex) {
                return StatusCode(500, "Error al procesar la billetera.");
            }
            
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id) {
            try {
                var wallet = _walletRepository.GetWalletByIdAsync(id) != null;
                if (!wallet)
                    return NotFound("Billetera no encontrada.");

                await _walletRepository.DeleteWalletAsync(id);
                return Ok("Billetera borrada exitosamente");
            }
            catch (Exception) {
                return StatusCode(500, "Error al procesar la billetera.");
            }
            
        }

        [HttpPut]
        public async Task<ActionResult> Update(WalletRequest request) {
            try {
                if(request.Id == 0)
                    return BadRequest("El Id debe ser diferente de 0");
                var wallet = await _walletRepository.GetWalletByIdAsync(request.Id);
                if (wallet != null) {
                    if(!string.IsNullOrEmpty(request.Name)) wallet.Name = request.Name;
                    if(!string.IsNullOrEmpty(request.DocumentId)) wallet.Name = request.DocumentId;
                    wallet.Balance = wallet.Balance;
                    wallet.UpdatedAt = DateTime.Now;
                    await _walletRepository.UpdateWalletAsync(wallet);
                    return Ok("Billetera actualizada exitosamente");
                }
                return NotFound("Billetera no encontrada.");
            }
            catch (Exception ex) {
                return StatusCode(500, "Error al procesar la billetera.");
            }       
        }
    }
}
