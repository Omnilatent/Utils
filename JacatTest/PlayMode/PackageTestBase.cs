using System;
using System.Linq;
using System.Collections;
using NUnit.Framework;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Omnilatent
{
    public abstract class PackageTestBase
    {
        protected abstract string PackageName { get; }
        protected virtual string MainSceneName { get => "Main"; }
        protected abstract string DefineSymbol { get; }
        protected abstract string TargetClassFullName { get; }

        protected virtual float InitTimeout => 10f;
        protected virtual float MinTestTime => 0.5f;

        protected virtual bool AssertPackageDefined(string symbol = null)
        {
            if (symbol == null)
            {
                symbol = DefineSymbol;
            }
            
            #if UNITY_EDITOR
            if (!DefineSymbolExists(symbol))
            {
                Assert.Fail($"No {PackageName} in project.");
                return false;
            }
            #endif
            return true;
        }

        protected virtual bool DefineSymbolExists(string symbol)
        {
            #if UNITY_EDITOR
            if (string.IsNullOrEmpty(symbol)) { return true; }

            var targetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);
            return defines.Contains(symbol);
            #else
            return true; // allow runtime test to proceed
            #endif
        }

        protected void AssertTargetClassExists(string targetClass = null)
        {
            if (targetClass == null) { targetClass = TargetClassFullName; }

            var type = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a =>
                {
                    try { return a.GetTypes(); }
                    catch { return Array.Empty<Type>(); }
                })
                .FirstOrDefault(t => t.FullName == targetClass);

            Assert.IsNotNull(type, $"{targetClass} class was not found in the project.");
        }

        /// <summary>
        /// Load Main scene, check if package instance exist, then wait for a while to check if Initialize success.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /*protected virtual IEnumerator RunInitializationTest<T>() where T : MonoBehaviour
        {
            LogAssert.ignoreFailingMessages = true;
            yield return new EnterPlayMode();

            SceneManager.LoadScene(MainSceneName);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == MainSceneName);
            yield return null;

            var instance = GameObject.FindObjectOfType<T>();
            Assert.IsNotNull(instance, typeof(T).Name + " instance was not found in scene.");

            float startTime = Time.time;
            float elapsed = 0f;
            while (elapsed < MinTestTime || (elapsed < InitTimeout && !IsPackageInitialized()))
            {
                yield return null;
                elapsed = Time.time - startTime;
            }

            Assert.IsTrue(IsPackageInitialized(), typeof(T).Name + $" did not finish initializing within {InitTimeout}s.");

            yield return new ExitPlayMode();
        }*/

        /// <summary>
        /// Load Main scene, then wait for a while to check if Initialize success.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected virtual IEnumerator RunInitializationTest()
        {
            LogAssert.ignoreFailingMessages = true;
            yield return new EnterPlayMode();

            yield return MainInitializationTest();

            yield return new ExitPlayMode();
        }

        protected virtual IEnumerator MainInitializationTest()
        {
            SceneManager.LoadScene(MainSceneName);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == MainSceneName);
            yield return null;
            
            CheckInstanceExist();

            float startTime = Time.time;
            float elapsed = 0f;
            while (elapsed < MinTestTime || (elapsed < InitTimeout && !IsPackageInitialized()))
            {
                yield return null;
                elapsed = Time.time - startTime;
            }

            Assert.IsTrue(IsPackageInitialized(), PackageName + $" did not finish initializing within {InitTimeout}s.");
        }

        protected abstract bool IsPackageInitialized();

        protected abstract void CheckInstanceExist();
        
        protected virtual void AssertInstanceExist<T>() where T : MonoBehaviour
        {
            var instance = GameObject.FindObjectOfType<T>();
            Assert.IsNotNull(instance, typeof(T).Name + " instance was not found in scene.");
        }
    }
}