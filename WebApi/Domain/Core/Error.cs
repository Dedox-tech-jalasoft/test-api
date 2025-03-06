namespace WebApi.Domain.Core
{
    public struct Error { }

    public struct Error<T>
    {
        public Error(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}
