using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        public static bool isApplicationQuiting;

        private static T _instance;
        private static readonly object _lock = new object();

        public static T Instance
        {
            get
            {
                if (isApplicationQuiting)
                    return null;

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<T>();

                        if (_instance == null)
                        {
                            var singleton = new GameObject("[S] " + typeof(T));

                            _instance = singleton.AddComponent<T>();
                        }
                    }
                }

                return _instance;
            }
        }

        public virtual void OnDestroy()
        {
            isApplicationQuiting = true;
        }

    }
}