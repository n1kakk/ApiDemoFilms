namespace Films.DAL.Helpers
{
    public class AppException: Exception
    {
        public AppException(): base() { } //конструктор
        public AppException(string message): base(message) { } //конструктор, который принимает сообщение

    }
}
