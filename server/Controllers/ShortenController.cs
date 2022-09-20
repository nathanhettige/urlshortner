using Microsoft.AspNetCore.Mvc;
using UrlShortener.Services;

namespace UrlShortener.Controllers;

public record SetDto(string ShortId, string Url);
public record GenerateDto(string Url);

[ApiController]
[Route("u")]
public class ShortenController : ControllerBase
{
	private readonly UrlShorteningService _shorteningService;

	public ShortenController(UrlShorteningService shorteningService)
	{
		_shorteningService = shorteningService;
	}

	[HttpGet("list")]
	public async Task<IActionResult> List([FromQuery]int page = 1)
	{
		return Ok(await _shorteningService.List(page));
	}

	[HttpGet("{shortId}")]
	public async Task<IActionResult> RedirectToUrl(string shortId)
	{
		var url = await _shorteningService.GetUrlRedirect(shortId);
		return url != null ? Redirect(url) : NotFound();
	}

	[HttpPost("set")]
	public async Task<IActionResult> SetOrUpdateUrl([FromBody] SetDto model)
	{
		await _shorteningService.SetOrUpdateUrl(model.ShortId, model.Url);
		return Ok();
	}

	[HttpPost("generate")]
	public async Task<IActionResult> GenerateNew([FromBody] GenerateDto model)
	{
		var shortId = await _shorteningService.Generate(model.Url);
		return Ok(shortId);
	}
}