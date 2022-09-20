using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Database;

namespace UrlShortener.Services;

public record ListResult<T>(IEnumerable<T> Results, int TotalRecords);

public class UrlShorteningService
{
	private const int PageSize = 20;
	private readonly UrlShortenerDbContext _dbContext;

	public UrlShorteningService(UrlShortenerDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<ListResult<KeyValuePair<string, string>>> List(int pageNo)
	{
		var results = await _dbContext.UrlMappings
			.Skip(PageSize * (pageNo - 1))
			.Take(PageSize)
			.OrderBy(x => x.Id)
			.ToDictionaryAsync(x => x.ShortId, x => x.Url);
		var count = await _dbContext.UrlMappings.CountAsync();

		return new ListResult<KeyValuePair<string, string>>(results, count);
	}

	public async Task<string?> GetUrlRedirect(string shortId)
	{
		var mapping = await _dbContext.UrlMappings.FirstOrDefaultAsync(x => x.ShortId == shortId);
		return mapping?.Url;
	}

	public async Task SetOrUpdateUrl(string shortId, string url)
	{
		var mapping = await _dbContext.UrlMappings.FirstOrDefaultAsync(x => x.ShortId == shortId);

		if (mapping == null)
		{
			_dbContext.UrlMappings.Add(new UrlMapping
			{
				Id = Guid.NewGuid(),
				Url = url,
				ShortId = shortId,
			});
		}
		else
		{
			mapping.Url = url;
		}

		await _dbContext.SaveChangesAsync();
	}

	public async Task<string> Generate(string url)
	{
		var shortId = await GenerateShortLink(url);
		await SetOrUpdateUrl(shortId, url);
		return shortId;
	}

	private async Task<string> GenerateShortLink(string url)
	{
		string hash;
		var done = false;

		do
		{
			hash = Convert.ToBase64String(MD5.HashData(Encoding.UTF8.GetBytes(url)))[..6];
			if (await _dbContext.UrlMappings.AnyAsync(x => x.ShortId == hash))
			{
				// Pad the string with extra junk so we get another hash value
				url += '1';
			}
			else
			{
				done = true;
			}
		} while (!done);

		return hash;
	}
}