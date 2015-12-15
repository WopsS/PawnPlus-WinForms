using Newtonsoft.Json;

namespace PawnPlus.Core
{
    public class InformationJSON
    {
        [JsonProperty("Version")]
        public string Version { get; set; }

        [JsonProperty("SA-MP-Version")]
        public string SAMPVersion { get; set; }

        [JsonProperty("SA-MP-ZIP-Name")]
        public string SAMPZIPName { get; set; }

        [JsonProperty("SA-MP-Files-Link")]
        public string SAMPFilesLink { get; set; }

        [JsonProperty("ZEEX-Compiler-ZIP-Name")]
        public string CompilerZIPName { get; set; }

        [JsonProperty("ZEEX-Compiler-Link")]
        public string CompilerLink { get; set; }
    }
}
