using System.Collections.Generic;

namespace DataStoreNamespace.Modules 
{
	public abstract class AbstractDataModule{
		static long sRevisionCount = 0;
		protected AbstractDataModule() {
			Revision = sRevisionCount++;
		}
		public readonly long Revision;

		protected T GetValue<T>(Dictionary<string, object> source, string key, T defaultVal) 
		{
			object res = null;
			if(source.TryGetValue(key, out res))
				return (T)res;
			return (T)defaultVal;
		}

		protected virtual bool IsStateDifference(AbstractDataModule a, AbstractDataModule b) 
		{
			return true;
		}

		public abstract AbstractDataModule MergeState(AbstractDataModule baseSate, Dictionary<string, object> other);
	}	
}

