using TBC.BooksCatalog.Write.Domain;

namespace TBC.BooksCatalog.ArchitectureTests;

public class ArtefactsGaurdTests
{
    private readonly Assembly _domainAssembly = AssemblyReference.Assembly;

    [Fact]
    public void Entities_ShouldBe_Sealed_Classes()
    {
        var testResult = Types
            .InAssembly(_domainAssembly)
            .That()
            .Inherit(typeof(EntityBase))
            .Should()
            .BeClasses()
            .And()
            .BeSealed()
            .GetResult();

        Assert.True(testResult.IsSuccessful);
    }

    [Fact]
    public void Entities_Should_Only_Have_Private_Constructors()
    {
        var entities = Types
            .InAssembly(_domainAssembly)
            .That()
            .Inherit(typeof(EntityBase))
            .GetTypes();

        List<Type> failedEntities = [];

        foreach (var entity in entities)
        {
            var constructors = entity.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

            if (constructors.Any(c => c is { IsConstructor: true, IsPublic: true }))
            {
                failedEntities.Add(entity);
            }
        }

        Assert.True(failedEntities.Count == 0);
    }
}
