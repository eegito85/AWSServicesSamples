using AWS.Services.DynamoDB.Models;

namespace AWS.Services.DynamoDB.Data.Repositories.Interfaces
{
    public interface IDynamoDBRepository
    {
        Task<List<MessageModelDTO>> GetAllMessages();
        Task<List<string>> GetThreadMessages(string thread);
        Task<List<string>> GetUserMessages(string user);
        Task<List<MessageModelDTO>> GetUserTHreadMessages(string user, string thread);
        void InsertMessages();

    }
}
