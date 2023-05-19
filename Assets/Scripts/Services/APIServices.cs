using UnityEngine.Networking;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public static class APIHelper{
    // private static string APIURL = "http://192.168.0.103:9000";
    private static string APIURL = "https://api-multiplayer-usermanagement.odinflux.com";
    private const string defaultContentType = "application/json";

    public static IEnumerator GetRequest(string path,System.Action<string> callback, IEnumerable<RequestHeader> headers = null){
        using(UnityWebRequest request = UnityWebRequest.Get($"{APIURL}/{path}")){
             if(headers != null)
            {
                foreach (RequestHeader header in headers)
                {
                    request.SetRequestHeader(header.Key, header.Value);
                }
            }
            yield return request.SendWebRequest();
            switch(request.result){
                case UnityWebRequest.Result.Success:
                    Debug.Log("Success");
                    callback(request.downloadHandler.text);
                    break;
                case UnityWebRequest.Result.ConnectionError:
                    Debug.Log("Some Error1");
                    callback(null);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.Log("Some Error2");
                    callback(request.downloadHandler.text);
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.Log("Some Error3");
                    callback(null);
                    break;
            }
        }
    }
    public static IEnumerator PostRequest(string path,string body,System.Action<string> callback, IEnumerable<RequestHeader> headers = null){
        using(UnityWebRequest request = UnityWebRequest.Post($"{APIURL}/{path}",body)){

            if(headers != null)
            {
                foreach (RequestHeader header in headers)
                {
                    request.SetRequestHeader(header.Key, header.Value);
                }
            }
            request.uploadHandler.contentType = defaultContentType;
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(body));
            yield return request.SendWebRequest();
            switch(request.result){
                case UnityWebRequest.Result.Success:
                    Debug.Log("Success");
                    callback(request.downloadHandler.text);
                    break;
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.ProtocolError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.Log("Some Error");
                    callback(null);
                    break;
            }
        }
    }
}