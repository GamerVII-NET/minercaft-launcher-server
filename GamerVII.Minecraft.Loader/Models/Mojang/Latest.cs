using Newtonsoft.Json;

namespace GamerVII.Minecraft.Loader.Models.Mojang;

public record Latest(
    [property: JsonProperty("release")] string Release,
    [property: JsonProperty("snapshot")] string Snapshot
);