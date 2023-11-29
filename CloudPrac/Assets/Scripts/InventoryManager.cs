using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Weapon;
    [SerializeField] TextMeshProUGUI Consumables;
    [SerializeField] TextMeshProUGUI Player;

    public Dictionary<string, CatalogItem> Map = new Dictionary<string, CatalogItem>();

    void UpdateWeapon(string msg)
    {
        Debug.Log(msg);
        Weapon.text += msg + '\n';
    }

    void UpdateConsumables(string msg)
    {
        Debug.Log(msg);
        Consumables.text += msg + '\n';
    }

    void UpdatePlayer(string msg)
    {
        Debug.Log(msg);
        Player.text += msg + '\n';
    }

    void OnError(PlayFabError e)
    {
        UpdatePlayer(e.GenerateErrorReport());
    }

    public void LoadScene(string scn)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scn);
    }

    public void GetVirtualCurrencies() 
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(),
            r => {
                    int gold = r.VirtualCurrency["GD"];
                    int diamond = r.VirtualCurrency["DM"];
                    UpdatePlayer("Gold: " + gold + '\n' + "Diamond: " + diamond);
            }, OnError);
    }

    public void AddGold()
    {
        var addreq = new AddUserVirtualCurrencyRequest
        {
            Amount = 50,
            VirtualCurrency = "GD"
        };
        PlayFabClientAPI.AddUserVirtualCurrency(addreq,
            result => { UpdatePlayer("Added Gold"); },
            OnError);
    }

    public void AddDiamond()
    {
        var addreq = new AddUserVirtualCurrencyRequest
        {
            Amount = 50,
            VirtualCurrency = "DM"
        };
        PlayFabClientAPI.AddUserVirtualCurrency(addreq,
            result => { UpdatePlayer("Added Diamond"); },
            OnError);
    }

    public void GetWeapStore()
    {
        var primaryCatalogName = "Shop"; // In your game, this should just be a constant matching your primary catalog
        var storeId = "WeapSh"; // In your game, this should be a constant for a permanent store, or retrieved from titleData for a time-sensitive store
        var request = new GetStoreItemsRequest
        {
            CatalogVersion = primaryCatalogName,
            StoreId = storeId
        };
        PlayFabClientAPI.GetStoreItems(request, 
            result =>
            {
                List<StoreItem> items = result.Store;
                UpdateWeapon("Shop Items");
                foreach (StoreItem i in items)
                {
                    UpdateWeapon(i.ItemId + "," + i.VirtualCurrencyPrices["GD"]);
                }

            }, OnError);
    }

    public void GetConStore()
    {
        var primaryCatalogName = "Shop"; // In your game, this should just be a constant matching your primary catalog
        var storeId = "ConSh"; // In your game, this should be a constant for a permanent store, or retrieved from titleData for a time-sensitive store
        var request = new GetStoreItemsRequest
        {
            CatalogVersion = primaryCatalogName,
            StoreId = storeId
        };
        PlayFabClientAPI.GetStoreItems(request,
            result =>
            {
                List<StoreItem> items = result.Store;
                UpdateConsumables("Shop Items");
                foreach (StoreItem i in items)
                {
                    UpdateConsumables(i.ItemId + "," + i.VirtualCurrencyPrices["GD"]);
                }

            }, OnError);
    }

    public void BuyLaser()
    {
        var buyreq = new PurchaseItemRequest
        {
            CatalogVersion = "Shop",
            StoreId = "WeapSh",
            ItemId = "Weap01LC",
            VirtualCurrency = "DM",
            Price = 2
        };
        PlayFabClientAPI.PurchaseItem(buyreq,
            result => { UpdateWeapon("Bought");},
            OnError);
    }

    public void BuyPlasma()
    {
        var buyreq = new PurchaseItemRequest
        {
            CatalogVersion = "Shop",
            StoreId = "WeapSh",
            ItemId = "Weap02PC",
            VirtualCurrency = "DM",
            Price = 4
        };
        PlayFabClientAPI.PurchaseItem(buyreq,
            result => { UpdateWeapon("Bought"); },
            OnError);
    }

    public void BuyWeapBundle()
    {
        var buyreq = new PurchaseItemRequest
        {
            CatalogVersion = "Shop",
            StoreId = "WeapSh",
            ItemId = "B001",
            VirtualCurrency = "DM",
            Price = 5
        };
        PlayFabClientAPI.PurchaseItem(buyreq,
            result => { UpdateWeapon("Bought"); },
            OnError);
    }

    public void BuyHealth()
    {
        var buyreq = new PurchaseItemRequest
        {
            CatalogVersion = "Shop",
            StoreId = "ConSh",
            ItemId = "Con01HP",
            VirtualCurrency = "GD",
            Price = 4
        };
        PlayFabClientAPI.PurchaseItem(buyreq,
            result => { UpdateConsumables("Bought"); },
            OnError);
    }

    public void BuyShield()
    {
        var buyreq = new PurchaseItemRequest
        {
            CatalogVersion = "Shop",
            StoreId = "ConSh",
            ItemId = "Con02SH",
            VirtualCurrency = "GD",
            Price = 2
        };
        PlayFabClientAPI.PurchaseItem(buyreq,
            result => { UpdateConsumables("Bought"); },
            OnError);
    }

    public void BuyConBundle()
    {
        var buyreq = new PurchaseItemRequest
        {
            CatalogVersion = "Shop",
            StoreId = "ConSh",
            ItemId = "CON01",
            VirtualCurrency = "GD",
            Price = 5
        };
        PlayFabClientAPI.PurchaseItem(buyreq,
            result => { UpdateConsumables("Bought"); },
            OnError);
    }


    public void GetPlayerInventory() 
    {
        var UserInv = new GetUserInventoryRequest();
        PlayFabClientAPI.GetUserInventory(UserInv,
            result =>
            {
                List<ItemInstance> ii = result.Inventory;
                UpdatePlayer("Player inventory");
                foreach (ItemInstance i in ii)
                {
                    UpdatePlayer(i.DisplayName + "," + i.ItemId + "," + i.ItemInstanceId);
                }
            }, OnError);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
