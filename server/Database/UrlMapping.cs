namespace UrlShortener.Database;

public class UrlMapping
{
	// TODO: Set ShortId to be unique
	public Guid Id { get; set; }
	public string ShortId { get; set; }
	public string Url { get; set; }
}