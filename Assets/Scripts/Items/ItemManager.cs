using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Studio.Core.Singleton;

namespace Items
{
    public enum ItemType
    {
        COIN,
        LIFE_PACK
    }
    public class ItemManager : Singleton<ItemManager>
    {
        public List<ItemSetup> itemSetups;

        private void Start()
        {
            Reset();
            LoadItemsFromSave();
        }

        public void LoadItemsFromSave()
        {
            AddByType(ItemType.COIN, SaveManager.Instance.Setup.coins);
            AddByType(ItemType.LIFE_PACK, SaveManager.Instance.Setup.medPacks);
        }

        private void Reset()
        {
            if (itemSetups != null)
            {
                foreach (var i in itemSetups)
                {
                    i.soInt.value = 0;
                }
            }
        }

        public ItemSetup GetItemByType(ItemType itemType)
        {
            return itemSetups.Find(i => i.itemType == itemType);
        }

        //caso não seja passado no parametro, ele vai ser naturalmente 1
        public void AddByType(ItemType itemType, int amount = 1)
        {
            if (amount < 0) return;
            itemSetups.Find(i => i.itemType == itemType).soInt.value += amount;
        }

        public void RemoveByType(ItemType itemType, int amount = 1)
        {
            var item = itemSetups.Find(i => i.itemType == itemType);
            item.soInt.value -= amount;

            if(item.soInt.value < 0) item.soInt.value = 0;
        }

        [NaughtyAttributes.Button]
        private void AddCoin()
        {
            AddByType(ItemType.COIN, 1);
        }

        [NaughtyAttributes.Button]
        private void AddLifePack()
        {
            AddByType(ItemType.LIFE_PACK, 1);
        }

    }

    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt soInt;
        public Sprite icon;
    }
}

