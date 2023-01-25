using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinGrid : MonoBehaviour
{
    [SerializeField] public List<SkinObject> skinList;
    [SerializeField] private GameObject skinUIElement, selectButton;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Sprite unacquiredSprite;

    private SkinObject selectedSkin;
    
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (SkinObject skin in skinList)
        {
            if (PlayerPrefsExtra.GetList<SkinObject>("AcquiredSkins").Contains(skin))
            {
                skinUIElement.GetComponent<Image>().sprite = skin.sprite;
            }
            else
            {
                skinUIElement.GetComponent<Image>().sprite = unacquiredSprite;
            }
            skinUIElement.GetComponentInChildren<TextMeshProUGUI>().text = skin.name;
            Instantiate(skinUIElement, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        nameText.text = selectedSkin.name;
    }
}
