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

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemCategory>().CategoryBoxId);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exit");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Mouse Up");
    }

}
