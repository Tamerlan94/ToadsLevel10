using UnityEngine;

public class EnemyBallMovement : MonoBehaviour
{
    public float speed;    
    private bool facingRight = true;

    private void Update()
    {
        if (transform.position.y < -6f)
        {
            gameObject.SetActive(false);
        }
    }
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);

        if (transform.position.x > 2.3f && facingRight) 
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            facingRight = false;
            SoundManager.current.PlaySFX(7);
        }
        else if(transform.position.x<-2.3f && !facingRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            facingRight = true;
            SoundManager.current.PlaySFX(7);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Enemy0");   
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Enemy");
        }
    }
}
