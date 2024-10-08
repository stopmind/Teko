using JetBrains.Annotations;
namespace Teko.Inject;


[MeansImplicitUse(ImplicitUseKindFlags.Assign)]
[AttributeUsage(AttributeTargets.Field)]
public class InjectAttribute : Attribute
{
    
}