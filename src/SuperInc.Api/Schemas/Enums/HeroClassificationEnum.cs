using GraphQL.Types;

namespace SuperInc.Api.Schemas.Enums
{
    public class HeroClassificationEnum : EnumerationGraphType
    {
        public HeroClassificationEnum()
        {
            Name = "HeroClassification";
            AddValue("Unknown", "Hero has not been classified", 0);
            AddValue("Alpha", "Basic", 10);
            AddValue("Beta", "Local", 20);
            AddValue("Gamma", "Regional", 30);
            AddValue("Omega", "Global", 40);
        }
    }
}