using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DFrameWork
{
    public class UnitySingleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        private static bool _shuttingDown = false;

        public static T Instance
        {
            get
            {
                if(Application.isPlaying && _shuttingDown)
                {
                    Debug.LogFormat("[Singleton] Instance '{0}' already destoryed. Returning null.", typeof(T));
                    return null;
                }

                if(_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if(_instance == null)
                    {
                        var singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).Name;

                        if (Application.isPlaying)
                        {
                            DontDestroyOnLoad(singletonObject);
                        }
                    }

                }
                return _instance;
            }
        }

        /// <summary>
        /// 用于控制初始化时机 在需要初始化的时候调用 
        /// </summary>
        public void CreateInstance()
        {
            //虽然无内容 但是调用时会Instance get操作 变相初始化了
            //MonoBehaviour的单例和普通单例不一样，得控制初始化时机，所以有所区别
        }

        private void Awake()
        {
            Init();
        }

        public virtual void Init() {  }

        public virtual void OnDestory()
        {
            if(_instance != null)
            {
                _instance = null;
            }
        }

        private void OnApplicationQuit()
        {
            _shuttingDown = true;
        }
    }
}
