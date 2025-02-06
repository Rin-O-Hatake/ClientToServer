using System;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Core.ServerToClient
{
    public static class ServerManager
    {
        public static async UniTask<DefaultRequestOut> SendRequestAsync(DefaultRequestParams defaultRequestParams)
        {
            if (defaultRequestParams.CancellationToken == default ||
                defaultRequestParams.CancellationToken == CancellationToken.None)
            {
                defaultRequestParams.CancellationToken = Application.exitCancellationToken;
            }
            
            UnityWebRequest request = null;
            
            int retries = 0;
            int maxRetries = defaultRequestParams.MaxRetries;
            
            string response;

            while (true)
            {
                retries++;

                try
                {
                    request = GenerateUnityWebRequest(defaultRequestParams);
                    await request.SendWebRequest()
                        .WithCancellation(defaultRequestParams.CancellationToken)
                        .Timeout(TimeSpan.FromSeconds(defaultRequestParams.TimeOut));
                    break;
                }
                catch (Exception exception)
                {
                    request!.Abort();
                    await UniTask.WaitUntil(() => request.isDone);
                    
                    maxRetries--;
                    if (maxRetries > 0)
                    {
                        continue;
                    }

                    response = request.downloadHandler.text;;
                    return HandleError(exception is TimeoutException, retries);
                }
            }
            
            response = request.downloadHandler.text;
            Debug.Log(request.downloadHandler.text);
            return request.result == UnityWebRequest.Result.Success
                ? HandleSuccess()
                : HandleError(false, retries);
            
            DefaultRequestOut HandleSuccess()
                => CreateResponseData(true);

            DefaultRequestOut HandleError(bool isTimeOut, int tryAmount)
            {
                Debug.LogError($"<color=red>API</color> {defaultRequestParams.Url} failed! Error: {response}");
                return CreateResponseData(false, isTimeOut, tryAmount);
            }
            
            DefaultRequestOut CreateResponseData(bool success, bool isTimeOut = false, int tryAmount = 1)
            {
                request.Dispose();
                var responseData = new DefaultRequestOut(success, response, isTimeOut, tryAmount);
                defaultRequestParams.TriggerAPIEvent(success, response);
                return responseData;
            }
        }
        
        private static UnityWebRequest GenerateUnityWebRequest(DefaultRequestParams defaultRequestParams)
        {
            UnityWebRequest request = UnityWebRequest.Post($"http://localhost:3000/{defaultRequestParams.Url}", defaultRequestParams.Form);
            
            request.downloadHandler = new DownloadHandlerBuffer();
            
            return request;
        }
    }
}

