using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Portfolio.Pages
{
	public class BlogsModel : PageModel
	{
		private readonly ILogger<BlogsModel> _logger;

		public BlogsModel(ILogger<BlogsModel> logger)
		{
			_logger = logger;
		}

		public void OnGet()
		{

		}
	}

}