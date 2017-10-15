using System.Collections.Generic;

namespace DataStoreNamespace.Modules
{
    [System.Serializable]
    public class DataModuleProfileInfo : AbstractDataModule
    {
        public readonly string Credential;
        public readonly string FacebookID;
        public readonly string AvatarURL;
        public readonly UnityEngine.Texture2D AvatarIMG;
        public readonly string[] Friends;

        public DataModuleProfileInfo()
        {
        }

        public DataModuleProfileInfo(DataModuleProfileInfo baseState, Dictionary<string, object> other)
        {
            if (other == null) return;
            if (baseState != null)
            {
                Credential = baseState.Credential;
                FacebookID = baseState.FacebookID;
                AvatarURL = baseState.AvatarURL;
                AvatarIMG = baseState.AvatarIMG;
                Friends = baseState.Friends;
            }

            Credential = GetValue<string>(other, "Credential", Credential);
            FacebookID = GetValue<string>(other, "FacebookID", FacebookID);
            AvatarURL = GetValue<string>(other, "AvatarURL", AvatarURL);
            AvatarIMG = GetValue<UnityEngine.Texture2D>(other, "AvatarIMG", AvatarIMG);
            Friends = GetValue<string[]>(other, "Friends", Friends);
        }

        public override AbstractDataModule MergeState(AbstractDataModule baseSate, Dictionary<string, object> other)
        {
			var newIns = new DataModuleProfileInfo((DataModuleProfileInfo)baseSate, other);
			if(!IsStateDifference(this, newIns))
				return this;
            return newIns;
        }

		// Optional - Just for Optimize performance
        protected override bool IsStateDifference(AbstractDataModule state1, AbstractDataModule state2)
        {
            DataModuleProfileInfo a = state1 as DataModuleProfileInfo;
			DataModuleProfileInfo b = state2 as DataModuleProfileInfo;

			return a.FacebookID != b.FacebookID 
					|| a.AvatarURL != b.AvatarURL
					|| a.Credential != b.Credential;
        }
    }
}

