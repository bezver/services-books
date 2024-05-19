using System.Text.Json;
using System.Xml.Serialization;

namespace Books.Domain.DTO
{
    public class Error
    {
        public int Status { get; set; } = 500;
        public string Message { get; set; } = "Internal Server Error";

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public string ToXml()
        {
            XmlSerializer serializer = new(typeof(Error));
            using StringWriter writer = new();
            serializer.Serialize(writer, this);
            return writer.ToString();
        }
    }
}
