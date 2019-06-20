namespace LoanOfferer.Domain.ValueObjects
{
    public interface IValueObject<T>
    {
        T Value { get; }
    }
}
