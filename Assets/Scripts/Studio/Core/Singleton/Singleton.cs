using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//lembrando, namespaces organizados conforme a organização de pastas
namespace Studio.Core.Singleton
{
    //ao colocar o t, e o monobehaviour, estamos limitando a classe, para que ela seja melhor filtrada
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        //desta maneira, o GameManager é "encontrado" e atribuido.
        public static T Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = GetComponent<T>();
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }

}
