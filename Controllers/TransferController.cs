using InfiCare.Domain.Entities;
using InfiCare.Infrastructure.Persistence;
using InfiCare.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace InfiCare.Controllers;

public class TransferController : Controller
{
    private readonly ApplicationDbContext _context;
    public TransferController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(TransferViewModel model)
    {
        if (ModelState.IsValid)
        {
            PersonDetails sender = new()
            {
                FirstName = model.Sender.FirstName,
                LastName = model.Sender.LastName,
                MiddleName = model.Sender.MiddleName,
                Address = model.Sender.Address,
                Country = model.Sender.Country,
            };

            PersonDetails receiver = new()
            {
                FirstName = model.Receiver.FirstName,
                LastName = model.Receiver.LastName,
                MiddleName = model.Receiver.MiddleName,
                Address = model.Receiver.Address,
                Country = model.Receiver.Country,
            };

            PaymentDetails payment = new()
            {
                BankName = model.PaymentDetails.BankName,
                AccountNumber = model.PaymentDetails.AccountNumber,
                TransferAmount = model.PaymentDetails.TransferAmount,
                ExchangeRate = model.PaymentDetails.ExchangeRate,
                PayoutAmount = model.PaymentDetails.PayoutAmount,
            };

            Transaction dto = new()
            {
                Created = DateTime.UtcNow,
                Status = "Pending",
                Remark = model.Remarks,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty,
                Sender = JsonSerializer.Serialize(sender),
                Receiver = JsonSerializer.Serialize(receiver),
                PaymentDetail = JsonSerializer.Serialize(payment)
            };

            _context.Transactions.Add(dto);
            await _context.SaveChangesAsync();

            return RedirectToAction("Success");
        }
        return View(model);
    }

    public IActionResult Success()
    {
        return View();
    }
}
