using Amazon.DynamoDBv2.DataModel;

namespace AWS.Services.DynamoDB.Models
{
    [DynamoDBTable("MessageBoard")]
    public class MessageModelDTO
    {
        [DynamoDBHashKey]
        public string User { get; set; }

        [DynamoDBRangeKey]
        public long Ticks { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey]
        public string Thread { get; set; }

        [DynamoDBProperty]
        public string Message { get; set; }
    }
}
