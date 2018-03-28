namespace XDependency.Abstractions
{
    public interface IPropertyMetadata
    {
        object DefaultValue { get; }
        CreateDefaultValueCallback CreateDefaultValueCallback { get; }
    }
}