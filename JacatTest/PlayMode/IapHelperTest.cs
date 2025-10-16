using System.Collections;
using JacatGames;
using NUnit.Framework;
#if OMNILATENT_APPSFLYER_WRAPPER
using Omnilatent.AppsFlyerWrapperNS;
#endif
using UnityEngine.TestTools;

namespace Omnilatent
{
    public class IapHelperTest : PackageTestBase
    {
        protected override string PackageName => "In-App Purchase Helper";
        protected override string DefineSymbol => "OMNILATENT_IAP_HELPER";
        protected override string TargetClassFullName => "UnityEngine.Purchasing.UnityPurchasing";

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
            #if OMNILATENT_IAP_HELPER
            yield return base.RunInitializationTest();
            #else
            yield return null;
            #endif
        }

        protected override IEnumerator MainInitializationTest()
        {
            yield return base.MainInitializationTest();
        }

        protected override bool IsPackageInitialized()
        {
            #if OMNILATENT_IAP_HELPER
            return InAppPurchaseHelper.Instance.IsInitialized();
            #endif
            return false;
        }

        protected override void CheckInstanceExist()
        {
            #if OMNILATENT_IAP_HELPER
            AssertInstanceExist<InAppPurchaseHelper>();
            #endif
        }
    }
}