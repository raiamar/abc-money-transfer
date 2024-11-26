using ABC.DTOs.Transaction;
using ABC.Models.Entities;
using ABC.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ABC.Controllers;

public class TransactionController : Controller
{
    private readonly IExchangeRateService _exchangeRateService;
    private readonly ITransactionService _transactionService;
    public TransactionController(IExchangeRateService exchangeRateService, ITransactionService transactionService)
    {
        _exchangeRateService = exchangeRateService;
        _transactionService = transactionService;
    }
    public async Task<IActionResult> ShowExchangeRate(string? fromDate, string? toDate, int page = 1)
    {
        int pageSize = 25;
        var response = await _exchangeRateService.GetExchangeRatesAsync(fromDate, toDate);

        if (response == null || response.Data == null || !response.Data.Payload.Any())
        {
            ViewBag.Message = "No exchange rates found for the selected date range.";
            return View();
        }

        var sortedData = response.Data.Payload
        .OrderBy(p => p.Date)
        .ToList();

        var paginatedData = sortedData
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalPages = (int)Math.Ceiling(sortedData.Count / (double)pageSize);
        ViewBag.FromDate = fromDate;
        ViewBag.ToDate = toDate;

        return View(paginatedData);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new TransactionDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TransactionDto transactionDto)
    {
        if (ModelState.IsValid)
        {
            var currentDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
            var exchangeRates = await _exchangeRateService.GetExchangeRatesAsync(currentDate, currentDate);

            var exchangeRate = exchangeRates.Data.Payload
                                    .SelectMany(p => p.Rates)
                                    .FirstOrDefault(r => r.Currency.Iso3 == "MYR")?.Buy;

            var transaction = new Transaction
            {
                TransactionId = Guid.NewGuid().ToString(),
                SenderFirstName = transactionDto.SenderFirstName,
                SenderMiddleName = transactionDto.SenderMiddleName,
                SenderLastName = transactionDto.SenderLastName,
                SenderAddress = transactionDto.SenderAddress,
                SenderCountry = transactionDto.SenderCountry,
                ReceiverFirstName = transactionDto.ReceiverFirstName,
                ReceiverMiddleName = transactionDto.ReceiverMiddleName,
                ReceiverLastName = transactionDto.ReceiverLastName,
                ReceiverAddress = transactionDto.ReceiverAddress,
                ReceiverCountry = transactionDto.ReceiverCountry,
                TransferAmount = transactionDto.TransferAmountMYR,
                ExchangeRate = decimal.Parse(exchangeRate),
                PayoutAmount = decimal.Parse(exchangeRate) * transactionDto.TransferAmountMYR,
                BankName = transactionDto.BankName,
                AccountNumber = transactionDto.AccountNumber,
                CreatedAt = DateTime.UtcNow,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
        };

            await _transactionService.AddTransactionAsync(transaction);

            //impliment payment logic for actual transaction
            TempData["SuccessMessage"] = "Transaction success!";
            return RedirectToAction("Index", "Home");
        }

        return View(transactionDto);
    }

    public IActionResult Report(int page = 1)
    {
        return View();
    }
}
