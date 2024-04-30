using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem {
    public class InventoryUIController : MonoBehaviour
    {
        [SerializeField] private ItemUIController _itemUIPrefab;
        private Canvas _canvas;
        private List<ItemUIController> _itemsUIControllers = new List<ItemUIController>();
        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            Inventory.InventoryUpdated += UpdateItemUI;
            UpdateItemUI(Inventory.CurrentItems);
        }

        private void UpdateItemUI(Item[] items)
        {
            while (_itemsUIControllers.Count < items.Length)
            {
                ItemUIController new_itemUI = Instantiate(_itemUIPrefab, transform);
                _itemsUIControllers.Add(new_itemUI);
            }
            foreach(ItemUIController itemUI in _itemsUIControllers)
            {
                int index = _itemsUIControllers.IndexOf(itemUI);
                if(index >= items.Length)
                {
                    _itemsUIControllers.Remove(itemUI);
                    Destroy(itemUI.gameObject);
                }
                else
                {
                    itemUI.ItemData = items[index];
                }
            }
        }
    }
}