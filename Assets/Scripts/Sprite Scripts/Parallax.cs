using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;
    public float parallaxEffect;
    public bool autoScroll = false;

    private void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }


    private void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        //transform.position = new Vector3(startpos + dist, transform.position.y,transform.position.z);

        float desiredXPos = startpos + dist;

        if (autoScroll)
        {
            // this will push bg to the left
           // desiredXPos = transform.position.x - parallaxEffect;
            desiredXPos -= parallaxEffect * Time.deltaTime;
        }

        transform.position = new Vector2(desiredXPos, transform.position.y);


        if (temp > startpos + length)
        { startpos += length; }
        else if (temp < startpos - length)
        { startpos -= length; }
    }
}



