namespace ElderlyCare.Domain.Configuration
{
    public class DatabaseConfiguration
    {
        public string DbServer { get; set; }
        public string DbDatabase { get; set; }
        public string DbUser { get; set; }
        public string DbPassword { get; set; }

        public ConnectionString GetConnectionString()
        {
            return new ConnectionString(
                DbServer, DbDatabase, DbUser, DbPassword);
        }
    }
}
