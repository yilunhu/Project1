using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using abcBank.Data;
using abcBank.Models;

namespace abcBank.Controllers
{
   
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static int CustomerUniqueid;

        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Account> GetAccounts(int id)
        {
            var CustAccounts = new List<Account>() { };
            foreach (var item in _context.Accounts)
            {
                if (Convert.ToInt32(item.CustomerID) == id)
                {
                    CustAccounts.Add(item);
                }
            }
            return CustAccounts;
        }
        // GET: Accounts
        public IActionResult Index(int id)
        {
            var x = new List<Account>();
           
            if(id == 0)
            {
                x = GetAccounts(CustomerUniqueid);
                return View(x);
            }
            else
            {
                CustomerUniqueid = id;
            }

            return View(x);
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }
            switch (account.AccountType.ToString())
            {
                case "Checking":
                    return View(account);
                case "Business":
                    return RedirectToAction(nameof(Business),account);
                case "Loan":
                    return RedirectToAction(nameof(Loan), account);
                case "TermDeposit":
                    return RedirectToAction(nameof(TermDeposit), account);
            }
            return View(account);
        }
        public IActionResult Business(Account account)
        {
            return View(account);

        }
        public IActionResult Loan(Account account)
        {
            return View(account);

        }
        public IActionResult TermDeposit(Account account)
        {
            return View(account);

        }

        // GET: Accounts/Create
        public IActionResult Create()
        {                    
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountType,Balance")] Account account)
        {
            
            if (ModelState.IsValid)
            {
                account.CustomerID = CustomerUniqueid;
                account.LastAccess = DateTime.Now;
                account.overdraft = 0;
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,AccountType,Balance,overdraft,LastAccess")] Account account)
        {
            if (id != account.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    account.Balance -= 50;
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }
        //get
        public async Task<IActionResult> Transfer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Transfer(int id, int UserId, decimal transfer)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            var account1 = await _context.Accounts
                .FirstOrDefaultAsync(m => m.AccountId == UserId);
            if (id != account.AccountId || UserId != account1.AccountId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    account1.Balance += transfer;
                    account.LastAccess = DateTime.Now;
                    account.Balance -= transfer;
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return ViewBag(account, account1);
        }
        //get
        public async Task<IActionResult> Deposit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }
       
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deposit(int id, decimal deposit)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (id != account.AccountId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    account.LastAccess = DateTime.Now;
                    account.Balance = account.Balance + deposit;
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }
        //get
        public async Task<IActionResult> Withdraw(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdraw(int id, decimal withdraw)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (id != account.AccountId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    account.LastAccess = DateTime.Now;
                    account.Balance = account.Balance - withdraw;                   
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }
        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }
    }
}
