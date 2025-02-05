using GuestBook.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuestBook.Controllers;

public class GuestBookController(GuestBookContext context, IWebHostEnvironment environment) : Controller
{
    public async Task<IActionResult> Index()
    {
        var entries = await context.GuestBooks.OrderByDescending(e => e.DatePosted).ToListAsync();
        return View(entries);
    }

    [HttpPost]
    public async Task<IActionResult> AddEntry(string name, string message, IFormFile photo)
    {
        string? photoPath = null;
        
        if (photo is { Length: > 0 })
        {
            var uploadsFolder = Path.Combine(environment.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder);
            var filePath = Path.Combine(uploadsFolder, photo.FileName);
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }
            photoPath = "/uploads/" + photo.FileName;
        }

        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(message))
        {
            context.GuestBooks.Add(new Core.Entities.GuestBook { Name = name, Message = message, PhotoPath = photoPath });
            await context.SaveChangesAsync();
        }
        
        return RedirectToAction("Index");
    }
}