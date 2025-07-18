using System.Collections;
using JacatGames;
using NUnit.Framework;
using Omnilatent;
using UnityEngine.TestTools;

namespace JacatGames
{
    public class MaxSdkTest : PackageTestBase
    {
        protected override string PackageName => "Max";
        protected override string DefineSymbol => "JACAT_MAX_ENABLED";

        protected override string TargetClassFullName => "MaxSdk";

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