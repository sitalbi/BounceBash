using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private GameObject notification;
// Start is called before the first frame update
    void Start()
    {
        /*bool canBuy = false;
        int money = PlayerPrefs.GetInt("Coins");
        foreach(SkinObject skin in PlayerPrefsExtra.GetList<SkinObject>("skinList"))
        {
            if (!skin.isAquired && skin.price <= money)
            {
                canBuy = true;
            }
        }
        
        notification.SetActive(canBuy);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
