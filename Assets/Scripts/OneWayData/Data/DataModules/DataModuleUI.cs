using System.Collections.Generic;

namespace DataStoreNamespace.Modules
{
    [System.Serializable]
    public class DataModuleUI : AbstractDataModule
    {
        public readonly bool IsShowLoading;
        public readonly bool IsHudShow;
        public readonly string CurrentMenuName;
        public readonly string CurrentPopupName;

        public DataModuleUI()
        {
            IsShowLoading = false;
            IsHudShow = false;
            CurrentMenuName = string.Empty;
            CurrentPopupName = string.Empty;
        }

        public DataModuleUI(DataModuleUI baseState, Dictionary<string, object> other)
        {
            if (other == null) return;
            if (baseState != null)
            {
                IsShowLoading = baseState.IsShowLoading;
                IsHudShow = baseState.IsHudShow;
                CurrentMenuName = baseState.CurrentMenuName;
                CurrentPopupName = baseState.CurrentPopupName;
            }

            IsShowLoading = GetValue<bool>(other, "IsShowLoading", IsShowLoading);
            IsHudShow = GetValue<bool>(other, "IsHudShow", IsHudShow);
            CurrentMenuName = GetValue<string>(other, "CurrentMenuName", CurrentMenuName);
            CurrentPopupName = GetValue<string>(other, "CurrentPopupName", CurrentPopupName);
        }

        public override AbstractDataModule MergeState(AbstractDataModule baseSate, Dictionary<string, object> other)
        {
            var newIns = new DataModuleUI((DataModuleUI)baseSate, other);
			if(!IsStateDifference(this, newIns))
				return this;
            return newIns;
        }
    }
}

