using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace DoorDashClient.Models.Classic
{
	public class BoolConverter : JsonConverter<bool>
	{
		public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) =>
			writer.WriteBooleanValue(value);

		public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			switch (reader.TokenType)
			{
				case JsonTokenType.True: return true;
				case JsonTokenType.False: return false;
				case JsonTokenType.String:
					return bool.TryParse(reader.GetString(), out bool b) ? b : throw new JsonException();
				case JsonTokenType.Number:
					return reader.TryGetInt64(out long l) ? Convert.ToBoolean(l) : reader.TryGetDouble(out double d) ? Convert.ToBoolean(d) : false;
				default:
					throw new JsonException();
			}
		}
	}
}
