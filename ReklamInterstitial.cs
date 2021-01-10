using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using System;


public class ReklamInterstitial : MonoBehaviour
{

    public static InterstitialAd reklamObjesi;

    void Start()
    {
        MobileAds.Initialize(reklamDurumu => { });
        YeniReklamAl(null, null);
    }


    public static IEnumerator ReklamiGoster()
    {
        while (!reklamObjesi.IsLoaded())
            yield return null;

        reklamObjesi.Show();
    }

    public void YeniReklamAl(object sender, EventArgs args)
    {
        if (reklamObjesi != null)
            reklamObjesi.Destroy();

        reklamObjesi = new InterstitialAd("ca-app-pub-5222100567207031/9612977357");
        // ca-app-pub-5222100567207031/9612977357 gercek id
        // ca-app-pub-3940256099942544/1033173712 test id
        reklamObjesi.OnAdClosed += YeniReklamAl; // Kullanıcı reklamı kapattıktan sonra çağrılır

        AdRequest reklamIstegi = new AdRequest.Builder().Build();
        reklamObjesi.LoadAd(reklamIstegi);
    }

    void OnDestroy()
    {
        if (reklamObjesi != null)
            reklamObjesi.Destroy();
    }



}