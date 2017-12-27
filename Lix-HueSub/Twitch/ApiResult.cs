using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LixHueSub
{
    namespace ApiResult
    {
        public class Authorization
        {
            [JsonProperty("created_at")]
            public DateTime CreatedAt { get; set; }

            [JsonProperty("scopes")]
            public string[] Scopes { get; set; }

            [JsonProperty("updated_at")]
            public DateTime UpdatedAt { get; set; }
        }

        public class Token
        {
            [JsonProperty("authorization")]
            public Authorization Authorization { get; set; }

            [JsonProperty("client_id")]
            public string ClientId { get; set; }

            [JsonProperty("expires_in")]
            public long ExpiresIn { get; set; }

            [JsonProperty("user_id")]
            public int UserId { get; set; }

            [JsonProperty("user_name")]
            public string UserName { get; set; }

            [JsonProperty("valid")]
            public bool Valid { get; set; }
        }

        public class TokenResult
        {
            public Token token { get; set; }
        }
    }
}