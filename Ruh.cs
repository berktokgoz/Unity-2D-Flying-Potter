using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruh : MonoBehaviour
{
    public float minAralik;
    public float maxAralik;

    public GameObject ust;
    public GameObject alt;
    public GameObject gold;
    private SpriteRenderer ustRend;
    private SpriteRenderer altRend;
    private SpriteRenderer goldRend;


    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        RuhKaldir();

    }


    void Setup()
    {

        ustRend = ust.GetComponent<SpriteRenderer>();
        altRend = alt.GetComponent<SpriteRenderer>();
        goldRend = gold.GetComponent<SpriteRenderer>();
        float gecici;
        float gecici2;

        if (Oyun.aralik)
        {
            gecici = Random.Range(-0.5f, 0.5f);
            ust.transform.position = new Vector2(transform.position.x, transform.position.y + gecici);
            gecici2 = Random.Range(minAralik, maxAralik);
            alt.transform.position = new Vector2(transform.position.x, transform.position.y - gecici2);
           
        }
        else
        {
            gecici = Random.Range(-2.5f + minAralik, 4.5f);
            ust.transform.position = new Vector2(transform.position.x, gecici);
            gecici2 = Random.Range(minAralik, maxAralik);
            alt.transform.position = new Vector2(transform.position.x, gecici - gecici2);

        }

        gold.transform.position = new Vector2(gold.transform.position.x, alt.transform.position.y+(((ust.transform.position.y - alt.transform.position.y)-1)/2));


    }

    void RuhKaldir()
    {
        if (transform.position.x < 0)
        {
            if (!ustRend.isVisible && !altRend.isVisible)
            {
                Destroy(this.gameObject);
            }
        }
    }


}
