using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyRocketMovement : MonoBehaviour
{
    public string enemy;
    public float speed;
    private bool facingRight = true;
    public float posX;
    public float negX;
    private bool isZone;
    private void Start()
    {
        GameEvent.current.OnTimeZoneBegin += OnTimeZoneBegin;
    }
    void FixedUpdate()
    {
        if(GameManager.current.isStart == true)
            transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);

        if (transform.position.x > posX && facingRight)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            facingRight = false;
            if (enemy == "rat" && !isZone)
                SoundManager.current.PlaySFX(5);
        }
        else if (transform.position.x < negX && !facingRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            facingRight = true;
            if (enemy == "rat" && !isZone)
                SoundManager.current.PlaySFX(5);
        }
    }
    private void OnTimeZoneBegin()
    {
        isZone = !isZone;
    }
}
