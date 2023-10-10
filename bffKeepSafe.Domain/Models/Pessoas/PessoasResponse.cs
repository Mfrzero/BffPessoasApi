using bffKeepSafe.Domain.Enums;
using System.Text.Json.Serialization;

namespace bffKeepSafe.Domain.Models.Pessoas
{
    public class PessoasResponse
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("Nome")]
        public string Nome { get; set; }

        [JsonPropertyName("Sexo")]
        public TipoSexo Sexo { get; set; }

        [JsonPropertyName("Idade")]
        public int Idade { get; set; }

        [JsonPropertyName("Cidade")]
        public string Cidade { get; set; }
    }
}
