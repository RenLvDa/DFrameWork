using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DFrameWork
{
    public interface ISingleton {  }

    /// <summary>
    /// 单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> : ISingleton where T : class, ISingleton, new()
    {
        private static T _instance;
        static object _lock = new object();
        public static T Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock (_lock)
                    {
                        CreateInstance();
                    }
                }
                return _instance;
            }
        }

        public static void CreateInstance()
        {
            if(_instance == null)
            {
                _instance = new T();
                (_instance as Singleton<T>).Init();
            }
        }

        public static void DestoryInstance()
        {
            if(_instance != null)
            {
                (_instance as Singleton<T>).UnInit();
                _instance = (T)((object)null);
            }
        }

        public virtual void Init() { }

        public virtual void UnInit() { }
    }
}