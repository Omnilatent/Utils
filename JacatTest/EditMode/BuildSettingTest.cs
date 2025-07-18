using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

namespace Omnilatent
{
    [TestFixture]
    public class BuildSettingTest
    {
        private const int MinTargetSdkVersion = 35;
        private const string IapPackageName = "com.unity.purchasing";
        private const string MinVersionV4 = "4.13.0";
        private const string MinVersionV5 = "5.0.0";

        [Test]
        public void TargetSdkVersion_Is_35_Or_Higher()
        {
            #if UNITY_ANDROID
            int targetSdkVersion = (int)PlayerSettings.Android.targetSdkVersion;
            Assert.GreaterOrEqual(targetSdkVersion, MinTargetSdkVersion,
                $"Target SDK Version must be {MinTargetSdkVersion} or higher. Current: {targetSdkVersion}");
            #else
        Assert.Ignore("Not an Android build target.");
            #endif
        }

        [Test]
        public void UnityIAP_Is_Installed_And_Valid_Version()
        {
            ListRequest listRequest = Client.List(true);
            while (!listRequest.IsCompleted) { }

            Assert.IsNull(listRequest.Error, $"Package list error: {listRequest.Error?.message}");

            var iapPackage = System.Array.Find(listRequest.Result.ToArray(), p => p.name == IapPackageName);
            if (iapPackage == null)
            {
                Assert.Ignore("Unity IAP package not installed; skipping version check.");
                return;
            }

            string currentVersion = iapPackage.version;
            bool isValid = IsValidIapVersion(currentVersion);
            Assert.IsTrue(isValid, $"Unity IAP version must be >= {MinVersionV4} or >= {MinVersionV5}. Current: {currentVersion}");
        }

        private bool IsValidIapVersion(string version)
        {
            System.Version v = new System.Version(version);
            return v >= new System.Version(MinVersionV4) || v >= new System.Version(MinVersionV5);
        }
    }
}