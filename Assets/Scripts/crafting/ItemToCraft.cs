using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemToCraft : MonoBehaviour, IPointerDownHandler
{
    public Image ItemImage;
    public string ItemName;
    CurrentCraftingItem currentCraftingItem;
    public int ItemBoxId;

    private void Start()
    {
        ItemImage = this.GetComponent<Image>();
        currentCraftingItem = GameObject.Find("CurrentItemToCraft").GetComponent<CurrentCraftingItem>();
    }

    public void GenerateImage()
    {
        ItemImage = this.GetComponent<Image>();
        ItemImage.sprite = Resources.Load<Sprite>("Items/" + ItemName);
    }

    public void HideSprite()
    {
        ItemImage = this.GetComponent<Image>();
        ItemImage.color = Color.clear;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        int index = eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemToCraft>().ItemBoxId;
        currentCraftingItem.UpdatePosition(index);
    }
}
