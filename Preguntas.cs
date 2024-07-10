using System.Text.Json.Serialization;
namespace FabricaDePreguntas;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<Root>>(myJsonResponse);

    public class Pregunta
    {
        [JsonPropertyName("id")]
        public string id { get; set; }

        [JsonPropertyName("category")]
        public string category { get; set; }

        [JsonPropertyName("level")]
        public string level { get; set; }

        [JsonPropertyName("question")]
        public string question { get; set; }

        [JsonPropertyName("answers")]
        public Answers answers { get; set; }

        [JsonPropertyName("correct_answer")]
        public string correct_answer { get; set; }

        [JsonPropertyName("feedback")]
        public string feedback { get; set; }
    }
    public class Answers
    {
        [JsonPropertyName("answer_a")]
        public string answer_a { get; set; }

        [JsonPropertyName("answer_b")]
        public string answer_b { get; set; }

        [JsonPropertyName("answer_c")]
        public string answer_c { get; set; }

        [JsonPropertyName("answer_d")]
        public string answer_d { get; set; }
    }



