using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemBase : MonoBehaviour
    {
        public ClothType clothType;
        public SFXType sfxType;
        public float durationChange = 2f;
        public string compareTag = "Player";

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        public virtual void Collect()
        {
            Debug.Log("Collected Cloth");
            var setup = ClothManager.Instance.GetSetupByType(clothType);
            Player.Instance.ChangeTexture(setup, false);
            PlaySFX();
            HideObject();
        }

        private void PlaySFX()
        {
            SFXPool.Instance.Play(sfxType);
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }
    }
}
