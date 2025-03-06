namespace WebApi.Domain.Core
{
    public struct Updated { }

    public struct Updated<T>
    {
        public Updated(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}
