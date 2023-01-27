using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace FS.TechDemo.Shared.communication.database;

public class MySQLHealthCheck :
    IHealthCheck
{
    private readonly string _connectionString;
    public MySQLHealthCheck(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("sql");
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken)
    {
        try
        {
            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            if (connection.State != ConnectionState.Open)
            {
                return new HealthCheckResult(context.Registration.FailureStatus, $"The {nameof(MySQLHealthCheck)} check fail.");
            }

            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            return new HealthCheckResult(context.Registration.FailureStatus, exception: ex);
        }
    }
}