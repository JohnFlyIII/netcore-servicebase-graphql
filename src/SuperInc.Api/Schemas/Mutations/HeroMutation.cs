using BOS.ServiceBase.GraphQL.Schemas.Types;
using GraphQL.Types;
using Microsoft.Extensions.Logging;
using SuperInc.Api.Datastores;
using SuperInc.Api.Models;
using SuperInc.Api.Schemas.Types;
using System;

namespace SuperInc.Api.Schemas.Mutations
{
    public class HeroMutation : ObjectGraphType<object>
    {
        // Summary:
        //  The same as Queries, mutations must inherit from ObjectGraphType<T>. Then in the constructor
        //  the mutation must be given a name, and as different Field<T> 's all the mutations that may be 
        //  performed and their mappings are done here.

            // **NOTE** the following two Fields represent two ways that validation can happen on the input from users.
            //  You can specify which fields are required and which are optional in the input type, by specifying <NonNullGraphType<T>>
            //  which is what is done in the first example. You can also use a generic input type that doesn't specify which values are
            // nullable, and includes all the properties for the object, and then perform checks against the incoming data. This
            //  allows code reusability but means more is stuffed in each mutation resolve argument.
        public HeroMutation(IHeroesDatastore store, ILogger<HeroMutation> logger)
        {
            Name = "heroMutation";
            FieldAsync<HeroType>("createHero",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<CreateHeroInputType>> { Name = "hero" }),
                resolve: async context =>
                {
                    var data = context.GetArgument<CreateHeroInput>("hero");
                    var id = Guid.NewGuid();
                    var newSuperHero = new Hero(id, data.Name, data.Power, data.SecretIdentity, data.HeroClassification);
                    
                    //Summary:
                    //  The TryAsyncResolve function available on FieldAsync<T> that this resolve parameter is
                    //  being defined for, allows passing of exceptions to be handled inside the method we are 
                    //  calling
                    return await context.TryAsyncResolve(
                        async c => await store.AddSuperHero(newSuperHero));
                });
        }
    }
}
