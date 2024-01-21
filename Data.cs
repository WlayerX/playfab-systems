using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.UI;

public class OyuncuVerileri : MonoBehaviour
{
    [Header("UI Elementleri")]
    [SerializeField] TMP_Text yazı;

    // Oyuncu verilerini PlayFab'dan çekme
    public void OyuncuVerileriniAl()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnVeriAlındı, OnHata);
    }

    // Oyuncu verileri başarıyla alındığında çağrılan fonksiyon
    void OnVeriAlındı(GetUserDataResult sonuç)
    {
        Debug.Log("Oyuncu Verileri Alındı.");
        if (sonuç.Data != null && sonuç.Data.ContainsKey("DenemeData"))
        {
            // Eğer "DenemeData" adında bir veri varsa, UI üzerinde göster
            yazı.text = sonuç.Data["DenemeData"].Value;
        }
        else
        {
            Debug.LogWarning("DenemeData bulunamadı veya değeri boş.");
        }
    }

    // Oyuncu verilerini PlayFab'a kaydetme
    public void VeriKaydet()
    {
        var istek = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"DenemeData", "İstediğiniz Yazı" }
            }
        };
        PlayFabClientAPI.UpdateUserData(istek, OnVeriGönderildi, OnHata);
    }

    // Oyuncu verileri başarıyla kaydedildiğinde çağrılan fonksiyon
    void OnVeriGönderildi(UpdateUserDataResult sonuç)
    {
        Debug.Log("Oyuncu Verileri Başarıyla Gönderildi.");
        // Buraya isteğe bağlı ek işlemler eklenebilir.
    }

    // PlayFab'dan gelen hataları işleme
    void OnHata(PlayFabError hata)
    {
        Debug.LogError("PlayFab Hatası: " + hata.ErrorMessage);
        // Hata durumlarına özel işlemler buraya eklenebilir.
    }
}
