using GraphQL.Types;
using SuperInc.Api.Datastores;
using SuperInc.Api.Schemas.Types;
using System;

namespace SuperInc.Api.Schemas.Queries
{
    public class HeroQuery : ObjectGraphType<object>
    {
        // Summary: 
        //  Queries must inherit from ObjectGraphType, and the query itself is given a name inside of the constructor.
        //  Field<T> represents the different queries that can be executed.
        public HeroQuery(IHeroesDatastore store)
        {
            Name = "SuperHeroesQuery";
            Field<ListGraphType<HeroType>>(
                "heroes",
                resolve: context => store.GetHeroes());

            Field<HeroType>(
                "hero",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    var objectId = context.Arguments["id"];

                    return store.GetHero(id);
                });
        }
    }
}
