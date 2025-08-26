using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Studio.Core.Singleton;

namespace Cloth
{
    public enum ClothType 
    {
        ALTERNATIVE,
        SPEED,
        STRONG,
    }

    public class ClothManager : Singleton<ClothManager>
    {
        public List<ClothSetup> clothSetups;

        public ClothSetup GetSetupByType(ClothType typeOfCloth)
        {
            return clothSetups.Find(i => i.clothType == typeOfCloth);
        }
    }

    [System.Serializable]
    public class ClothSetup
    {
        public ClothType clothType;
        public Texture2D texture;
    }
}
