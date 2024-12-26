using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MiniApi.Entities
{
    public class Raccoon
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Range(0, 5, ErrorMessage = "Age cannot be greater than 5")]
        public int Age { get; set; }
        public int? OwnerId { get; set; }
        [JsonIgnore]
        public Owner? Owner { get; set; }
    }
}