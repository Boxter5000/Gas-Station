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

        public Sprite Ziptie;
        public Sprite LighterSprite;
        public Sprite DynamitSprite;
        public Sprite KeysSprite;
        public Sprite Battery;

    }
}