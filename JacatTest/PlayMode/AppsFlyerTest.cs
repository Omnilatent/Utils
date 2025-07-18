using System.Collections;
using JacatGames;
using NUnit.Framework;
#if OMNILATENT_APPSFLYER_WRAPPER
using Omnilatent.AppsFlyerWrapperNS;
#endif
using UnityEngine.TestTools;

namespace Omnilatent
{
    public class AppsFlyerTest : PackageTestBase
    {
        protected override string PackageName => "AppsFlyer";
        protected override string DefineSymbol => "OMNILATENT_APPSFLYER_WRAPPER";
        protected override string TargetClassFullName => "AppsFlyerSDK.AppsFlyer";

        [Test]
        public void CheckDependency()
        {
            AssertPackageDefined();
            AssertTargetClassExists();
        }

        [UnityTest]
        public IEnumerator InitializesInTime()
        {
            AssertPackageDefined();
            #if OMNILATENT_APPSFLYER_WRAPPER
            yield return base.RunInitializationTest();
            #else
            yield return null;
            #endif
        }

        protected override IEnumerator MainInitializationTest()
        {
            yield return base.MainInitializationTest();
#if OMNILATENT_APPSFLYER_WRAPPER
            Assert.IsNotNull(AppsFlyerWrapper.Instance.devKey, "AppsFlyer Dev Key is null");
            Assert.IsNotEmpty(AppsFlyerWrapper.Instance.devKey, "AppsFlyer Dev Key is empty");
#endif
            
            #if UNITY_IOS
            Assert.IsNotNull(AppsFlyerWrapper.Instance.appID, "AppsFlyer App ID is null");
            Assert.IsNotEmpty(AppsFlyerWrapper.Instance.appID, "AppsFlyer App ID is empty");
            #endif
        }

        protected override bool IsPackageInitialized()
        {
            #if OMNILATENT_APPSFLYER_WRAPPER
            return AppsFlyerWrapper.Initialized;
            #endif
            return false;
        }

        protected override void CheckInstanceExist()
        {
            #if OMNILATENT_APPSFLYER_WRAPPER
            AssertInstanceExist<AppsFlyerWrapper>();
            #endif
        }
    }
}