using Firebase;
using Firebase.RemoteConfig;
using Firebase.Extensions;
using UnityEngine;
using System.Collections;

namespace Game.MainGame
{
    public class FirebaseRemoteConfigManager : MonoBehaviour
    {
        private bool srDebuggerEnabled = false;

        void Start()
        {
            InitializeFirebase();
        }

        private void InitializeFirebase()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
                if (task.Result == DependencyStatus.Available)
                {
                    Debug.Log("Firebase Remote Config Initialized!");
                    StartCoroutine(WaitForFirebaseAndFetchConfig());
                }
                else
                {
                    Debug.LogError("Could not resolve Firebase dependencies: " + task.Result);
                }
            });
        }
        private IEnumerator WaitForFirebaseAndFetchConfig()
        {
            while (FirebaseRemoteConfig.DefaultInstance == null)
            {
                Debug.Log("Waiting for FirebaseRemoteConfig...");
                yield return null;
            }
            FetchRemoteConfigValues();
        }
        private void FetchRemoteConfigValues()
        {
            //// Cấu hình giá trị mặc định (nếu không có trên Firebase, sẽ dùng giá trị này)
            //var defaults = new System.Collections.Generic.Dictionary<string, object>
            //{
            //    { "srdebugger_enabled", false }
            //};
            //FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults);

            // Lấy dữ liệu từ Firebase
            FirebaseRemoteConfig.DefaultInstance.FetchAndActivateAsync().ContinueWithOnMainThread(task => {
                if (task.IsCompleted)
                {
                    srDebuggerEnabled = FirebaseRemoteConfig.DefaultInstance.GetValue("srdebugger_enabled").BooleanValue;

                    Debug.Log($"srdebugger_enabled: {srDebuggerEnabled}");

                    string welcomeMessage = FirebaseRemoteConfig.DefaultInstance.GetValue("game_value").StringValue;

                    Debug.Log($"srdebugger_enabled: {welcomeMessage}");

                    // Nếu giá trị true, kích hoạt SRDebugger
                    if (srDebuggerEnabled)
                    {
                        SRDebug.Instance.ShowDebugPanel();

                    }
                }
                else
                {
                    Debug.LogError("Failed to fetch Remote Config values.");
                }
            });
        }

    }
}
