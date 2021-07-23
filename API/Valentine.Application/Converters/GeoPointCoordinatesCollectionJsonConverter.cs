using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Valentine.Application.Models;

namespace Valentine.Application.Converters
{
    internal class GeoPointCoordinatesCollectionJsonConverter : JsonConverter<GeoPointCoordsCollection>
    {
        public override GeoPointCoordsCollection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException("Expected StartArray token");

            var coordinates = new List<GeoPointCoordinates>();
            var closingBrackets = 0;

            while (closingBrackets < 2)
            {
                while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                {
                    closingBrackets = 0;

                    reader.Read();
                    var latitude = reader.GetDouble();

                    reader.Read();
                    var longitude = reader.GetDouble();

                    coordinates.Add(new GeoPointCoordinates(latitude, longitude));
                }

                closingBrackets++;
            }

            return new GeoPointCoordsCollection(coordinates);
        }

        public override void Write(Utf8JsonWriter writer, GeoPointCoordsCollection value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (var coordinates in value.Coordinates)
            {
                writer.WriteStartArray();
                writer.WriteNumberValue(coordinates.Coordinates[0]);
                writer.WriteNumberValue(coordinates.Coordinates[1]);
                writer.WriteEndArray();
            }

            writer.WriteEndArray();
        }
    }
}
