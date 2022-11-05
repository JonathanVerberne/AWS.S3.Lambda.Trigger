using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AWS.S3.Lambda.Trigger
{
    public class CloudWatchEvent
    {
        [JsonPropertyName("Version")]
        public string? Version { get; set; }

        [JsonPropertyName("Account")]
        public string? Account { get; set; }

        [JsonPropertyName("Region")]
        public string? Region { get; set; }

        [JsonPropertyName("Detail")]
        public Detail? Detail { get; set; }

        [JsonPropertyName("detail-type")]
        public string? DetailType { get; set; }

        [JsonPropertyName("Source")]
        public string? Source { get; set; }

        [JsonPropertyName("Time")]
        public DateTime Time { get; set; }

        [JsonPropertyName("Id")]
        public string? Id { get; set; }

        [JsonPropertyName("Resources")]
        public List<string>? Resources { get; set; }
    }

    public class Bucket
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class Detail
    {
        [JsonPropertyName("version")]
        public string? Version { get; set; }

        [JsonPropertyName("bucket")]
        public Bucket? Bucket { get; set; }

        [JsonPropertyName("object")]
        public Object? Object { get; set; }

        [JsonPropertyName("request-id")]
        public string? RequestId { get; set; }

        [JsonPropertyName("requester")]
        public string? Requester { get; set; }

        [JsonPropertyName("source-ip-address")]
        public string? SourceIpAddress { get; set; }

        [JsonPropertyName("reason")]
        public string? Reason { get; set; }
    }

    public class Object
    {
        [JsonPropertyName("key")]
        public string? Key { get; set; }

        [JsonPropertyName("size")]
        public int? Size { get; set; }

        [JsonPropertyName("etag")]
        public string? Etag { get; set; }

        [JsonPropertyName("sequencer")]
        public string? Sequencer { get; set; }
    }
}
