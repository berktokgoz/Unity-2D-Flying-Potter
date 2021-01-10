using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class OyuncuKontrolleri : MonoBehaviour
{

    public float ucusGucu;
    private Rigidbody2D rigidBody;
    public bool oyunBasladi;
    public bool oyunBitti;
    public int puan;
    public GameObject oyunBittiPaneli;
    public GameObject mutluSonPaneli;
    public Text HighScoreText;
    public Text HighScoreTextAnaMenu;
    public Text ScoreText;
    public GameObject AnaMenu;
    public bool OyunBaslangici = false;
    public float gecisreklamiSayaci;
    //public static string HangiButon;
    //public static bool InternetVarmi;
    //public static float secondsCount;
    public AudioSource ses;
    public AudioClip[] sesler;
    public GameObject buttonMutluSon;
    public GameObject textMutluSon;
    public GameObject textBaslangic;
    public GameObject sheiswaiting;
    public GameObject ImageGinny;
    public int coin;
    public Text Altin;
    public Text AltinElement;
    public GameObject ButtonDevam;
    public Text anapuan;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        ses = GetComponent<AudioSource>();
        coin = PlayerPrefs.GetInt("AltinHesabi");
       


        if (!Oyun.yenioyunmu)
        {

            AnaMenu.SetActive(true);

            PlayerPrefs.SetInt("devamabastimi", 0);

            PlayerPrefs.SetInt("PuanTutucu", 0);

            puan = PlayerPrefs.GetInt("PuanTutucu");

            buttonMutluSon = GameObject.Find("Button_mutluSon");
            textMutluSon = GameObject.Find("Text_mutluSon");
            textBaslangic = GameObject.Find("Text_Baslangic");
            ImageGinny = GameObject.Find("Image_Ginny");
            sheiswaiting = GameObject.Find("Text_Baslangic2");
       

            GameObject.Find("AnamenuGold").GetComponent<Text>().text = coin.ToString();

            buttonMutluSon.SetActive(false);
            textMutluSon.SetActive(false);
     


            if (PlayerPrefs.GetInt("HighScore") >= 999)
            {
              
                buttonMutluSon.SetActive(true);
                textMutluSon.SetActive(true);
                textBaslangic.SetActive(false);
                ImageGinny.SetActive(false);
                sheiswaiting.SetActive(false);

            }
            if (PlayerPrefs.GetInt("HighScore") < 999)
            {
                StartCoroutine(GinnyLoading());
            }
                

            StartCoroutine(ButonLoadingPlay());
            OyunBaslangici = true;
            HighScoreTextAnaMenu.text = PlayerPrefs.GetInt("HighScore").ToString();
           
            //if(!Oyun.mainmenu)
            //{
            //    PlayerPrefs.SetFloat("reklamSayaci", 0);
            //    HangiButon = "Play";
            //    ReklamInterstitial.GecisReklami();
            //    reklamMetoduCalistimi = true;
            //    TextButton.changePlayText();
            //    StartCoroutine(ReklamInterstitial.ReklamiGoster());
            //    Oyun.mainmenu = false;
            //}




        }

        else
        {
            puan = PlayerPrefs.GetInt("PuanTutucu");
            rigidBody.gravityScale = 1;
        

        }




    }

    // Update is called once per frame
    void Update()
    {
        if (OyunBaslangici == false)
        {

            if (Input.GetButtonDown("Fire1") && !oyunBitti)
            {

                if (!oyunBasladi)
                {
                    rigidBody.gravityScale = 1;
                    oyunBasladi = true;
                }

                uc();
                ses.PlayOneShot(sesler[0],1);

            }
        }

       
       altinci();
       

        Puan();

    }

    private void FixedUpdate()
    {
        if (oyunBasladi && !oyunBitti)
            UcusAcisi();

    }

    //public static void HasConnection()
    //{
    //    try
    //    {
    //        using (var client = new WebClient())
    //        using (var stream = new WebClient().OpenRead("http://www.google.com"))
    //        {
    //            InternetVarmi=true;
    //        }
    //    }
    //    catch
    //    {
    //        InternetVarmi=false;
    //    }
    //}

    void uc()
    {
        rigidBody.velocity = Vector2.zero;
        rigidBody.AddForce(new Vector2(0, ucusGucu));

    }

    void altinci()
    {
        Altin.text  = coin.ToString();
    }
    void Puan()
    {
        anapuan.text = puan.ToString();

    }

    void OnTriggerEnter2D(Collider2D diger)
    {
        if (diger.tag == "olumAlani")
        {
            ses.PlayOneShot(sesler[1], 1);
            oyunBitti = true;
            StartCoroutine(OyunBitti());
        }

        if (diger.tag == "PuanAlani")
        {
            puan++;
        }

        if (diger.tag == "altin")
        {
            ses.PlayOneShot(sesler[2], 1);
            Destroy(diger.gameObject);
            coin++;
          
        }

    }

    void OnCollisionEnter2D(Collision2D diger)
    {
        if (diger.transform.tag == "olumAlani")
        {
            ses.PlayOneShot(sesler[1], 1);
            oyunBitti = true;
            StartCoroutine(OyunBitti());
        }

    }

    void UcusAcisi()
    {
        float aci = 35;

        if (rigidBody.velocity.y < 0)
        {
            aci = Mathf.Lerp(35, -60, -(rigidBody.velocity.y) / 10);
        }

        transform.rotation = Quaternion.Euler(0, 0, aci);

    }

    IEnumerator OyunBitti()
    {
        yield return new WaitForSeconds(1);

        gecisreklamiSayaci = PlayerPrefs.GetFloat("reklamSayaci");
        gecisreklamiSayaci++;
        PlayerPrefs.SetFloat("reklamSayaci", gecisreklamiSayaci);


        if (gecisreklamiSayaci % 3f == 0f)
        {
            PlayerPrefs.SetFloat("reklamSayaci", 0);
            StartCoroutine(ReklamInterstitial.ReklamiGoster());
        }

        if (puan >= 999 && PlayerPrefs.GetInt("HighScore") < 999)
        {
            mutluSonPaneli.SetActive(true);
            if(coin>=1000 && PlayerPrefs.GetInt("devamabastimi") == 0)
            {
                GameObject.Find("mutluSonDevam").GetComponent<Button>().enabled = true;
                GameObject.Find("mutluSonDevam").GetComponentInChildren<Text>().text = "Congratulations! You saved her! Do you want to continue? (1000 Golds)";
            }
           else
            {
                GameObject.Find("mutluSonDevam").GetComponent<Button>().enabled = false;
                GameObject.Find("mutluSonDevam").GetComponentInChildren<Text>().text = "Congratulations! You saved her!";
            }
            PlayerPrefs.SetInt("HighScore", puan);
            PlayerPrefs.SetInt("AltinHesabi", coin);
            PlayerPrefs.SetInt("PuanTutucu", puan);
        }
       
        else
        {
            oyunBittiPaneli.SetActive(true);


            if (coin>=1000 && PlayerPrefs.GetInt("devamabastimi") == 0)
            {
                GameObject.Find("Button_Devam").GetComponent<Button>().enabled = true;
                GameObject.Find("Button_Devam").GetComponentInChildren<Text>().text = "Do you want to continue? (1000 Golds)";

            }
            else if (PlayerPrefs.GetInt("devamabastimi") == 1 && coin>= 1000)
            {
                GameObject.Find("Button_Devam").GetComponent<Button>().enabled = false;
                GameObject.Find("Button_Devam").GetComponentInChildren<Text>().text = "You used your extra life!";
            }
            else
            {
                GameObject.Find("Button_Devam").GetComponent<Button>().enabled = false;
                GameObject.Find("Button_Devam").GetComponentInChildren<Text>().text = "You need to pay 1000 golds to continue.";
            }


            if (puan > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", puan);
            }

            ScoreText.text = puan.ToString();
            HighScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
            PlayerPrefs.SetInt("AltinHesabi", coin);
            GameObject.Find("Gold").GetComponent<Text>().text = coin.ToString();
            PlayerPrefs.SetInt("PuanTutucu", puan);

        }


        Time.timeScale = 0;

    }


    IEnumerator ButonLoadingPlay()
    {
        //This is a coroutine
        GameObject.Find("Button_Play").GetComponent<Button>().enabled = false;
        GameObject.Find("Button_Exit").GetComponent<Button>().enabled = false;
        GameObject.Find("Button_Play").GetComponentInChildren<Text>().text = "Loading...";

        yield return new WaitForSeconds(4);  //Wait

        GameObject.Find("Button_Play").GetComponent<Button>().enabled = true;
        GameObject.Find("Button_Exit").GetComponent<Button>().enabled = true;
        GameObject.Find("Button_Play").GetComponentInChildren<Text>().text = "Play";
    }

    public void ResetScore()

    {
        //PlayerPrefs.DeleteAll(); // Reset the highscore. 
        PlayerPrefs.SetInt("HighScore", 0);
        HighScoreText.text = "0"; // When reset button is pressed, "0" is displayed.
    }


    public void Play()

    {
        DestroyAd();
        AnaMenu.SetActive(false);
        OyunBaslangici = false;
        rigidBody.gravityScale = 1;
        PlayerPrefs.SetInt("devamabastimi", 0);
   

    }

    public static void DestroyAd()
    {
        if (ReklamBannerUst.reklamObjesi != null)
            ReklamBannerUst.reklamObjesi.Destroy();
        if (ReklamBannerAlt.reklamObjesi != null)
            ReklamBannerAlt.reklamObjesi.Destroy();

    }

    public void button_MutluSon()
    {
        
        mutluSonPaneli.SetActive(true);
        GameObject.Find("mutluSonDevam").GetComponent<Button>().enabled = false;
        GameObject.Find("mutluSonDevam").GetComponentInChildren<Text>().text = "Congratulations! You saved her!";

    }

    IEnumerator GinnyLoading()
    {
        textBaslangic.SetActive(false);
        ImageGinny.SetActive(true);
        yield return new WaitForSeconds(2);  //Wait
        ImageGinny.SetActive(false);
        textBaslangic.SetActive(true);
    }

    public void Devam()

    {
       
        Oyun.yenioyunmu = true;
        DestroyAd();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        PlayerPrefs.SetInt("AltinHesabi", coin-1000);
        PlayerPrefs.SetInt("devamabastimi", 1);

    }




}
