using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

/// <summary>
/// Kullanıcı kimlik doğrulama ve PlayFab servisleri kullanılarak kayıt işlemlerini yönetir.
/// </summary>
public class AuthenticationManager : MonoBehaviour
{
    public TextMeshProUGUI topText;
    public TextMeshProUGUI messageText;

    public TMP_InputField emailLoginInput;
    public TMP_InputField passwordLoginInput;
    public GameObject loginPage;

    public TMP_InputField usernameRegisterInput;
    public TMP_InputField emailRegisterInput;
    public TMP_InputField passwordRegisterInput;
    public GameObject registerPage;

    /// <summary>
    /// Başlangıçta kimlik doğrulama işlemini başlatır.
    /// </summary>
    private void Start()
    {
        InitiateLogin();
    }

    /// <summary>
    /// Yeni bir kullanıcı için kayıt işlemini başlatır.
    /// </summary>
    public void RegisterUser()
    {
        var request = new RegisterPlayFabUserRequest
        {
            DisplayName = usernameRegisterInput.text,
            Email = emailRegisterInput.text,
            Password = passwordRegisterInput.text,

            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    /// <summary>
    /// Mevcut bir kullanıcı için giriş işlemini başlatır.
    /// </summary>
    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailLoginInput.text,
            Password = passwordLoginInput.text,
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    /// <summary>
    /// Başarılı bir giriş için geri çağrı.
    /// </summary>
    private void OnLoginSuccess(LoginResult result)
    {
        messageText.text = "Giriş başarılı";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Kimlik doğrulama sırasında bir hata için geri çağrı.
    /// </summary>
    private void OnError(PlayFabError error)
    {
        messageText.text = error.ErrorMessage;
        Debug.Log("Hata! " + error.ErrorDetails);
    }

    /// <summary>
    /// Başarılı bir kullanıcı kaydı için geri çağrı.
    /// </summary>
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        messageText.text = "Hesap oluşturuldu.";
        InitiateLogin();
    }

    /// <summary>
    /// Arayüzü giriş sayfasına geçirir.
    /// </summary>
    public void InitiateLogin()
    {
        loginPage.SetActive(true);
        registerPage.SetActive(false);
        topText.text = "Giriş";
    }

    /// <summary>
    /// Arayüzü kayıt sayfasına geçirir.
    /// </summary>
    public void InitiateRegistration()
    {
        loginPage.SetActive(false);
        registerPage.SetActive(true);
        topText.text = "Kayıt Ol";
    }
}
