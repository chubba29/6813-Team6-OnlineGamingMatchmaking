namespace WebServiceGame6813Team6.Authorization
{

    // custom allow anonymous (instead of using the built in one) for consistency
    // and to avoid ambiguous reference errors between namespaces

    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    {
    }
}
