using System.Security.Claims;
using ArtikelKu.Api.Data;
using ArtikelKu.Api.Dtos;
using ArtikelKu.Api.Hubs;
using ArtikelKu.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ArtikelKu.Api.Controllers;

[ApiController]
[Route("api/articles")]
public class ArticlesController(AppDbContext db, IHubContext<ArticleHub> hub) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArticleDto>>> GetAll()
    {
        var articles = await db.Articles
            .AsNoTracking()
            .OrderByDescending(article => article.CreatedAt)
            .Select(article => ToDto(article))
            .ToListAsync();

        return Ok(articles);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ArticleDto>> GetById(int id)
    {
        var article = await db.Articles
            .AsNoTracking()
            .SingleOrDefaultAsync(candidate => candidate.Id == id);

        return article is null ? NotFound() : Ok(ToDto(article));
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ArticleDto>> Create(CreateArticleRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title)
            || string.IsNullOrWhiteSpace(request.Summary)
            || string.IsNullOrWhiteSpace(request.Content))
        {
            return BadRequest("Title, summary, dan content wajib diisi");
        }

        var authorName = User.FindFirstValue(ClaimTypes.Name);
        if (string.IsNullOrWhiteSpace(authorName))
        {
            return Unauthorized();
        }

        var article = new Article
        {
            Title = request.Title.Trim(),
            Summary = request.Summary.Trim(),
            Content = request.Content.Trim(),
            AuthorName = authorName
        };

        db.Articles.Add(article);
        await db.SaveChangesAsync();

        var articleDto = ToDto(article);

        await hub.Clients.All.SendAsync("ArticleCreated", articleDto);

        return CreatedAtAction(nameof(GetById), new { id = article.Id }, articleDto);
    }

    private static ArticleDto ToDto(Article article) =>
        new(
            article.Id,
            article.Title,
            article.Summary,
            article.Content,
            article.AuthorName,
            DateTime.SpecifyKind(article.CreatedAt, DateTimeKind.Utc));
}
