using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using CONDUIT.UnityCL;
using System.Linq;
using System;

public class HttpHandler : MonoBehaviour
{
    private string _baseUrl;
    
    public void Awake()
    {
        _baseUrl = "http://192.168.1.169:8080/api/";
    }

    private IEnumerator WaitForRequest<T>(UnityWebRequest request, Action<T> callback)
    {
        yield return request.Send();

        T response = default(T);
        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log(string.Format("REQUEST COMPLETED - Response: {0}", request.downloadHandler.text));
            if (typeof(T).IsPrimitive)
            {
                response = (T)Convert.ChangeType(request.downloadHandler.text, typeof(T));
            }else
            {
                response = JsonUtility.FromJson<T>(request.downloadHandler.text);
            }

            callback(response);
        }else
        {
            Debug.Log(string.Format("WaitForRequest() ERROR: {0}", request.error));
        }
    }

	public void POST<T>(string controller, object obj, Action<T> callback)
    {
        var jsonObj = JsonUtility.ToJson(obj);

        string ApiAddress = string.Format("{0}{1}?obj={2}", _baseUrl, controller, jsonObj);

        WWWForm form = new WWWForm();
        form.AddField("obj", jsonObj);

        UnityWebRequest request = UnityWebRequest.Post(ApiAddress, form);

        StartCoroutine(WaitForRequest(request, callback));
    }
}
