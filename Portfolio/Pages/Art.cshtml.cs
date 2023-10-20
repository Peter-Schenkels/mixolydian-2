using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Portfolio.Pages
{
    public class ArtModel : PageModel
	{
		private readonly ILogger<ArtModel> _logger;

		public ArtModel(ILogger<ArtModel> logger)
		{
			_logger = logger;
		}

		public void OnGet()
		{
		}
	}
}