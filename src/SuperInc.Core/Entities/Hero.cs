using System;
using SuperInc.Core.Enums;

namespace SuperInc.Core.Entities
{
    public class Hero
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SecretIdenitty { get; set; }
        public string Power { get; set; }
        public Classification Classification { get; set; }

        public Hero(Guid id,
                    string name,
                    string power,
                    string secretIdentity,
                    Classification classification)
        {
            Id = id;
            Name = name;
            Power = power;
            SecretIdenitty = secretIdentity;
            Classification = classification;
        }

    }
}
