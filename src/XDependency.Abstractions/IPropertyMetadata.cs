namespace XDependency.Abstractions
{
    public interface IPropertyMetadata
    {
        void Merge(IPropertyMetadata baseMetadata);

        object DefaultValue { get; }
        CreateDefaultValueCallback CreateDefaultValueCallback { get; }
    }
}