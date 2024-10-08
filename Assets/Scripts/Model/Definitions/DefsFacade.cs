﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/DefsFacade", fileName = "DefsFacade")]

    public class DefsFacade : ScriptableObject
    {
        [SerializeField] private InventoryItemDef _items;

        public InventoryItemDef Items => _items;

        private static DefsFacade _instance;
        public static DefsFacade I => _instance == null ? LoadDefs() : _instance;

        private static DefsFacade LoadDefs()
        {
            return _instance = Resources.Load<DefsFacade>("DefsFacade");
        }
    }
}
