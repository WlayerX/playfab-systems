using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class Inventory : MonoBehaviour
{
    public void GetInventory()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnError);
    }

    void OnGetUserInventorySuccess(GetUserInventoryResult result)
    {
        foreach (ItemInstance item in result.Inventory)
        {
            Debug.Log(item.ItemId);
        }
    }

    void OnError(PlayFabError error)
    {
        Debug.LogError(error.ErrorMessage);
    }
}
