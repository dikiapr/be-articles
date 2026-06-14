namespace ArtikelKu.Api.Dtos;

public record CreateArticleRequest(string Title, string Summary, string Content);
public record ArticleDto(
    int Id,
    string Title,
    string Summary,
    string Content,
    string AuthorName,
    DateTime CreatedAt);
