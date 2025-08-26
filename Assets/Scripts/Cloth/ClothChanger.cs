using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Cloth
{
    public class ClothChanger : MonoBehaviour
    {

        public List<SkinnedMeshRenderer> meshes;
        public Texture2D texture;
        public string shaderIDName = "_EmissionMap";

        private List<Texture2D> _defaultTextures = new List<Texture2D>();

        private void Awake()
        {
            _defaultTextures.Clear();

            foreach (var skinnedMesh in meshes)
            {
                if (skinnedMesh != null && skinnedMesh.materials.Length > 0)
                {
                    _defaultTextures.Add((Texture2D)skinnedMesh.materials[0].GetTexture(shaderIDName));
                }
                else
                {
                    _defaultTextures.Add(null);
                }
            }
        }

        [NaughtyAttributes.Button]
        private void ChangeTexture()
        {
            foreach (var skinnedMesh in meshes)
            {
                if (skinnedMesh != null && skinnedMesh.materials.Length > 0)
                {
                    skinnedMesh.materials[0].SetTexture(shaderIDName, texture);
                }
            }
        }

        public void ChangeTexture(ClothSetup setup)
        {
            foreach (var skinnedMesh in meshes)
            {
                if (skinnedMesh != null && skinnedMesh.materials.Length > 0)
                {
                    skinnedMesh.materials[0].SetTexture(shaderIDName, setup.texture);
                }
            }
        }

        public void ResetTexture()
        {
            for (int i = 0; i < meshes.Count; i++)
            {
                if (meshes[i] != null && meshes[i].materials.Length > 0 && _defaultTextures[i] != null)
                {
                    meshes[i].materials[0].SetTexture(shaderIDName, _defaultTextures[i]);
                }
            }
        }
    }
}
