using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemCollectableBase : MonoBehaviour
    {
        public ItemType itemType;
        public SFXType sfxType;

        public string compareTag = "Player";
        public ParticleSystem particleSystem;
        public float timeToHide = 3f;
        public GameObject graphicItem;
        public Collider collider;

        [Header("Sounds")]
        public AudioSource audioSource;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        private void PlaySFX()
        {
            SFXPool.Instance.Play(sfxType);
        }

        protected virtual void Collect()
        {
            PlaySFX();
            if (graphicItem != null) graphicItem.SetActive(false);
            if (collider != null) collider.enabled = false;

            Invoke("HideObject", timeToHide);
            OnCollect();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnCollect()
        {
            if (particleSystem != null) particleSystem.Play();
            if (audioSource != null) audioSource.Play();
            ItemManager.Instance.AddByType(itemType);  
        }
    }

}