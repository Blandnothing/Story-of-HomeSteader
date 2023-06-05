using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIFrameWork
{
    public class UIManager
    {
        private static UIManager instance;
        public static UIManager Instance { get { if (instance == null) instance = new UIManager(); return instance; } }
        private Dictionary<string, string> pathDict;
        private UIManager()
        {
            InitDicts();
        }

        private void InitDicts()
        {
            pathDict = new Dictionary<string, string>()
            {
                {UIConst.InventoryPanel,"Assets/Prefabs/UI/Inventory Panel.prefab" },
                {UIConst.SettingsPanel,"Assets/Prefabs/UI/Settings Panel.prefab" }
            };   
        }
    }

    public class UIConst
    {
        public const string InventoryPanel = "InventoryPanel";
        public const string SettingsPanel = "SsttingsPanel";
    }
}
