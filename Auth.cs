using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class Auth : MonoBehaviour
{
    public TextMeshProUGUI TopText;
    public TextMeshProUGUI MessageText;

    public TMP_InputField EmailLoginInput;
    public TMP_InputField PasswordLoginput;
    public GameObject LoginPage;

    public TMP_InputField UsernameRegisterInput;
    public TMP_InputField EmailRegisterInput;
    public TMP_InputField PasswordRegisterinput;
    public GameObject RegisterPage;

    private void Start()
    {
        LogineGit();
    }

    public void RegisterUser()
    {
        var request = new RegisterPlayFabUserRequest
        {
            DisplayName = UsernameRegisterInput.text,
            Email = EmailRegisterInput.text,
            Password = PasswordRegisterinput.text,

            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnregisterSucces, OnError);
    }
    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = EmailLoginInput.text,
            Password = PasswordLoginput.text,
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSucces, OnError);
    }

    private void OnLoginSucces(LoginResult result)
    {
        MessageText.text = "Giriþ Yapýlýyor";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnError(PlayFabError Error)
    {
        MessageText.text = Error.ErrorMessage;
        Debug.Log("Hata! " + Error.ErrorDetails);
    }

    private void OnregisterSucces(RegisterPlayFabUserResult Result)
    {
        MessageText.text = "Hesap oluþturuldu.";
        LogineGit();
    }

    public void LogineGit()
    {
        LoginPage.SetActive(true);
        RegisterPage.SetActive(false);
        TopText.text = "Giriþ";
    }
    public void RegistereGit()
    {
        LoginPage.SetActive(false);
        RegisterPage.SetActive(true);
        TopText.text = "Kayýt Ol";
    }
}
