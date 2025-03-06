namespace WebApi.Domain.Core
{
    public struct Success { }

    public struct Success<T>
    {
        public Success(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }

}
