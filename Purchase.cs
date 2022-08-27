using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class Purchase : MonoBehaviour
{
    public string ItemIds;
    public int Prices;

    public string CurrencyType;

    public void PurchaseItem()
    {
        var request = new PurchaseItemRequest
        {
            Price = Prices,
            ItemId = ItemIds,
            VirtualCurrency = CurrencyType,
        };
        PlayFabClientAPI.PurchaseItem(request, PurchaseItemResult, OnError);
    }

    public void PurchaseItemResult(PurchaseItemResult result)
    {
        Debug.Log("Baþarýyla Satýn Alýndý" + result.Items);
    }

    void OnError(PlayFabError error)
    {

    }
}
