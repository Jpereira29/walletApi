using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Collections;
using WalletApi.Context;
using WalletApi.Models;
using WalletApi.Repository;

namespace WalletApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IUnitOfWork _uof;

        public WalletController(IUnitOfWork context)
        {
            _uof = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection>> GetAsync()
        {
            return await _uof.WalletRepository.Get().ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ICollection>> GetAsyncByCode(int id)
        {
            var wallet = await _uof.WalletRepository.GetWalletWithOperation(p => p.WalletId == id);
            if (wallet == null)
            {
                return BadRequest("Carteira não encontrada!");
            }
            return Ok(wallet);
        }

        [HttpPost]
        public async Task<ActionResult<Wallet>> PostAsync(Wallet wallet)
        {
            try
            {
                _uof.WalletRepository.Add(wallet);
                await _uof.Commit();

                if (wallet.Value > 0)
                {
                    _uof.OperationRepository.Add(new Operation
                    {
                        Value = wallet.Value,
                        Description = "Valor inicial",
                        Date = DateTime.Now,
                        Type = Enums.OperationType.Deposit,
                        WalletId = wallet.WalletId,
                    });
                    await _uof.Commit();
                }
                return Ok(wallet);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Ocorreu um problema ao tratar a sua solicitação." + e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Wallet>> PutAsync(Wallet wallet, int id)
        {
            try
            {
                if (wallet.WalletId != id)
                {
                    return BadRequest();
                }

                _uof.WalletRepository.Update(wallet);
                await _uof.Commit();
                return Ok("Carteira atualizada.");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Ocorreu um problema ao tratar a sua solicitação." + e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var isWallet = await _uof.WalletRepository.GetByCode(p => p.WalletId == id);
                if (isWallet == null)
                {
                    return BadRequest("Carteira não encontrada!");
                }
                _uof.WalletRepository.Delete(isWallet);
                await _uof.Commit();
                return Ok("Carteira deletada.");

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Ocorreu um problema ao tratar a sua solicitação." + e.Message);
            }
        }

        [HttpPost("Withdraw")]
        public async Task<ActionResult> Withdraw(Operation operation)
        {
            try
            {
                Wallet wallet = await _uof.WalletRepository.GetByCode(p => p.WalletId == operation.WalletId);
                if (wallet == null)
                {
                    return BadRequest("Carteira não encontrada.");
                }
                wallet.Value -= operation.Value;

                _uof.WalletRepository.Update(wallet);
                operation.Date = DateTime.Now;
                operation.Date.ToString("dd/MM/yyyy HH:mm");
                _uof.OperationRepository.Add(operation);
                await _uof.Commit();

                return Ok("Saque efetuado.");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Ocorreu um problema ao tratar a sua solicitação." + e.Message);
            }
        }

        [HttpPost("Deposit")]
        public async Task<ActionResult> Deposit(Operation operation)
        {
            try
            {
                Wallet wallet = await _uof.WalletRepository.GetByCode(p => p.WalletId == operation.WalletId);
                if (wallet == null)
                {
                    return BadRequest("Carteira não encontrada.");
                }
                wallet.Value += operation.Value;

                _uof.WalletRepository.Update(wallet);
                operation.Date = DateTime.Now;
                operation.Date.ToString("dd/MM/yyyy HH:mm");
                _uof.OperationRepository.Add(operation);
                await _uof.Commit();

                return Ok("Depósito efetuado.");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Ocorreu um problema ao tratar a sua solicitação." + e.Message);
            }
        }
    }
}
