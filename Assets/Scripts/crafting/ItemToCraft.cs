using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ItemToCraft : MonoBehaviour
{
    public Image ItemImage;
    public string ItemName;

    private void Start()
    {
        ItemImage = this.GetComponent<Image>();
    }

    public void GenerateImage()
    {
        ItemImage.sprite = Resources.Load<Sprite>("Items/" + ItemName);
    }

    public void HideSprite()
    {
        ItemImage.color = Color.clear;
    }
}
