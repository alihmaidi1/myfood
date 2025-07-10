using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Test.Domain;

public class DomainArchitectureTests
{
    [Fact]
    public void IdentityDomain_Should_Not_HaveDependencyOnOtherProjects()
    {
        
        var assembly = Identity.Domain.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
            "Identity.infrastructure",
            "Identity.Application",
            
        };

        var result=Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();
        result.IsSuccessful.Should().BeTrue();
    }
    
    
    [Fact]
    public void IdentityApplication_Should_Not_HaveDependencyOnIdentityInfrastructureProject()
    {
        
        var assembly = Identity.Application.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
            "Identity.infrastructure",
            
        };

        var result=Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();
        result.IsSuccessful.Should().BeTrue();
    }
    
}