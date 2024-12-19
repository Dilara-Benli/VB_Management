using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VB_api.Models;
using VB_api.Services.Interfaces;

namespace VB_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> DisplayAllAccounts()
        {
            try
            {
                var accounts = await _accountService.GetAllAccountsAsync();
                return Ok(new { accounts });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{customerID}")]
        public async Task<IActionResult> DisplayAccounts([FromRoute] long customerID)
        {
            try
            {
                var accounts = await _accountService.GetAccountsByCustomerAsync(customerID);
                return Ok(new { accounts });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAccountsByCustomer()
        {
            try
            {
                var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var customerId = long.Parse(customerIdClaim);

                var accounts = await _accountService.GetAccountsByCustomerAsync(customerId);
                return Ok(new { accounts });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRequest request)
        {
            try
            {
                var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var customerId = long.Parse(customerIdClaim);

                var result = await _accountService.CreateAccountAsync(customerId, request);
                return Ok(new { message = "Hesap başarıyla oluşturuldu!", accountID = result.AccountID });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("{accountID}")]
        public async Task<IActionResult> UpdateAccount(long accountID, [FromBody] AccountRequest request)
        {
            try
            {
                await _accountService.UpdateAccountAsync(accountID, request);
                return Ok(new { message = "Hesap başarıyla güncellendi!", accountID });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{accountID}")]
        public async Task<IActionResult> DeleteAccount(long accountID)
        {
            try
            {
                await _accountService.DeleteAccountAsync(accountID);
                return Ok(new { message = "Hesap silindi!", accountID });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
