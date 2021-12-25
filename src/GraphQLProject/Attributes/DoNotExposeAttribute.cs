using System;

namespace GraphQLProject.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DoNotExposeAttribute:Attribute
    {
    }
}
