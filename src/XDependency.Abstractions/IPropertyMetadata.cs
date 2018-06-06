namespace XDependency.Abstractions
{
    public interface IPropertyMetadata
    {
        void Merge(IPropertyMetadata baseMetadata);

        object DefaultValue { get; }

        bool Inherits { get; }

        CreateDefaultValueCallback CreateDefaultValueCallback { get; }

        DependencyPropertyChangedCallback PropertyChangedCallback { get; }
    }
}