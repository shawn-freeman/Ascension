using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.Networking;

using CONDUIT.UnityCL.Enums;
using CONDUIT.UnityCL.Transports.Account;
using CONDUIT.UnityCL.Transports.ErrorHandling;

public class MenuSceneButtonHandler : MonoBehaviour
{
    private const string LOGIN = "Login";
    private const string MAIN_MENU = "MainMenu";
    private const string CHANGE_PASSWORD = "ChangePassword";

    public InputField Username = null;
    public InputField Password = null;
    public InputField ChangePassword = null;
    public InputField ConfirmPassword = null;

    public Text ErrorText = null;

    public GameObject LoadingIndicator = null;

    public GameObject LoginPanel = null;
    private Animator LoginPanelAnimator;

    public GameObject MainMenuPanel = null;
    private Animator MainMenuPanelAnimator;

    public GameObject ChangePasswordPanel = null;
    private Animator ChangePasswordPanelAnimator;

    public HttpHandler httpHandler;

    public void Start()
    {
        httpHandler = GetComponent<HttpHandler>(); ;
        if (LoginPanel != null) LoginPanelAnimator = LoginPanel.GetComponent<Animator>();
        if(MainMenuPanel != null) MainMenuPanelAnimator = MainMenuPanel.GetComponent<Animator>();
        if(ChangePasswordPanel != null) ChangePasswordPanelAnimator = ChangePasswordPanel.GetComponent<Animator>();
    }

    public void OnLoginClicked()
    {
        LoadingIndicator.SetActive(true);

        var loginRequest = new LoginRequest()
        {
            Username = Username.text,
            Password = Password.text
        };

        httpHandler.GET<LoginRequest, UserInfo>(ControllerTypes.Json, loginRequest, OnLoginResponse);

        //StartCoroutine(AttemptLogin());
    }

    public void OnLoginResponse(UserInfo response)
    {
        LoadingIndicator.SetActive(false);
    }

    //private IEnumerator AttemptLogin()
    //{
    //    LoadingIndicator.SetActive(true);

    //    string ApiAddress = string.Format("{0}Login?username={1}&password={2}", BaseApiAddress, Username.text, Password.text);
    //    UnityWebRequest request = UnityWebRequest.Get(ApiAddress);

    //    yield return request.Send();

    //    int userId;
    //    if (request.error == null && Int32.TryParse(request.downloadHandler.text, out userId))
    //    {
    //        if (userId != -1)
    //        {
    //            TransToMainMenu(LOGIN);
    //        }
    //        else
    //        {
    //            ErrorText.text = "Username and/or Password was entered incorrect.";
    //        }
    //    }else
    //    {
    //        ErrorText.text = "Username and/or Password was entered incorrect.";
    //    }
    //    LoadingIndicator.SetActive(false);
    //}

    public void OnLogoutClicked()
    {
        TransToLogin(MAIN_MENU);
    }

    public void TransToLogin(string from)
    {
        switch (from)
        {
            case MAIN_MENU:
                Username.text = string.Empty;
                Password.text = string.Empty;
                ErrorText.text = string.Empty;
                LoginPanelAnimator.SetBool(AnimationHashes.MENU_SwingFrontToView, true);
                MainMenuPanelAnimator.SetBool(AnimationHashes.MENU_SwingViewToFront, true);
                break;
        }
    }

    public void TransToMainMenu(string from)
    {
        switch (from)
        {
            case LOGIN:
                LoginPanelAnimator.SetBool(AnimationHashes.MENU_SwingViewToFront, true);
                MainMenuPanelAnimator.SetBool(AnimationHashes.MENU_SwingFrontToView, true);
                break;
            case CHANGE_PASSWORD:
                MainMenuPanelAnimator.SetBool(AnimationHashes.MENU_SwingFrontToView, true);
                ChangePasswordPanelAnimator.SetBool(AnimationHashes.MENU_SwingViewToFront, true);
                break;
        }
    }

    public void TransToChangePassword(string from)
    {
        switch (from)
        {
            case MAIN_MENU:
                ChangePassword.text = string.Empty;
                ConfirmPassword.text = string.Empty;
                MainMenuPanelAnimator.SetBool(AnimationHashes.MENU_SwingViewToFront, true);
                ChangePasswordPanelAnimator.SetBool(AnimationHashes.MENU_SwingFrontToView, true);
                break;
        }
    }

    public void OnChangePasswordClicked()
    {
        TransToChangePassword(MAIN_MENU);
    }

    public void OnAttemptChangePasswordClicked()
    {
        //client side validation
        ErrorText.text = "";
        if (ChangePassword.text != ConfirmPassword.text)
        {
            ErrorText.text = "Passwords does not match!";
            return;
        }

        LoadingIndicator.SetActive(true);

        var changeRequest = new ChangePasswordRequest();
        changeRequest.UserId = 1;
        changeRequest.CurrentPassword = "foobar";
        changeRequest.NewPassword = ChangePassword.text;

        httpHandler.POST<ChangePasswordRequest, bool>(ControllerTypes.Login, changeRequest, OnChangePasswordResponse);
    }

    public void OnChangePasswordResponse(bool success)
    {
        LoadingIndicator.SetActive(false);

        if (success)
        {
            TransToMainMenu(CHANGE_PASSWORD);
        }else
        {
            ErrorText.text = "Failed to change password.";
        }
    }


}
