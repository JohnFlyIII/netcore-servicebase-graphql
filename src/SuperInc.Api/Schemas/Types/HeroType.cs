using GraphQL.Types;
using SuperInc.Api.Datastores;
using SuperInc.Api.Models;
using SuperInc.Api.Schemas.Enums;

namespace SuperInc.Api.Schemas.Types
{
    public class HeroType : ObjectGraphType<Hero>
    {
        public HeroType(IHeroesDatastore store)
        {
            Field(f => f.Id, type: typeof(IdGraphType));
            Field(f => f.Name);
            Field(f => f.SecretIdenitty);
            Field(f => f.Power);
            Field<HeroClassificationEnum>("Classification", "Hero classification", resolve: context=>context.Source.Classification);
        }
    }
}