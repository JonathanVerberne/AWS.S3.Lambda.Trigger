using Amazon.Lambda.Core;
using Amazon.S3;
using Amazon.Lambda.CloudWatchEvents;
using System.Text.Json;
using Amazon.S3.Model;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWS.S3.Lambda.Trigger;

public class Function
{
    public async Task FunctionHandler(CloudWatchEvent<dynamic> input, ILambdaContext context)
    {
        context.Logger.LogInformation("--- Event Bridge Logger ---");

        try
        {
            context.Logger.LogInformation("Lambda triggered by EventBridge");
            context.Logger.LogInformation($"EventBridge request received -> {JsonSerializer.Serialize(input)}");

            var s3Event = JsonSerializer.Deserialize<CloudWatchEvent>(JsonSerializer.Serialize(input));
            context.Logger.LogInformation($"S3 file size -> {s3Event?.Detail?.Object?.Size}");
            context.Logger.LogInformation($"S3 object key -> {s3Event?.Detail?.Object?.Key}");

            if (s3Event?.Detail?.Object?.Size > 0)
            {
                var s3Client = new AmazonS3Client();
                var getObjectRequest = new GetObjectRequest
                {
                    BucketName = s3Event?.Detail?.Bucket?.Name,
                    Key = s3Event?.Detail?.Object?.Key
                };
                
                //read file
                using (var getObjectResponse = await s3Client.GetObjectAsync(getObjectRequest))
                {
                    using (StreamReader sr = new StreamReader(getObjectResponse.ResponseStream))
                    {
                        string line = String.Empty;

                        while ((line = sr.ReadLine()) != null)
                        {
                            context.Logger.LogInformation($"file content" + sr.ReadLine());
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            context.Logger.LogInformation($"[ERROR] Failed to serialize CloudWatchEvent {ex.Message}");
            throw;
        }
    }
}