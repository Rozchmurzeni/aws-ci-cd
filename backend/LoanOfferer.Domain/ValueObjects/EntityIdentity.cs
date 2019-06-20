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
        
        public static EntityIdentity New => new EntityIdentity(Guid.NewGuid());

        public Guid Value { get; }
    }
}
