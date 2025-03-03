using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Game.MainGame
{
    public class GoogleAdsManager : MonoBehaviour
    {
        private RewardedAd rewardedAd;

        void Start()
        {
            MobileAds.Initialize(initStatus => { });
            RequestRewardedAd();
        }

        public void RequestRewardedAd()
        {
            string adUnitId = "ca-app-pub-3940256099942544/5224354917"; // Test Ad ID

            AdRequest request = new AdRequest();

            RewardedAd.Load(adUnitId, request, (ad, error) =>
            {
                if (error != null)
                {
                    Debug.LogError("Failed to load rewarded ad: " + error.GetMessage());
                    return;
                }

                rewardedAd = ad;
                Debug.Log("Rewarded Ad loaded successfully.");
                ShowRewardedAd();
            });
        }

        public void ShowRewardedAd()
        {
            if (rewardedAd != null && rewardedAd.CanShowAd())
            {
                rewardedAd.Show((Reward reward) =>
                {
                    Debug.Log("User earned reward: " + reward.Amount);
                });
            }
            else
            {
                Debug.Log("Rewarded Ad is not ready yet.");
            }
        }

    }

}
