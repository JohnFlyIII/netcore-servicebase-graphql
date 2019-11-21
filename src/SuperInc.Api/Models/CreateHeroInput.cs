namespace SuperInc.Api.Models
{
    public class CreateHeroInput
    {
        // Summary:
        //  This class is a specific and explicit implementation of an input model.
        //  It does not include the ID field, because the user will not provide that.
        //  This will allow for using an input type and can specify which values can be
        //  not null. 
        public string Name { get; set; }
        public string SecretIdentity { get; set; }
        public string Power { get; set; }
        public HeroClassification HeroClassification { get; set; }
    }
}