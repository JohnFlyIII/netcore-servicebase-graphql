using System;

namespace SuperInc.Api.Models
{
     public class Hero
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SecretIdenitty { get; set; }
        public string Power { get; set; }
        public HeroClassification Classification { get; set; }

        public Hero(Guid id,
                    string name,
                    string power,
                    string secretIdentity,
                    HeroClassification classification)
        {
            Id = id;
            Name = name;
            Power = power;
            SecretIdenitty = secretIdentity;
            Classification = classification;
        }

    }
}