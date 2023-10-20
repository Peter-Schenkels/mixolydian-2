using Portfolio.Models;
using System.Text.Json;

namespace Portfolio.Services
{
    public class JsonDataService
    {
        public string FileNameProjects => Path.Combine(WebHostEnvironment.WebRootPath, "data", "projects.json");
        public string FileNameArt => Path.Combine(WebHostEnvironment.WebRootPath, "data", "art.json");
        public string FileNameMusic => Path.Combine(WebHostEnvironment.WebRootPath, "data", "music.json");

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
            var projects = GetData<Project>(FileNameProjects).ToList();
            projects.Sort((a, b) => DateTime.Compare(DateTime.Parse(b.Date), DateTime.Parse(a.Date)));
            return projects;
        }

        public IEnumerable<Art> GetArt()
        {
            var art = GetData<Art>(FileNameArt).ToList();
            art.Sort((a, b) => DateTime.Compare(DateTime.Parse(b.Date), DateTime.Parse(a.Date)));
            return art;
        }

        public IEnumerable<Music> GetMusic() 
        {
            var music = GetData<Music>(FileNameMusic).ToList();
            music.Sort((a, b) => DateTime.Compare(DateTime.Parse(b.Date), DateTime.Parse(a.Date)));
            return music;
        }


        public IEnumerable<T> GetData<T>(string path)
        {
            try
            {
                using var jsonFileReader = File.OpenText(path);
                var serializingOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };


                var projects = JsonSerializer.Deserialize<T[]>(jsonFileReader.ReadToEnd(), serializingOptions);
                if (projects == null)
                {
                    Console.WriteLine($"Error: {path} is empty.");
                    return new List<T>();
                }
                return projects;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: Something went wrong deserializing {path}");
                Console.WriteLine($"Error: {e.Message}");
                return new List<T>();
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
