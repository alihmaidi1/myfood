using System.Reflection;

namespace Identity.infrastructure;

public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    
}