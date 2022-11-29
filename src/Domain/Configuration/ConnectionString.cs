namespace ElderlyCare.Domain.Configuration
{
    public record ConnectionString(
        string Server,
        string Database,
        string User,
        string Password)
    {
        public override string ToString()
        {
            return $"Server={Server};Database={Database};User Id={User};Password={Password}";
        }

        public static implicit operator string(ConnectionString connectionString) => connectionString.ToString();
    }
}
