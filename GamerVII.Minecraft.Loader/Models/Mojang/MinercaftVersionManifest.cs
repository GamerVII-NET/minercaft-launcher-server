using Newtonsoft.Json;

namespace GamerVII.Minecraft.Loader.Models.Mojang;

public record MinercaftVersionManifest(
    [property: JsonProperty("latest")] Latest Latest,
    [property: JsonProperty("versions")] IReadOnlyList<Version> Versions
);