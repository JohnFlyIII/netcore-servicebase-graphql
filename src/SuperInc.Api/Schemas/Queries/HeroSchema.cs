using GraphQL;
using GraphQL.Types;
using SuperInc.Api.Schemas.Mutations;

namespace SuperInc.Api.Schemas.Queries
{
    public class HeroSchema : Schema
    {
        public HeroSchema(HeroQuery query, HeroMutation mutation, IDependencyResolver resolver)
        {
            // Summary:
            //  Here all the queries, mutations and the DependencyResolver injected in the startup class are all
            //  brought together to represent the queries and mutations users may perform on the service.
            Query = query;
            Mutation = mutation;
            DependencyResolver = resolver;
        }
    }
}