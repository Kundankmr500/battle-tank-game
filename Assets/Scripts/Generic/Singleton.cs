using UnityEngine;

namespace Generic
{
    public class Singleton<T> where T : Singleton<T>
    {
        private static T instance;
        public static T Instance { get { return instance; } }

        protected Singleton()
        {
            if (instance == null)
                instance = (T)this;
            else
            {
                Debug.LogError("Someone trying to create dublicate singalton");
                instance = null;
            }
        }

    }
}
