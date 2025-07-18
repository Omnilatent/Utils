using System.Collections;
using JacatGames;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Omnilatent
{
    public class FirebaseTest : PackageTestBase
    {
        protected override string PackageName => "FirebaseManager";
        protected override string DefineSymbol => "OMNILATENT_FIREBASE_MANAGER";

        protected override string TargetClassFullName => "Firebase.Analytics.FirebaseAnalytics";
        // protected override string MainSceneName => FirebaseManagerConfig.Instance != null ? FirebaseManagerConfig.Instance.MainSceneName : base.MainSceneName;

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
            #if OMNILATENT_FIREBASE_MANAGER
            yield return base.RunInitializationTest();
            #else
            yield return null;
            #endif
        }

        protected override bool IsPackageInitialized()
        {
            #if OMNILATENT_FIREBASE_MANAGER
            return FirebaseManager.FirebaseReady;
            #else
            return false;
            #endif
        }

        protected override void CheckInstanceExist()
        {
            #if OMNILATENT_FIREBASE_MANAGER
            AssertInstanceExist<FirebaseManager>();
            #endif
        }
    }
}