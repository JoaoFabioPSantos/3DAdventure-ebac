using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Studio.Core.Singleton;
using System;

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

        private void Start()
        {
            SaveManager.Instance.FileLoaded += OnFileLoaded;
        }

        private void OnDestroy()
        {
            // Importante: Remover a assinatura do evento para evitar erros quando o objeto é destruído.
            SaveManager.Instance.FileLoaded -= OnFileLoaded;
        }

        private void OnFileLoaded(SaveSetup loadedSave)
        {
            _curClothSetup = loadedSave.clothSetup;
        }

        public ClothSetup GetSetupByType(ClothType typeOfCloth)
        {
            _curClothSetup = clothSetups.Find(i => i.clothType == typeOfCloth);
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
