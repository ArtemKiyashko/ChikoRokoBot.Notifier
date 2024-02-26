using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ChikoRokoBot.Notifier.Models
{
    public record Drop(
        [property: JsonPropertyName("id")] int? Id,
        [property: JsonPropertyName("title")] string Title,
        [property: JsonPropertyName("description")] string Description,
        [property: JsonPropertyName("created")] DateTime Created,
        [property: JsonPropertyName("finish")] DateTime Finish,
        [property: JsonPropertyName("start")] DateTime Start,
        [property: JsonPropertyName("toyid")] int? Toyid,
        [property: JsonPropertyName("mechanic")] string Mechanic,
        [property: JsonPropertyName("allowedCountries")] IReadOnlyList<object> AllowedCountries,
        [property: JsonPropertyName("blindBoxId")] int? BlindBoxId,
        [property: JsonPropertyName("alternativeCollect")] string AlternativeCollect,
        [property: JsonPropertyName("order")] int Order,
        [property: JsonPropertyName("toy")] Toy Toy
    );
}

