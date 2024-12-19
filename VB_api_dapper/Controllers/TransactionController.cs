using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VB_api.Models;
using VB_api.Services.Interfaces;

namespace VB_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> AllTransactionHistory()
        {
            try
            {
                var transactions = await _transactionService.GetAllTransactionsAsync();
                return Ok(new { transactions });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{accountID}")]
        public async Task<IActionResult> AccountTransactionHistory([FromRoute] long accountID)
        {
            try
            {
                var transactions = await _transactionService.GetAccountTransactionsAsync(accountID);
                return Ok(new { transactions });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> TransactionHistory()
        {
            try
            {
                var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var customerId = long.Parse(customerIdClaim);
                var transactions = await _transactionService.GetCustomerTransactionsAsync(customerId);
                return Ok(new { transactions });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{accountID}")]
        public async Task<IActionResult> CheckBalance([FromRoute] long accountID)
        {
            try
            {
                var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var customerId = long.Parse(customerIdClaim);
                var balance = await _transactionService.GetAccountBalanceAsync(customerId, accountID);
                return Ok(new { accountBalance = balance });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Deposit([FromBody] TransactionRequest request)
        {
            try
            {
                var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var customerId = long.Parse(customerIdClaim);
                var result = await _transactionService.DepositAsync(request, customerId);
                return Ok(new
                {
                    message = "Para yatırma işlemi başarılı!", 
                    transactionId = result.TransactionID,
                    newBalance = result.NewBalance
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Withdraw([FromBody] TransactionRequest request)
        {
            try
            {
                var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var customerId = long.Parse(customerIdClaim);
                var result = await _transactionService.WithdrawAsync(request, customerId);
                return Ok(new
                {
                    message = "Para çekme işlemi başarılı!",
                    transactionId = result.TransactionID,
                    newBalance = result.NewBalance
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> TransferMoney([FromBody] TransferRequest request)
        {
            try
            {
                var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var customerId = long.Parse(customerIdClaim);
                var result = await _transactionService.TransferMoneyAsync(request, customerId);
                return Ok(new
                {
                    message = "Transfer işlemi başarılı!",
                    transferID = result.TransactionID,
                    fromAccountNewBalance = result.SourceNewBalance,
                    toAccountNewBalance = result.TargetNewBalance
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
