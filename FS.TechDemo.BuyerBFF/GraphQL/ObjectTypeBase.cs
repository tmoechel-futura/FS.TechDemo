namespace FS.TechDemo.BuyerBFF.GraphQL;

public class ObjectTypeBase<T> : ObjectType<T>
where T: class, new()
{
    protected override void Configure(IObjectTypeDescriptor<T> descriptor)
    {
        var name = GetType().Name.Replace('`', '_');
        descriptor.Name(name);
        descriptor.BindFieldsExplicitly();
    }
}