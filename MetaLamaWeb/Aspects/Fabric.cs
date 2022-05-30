using Metalama.Framework.Fabrics;

namespace MetaLamaWeb.Aspects;

internal class Fabric : ProjectFabric
{
    public override void AmendProject(IProjectAmender project)
    {
        project.With(p => p.Types.Where(w=>w.Namespace.FullName.StartsWith("MetaLamaWeb")).SelectMany(t => t.Methods)).AddAspect<LogAttribute>();
    }
}