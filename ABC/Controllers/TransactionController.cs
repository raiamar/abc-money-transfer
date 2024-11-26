using ABC.DTOs.Transaction;
using ABC.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ABC.Controllers;

public class TransactionController : Controller
{
    private readonly IExchangeRateService _exchangeRateService;
    public TransactionController(IExchangeRateService exchangeRateService)
    {
        _exchangeRateService = exchangeRateService;
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

    public IActionResult Create()
    {
        // Render a form for creating transactions
        return View();
    }

    public IActionResult Report(int page = 1)
    {
        // Implement pagination and filtering for transaction reports
        return View();
    }
}
