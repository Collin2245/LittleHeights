using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientBox : MonoBehaviour
{
    public Image IngredientImage;

    private void Start()
    {
        IngredientImage = this.GetComponent<Image>();
        IngredientImage.sprite = null;
    }

    public void GenerateImage(string ItemName)
    {
        IngredientImage.enabled = true;
        try
        {
            IngredientImage.sprite = Resources.Load<Sprite>("Items/" + ItemName);
        }catch
        {
            IngredientImage.sprite = null;
        }
        
    }

    public void HideSprite()
    {
        IngredientImage.color = Color.clear;
    }

    public void GenerateQuantity(int count)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = "x " + count.ToString();
    }
}
