using System.Reflection;

namespace TBC.BooksCatalog.Write.Domain;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
