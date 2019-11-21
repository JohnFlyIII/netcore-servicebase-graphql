using System.Security.Claims;
using GraphQL.Authorization;

namespace SuperInc.Auth
{
    public class GraphQLUserContext : IProvideClaimsPrincipal
    {
        public ClaimsPrincipal User { get; set; }
    }
}