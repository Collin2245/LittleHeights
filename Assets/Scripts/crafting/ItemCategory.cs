using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemCategory : MonoBehaviour  , IPointerDownHandler
{
    public Image ItemCategoryImage;
    public List<string> Items;
    public string CategoryName;
    public int CategoryBoxId;
    CurrentCategorySelector CurrentCategorySelector;

    private void Start()
    {
        //ItemCategoryImage = this.GetComponent<Image>();
        CurrentCategorySelector = GameObject.Find("CurrentCategory").GetComponent<CurrentCategorySelector>();
    }

    public void GenerateImage()
    {
        ItemCategoryImage = this.GetComponent<Image>();
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
        ItemCategoryImage = this.GetComponent<Image>();
        ItemCategoryImage.color = Color.clear;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        CurrentCategorySelector.UpdatePosition(eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemCategory>().CategoryBoxId);
    }

}
