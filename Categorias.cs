namespace FabricaDePreguntas;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);

public class Root
    {
        [JsonPropertyName("categories")]
        public List<Category> categories { get; set; }

        [JsonPropertyName("totalCategories")]
        public int totalCategories { get; set; }

        [JsonPropertyName("totalQuestions")]
        public int totalQuestions { get; set; }

    }
    public class Category
    {
        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("count_questions")]
        public int count_questions { get; set; }

        [JsonPropertyName("link")]
        public string link { get; set; }
    }

    
    





   
