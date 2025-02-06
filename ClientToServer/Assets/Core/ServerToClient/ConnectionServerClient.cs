using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace Core.ServerToClient
{
    public sealed class APIResponseDataT<T> where T : class
    {
        #region Properties
        
        public DefaultRequestOut APIResponseData { get; }

        public T Data { get; private set; }

        public bool APISuccess => APIResponseData.Success;

        public bool APISuccessAndValidData => APISuccess && Data != null;

        [CanBeNull]
        public ErrorData Error { get; private set; }

        #endregion

        #region Constructor

        private APIResponseDataT(DefaultRequestOut response)
            => APIResponseData = response;

        #endregion

        private static APIResponseDataT<T> Create(DefaultRequestOut response)
        {
            var responseT = new APIResponseDataT<T>(response);
            if (!response.Success)
            {
                try
                {
                    responseT.Error = JsonConvert.DeserializeObject<ErrorRootData>(response.Response)?.Error;
                    return responseT;
                }
                catch (Exception exception)
                {
                    responseT.Error = ErrorData.Create(exception);
                    return responseT;
                }
            }

            try
            {
                responseT.Data = JsonConvert.DeserializeObject<T>(response.Response);
                return responseT;
            }
            catch (Exception exception)
            {
                responseT.Error = ErrorData.Create(exception);
                return responseT;
            }
        }

        public static async UniTask<APIResponseDataT<T>> Create(DefaultRequestParams requestParams)
            => Create(await ServerManager.SendRequestAsync(requestParams));
    }
}
