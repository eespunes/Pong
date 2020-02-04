using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour {

    private Vector3 direction;
    public float speed;
    public Text scoreLeftT, scoreRightT;
    private int scoreLeft, scoreRight;
    public GameObject left, right;
    public AudioClip leftSound, rightSound;
    private SpriteRenderer rightPad, leftPad;
    private SpriteRenderer ballSP;
    private AudioSource audio;

    // Use this for initialization
    void Start () {
        scoreLeft = 0;
        scoreRight = 0;
        direction = new Vector2(1, 0);
        ballSP = GetComponent<SpriteRenderer>();
        rightPad = right.GetComponent<SpriteRenderer>();
        leftPad = left.GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        direction = PadCollision();
        Bounds b = ballSP.bounds;
        if (Camera.main.WorldToViewportPoint(b.max).y > 0.9)
            direction = new Vector2(direction.x, -1);
        if (Camera.main.WorldToViewportPoint(b.min).y < 0.1)
            direction = new Vector2(direction.x, 1);
        transform.position += direction*speed*Time.deltaTime;
        EarnPoint();
    }
    private void EarnPoint()
    {
        Vector3 point = Camera.main.WorldToViewportPoint(transform.position);
        if (point.x > 1)
        {
            scoreLeft++;
            scoreLeftT.text = scoreLeft.ToString();
            direction = new Vector2(-direction.x, -direction.y);
            transform.position = Vector2.zero;
        }
        else if (point.x < 0)
        {
            scoreRight++;
            scoreRightT.text = scoreRight.ToString();
            direction = new Vector2(-direction.x, -direction.y);
            transform.position = Vector2.zero;
        }
    }

    private Vector2 PadCollision()
    {
        if(ballSP.bounds.min.x<leftPad.bounds.max.x&& ballSP.bounds.min.x > leftPad.bounds.min.x&& ballSP.bounds.min.y < leftPad.bounds.max.y&& ballSP.bounds.min.y > leftPad.bounds.min.y)
            return BallDirectionLeft();
        if((ballSP.bounds.max.x < rightPad.bounds.max.x && ballSP.bounds.max.x > rightPad.bounds.min.x && ballSP.bounds.max.y < rightPad.bounds.max.y && ballSP.bounds.max.y > rightPad.bounds.min.y))
            return BallDirectionRight();
        return direction;
    }
    private Vector2 BallDirectionLeft()
    {
        audio.clip = leftSound;
        audio.Play();
        float j = leftPad.bounds.max.y - leftPad.bounds.min.y;
 
        if (transform.position.y > (leftPad.bounds.max.y - j / 3) && transform.position.y <= leftPad.bounds.max.y)
            return new Vector2(1, 1);
        if (transform.position.y <= (leftPad.bounds.max.y - j / 3) && transform.position.y >= (leftPad.bounds.max.y - j * 2 / 3))
            return new Vector2(1, 0);
        if (transform.position.y < (leftPad.bounds.max.y - j * 2 / 3) && transform.position.y >= leftPad.bounds.min.y)
            return new Vector2(1, -1);
        return -direction;
    }

    private Vector2 BallDirectionRight()
    {
        audio.clip = rightSound;
        audio.Play();
        float j = rightPad.bounds.max.y - rightPad.bounds.min.y;

        if (transform.position.y > (rightPad.bounds.max.y - j / 3) && transform.position.y <= rightPad.bounds.max.y)
            return new Vector2(-1, 1);
        if (transform.position.y <= (rightPad.bounds.max.y - j / 3) && transform.position.y >= (rightPad.bounds.max.y - j * 2 / 3))
            return new Vector2(-1, 0);
        if (transform.position.y < (rightPad.bounds.max.y - j * 2 / 3) && transform.position.y >= rightPad.bounds.min.y)
            return new Vector2(-1, -1);
        return -direction;
    }
}
