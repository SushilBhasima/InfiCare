using InfiCare.Common;
using InfiCare.Common.Interface;
using InfiCare.Common.Service;
using InfiCare.Migrations;
using InfiCare.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InfiCare.Controllers;

public class AdminController : Controller
{
    private readonly ILogger _logger;
    private readonly ITransactionServices _transactionServices;

    public AdminController(
        ILogger<AdminController> logger,
        ITransactionServices transactionServices)
    {
        _logger = logger;
        _transactionServices = transactionServices;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> ExchangeRate(Exception ex, int? page, int? per_page, string from, string to)
    {
        _logger.LogInformation("Get Exchange Rate.");
        try
        {
            page ??= 1;
            per_page ??= 5;
            if (string.IsNullOrEmpty(from))
                from = DateTime.UtcNow.ToString("yyyy-MM-dd");

            if (string.IsNullOrEmpty(to))
                to = DateTime.UtcNow.ToString("yyyy-MM-dd");

            var exchangeRateApi = new ExchangeRateAPIService("https://www.nrb.org.np"); // Replace with your API base URL
            var response = await exchangeRateApi.GetExchangeRateData(page.Value, per_page.Value, from, to);
            if (response is not null && response?.Data?.Payload is not null)
            {
                // Extract the exchange rate data from the payload
                var exchangeRates = response.Data.Payload
                    .SelectMany(p => p.Rates)
                    .ToList();


                ViewBag.FromDate = from;
                ViewBag.ToDate = to;
                ViewBag.Page = page;
                ViewBag.PerPage = per_page;

                return View("ExchangeRate", exchangeRates); // Pass exchangeRates to the view

            }

            ModelState.AddModelError("", "Failed to retrieve rate data.");
            return View("Error");
        }
        catch (Exception)
        {
            _logger.LogError(message: ex.Message);
            // Handle any unexpected errors (e.g., network issues)
            ModelState.AddModelError("", "An unexpected error occurred: " + ex.Message);
            return View("Error");
        }
    }

    [HttpGet]
    public async Task<ActionResult> TransactionHistory(DateQueryParam? date, PaginationQueryParams? pagination)
    {
        date ??= null;

        if (date?.StartDate is null || date.EndDate is null)
            date = null;
        // Filter transactions by date range
        var transaction = _transactionServices.GetTransactionList(User.FindFirstValue(ClaimTypes.NameIdentifier), date, pagination);
        TransactionHistoryViewModel result = TransactionHistoryViewModel.FromTransactionListDto(transaction);
        if (date is not null && date.StartDate is not null)
            ViewBag.FromDate = date?.StartDate;
        if (date is not null && date.EndDate is not null)
            ViewBag.ToDate = date?.EndDate;
        return View(result);
    }

}
