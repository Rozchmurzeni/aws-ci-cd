using System;
using LoanOfferer.Domain.ValueObjects;
using Xunit;

namespace LoanOfferer.Domain.Test.ValueObjects
{
    public class When_creating_entity_identity
    {
        [Fact]
        public void It_should_create_for_correct_guid()
        {
            // Given
            var input = Guid.NewGuid();

            // When
            var id = new EntityIdentity(input);

            // Then
            Assert.NotNull(id);
            Assert.Equal(input, id.Value);
        }

        [Fact]
        public void It_should_create_for_correct_guid_as_string()
        {
            
        }

        [Fact]
        public void It_should_throw_exception_for_empty_guid()
        {
            
        }

        [Fact]
        public void It_should_throw_exception_for_incorrect_guid_as_string()
        {
            
        }
    }
}
