using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ItemCategory : MonoBehaviour
{
    public Image ItemCategoryImage;
    public List<string> Items;
    public string CategoryName;

    private void Start()
    {
        ItemCategoryImage = this.GetComponent<Image>();
    }

    public void GenerateImage()
    {
        if (Resources.Load<Sprite>("Categories/" + CategoryName) != null)
        {
            ItemCategoryImage.sprite = Resources.Load<Sprite>("Categories/" + CategoryName);
        }
        else
        {
            HideSprite();
        }

    }

    public List<string> GetItems()
    {
        return CraftingProperties.categoyItems[CategoryName];
    }

    public void HideSprite()
    {
        ItemCategoryImage.color = Color.clear;
    }

}
