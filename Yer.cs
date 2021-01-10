using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yer : MonoBehaviour
{

    public Transform yerOlusturmaNoktasi;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        YerDegistirme();
    }

    void YerDegistirme()
    {
        //if (transform.position.x <= -22.35)
        //{
        //    transform.position = yerOlusturmaNoktasi.position;
        //}
        if (transform.position.x <= -yerOlusturmaNoktasi.position.x)
        {
            transform.position = yerOlusturmaNoktasi.position;
        }
    }

}
