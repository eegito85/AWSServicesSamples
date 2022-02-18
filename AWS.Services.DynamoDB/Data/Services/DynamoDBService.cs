using AWS.Services.DynamoDB.Data.Repositories.Interfaces;
using AWS.Services.DynamoDB.Data.Services.Interfaces;
using AWS.Services.DynamoDB.Models;

namespace AWS.Services.DynamoDB.Data.Services
{
    public class DynamoDBService : IDynamoDBService
    {
        private readonly IDynamoDBRepository _dynamoDBRepository;

        public DynamoDBService(IDynamoDBRepository dynamoDBRepository)
        {
            _dynamoDBRepository = dynamoDBRepository;
        }

        public Task<List<MessageModelDTO>> GetAllMessages()
        {
            return _dynamoDBRepository.GetAllMessages();
        }

        public Task<List<string>> GetThreadMessages(string thread)
        {
            return _dynamoDBRepository.GetThreadMessages(thread);
        }

        public Task<List<string>> GetUserMessages(string user)
        {
            return _dynamoDBRepository.GetUserMessages(user);
        }

        public Task<List<MessageModelDTO>> GetUserTHreadMessages(string user, string thread)
        {
            return _dynamoDBRepository.GetUserTHreadMessages(user, thread);
        }

        public void InsertMessages()
        {
            _dynamoDBRepository.InsertMessages();
        }
    }
}
