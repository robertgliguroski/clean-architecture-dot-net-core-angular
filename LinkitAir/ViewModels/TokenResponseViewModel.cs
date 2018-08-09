using Newtonsoft.Json;

namespace LinkitAir.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class TokenResponseViewModel
    {
        public string token { get; set; }
        public int expiration { get; set; }
    }
}
