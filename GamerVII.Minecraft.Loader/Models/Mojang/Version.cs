using Newtonsoft.Json;

namespace GamerVII.Minecraft.Loader.Models.Mojang;

public record Version(
    [property: JsonProperty("id")] string Id,
    [property: JsonProperty("type")] string Type,
    [property: JsonProperty("url")] string Url,
    [property: JsonProperty("time")] DateTime? Time,
    [property: JsonProperty("releaseTime")]
    DateTime? ReleaseTime
);