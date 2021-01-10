using UnityEngine;
using GoogleMobileAds.Api;

public class ReklamBannerAlt : MonoBehaviour
{
    public static BannerView reklamObjesi;

    void Start()
    {
        MobileAds.Initialize(reklamDurumu => { });

        reklamObjesi = new BannerView("ca-app-pub-5222100567207031/5673732348", AdSize.SmartBanner, AdPosition.Bottom);
        // ca-app-pub-5222100567207031/5673732348 gercek id
        // ca-app-pub-3940256099942544/6300978111 test id
        AdRequest reklamIstegi = new AdRequest.Builder().Build();
        reklamObjesi.LoadAd(reklamIstegi);
    }

    void OnDestroy()
    {
        if (reklamObjesi != null)
            reklamObjesi.Destroy();
    }
}
