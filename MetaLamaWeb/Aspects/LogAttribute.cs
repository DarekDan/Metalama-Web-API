// This is an open-source Metalama example. See https://github.com/postsharp/Metalama.Samples for more.

using Metalama.Framework.Aspects;
using Serilog;

namespace MetaLamaWeb.Aspects;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var log = Log.Logger.ForContext(meta.Target.Type.ToType());
        var methodName = meta.Target.Method.ToDisplayString();
        log.Debug("{MethodName} started", methodName);
        try
        {
            var result = meta.Proceed();
            log.Debug("{MethodName} succeeded", methodName);
            return result;
        }
        catch (Exception e)
        {
            log.Warning(e, "{MethodName} failed", methodName);
            throw;
        }
    }
}