using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private GameObject notification, purchaseText;
// Start is called before the first frame update
    private void Start()
    {
        CheckNotifications();
    }

    public void CheckNotifications()
    {
        if (PlayerPrefsExtra.GetList<SkinObject>("acquiredSkin").Count == 0 || PlayerPrefsExtra.GetList<SkinObject>("acquiredSkin")==null)
        {
            PlayerPrefsExtra.SetList("acquiredSkin",
                new List<SkinObject>() { PlayerPrefsExtra.GetList<SkinObject>("skinList")[0] });
            CheckNotifications();
        }
        bool canBuy = false;
        int money = PlayerPrefs.GetInt("Coins");
        List<SkinObject> skinList = PlayerPrefsExtra.GetList<SkinObject>("skinList");
        List<SkinObject> acquiredSkins = PlayerPrefsExtra.GetList<SkinObject>("acquiredSkin");
        foreach(SkinObject skin in skinList)
        {
            Debug.Log(skin.name + ", " + acquiredSkins.Contains(skin));
            if (money > 0 && !acquiredSkins.Contains(skin) && skin.price <= money)
            {
                canBuy = true;
            }
        }
        
        notification.SetActive(canBuy);
        purchaseText.SetActive(canBuy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}