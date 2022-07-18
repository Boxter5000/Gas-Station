using System;
using UnityEngine;

namespace _Scripts.Inventory
{
    public class ItemAssets : MonoBehaviour
    {
        public static ItemAssets Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public Transform itemWorldPf;
        
        public Sprite LighterSprite;
        public Sprite DynamitSprite;
        public Sprite KeysSprite;

    }
}