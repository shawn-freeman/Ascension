using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class LoginButton : MonoBehaviour
{
    private string BaseApiAddress = "http://192.168.1.169:8080/api/Login?";

    public InputField Username = null;
    public InputField Password = null;
    public Text ErrorText = null;
    public GameObject LoadingIndicator = null;

    public GameObject MenuPanel = null;
    private Animator MenuAnimator;

    public void Start()
    {
        MenuAnimator = MenuPanel.GetComponent<Animator>();
    }

    public void OnClick()
    {
        string ApiAddress = string.Format("{0}username={1}&password={2}", BaseApiAddress, Username.text, Password.text);
        WWW www = new WWW(ApiAddress);

        LoadingIndicator.SetActive(true);
        StartCoroutine(AttemptLogin(www));
    }

    public IEnumerator AttemptLogin(WWW www)
    {
        yield return www;

        int userId;
        if (www.error == null && Int32.TryParse(www.text, out userId))
        {
            if (userId != -1)
            {
                ErrorText.text = "Login Successful for: " + www.text;
                MenuAnimator.SetBool(AnimationHashes.MENU_SwingViewToFront, true);
            }
            else
            {
                ErrorText.text = "Username and/or Password was entered incorrect.";
            }
        }else
        {
            ErrorText.text = "Username and/or Password was entered incorrect.";
        }
        LoadingIndicator.SetActive(false);
    }
}
