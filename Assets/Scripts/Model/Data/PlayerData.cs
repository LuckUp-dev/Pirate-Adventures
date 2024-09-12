using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Model.Data
{
    [Serializable]
    public class PlayerData 
    {
        [SerializeField] private InventoryData _inventory;

        public int Hp;

        public InventoryData Inventory => _inventory;

    }
}