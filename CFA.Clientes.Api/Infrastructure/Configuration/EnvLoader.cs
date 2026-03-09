using DotNetEnv;

namespace CFA.Clientes.Api.Infrastructure.Configuration;

public static class EnvLoader
{
    public static void Load()
    {
        Env.Load();
    }
}