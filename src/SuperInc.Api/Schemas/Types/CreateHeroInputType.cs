using GraphQL.Types;
using SuperInc.Api.Schemas.Enums;

namespace BOS.ServiceBase.GraphQL.Schemas.Types
{
    public class CreateHeroInputType : InputObjectGraphType
    {
        public CreateHeroInputType()
        {
            // Summary:
            //  This is an input type that specifies which values can and cannot be null.
            //  This is powerful, as it won't allow a mutation that utilizes it to leave out
            //  values that you need. However, it comes with the trade off that you can't re-use
            //  this as well. In some cases you don't want all these fields, and it would force the 
            //  user to provide them. The alternative method is to use a generic object that accepts
            //  all the model fields, and performs a check in the mutation and throws an exception if 
            //  something required is not present.
            Name = "createHeroData";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<StringGraphType>>("secretIdentity");
            Field<NonNullGraphType<StringGraphType>>("power");
            Field<NonNullGraphType<HeroClassificationEnum>>("classification");
        }
    }
}
