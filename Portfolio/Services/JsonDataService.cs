using System.Globalization;
using System.Net;
using System.Text.Json;
using Portfolio.Models;

namespace Portfolio.Services
{
	public class JsonDataService
	{
		public string FileNameProjects => "http://data.peterschenkels.nl//projects.json";
		public string FileNameArt => "http://data.peterschenkels.nl//art.json";
		public string FileNameMusic => "http://data.peterschenkels.nl//music.json";
		public string FileNamePersonalia => "http://data.peterschenkels.nl//personalia.json";

		public IWebHostEnvironment WebHostEnvironment { get; }

		public JsonDataService(IWebHostEnvironment webHostEnvironment)
		{
			WebHostEnvironment = webHostEnvironment;
		}

		public List<Project> GetMostRecentProjects()
		{
			return GetProjects().Take(2).ToList();
		}

		public IEnumerable<Project> GetProjects()
		{
			var projects = GetDataList<Project>(FileNameProjects).ToList();
			SortByDate(projects);
			return projects;
		}

		public IEnumerable<Art> GetArt()
		{
			var art = GetDataList<Art>(FileNameArt).ToList();
			SortByDate(art);
			return art;
		}

		public IEnumerable<Music> GetMusic()
		{
			var music = GetDataList<Music>(FileNameMusic).ToList();
			SortByDate(music);
			return music;
		}

		public Personalia GetPersonalia()
		{
			return GetData<Personalia>(FileNamePersonalia);
		}


		private static void SortByDate<T>(List<T> items)  where T : ITemInterface
		{
			items.Sort((a, b) =>
				DateTime.Compare(
					DateTime.Parse(b.Date, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal),
					DateTime.Parse(a.Date, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal)
				)
			);
		}

		public IEnumerable<T> GetDataList<T>(string path)
		{
			try
			{
				var request = WebRequest.CreateHttp(path);
				request.Method = "GET";

				var options = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				};

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					var stream = response.GetResponseStream();
					var projects = JsonSerializer.Deserialize<T[]>(stream, options: options);

					if (projects == null)
					{
						Console.WriteLine($"Error: {path} is empty.");
						return new List<T>();
					}

					return projects;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error: Something went wrong deserializing {path}");
				Console.WriteLine($"Error: {e.Message}");
				return new List<T>();
			}
		}

		public T GetData<T>(string path) where T : class, new()
		{
			try
			{
				HttpClient httpClient = new ();

				var request = WebRequest.CreateHttp(path);
				request.Method = "GET";

				var options = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				};

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					var stream = response.GetResponseStream();
					var projects = JsonSerializer.Deserialize<T>(stream, options: options);

					if (projects == null)
					{
						Console.WriteLine($"Error: {path} is empty.");
						return new T();
					}

					return projects;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error: Something went wrong deserializing {path}");
				Console.WriteLine($"Error: {e.Message}");
				return new T();
			}
		}


		public void AddProject(Project project)
		{
			var projects = GetProjects().ToList();
			projects.Add(project);
			SaveProjects(projects);
		}


		private void SaveProjects(IEnumerable<Project> projects)
		{
			var serializingOptions = new JsonSerializerOptions
			{
				WriteIndented = true
			};
			var json = JsonSerializer.Serialize(projects, serializingOptions);
			File.WriteAllText(FileNameProjects, json);
		}
	}
}
