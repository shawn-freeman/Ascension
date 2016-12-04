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
    private const string CREATE_ACCOUNT = "CreateAccount";

    public InputField Username = null;
    public InputField Password = null;
    public InputField Email = null;
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

    public GameObject CreateAccountPanel = null;
    private Animator CreateAccountPanelAnimator;

    public HttpHandler httpHandler;

    public void Start()
    {
        httpHandler = GetComponent<HttpHandler>(); ;
        if (LoginPanel != null) LoginPanelAnimator = LoginPanel.GetComponent<Animator>();
        if(MainMenuPanel != null) MainMenuPanelAnimator = MainMenuPanel.GetComponent<Animator>();
        if(ChangePasswordPanel != null) ChangePasswordPanelAnimator = ChangePasswordPanel.GetComponent<Animator>();
        if(CreateAccountPanel != null) CreateAccountPanelAnimator = CreateAccountPanel.GetComponent<Animator>();
    }

    #region MenuNavigation

    public void TransToLogin(string from)
    {
        Username.text = string.Empty;
        Password.text = string.Empty;
        ErrorText.text = string.Empty;
        switch (from)
        {
            case MAIN_MENU:
                LoginPanelAnimator.SetBool(AnimationHashes.MENU_SwingFrontToView, true);
                MainMenuPanelAnimator.SetBool(AnimationHashes.MENU_SwingViewToFront, true);
                break;
            case CREATE_ACCOUNT:
                LoginPanelAnimator.SetBool(AnimationHashes.MENU_SwingFrontToView, true);
                CreateAccountPanelAnimator.SetBool(AnimationHashes.MENU_SwingViewToFront, true);
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

    public void TransToCreateAccount(string from)
    {
        Username.text = string.Empty;
        Password.text = string.Empty;
        Email.text = string.Empty;
        ErrorText.text = string.Empty;
        switch (from)
        {
            case LOGIN:
                LoginPanelAnimator.SetBool(AnimationHashes.MENU_SwingViewToFront, true);
                CreateAccountPanelAnimator.SetBool(AnimationHashes.MENU_SwingFrontToView, true);
                break;
        }
    }

    #endregion

    #region Login

    public void OnLoginClicked()
    {
        LoadingIndicator.SetActive(true);

        var loginRequest = new LoginRequest()
        {
            Username = Username.text,
            Password = Password.text
        };

        httpHandler.GET<LoginRequest, ReturnResult<UserInfo>>(ControllerTypes.Login, loginRequest, OnLoginResponse);
    }

    public void OnLoginResponse(ReturnResult<UserInfo> response)
    {
        LoadingIndicator.SetActive(false);

        if (response.HasError)
        {
            ErrorText.text = "Login Failed.";
            return;
        }

        TransToMainMenu(LOGIN);
    }

    public void OnLogoutClicked()
    {
        TransToLogin(MAIN_MENU);
    }

    #endregion

    #region ChangePassword
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

    #endregion

    #region CreateAccount

    public void OnCreateAccountClicked()
    {
        TransToCreateAccount(LOGIN);
    }

    public void OnAttemptCreateAccountClicked()
    {
        if (string.IsNullOrEmpty(Username.text) || 
            string.IsNullOrEmpty(Email.text) || 
            string.IsNullOrEmpty(Password.text) || 
            string.IsNullOrEmpty(ConfirmPassword.text)) {
            ErrorText.text = "All fields are required.";
            return;
        }
        //client side validation
        ErrorText.text = "";
        if (Password.text != ConfirmPassword.text)
        {
            ErrorText.text = "Passwords does not match.";
            return;
        }

        LoadingIndicator.SetActive(true);

        var accountRequest = new CreateAccountRequest();
        accountRequest.Username = Username.text;
        accountRequest.Email = Email.text;
        accountRequest.Password = Password.text;

        httpHandler.POST<CreateAccountRequest, ReturnResult<bool>>(ControllerTypes.Account, accountRequest, OnAttemptCreateAccountResponse);
    }

    public void OnAttemptCreateAccountResponse(ReturnResult<bool> response)
    {
        LoadingIndicator.SetActive(false);
        if (!response.HasError)
        {
            if (response.Value)
            {
                TransToLogin(CREATE_ACCOUNT);
            }else
            {
                ErrorText.text = response.ExceptionMessage;
            }
            
        }else
        {
            ErrorText.text = response.ExceptionMessage;
        }
    }

    #endregion
}
