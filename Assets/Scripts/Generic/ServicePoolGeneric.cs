using UnityEngine;
using System.Collections.Generic;

namespace Generic
{
    public class ServicePoolGeneric<T> : MonoSingletonGeneric<ServicePoolGeneric<T>>
        where T: class
    {
        private List<PooledItem<T>> pooledItems = new List<PooledItem<T>>();

        public virtual T GetItem()
        {
            if(pooledItems.Count > 0)
            {
                PooledItem<T> item = pooledItems.Find(i => i.IsUsed == false);
                if(item != null)
                {
                    item.IsUsed = true;
                    Debug.Log("item.IsUsed "+ item.IsUsed);
                    return item.Item;
                }
            }
            return CreateNewPooledItem();
        }


        private T CreateNewPooledItem()
        {
            PooledItem<T> pooledItem = new PooledItem<T>();
            pooledItem.Item = CreateItem();
            pooledItem.IsUsed = true;
            pooledItems.Add(pooledItem);
            Debug.Log("Pool item Created " + pooledItems.Count);
            return pooledItem.Item;
        }


        public virtual void ReturnItem(T item)
        {
            PooledItem<T> pooledItem = pooledItems.Find(i => i.Item.Equals(item));
            pooledItem.IsUsed = false;
            Debug.Log("Returning to Pool " + pooledItems.Count);
        }


        public virtual void InitItem(T item)
        {
            
        }


        protected virtual T CreateItem()
        {
            return (T) null;
        }


        private class PooledItem<T>
        {
            public T Item;
            public bool IsUsed;
        }
    }
}
