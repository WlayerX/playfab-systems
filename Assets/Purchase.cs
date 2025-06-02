using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class Purchase : MonoBehaviour
{
    public string itemId;       // Satın alınacak öğenin kimliği
    public int price;           // Öğenin fiyatı
    public string currencyType; // Kullanılan sanal para birimi

    // Satın alma işlemi başlatmak için kullanılan fonksiyon
    public void PurchaseItem()
    {
        var request = new PurchaseItemRequest
        {
            Price = price,
            ItemId = itemId,
            VirtualCurrency = currencyType,
        };
        PlayFabClientAPI.PurchaseItem(request, OnPurchaseSuccess, OnError);
    }

    // Satın alma başarılı olduğunda çağrılan geri çağırma fonksiyonu
    void OnPurchaseSuccess(PurchaseItemResult result)
    {
        Debug.Log("Satın Alma Başarılı. Satın Alınan Öğe: " + result.Items[0].DisplayName);

        // Eğer envanter güncelleniyorsa, mevcut envanteri getir
        // Bu örnekte, envanter güncellendiğinde GetPlayerInventory fonksiyonu çağrılır
        GetPlayerInventory(UpdateInventory);
    }

    // Oyuncu envanterini temsil eden PlayerItem sınıfı
    [System.Serializable]
    public class PlayerItem
    {
        public string itemId;
        public int quantity;
        // Diğer ihtiyacınız olan öğe bilgilerini buraya ekleyebilirsiniz.
    }

    // Oyuncu envanterini almak için kullanılan fonksiyon
    private void GetPlayerInventory(Action<List<PlayerItem>> onInventoryUpdated)
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), result =>
        {
            List<PlayerItem> playerInventory = new List<PlayerItem>();

            // PlayFab'den alınan öğelerle envanteri doldur
            foreach (ItemInstance itemInstance in result.Inventory)
            {
                PlayerItem playerItem = new PlayerItem();
                playerItem.itemId = itemInstance.ItemId;
                playerItem.quantity = itemInstance.RemainingUses.HasValue ? itemInstance.RemainingUses.Value : 1;

                // Diğer ihtiyacınız olan öğe bilgilerini buraya ekleyebilirsiniz.

                playerInventory.Add(playerItem);
            }

            // Güncellenmiş envanterle geri çağırma fonksiyonunu çağır
            onInventoryUpdated?.Invoke(playerInventory);
        }, OnError);
    }

    // Envanteri güncellemek için geri çağırma fonksiyonu
    private void UpdateInventory(List<PlayerItem> playerInventory)
    {
        // Envantarı güncellemek için istediğiniz işlemleri buraya ekleyin.
        // Örneğin, güncellenmiş envanteri ekranda gösterme, UI güncelleme, vb.
    }

    // PlayFab'den gelen hataları işleme fonksiyonu
    void OnError(PlayFabError error)
    {
        Debug.LogError("PlayFab Hatası: " + error.ErrorMessage);
        // Buraya ek başka hata işleme mantığı ekleyebilirsiniz.
    }
}
