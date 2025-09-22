using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Studio.Core.Singleton;

namespace Cloth
{
    public enum ClothType 
    {
        NONE,
        ALTERNATIVE,
        SPEED,
        STRONG,
    }

    public class ClothManager : Singleton<ClothManager>
    {
        public List<ClothSetup> clothSetups;
        public ClothSetup _curClothSetup;

        public ClothSetup GetSetupByType(ClothType typeOfCloth)
        {
            _curClothSetup = clothSetups.Find(i => i.clothType == typeOfCloth);
            SaveManager.Instance.Setup.clothSetup = _curClothSetup;
            return _curClothSetup;
        }

        public ClothSetup GetCurrentSetup()
        {
            return _curClothSetup;
        }

        public ClothSetup ResetSetup()
        {
            return GetSetupByType(ClothType.NONE);
        }
    }

    [System.Serializable]
    public class ClothSetup
    {
        public ClothType clothType;
        public Texture2D texture;
    }
}
