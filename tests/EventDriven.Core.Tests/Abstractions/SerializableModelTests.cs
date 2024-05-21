using System.Text.Json;
using AutoFixture;
using Moq;
using UpscaleDown.EventDriven.Core.Models;

namespace UpscaleDown.EventDriven.Core.Tests.Models;

public abstract class SerializableModelTests<T>
{
    protected readonly Fixture _fixture = new Fixture();
    [Fact]
    public virtual void SerializeRecord_Should_AllowCorrectDeserialize()
    {
        // Arrange
        var record = _fixture.Create<T>();

        // Act
        var json = JsonSerializer.Serialize(record);
        var deserialized = JsonSerializer.Deserialize<T>(json);

        // Assert
        Assert.NotEqual(json, string.Empty);
        foreach (var prop in record.GetType().GetProperties())
        {
            Assert.Equal(prop.GetValue(record), prop.GetValue(deserialized));
        }
    }
}