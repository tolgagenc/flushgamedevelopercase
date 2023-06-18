using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pools : MonoBehaviour
    {
        public enum Types
        {
            Gem1 = 0,
            Gem2 = 1,
            Gem3 = 2,
            Money = 11,
        }

        public static string GetTypeStr(Types poolType)
        {
            return Enum.GetName(typeof(Types), poolType);
        }
    }
