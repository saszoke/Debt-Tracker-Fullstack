using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models.DTOs;
using webapi.Models;
using Microsoft.Identity.Client;
using System.Text.Json.Nodes;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.VisualBasic;
using System.IO;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly webapiContext _context;

        public AccountsController(webapiContext context) {
            _context = context;
        }
        // GET: api/<AccountsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> Get()
        {
            List<Account> accounts = await _context.Account.ToListAsync();
            return Ok(accounts);
        }

        [HttpGet("{id}/Balance")]
        public async Task<ActionResult<Account>> GetBalance(int id)
        {
            int balance = 0;
            var account = await _context.Account.FindAsync(id);
            var transactions = await _context.Transaction.Where(transaction => transaction.Account == account).ToListAsync();
            transactions.ForEach(transaction =>
            {
                if (transaction.IsIncoming)
                {
                    balance -= transaction.Amount;
                }
                else
                {
                    balance += transaction.Amount;
                }
            });
            return Ok(new {
                Name = account.Name,
                Id = account.Id,
                Balance = balance,
            });
        }

        // GET api/<AccountsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> Get(int id)
        {
            var account = _context.Account.FindAsync(id);
            if (account == null) return NotFound();
            return Ok(account);
        }

        [HttpPost("uploadImage/{id}")]
        public async Task<IActionResult> UploadImage(int id, IFormFile image)
        {
            try
            {
                var account = await _context.Account.FindAsync(id);
                if (account is null)
                {
                    return BadRequest();
                }
                if (image.FileName == "empty")
                {
                    if(account is null)
                    {
                        return BadRequest();
                    }
                    if(account.ProfilePicture == "002d36ea-21c7-4028-86ed-12674060d512_unknown.jpg")
                    {
                        return Ok(new { message = "No image sent. Default or already existing image will be used.", imageName = "002d36ea-21c7-4028-86ed-12674060d512_unknown.jpg" });
                    }
                    else
                    {
                        return Ok(new { message = "No image sent. Default or already existing image will be used.", imageName = account.ProfilePicture });
                    }
                }

                string uniqueName = Guid.NewGuid().ToString() + "_" + image.FileName;
                account.ProfilePicture = uniqueName;

                var filePath = Path.Combine("wwwroot/images", uniqueName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                _context.SaveChangesAsync();

                return Ok(new { message = "Image uploaded successfully!", imageName = uniqueName });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        // POST api/<AccountsController>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Account>>> Post([FromBody] AccountDto accountDto)
        {
            Account account = await _context.Account.FirstOrDefaultAsync(account => account.Name == accountDto.Name);
            
            var newAccount = new Account()
            {
                Name = accountDto.Name,
                Transactions = new List<Transaction>()
            };
            _context.Account.Add(newAccount);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = newAccount.Id }, newAccount);
        }


        // PUT api/<AccountsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Account>>> Put(int id, [FromBody] AccountDto accountDto)
        {
            Account account = await _context.Account.FindAsync(id);
            if (account != null)
            {
                //_context.Entry(account).State = EntityState.Modified;
                account.Name = accountDto.Name;
                //_context.Update(account)
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        // DELETE api/<AccountsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Account>>> Delete(int id)
        {
            var account = await _context.Account.FindAsync(id);
            if (account == null) return NotFound();
            var filePath = Path.Combine("wwwroot/images", account.ProfilePicture);

            FileInfo file = new FileInfo(filePath);
            file.Delete();
            
            _context.Account.Remove(account);
            _context.SaveChanges();
            return Ok();
        }
    }
}
