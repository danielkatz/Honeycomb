namespace Honeycomb.Xaml.Abstractions
{
    public interface IPropertyMetadata
    {
        void Merge(IPropertyMetadata baseMetadata);

        object DefaultValue { get; }

        bool IsInherited { get; }

        CreateDefaultValueCallback CreateDefaultValueCallback { get; }

        DependencyPropertyChangedCallback PropertyChangedCallback { get; }
    }
}