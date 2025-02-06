using System;
using Newtonsoft.Json;

namespace Core.ServerToClient
{
    [Serializable]
    public sealed class DataPerson
    {
        #region Properties

        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public UserData UserData { get; set; }

        #endregion
    }

    [Serializable]
    public class UserData
    {
        #region Properties

        [JsonProperty("username")]
        public string UserName { get; set; }
        [JsonProperty("age")]
        public string Age { get; set; }

        #endregion
    }
}