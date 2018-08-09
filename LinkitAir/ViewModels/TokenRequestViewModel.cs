using Newtonsoft.Json;

namespace LinkitAir.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class TokenRequestViewModel
    {
        public string grant_type { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
