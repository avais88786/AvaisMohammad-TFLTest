using Newtonsoft.Json;
using System;

namespace TFLTest_AvaisMohammad.Models
{
    public class TflException : Exception
    {
        public TflException()
        {
        }

        public TflException(string message): base(message)
        {
            Message = message;
        }

        public TflException(string message, Exception exception) : base(message, exception)
        {
            Message = message;
        }

        [JsonProperty("$type")]
        public string Type { get; set; }

        [JsonProperty("timestampUtc")]
        public DateTimeOffset TimestampUtc { get; set; }

        [JsonProperty("exceptionType")]
        public string ExceptionType { get; set; }

        [JsonProperty("httpStatusCode")]
        public long HttpStatusCode { get; set; }

        [JsonProperty("httpStatus")]
        public string HttpStatus { get; set; }

        [JsonProperty("relativeUri")]
        public string RelativeUri { get; set; }

        [JsonProperty("message")]
        public new string Message { get; set; }
    }
}
