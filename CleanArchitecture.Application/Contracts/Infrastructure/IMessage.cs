namespace CleanArchitecture.Application.Contracts.Infrastructure
{
    public interface IMessage
    {
        void SendEmail(string subject, string body, string to);

    }
}
