using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientBox : MonoBehaviour
{
    public Image IngredientImage;


    public void GenerateImage(string ItemName)
    {
        IngredientImage = this.GetComponent<Image>();
        if (IngredientImage.color == Color.clear)
        {
            IngredientImage.color = Color.white;
        }
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
        IngredientImage = this.GetComponent<Image>();
        IngredientImage.color = Color.clear;
    }

    public void GenerateQuantity(int count)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = "x " + count.ToString();
    }
    public void ClearQuantity()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = "";
    }
}
