using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class PlayerItem
    {
        public string itemId;
        public int quantity;
        // Buraya gerekli diğer öğe bilgilerini ekleyebilirsiniz.
    }

    private List<PlayerItem> playerInventory = new List<PlayerItem>();

    // Oyuncunun envanterini almak için fonksiyon
    public void GetPlayerInventory(Action<List<PlayerItem>> onInventoryUpdated)
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), result =>
        {
            Debug.Log("Oyuncu Envanteri Alındı.");

            // Mevcut envanteri temizle
            playerInventory.Clear();

            // PlayFab'den alınan öğelerle envanteri doldur
            foreach (ItemInstance itemInstance in result.Inventory)
            {
                PlayerItem playerItem = new PlayerItem();
                playerItem.itemId = itemInstance.ItemId;
                playerItem.quantity = itemInstance.RemainingUses.HasValue ? itemInstance.RemainingUses.Value : 1;

                // Buraya gerekli diğer öğe bilgilerini ekleyebilirsiniz.

                playerInventory.Add(playerItem);
            }

            // Güncellenmiş envanter ile geri çağrı fonksiyonunu çağır
            onInventoryUpdated?.Invoke(playerInventory);
        }, OnError);
    }

    // Oyuncunun envanterinden bir öğe kullanmak için fonksiyon
    public void UseItem(string itemId, Action<List<PlayerItem>> onInventoryUpdated)
    {
        // Envanterde öğeyi bul
        PlayerItem item = playerInventory.Find(x => x.itemId == itemId);

        // Öğe bulundu ve pozitif miktarda ise kullan
        if (item != null && item.quantity > 0)
        {
            // Öğeyi kullanma mantığını burada uygula

            // Miktarı güncelle ve geri çağrı fonksiyonunu güncellenmiş envanterle çağır
            item.quantity--;
            onInventoryUpdated?.Invoke(playerInventory);
        }
        else
        {
            Debug.LogWarning("Öğe bulunamadı veya stokta yok.");
        }
    }

    // Oyuncunun envanterine öğe eklemek için fonksiyon
    public void AddItem(string itemId, int quantity, Action<List<PlayerItem>> onInventoryUpdated)
    {
        // Öğenin envanterde olup olmadığını kontrol et
        PlayerItem existingItem = playerInventory.Find(x => x.itemId == itemId);

        // Eğer öğe bulunduysa miktarını arttır
        if (existingItem != null)
        {
            existingItem.quantity += quantity;
        }
        else
        {
            // Eğer öğe envanterde yoksa ekle
            PlayerItem newItem = new PlayerItem { itemId = itemId, quantity = quantity };
            playerInventory.Add(newItem);
        }

        // Güncellenmiş envanterle geri çağrı fonksiyonunu çağır
        onInventoryUpdated?.Invoke(playerInventory);
    }

    // Oyuncunun envanterinden bir öğeyi kaldırmak için fonksiyon
    public void RemoveItem(string itemId, Action<List<PlayerItem>> onInventoryUpdated)
    {
        // Öğeyi envanterden kaldır
        playerInventory.RemoveAll(x => x.itemId == itemId);

        // Güncellenmiş envanterle geri çağrı fonksiyonunu çağır
        onInventoryUpdated?.Invoke(playerInventory);
    }

    // PlayFab hatalarını ele almak için fonksiyon
    private void OnError(PlayFabError error)
    {
        Debug.LogError("PlayFab Hatası: " + error.ErrorMessage);
        // Buraya ek hata işleme mantığı ekleyebilirsiniz.
    }
}
