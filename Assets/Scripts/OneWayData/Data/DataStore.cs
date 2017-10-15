using System.Collections.Generic;
using DataStoreNamespace.Modules;

namespace DataStoreNamespace
{
    public class DataStore : Singleton<DataStore>
    {
        public DataModuleProfileInfo BasicUserInfo
        {
            get
            {
                return mModules[EDataStoreModules.USER_PROFILE] as DataModuleProfileInfo;
            }
        }

        public DataModuleUI UserInterfaceInfo 
        {
            get 
            {
                return mModules[EDataStoreModules.UI] as DataModuleUI;
            }
        }

        public DataStore()
        {
            mModules = new Dictionary<EDataStoreModules, AbstractDataModule>();
            mNextVal = new Dictionary<EDataStoreModules, AbstractDataModule>();
            Init();
        }

        // Add Init value for Each Module Here
        private void Init()
        {
            mModules[EDataStoreModules.USER_PROFILE] = new DataModuleProfileInfo();
            mModules[EDataStoreModules.UI] = new DataModuleUI();
        }

        public void SetState(EDataStoreModules modules, Dictionary<string, object> newVal)
        {
            AbstractDataModule nextVal = null;
            mNextVal.TryGetValue(modules, out nextVal);
            if (nextVal == null)
                nextVal = mModules[modules];

            mNextVal[modules] = nextVal.MergeState(nextVal, newVal);
        }

        public void LateUpdate()
        {
            if (mNextVal.Count <= 0) return;

            // Swap new Module
            mModules.Clear();
            foreach (var item in mNextVal)
            {
                mModules.Add(item.Key, item.Value);
            }
            mNextVal.Clear();
        }

        public bool HasChange(EDataStoreModules module, long lastKnownRevision = -1)
        {
            AbstractDataModule val;
            mModules.TryGetValue(module, out val);
            return val.Revision != lastKnownRevision;
        }

        private Dictionary<EDataStoreModules, AbstractDataModule> mModules;
        private Dictionary<EDataStoreModules, AbstractDataModule> mNextVal;
    }

    public enum EDataStoreModules
    {
        USER_PROFILE,
        UI
    }
}

