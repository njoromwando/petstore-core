using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PetStore.Data.ViewModels
{

    public partial class Pets
    {
        [JsonProperty("breeds")] 
        public List<Breed> Breeds { get; set; } = new List<Breed>();

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public partial class Breed
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("bred_for")]
        public string BredFor { get; set; }

        [JsonProperty("breed_group")]
        public string BreedGroup { get; set; }

        [JsonProperty("life_span")]
        public string LifeSpan { get; set; }

        [JsonProperty("temperament")]
        public string Temperament { get; set; }
    }


    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
