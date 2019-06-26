using System;
using FluentAssertions;
using FluentAssertions.Common;
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
            var guid = Guid.NewGuid();

            // When
            var id = new EntityIdentity(guid);

            // Then
            id.Should().NotBeNull();
            id.Value.IsSameOrEqualTo(guid);
        }

        [Fact]
        public void It_should_create_for_correct_guid_as_string()
        {
            // Given
            var guid = Guid.NewGuid();
            
            // When
            var id = new EntityIdentity(guid.ToString());

            // Then
            id.Should().NotBeNull();
            id.Value.IsSameOrEqualTo(guid);
        }

        [Fact]
        public void It_should_throw_exception_for_empty_guid()
        {
            // Given
            var emptyGuid = Guid.Empty;

            // When
            var action = new Func<EntityIdentity>(() => new EntityIdentity(emptyGuid));

            // Then
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void It_should_throw_exception_for_incorrect_guid_as_string() 
        {
            // Given
            const string notGuidString = "DefinitelyNotGuid";

            // When
            var action = new Func<EntityIdentity>(() => new EntityIdentity(notGuidString));

            // Then
            action.Should().Throw<ArgumentException>();
        }
    }
}

