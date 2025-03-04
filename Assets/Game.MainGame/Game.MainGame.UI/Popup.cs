using System.Linq;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using UnityEngine;
using UnityEngine.UI;

namespace Game.MainGame
{
    public class Popup : MonoBehaviour
    {
        [SerializeField]
        private InputField input;
        [SerializeField]
        private Button btnClaim;

        private void Start()
        {
            btnClaim.onClick.AddListener(() => Btn_Claim());
        }

        private void Btn_Claim()
        {
            string gift = input.text.Trim();
            if (string.IsNullOrEmpty(gift))
            {
                Debug.LogWarning("Gift code is empty!");
                ClosePopup();
                return;
            }
            FetchRemoteConfig(gift);
        }

        void FetchRemoteConfig(string key)
        {
            FirebaseRemoteConfig.DefaultInstance.FetchAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted && !task.IsFaulted)
                {
                    FirebaseRemoteConfig.DefaultInstance.ActivateAsync().ContinueWithOnMainThread(_ =>
                    {
                        if (FirebaseRemoteConfig.DefaultInstance.Keys.Contains(key))
                        {
                            bool value = FirebaseRemoteConfig.DefaultInstance.GetValue(key).BooleanValue;

                            // Hiển thị SRDebugger nếu lấy được giá trị
                            SRDebug.Instance.ShowDebugPanel();

                            ClosePopup();
                        }
                        else
                        {
                            Debug.LogWarning($"Gift Code '{key}' not found in Remote Config.");
                            ClosePopup();
                        }
                    });
                }
                else
                {
                    Debug.Log("Failed to fetch Remote Config.");
                    ClosePopup();
                }
            });
        }

        private void ClosePopup()
        {
            gameObject.SetActive(false);
        }
    }
}
