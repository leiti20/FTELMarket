namespace FTELMarket.Models
{
    public record ProductResponse(
        int Id,
        string Name,
        string? Description,
        decimal Price,
        int Stock,
        int CategoryId,
        DateTime CreatedAt,
        string CategoryName);
}