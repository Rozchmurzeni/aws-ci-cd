using System;

namespace LoanOfferer.Domain.ValueObjects
{
    public class EntityIdentity : IValueObject<Guid>
    {
        public EntityIdentity(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Entity identity cannot be an empty GUID.");
            }

            Value = value;
        }

        public EntityIdentity(string id)
        {
            if (!Guid.TryParse(id, out var value))
            {
                throw new ArgumentException("Argument is not correct GUID.");
            }

            Value = value;
        }

        public static EntityIdentity New => new EntityIdentity(Guid.NewGuid());

        public Guid Value { get; }

        public override string ToString() => Value.ToString();
    }
}
