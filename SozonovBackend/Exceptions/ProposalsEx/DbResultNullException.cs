namespace SozonovBackend.Exceptions.ProposalsEx
{
    public class DbResultNullException: Exception
    {
        public DbResultNullException() : base("Объект отсутствует в базе данных") { }
    }
}
