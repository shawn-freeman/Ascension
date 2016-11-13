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

    public GameObject LoginPanel = null;
    private Animator LoginPanelAnimator;

    public GameObject MainMenuPanel = null;
    private Animator MainMenuPanelAnimator;

    public void Start()
    {
        LoginPanelAnimator = LoginPanel.GetComponent<Animator>();
        MainMenuPanelAnimator = MainMenuPanel.GetComponent<Animator>();
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
                LoginPanelAnimator.SetBool(AnimationHashes.MENU_SwingViewToFront, true);
                MainMenuPanelAnimator.SetBool(AnimationHashes.MENU_SwingFrontToView, true);
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

    public void Logout()
    {
        LoginPanelAnimator.SetBool(AnimationHashes.MENU_SwingFrontToView, true);
        MainMenuPanelAnimator.SetBool(AnimationHashes.MENU_SwingViewToFront, true);
    }
}
