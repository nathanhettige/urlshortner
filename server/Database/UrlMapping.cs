namespace UrlShortener.Database;

public class UrlMapping
{
	public Guid Id { get; set; }
	public string ShortId { get; set; }
	public string Url { get; set; }
}