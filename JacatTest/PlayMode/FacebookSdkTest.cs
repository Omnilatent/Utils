using System;
using System.Collections;
using System.Reflection;
using JacatGames;
using NUnit.Framework;
using Omnilatent;
using UnityEngine.TestTools;

namespace JacatGames
{
    public class FacebookSdkTest : PackageTestBase
    {
        protected override string PackageName => "Facebook";
        protected override string DefineSymbol => "";
        protected override string TargetClassFullName => "Facebook.Unity.FB";

        [Test]
        public void CheckDependency()
        {
            AssertTargetClassExists();
        }

        [UnityTest]
        public IEnumerator InitializesInTime()
        {
            AssertTargetClassExists();
            yield return RunInitializationTest();
        }

        protected override bool IsPackageInitialized()
        {
            var fbType = Type.GetType("Facebook.Unity.FB, Facebook.Unity");

            if (fbType == null)
                return false;

            var isInitializedProperty = fbType.GetProperty("IsInitialized", BindingFlags.Public | BindingFlags.Static);

            if (isInitializedProperty == null)
                return false;

            var value = isInitializedProperty.GetValue(null);
            return value is bool result && result;
        }

        protected override void CheckInstanceExist() { }
    }
}