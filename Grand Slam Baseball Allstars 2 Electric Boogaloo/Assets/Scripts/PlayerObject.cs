using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerObject
    {
        public string PlayerName;
        public Sprite PlayerSprite;
        public string PlayerRarity;
        public int PlayerStrength;
        public int PlayerStamina;
        public int PlayerSpeed;
        public int PlayerStyle;
        public int PlayerAccuracy;
        public Ball ball;
        private Sprite sprite;
        public Vector3 oldPosition;
        public int targetBase;
        public bool running;
        public bool safe;

        public PlayerObject()
        {

        }
    }
}
