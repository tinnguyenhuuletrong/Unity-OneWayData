using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using DataStoreNamespace;

public class DataStoreTest
{

    [Test]
    public void BasicSimplePasses()
    {
        DataStore.Destroy();
        DataStore store = DataStore.Instance;
        Assert.NotNull(store);
    }

    [Test]
    public void BasicPasses()
    {
        DataStore.Destroy();
        DataStore store = DataStore.Instance;
        Dictionary<string, object> newVals = new Dictionary<string, object>(){
            {"Credential", "facebook:123"}
        };
        store.SetState(EDataStoreModules.USER_PROFILE, newVals);
        store.LateUpdate();
        Assert.AreEqual(store.BasicUserInfo.Credential, "facebook:123", "Value not correct");
    }

    [Test]
    public void ChangeDetectedProfile()
    {
        DataStore.Destroy();
        DataStore store = DataStore.Instance;
        Dictionary<string, object> newVals = new Dictionary<string, object>(){
            {"Credential", "facebook:123"}
        };
        store.SetState(EDataStoreModules.USER_PROFILE, newVals);
        store.LateUpdate();

        var ref1 = store.BasicUserInfo;

        newVals["FacebookID"] = "123";
        store.SetState(EDataStoreModules.USER_PROFILE, newVals);
        store.LateUpdate();
        Assert.AreNotSame(ref1, store.BasicUserInfo, "Should difference ins");

        ref1 = store.BasicUserInfo;
        store.SetState(EDataStoreModules.USER_PROFILE, newVals);
        store.LateUpdate();
        Assert.AreSame(ref1, store.BasicUserInfo, "Should same ins - Update same val");

        ref1 = store.BasicUserInfo;
        store.LateUpdate();
        Assert.AreSame(ref1, store.BasicUserInfo, "Should same ins - Without Update");
    }

    [Test]
    public void MultipleUpdatePasses()
    {
        DataStore.Destroy();
        DataStore store = DataStore.Instance;
        Dictionary<string, object> newVals = new Dictionary<string, object>(){
            {"Credential", "facebook:123"}
        };
        store.SetState(EDataStoreModules.USER_PROFILE, newVals);

        // Somewhere
        newVals = new Dictionary<string, object>(){
            {"FacebookID", "123"}
        };
        store.SetState(EDataStoreModules.USER_PROFILE, newVals);

        store.LateUpdate();
        Assert.AreEqual(store.BasicUserInfo.Credential, "facebook:123", "Credential not correct");
        Assert.AreEqual(store.BasicUserInfo.FacebookID, "123", "FacebookID Value not correct");
    }

	[Test]
    public void RevisionCheckPasses()
    {
        DataStore.Destroy();
        DataStore store = DataStore.Instance;

		long revision = store.BasicUserInfo.Revision;

        Dictionary<string, object> newVals = new Dictionary<string, object>(){
            {"Credential", "facebook:123"}
        };
        store.SetState(EDataStoreModules.USER_PROFILE, newVals);
		
        // Somewhere
        newVals = new Dictionary<string, object>(){
            {"FacebookID", "123"}
        };
        store.SetState(EDataStoreModules.USER_PROFILE, newVals);

        store.LateUpdate();
        long revision2 = store.BasicUserInfo.Revision;
		Assert.AreNotEqual(revision, revision2);
		store.LateUpdate();
		Assert.AreEqual(store.BasicUserInfo.Revision, revision2);
    }
}
