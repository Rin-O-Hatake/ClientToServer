using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace Core.ServerToClient
{
    public struct DefaultRequestOut
    {
        #region Properties

        public bool Success { get; }
        public string Response { get; }
        public bool IsTimeOun { get; }
        public int TriesAmount { get; }

        #endregion

        public DefaultRequestOut(bool success, string respons, bool timeOun, int triesAmount)
        {
            Success = success;
            Response = respons;
            IsTimeOun = timeOun;
            TriesAmount = triesAmount;
        }
    }

    public  class DefaultRequestParams
    {
        #region Properties

        public string Url { get; private set; }

        public APIRequestType RequestType { get; set; } = APIRequestType.POST;

        public List<IMultipartFormSection> Form { get; private set; }

        public float TimeOut { get; }

        public int MaxRetries { get; set; }

        public CancellationToken CancellationToken { get; internal set; }

        public Action<string> SuccessCallback { get; }

        public Action<string> ErrorCallback { get; }

        #endregion

        public DefaultRequestParams(
            string url, List<IMultipartFormSection> form,
            float timeOut = 30, int maxRetries = 1,
            CancellationToken cancellationToken = default,
            Action<string> successCallback = null, Action<string> errorCallback = null)
        {
            Url = url;
            Form = form;
            TimeOut = timeOut;
            MaxRetries = maxRetries;
            CancellationToken = cancellationToken;
            SuccessCallback = successCallback;
            ErrorCallback = errorCallback;
        }
        
        internal void TriggerAPIEvent(bool success, string response)
        {
            if (success)
            {
                SuccessCallback?.Invoke(response);
                return;
            }
            
            ErrorCallback?.Invoke(response);
        }
    }

    public enum APIRequestType
    {
        GET,
        POST,
        PUT,
        DELETE
    }
    
    public sealed class ErrorData
    {
        #region Properties

        [JsonProperty("message")]
        public string Message { get; internal set; }

        [JsonProperty("code")]
        public int Code { get; private set; }

        [JsonProperty("type")]
        public string Type { get; private set; }

        #endregion

        public static ErrorData Create(string message, int code = default, string type = default)
            => new() { Message = message, Code = code, Type = type };

        public static ErrorData Create(Exception exception)
            => Create(exception.Message);
    }
    
    public sealed class ErrorRootData
    {
        #region Properties

        [JsonProperty("error")]
        public ErrorData Error { get; private set; }

        #endregion
    }
}
