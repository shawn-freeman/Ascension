using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using CONDUIT.UnityCL;
using System.Linq;
using System;
using CONDUIT.UnityCL.Enums;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CONDUIT.UnityCL.Helpers;
using System.Text.RegularExpressions;

public class HttpHandler : MonoBehaviour
{
    public bool UseLocalAddress;
    private string _baseUrl;
    
    public void Awake()
    {
        if (UseLocalAddress) _baseUrl = "http://localhost:57871/api/";
        else _baseUrl = "http://192.168.1.169:8080/api/";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Object type to be serialized to JSON</typeparam>
    /// <typeparam name="K">Object type your expecting back</typeparam>
    /// <param name="controller">The controller to use</param>
    /// <param name="obj">Object to be serialized to JSON</param>
    /// <param name="callback">The callback function when response is finished</param>
    public void GET<T, K>(ControllerTypes controller, T obj, Action<K> callback)
    {
        var jsonObj = JsonUtility.ToJson(obj);

        string ApiAddress = string.Format("{0}{1}?obj={2}", _baseUrl, controller, jsonObj);

        UnityWebRequest request = UnityWebRequest.Get(ApiAddress);

        StartCoroutine(WaitForRequest(request, callback));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Object type to be serialized to JSON</typeparam>
    /// <typeparam name="K">Object type your expecting back</typeparam>
    /// <param name="controller">The controller to use</param>
    /// <param name="obj">Object to be serialized to JSON</param>
    /// <param name="callback">The callback function when response is finished</param>
    public void POST<T, K>(ControllerTypes controller, T obj, Action<K> callback)
    {
        var jsonObj = JsonUtility.ToJson(obj);

        string ApiAddress = string.Format("{0}{1}?obj={2}", _baseUrl, controller, jsonObj);

        WWWForm form = new WWWForm();
        form.AddField("obj", jsonObj);

        UnityWebRequest request = UnityWebRequest.Post(ApiAddress, form);

        StartCoroutine(WaitForRequest(request, callback));
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
            }
            else
            {
                var json = ParseStr(request.downloadHandler.text);
                response = JsonUtility.FromJson<T>(json);
            }

            callback(response);
        }
        else
        {
            Debug.Log(string.Format("WaitForRequest() ERROR: {0}", request.error));
        }
    }

    private string ParseStr(string shittyStr)
    {
        var json = string.Empty;
        for (var i = 0; i < shittyStr.Length - 1; i++)
        {
            string str = shittyStr[i].ToString();
            //skip the character we dont want
            if (str == @"\" || i == 0) continue;

            json += str;
        }

        return json;
    }
}
