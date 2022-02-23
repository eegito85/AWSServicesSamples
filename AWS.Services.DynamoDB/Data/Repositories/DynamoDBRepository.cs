using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AWS.Services.DynamoDB.Data.DataModels;
using AWS.Services.DynamoDB.Data.Repositories.Interfaces;
using AWS.Services.DynamoDB.Models;
using AutoMapper;
using Amazon.DynamoDBv2.Model;

namespace AWS.Services.DynamoDB.Data.Repositories
{
    public class DynamoDBRepository : IDynamoDBRepository
    {
        private readonly DynamoDBContext _dynamoDBContext;
        private IMapper _mapper;

        public DynamoDBRepository(IAmazonDynamoDB dynamoDBContext, IMapper mapper)
        {
            _dynamoDBContext = new DynamoDBContext(dynamoDBContext);
            _mapper = mapper;
        }

        public async Task<List<MessageModelDTO>> GetAllMessages()
        {
            var scanConditions = new List<ScanCondition>();
            try
            {
                var allDocs = await _dynamoDBContext.ScanAsync<MessageModel>(scanConditions).GetRemainingAsync();
                var listAllDocs = allDocs.ToList();

                var allDocsDTO = _mapper.Map<List<MessageModel>, List<MessageModelDTO>>(listAllDocs);

                return allDocsDTO;
            }
            catch (Exception ex)
            {
                throw new ResourceNotFoundException(ex.Message);
            }
            
        }

        public async Task<List<string>> GetThreadMessages(string thread)
        {
            var config = new DynamoDBOperationConfig
            {
                IndexName = "Thread-Ticks-index",
                QueryFilter = new List<ScanCondition>(),
                BackwardQuery = true
            };

            var threadMessages = await _dynamoDBContext.QueryAsync<MessageModel>(thread, config).GetRemainingAsync();

            List<string> messages = new List<string>();
            foreach (var threadMessage in threadMessages)
            {
                messages.Add(threadMessage.Message);
            }

            return messages;

        }

        public async Task<List<string>> GetUserMessages(string user)
        {
            var userMessages = await _dynamoDBContext.QueryAsync<MessageModel>(user).GetRemainingAsync();

            List<string> messages = new List<string>();

            foreach (var message in userMessages)
            {
                messages.Add(message.Message);
            }

            return messages;
        }

        public async Task<List<MessageModelDTO>> GetUserTHreadMessages(string user, string thread)
        {
            var conditions = new List<ScanCondition>();
            conditions.Add(new ScanCondition("Thread", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal, thread));

            var config = new DynamoDBOperationConfig
            {
                QueryFilter = conditions,
                BackwardQuery = true
            };

            var userThreadMessages = await _dynamoDBContext.QueryAsync<MessageModel>(user, config).GetRemainingAsync();
            var listUserThreadMessages = userThreadMessages.ToList();
            
            var userThreadMessagesDTO = _mapper.Map<List<MessageModel>, List<MessageModelDTO>>(listUserThreadMessages);

            return userThreadMessagesDTO;
        }

        public async void InsertMessages()
        {
            var msg1DTO = new MessageModelDTO
            {
                User = "Egito",
                Ticks = DateTime.UtcNow.Ticks,
                Thread = "Volleyball",
                Message = "Volleyball is the best"
            };
            var msg1 = _mapper.Map<MessageModelDTO, MessageModel>(msg1DTO);
            await _dynamoDBContext.SaveAsync(msg1);

            var mess2DTO = new MessageModelDTO
            {
                User = "Jim",
                Ticks = DateTime.UtcNow.Ticks,
                Thread = "Rugby",
                Message = "Rugby is bad its too rough"
            };
            var mess2 = _mapper.Map<MessageModelDTO, MessageModel>(mess2DTO);
            await _dynamoDBContext.SaveAsync(mess2);

            var mess3DTO = new MessageModelDTO
            {
                User = "Carrey",
                Ticks = DateTime.UtcNow.Ticks,
                Thread = "Rugby",
                Message = "I like rugby too"
            };
            var mess3 = _mapper.Map<MessageModelDTO, MessageModel>(mess3DTO);
            await _dynamoDBContext.SaveAsync(mess3);

            var mess4DTO = new MessageModelDTO
            {
                User = "Tony",
                Ticks = DateTime.UtcNow.Ticks,
                Thread = "Football",
                Message = "Rugby is the best"
            };
            var mess4 = _mapper.Map<MessageModelDTO, MessageModel>(mess4DTO);
            await _dynamoDBContext.SaveAsync(mess4);

            var mess5DTO = new MessageModelDTO
            {
                User = "Mark",
                Ticks = DateTime.UtcNow.Ticks,
                Thread = "Football",
                Message = "No football is the best"
            };
            var mess5 = _mapper.Map<MessageModelDTO, MessageModel>(mess5DTO);
            await _dynamoDBContext.SaveAsync(mess5);

            var mess6DTO = new MessageModelDTO
            {
                User = "Egito",
                Ticks = DateTime.UtcNow.Ticks,
                Thread = "Football",
                Message = "I was wrong"
            };
            var mess6 = _mapper.Map<MessageModelDTO, MessageModel>(mess6DTO);
            await _dynamoDBContext.SaveAsync(mess6);


        }
    }
}
