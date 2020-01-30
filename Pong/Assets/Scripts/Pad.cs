using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : MonoBehaviour {

    public float speed;
    private SpriteRenderer mySpriteRenderer;

	// Use this for initialization
	void Start () {
        speed = 10;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Bounds b = mySpriteRenderer.bounds;
        Vector3 up =Camera.main.WorldToViewportPoint(b.max);
        Vector3 down = Camera.main.WorldToViewportPoint(b.min);
        float direction=0;
        if (this.name.Equals("left_pad"))
            {
                if (Input.GetKey(KeyCode.W))
                if (up.y < 0.9)
                    direction = 1;
                if (Input.GetKey(KeyCode.S))
                if (down.y > 0.1)
                    direction = -1;
            } 
        else
            {
                if (Input.GetKey(KeyCode.UpArrow) )
                if(up.y < 0.9)
                    direction = 1;
                if (Input.GetKey(KeyCode.DownArrow) )
                if (down.y > 0.1)
                    direction = -1;
            }

        transform.position += Vector3.up * direction * speed * Time.deltaTime;
    }
}
