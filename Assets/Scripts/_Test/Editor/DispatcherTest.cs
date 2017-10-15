using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using DispatcherNamespace;

public class DispatcherTest {

	[Test]
	public void DispatcherTestSimplePasses() {
		Dispatcher.Destroy();
		var dispatcher = Dispatcher.Instance;

		Assert.NotNull(dispatcher);
	}

	[Test]
	public void DispatcherBasicEventPasses() {
		Dispatcher.Destroy();
		var dispatcher = Dispatcher.Instance;

		dispatcher.PostAction(EDispatcherEvents.Login, null, (int errCode, object result) => {
			Assert.AreEqual(errCode, 400);
		});
	}
}
