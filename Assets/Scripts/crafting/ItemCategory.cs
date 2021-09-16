using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.crafting
{
    class ItemCategory : MonoBehaviour
    {
        public Image ItemCategoryImage;
        public List<string> Items;
        public string CategoryName;

        private void Start()
        {
            Items = CraftingRequirements.categoyItems[CategoryName];
        }


    }
}
