namespace WebApi.Domain.Core
{
    public struct Deleted { }

    public struct Deleted<T>
    {
        public Deleted(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}
