using System.Collections;
using JacatGames;
using NUnit.Framework;
using Omnilatent;
using UnityEngine.TestTools;

namespace JacatGames
{
    public class AdmobTest : PackageTestBase
    {
        protected override string PackageName => "GoogleMobileAd";
        protected override string DefineSymbol => "JACAT_ADMOB_ENABLED";

        protected override string TargetClassFullName => "GoogleMobileAds.Api.MobileAds";

        [Test]
        public void CheckDependency()
        {
            AssertPackageDefined();
            AssertTargetClassExists(TargetClassFullName);
        }

        /*[UnityTest]
        public IEnumerator InitializesInTime()
        {
            AssertPackageDefined();
            yield return base.RunInitializationTest();
        }*/

        protected override bool IsPackageInitialized()
        {
            return true;
        }

        protected override void CheckInstanceExist()
        {
        }
    }
}