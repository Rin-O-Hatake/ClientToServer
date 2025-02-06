using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace Core.ServerToClient
{
    public static class ClientManager
    {
        public static async UniTask<APIResponseDataT<DataPerson>> SetPerson_Server(string name, int age)
        {
            var form = new List<IMultipartFormSection>
            {
                new MultipartFormDataSection("username", name),
                new MultipartFormDataSection("age", age.ToString())
            };

            return await APIResponseDataT<DataPerson>.Create(
                new DefaultRequestParams("upload", form));
        }
    }
}
