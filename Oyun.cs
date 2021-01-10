using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Oyun : MonoBehaviour
{

    private float zamanlayici;
    public float maxZaman;
    public float minZaman;

    public static bool aralik = true; 
    public static bool yenioyunmu = false;
    public static bool mainmenu;


    public GameObject ruhPrefab;
    public Transform RuhOlusturmaNoktasi;
    public OyuncuKontrolleri oyunKontrolleri;
   
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (zamanlayici <= 0 && oyunKontrolleri.oyunBasladi == true)
        {
            ruhOlustur();
        }
        zamanlayici -= Time.deltaTime;
        
    }

    void ruhOlustur()
    {
        Instantiate(ruhPrefab, RuhOlusturmaNoktasi.position, RuhOlusturmaNoktasi.rotation);
        zamanlayici = Random.Range(minZaman, maxZaman);
        if (zamanlayici >= 1f && zamanlayici <= 1.6f)
        {
            aralik = true;
        }
        else
        {
            aralik = false;
        }
    }
   

    public void yeniOyun()
    {
        PlayerPrefs.SetInt("PuanTutucu", 0);
        PlayerPrefs.SetInt("devamabastimi", 0);
        yenioyunmu = true;
        OyuncuKontrolleri.DestroyAd();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
      
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    public void AnaMenu()
    {
        yenioyunmu = false;
        mainmenu = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }




}
