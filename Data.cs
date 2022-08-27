using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayFab;
using PlayFab.ClientModels;

using TMPro;
using UnityEngine.UI;

public class Data : MonoBehaviour
{
    [Header("Yazılar")]
    [SerializeField] TMP_Text Yazı;

    public void GetUserData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
    }

    void OnDataReceived(GetUserDataResult result)
    {
        Debug.Log("User Data Alındı.");
        if (result.Data != null)
        {
            Yazı.text = result.Data["DenemeData"].Value;
        }
    }


    public void SaveData()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"DenemeData", "İstediğiniz Yazı" }
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    void OnDataSend(UpdateUserDataResult result)
    {

    }

    void OnError(PlayFabError error)
    {
        
    }
}
