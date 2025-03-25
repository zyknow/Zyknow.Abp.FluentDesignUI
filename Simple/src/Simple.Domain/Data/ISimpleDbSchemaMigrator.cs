namespace Simple.Data;

public interface ISimpleDbSchemaMigrator
{
    Task MigrateAsync();
}
