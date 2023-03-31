using System;
using System.Text.Json.Serialization;

namespace ChikoRokoBot.Notifier.Models
{
    public record ToyModel(
        [property: JsonPropertyName("toyId")] int? ToyId,
        [property: JsonPropertyName("archiveName")] string ArchiveName
    );
}

