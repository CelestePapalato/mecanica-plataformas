using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class InventoryItem : MonoBehaviour, IItem
    {
        [SerializeField] private Item _itemData;
        public void grab()
        {
            Inventory.AddItem(_itemData);
        }
    }
}