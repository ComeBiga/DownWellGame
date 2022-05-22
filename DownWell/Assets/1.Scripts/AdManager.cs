using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;



public class AdManager : MonoBehaviour
{
    public GameObject opening;
    public bool isTestMode;

    bool canShowBanner = true;

    //Banner
    const string bannerTestID = "ca-app-pub-3940256099942544/6300978111";  //Test ID
    const string bannerID = "ca-app-pub-2203416744079510/7296942413";  //Host ID
    BannerView bannerAd;


    // Start is called before the first frame update
    void Start()
    {
        var requestConfiguration = new RequestConfiguration.Builder()
           .SetTestDeviceIds(new List<string>() { "E9D03ABD5B8FC84D" }) //Test Device ID
           .build();

        MobileAds.SetRequestConfiguration(requestConfiguration);


        BannerAd();
    }
    void FixedUpdate()
    {
        if(!opening.activeInHierarchy&& canShowBanner)
        {
            canShowBanner = false;
            StartCoroutine(IShowBanner());
        }
    }
    IEnumerator IShowBanner()
    {
        yield return new WaitForSeconds(0.2f);
        bannerAd.Show();
    }

    AdRequest GetAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    void BannerAd()
    {
        //테스트 버전인지 아닌지에 따라 ID 지정
        bannerAd = new BannerView(isTestMode ? bannerTestID : bannerID,
            AdSize.SmartBanner, AdPosition.Top);
        bannerAd.LoadAd(GetAdRequest());
        bannerAd.Hide();
    }
}
