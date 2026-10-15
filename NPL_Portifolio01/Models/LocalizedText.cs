using System.Text.Json;
using System.Text.Json.Serialization;

namespace NPL_Portifolio01.Models;

/// <summary>
/// Text that is either a plain JSON string (shared by every language)
/// or an object keyed by language code, e.g. <c>{ "en": "Hello", "vi": "Xin chào" }</c>.
/// </summary>
[JsonConverter(typeof(LocalizedTextJsonConverter))]
public sealed class LocalizedText
{
    public const string FallbackLanguage = "en";

    public static LocalizedText Empty { get; } = new(string.Empty);

    private readonly string? _invariant;
    private readonly Dictionary<string, string> _values;

    public LocalizedText(string value)
    {
        _invariant = value;
        _values = [];
    }

    public LocalizedText(IDictionary<string, string> values)
    {
        _values = new Dictionary<string, string>(values, StringComparer.OrdinalIgnoreCase);
    }

    public bool IsEmpty => _invariant is not null
        ? string.IsNullOrWhiteSpace(_invariant)
        : _values.Values.All(string.IsNullOrWhiteSpace);

    public string Get(string language)
    {
        if (_invariant is not null)
        {
            return _invariant;
        }

        if (_values.TryGetValue(language, out var value) && !string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        if (_values.TryGetValue(FallbackLanguage, out value) && !string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        return _values.Values.FirstOrDefault(v => !string.IsNullOrWhiteSpace(v)) ?? string.Empty;
    }

    public override string ToString() => Get(FallbackLanguage);

    internal void WriteTo(Utf8JsonWriter writer)
    {
        if (_invariant is not null)
        {
            writer.WriteStringValue(_invariant);
            return;
        }

        writer.WriteStartObject();
        foreach (var (language, text) in _values)
        {
            writer.WriteString(language, text);
        }
        writer.WriteEndObject();
    }
}

public sealed class LocalizedTextJsonConverter : JsonConverter<LocalizedText>
{
    public override bool HandleNull => true;

    public override LocalizedText Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Null:
                return LocalizedText.Empty;

            case JsonTokenType.String:
                return new LocalizedText(reader.GetString() ?? string.Empty);

            case JsonTokenType.StartObject:
                var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        return new LocalizedText(values);
                    }

                    var language = reader.GetString() ?? string.Empty;
                    reader.Read();
                    values[language] = reader.TokenType switch
                    {
                        JsonTokenType.String => reader.GetString() ?? string.Empty,
                        JsonTokenType.Null => string.Empty,
                        _ => throw new JsonException($"Expected a string for language '{language}'.")
                    };
                }
                throw new JsonException("Unexpected end of JSON while reading localized text.");

            default:
                throw new JsonException($"Expected a string or an object of translations but found {reader.TokenType}.");
        }
    }

    public override void Write(Utf8JsonWriter writer, LocalizedText value, JsonSerializerOptions options) =>
        value.WriteTo(writer);
}
