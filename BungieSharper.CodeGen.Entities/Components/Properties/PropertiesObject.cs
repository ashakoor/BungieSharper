﻿using BungieSharper.CodeGen.Entities.Common;
using System.Text.Json.Serialization;

namespace BungieSharper.CodeGen.Entities.Components.Properties
{
    public class PropertiesObject
    {
        [JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public TypeEnum? Type { get; set; }

        [JsonPropertyName("items"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ItemClass Items { get; set; }

        [JsonPropertyName("description"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }

        [JsonPropertyName("format"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public FormatEnum? Format { get; set; }

        [JsonPropertyName("nullable"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Nullable { get; set; }

        [JsonPropertyName("x-enum-reference"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public XEnumReferenceClass? XEnumReference { get; set; }

        [JsonPropertyName("x-enum-is-bitmask"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? XEnumIsBitmask { get; set; }

        [JsonPropertyName("$ref"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Ref { get; set; }

        [JsonPropertyName("additionalProperties"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AdditionalPropertiesClass? AdditionalProperties { get; set; }

        [JsonPropertyName("x-dictionary-key"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public XDictionaryKeyClass? XDictionaryKey { get; set; }

        [JsonPropertyName("x-mapped-definition"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public XMappedDefinitionClass? XMappedDefinition { get; set; }

        [JsonPropertyName("allOf"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AllOfElementClass[]? AllOf { get; set; }

        [JsonPropertyName("enum"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long[]? Enum { get; set; }

        [JsonPropertyName("x-enum-values"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public XEnumValueClass[]? XEnumValues { get; set; }

        [JsonPropertyName("x-destiny-component-type-dependency"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? XDestinyComponentTypeDependency { get; set; }
    }
}