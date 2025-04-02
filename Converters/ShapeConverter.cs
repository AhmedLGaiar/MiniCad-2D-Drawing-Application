using System.Text.Json;
using System.Text.Json.Serialization;
using WPF_Final_Project.Shapes;

namespace WPF_Final_Project.Converters
{
    public class ShapeConverter : JsonConverter<AddShapes>
    {
        public override AddShapes Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;
                string shapeType = root.GetProperty("ShapeType").GetString();

                return shapeType switch
                {
                    nameof(AddLine) => JsonSerializer.Deserialize<AddLine>(root.GetRawText(), options),
                    nameof(AddRectangle) => JsonSerializer.Deserialize<AddRectangle>(root.GetRawText(), options),
                    nameof(AddCircle) => JsonSerializer.Deserialize<AddCircle>(root.GetRawText(), options),
                    nameof(AddPolygon) => JsonSerializer.Deserialize<AddPolygon>(root.GetRawText(), options),
                    _ => throw new NotSupportedException($"Shape type '{shapeType}' is not supported.")
                };
            }
        }

        public override void Write(Utf8JsonWriter writer, AddShapes value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
