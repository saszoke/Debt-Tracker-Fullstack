using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;
using webapi.Models.DTOs;

namespace webapi.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly webapiContext _context;

        public TransactionsController(webapiContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransaction()
        {
          if (_context.Transaction == null)
          {
              return NotFound();
          }
            return await _context.Transaction.ToListAsync();
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
          if (_context.Transaction == null)
          {
              return NotFound();
          }
            var transaction = await _context.Transaction.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        [HttpGet("Accounts/{accountId}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> Transactions(int accountId)
        {
            var account = await _context.Account.FindAsync(accountId);
            if (account == null) return NotFound();
            var transactions = await _context.Transaction.Where(transaction => transaction.AccountId == accountId).ToListAsync();
            return Ok(transactions);
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, TransactionDto transaction)
        {
            if (id != transaction.TransactionId) return BadRequest();
            var transactionDb = await _context.Transaction.FindAsync(id);
            transactionDb.Amount = transaction.Amount;
            transactionDb.TransactionDate = transaction.TransactionDate;
            transactionDb.IsIncoming = transaction.IsIncoming;
            await _context.SaveChangesAsync();


            return CreatedAtAction("GetTransaction", new { id = transactionDb.Id }, transactionDb);
        }

        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(TransactionDto transaction)
        {
            if (_context.Transaction == null)
            {
                return Problem("Entity set 'webapiContext.Transaction'  is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var account = _context.Account.Find(transaction.AccountId);
            if(account == null)
            {
                return NotFound();
            }

            Transaction newTransaction = new Transaction();
            newTransaction.Amount = transaction.Amount;
            newTransaction.TransactionDate = transaction.TransactionDate;
            newTransaction.IsIncoming = transaction.IsIncoming;
            newTransaction.Account = account;

            _context.Transaction.Add(newTransaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaction", new { id = newTransaction.Id }, newTransaction);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            if (_context.Transaction == null)
            {
                return NotFound();
            }
            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transaction.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionExists(int id)
        {
            return (_context.Transaction?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
