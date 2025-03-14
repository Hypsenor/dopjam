using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float Horizontal;
    public float speed;
    public bool onGround;
    public Vector2 bottomoffset;
    [SerializeField] bool canJump;
    [SerializeField] float jump;
    [SerializeField] float collisionRadius;
    public LayerMask layer;
    public Slider slider;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        if (rb.linearVelocity.y < 0)
            rb.gravityScale = 10;
        else
            rb.gravityScale = 8;

        if (speed == 500)
            fixspeed();
        if (slider != null && slider.value >= 100)
        {
            SceneManager.LoadScene("Menu");
        }
        if (onGround && rb.linearVelocity.y <= 0)
            canJump = true;
        else
            canJump = false;

        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomoffset, collisionRadius, layer);
        ScaleControl();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomoffset, collisionRadius);
    }
    async void fixspeed()
    {
        await Task.Delay(3000);
        speed = 900;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Clown")
            SceneManager.LoadScene(2);
    }

    void ScaleControl()
    {
        if (Horizontal > 0 && this.gameObject.transform.localScale.x < 0)
            this.gameObject.transform.localScale = new Vector2(this.gameObject.transform.localScale.x * -1, this.gameObject.transform.localScale.y);
        if (Horizontal < 0 && this.gameObject.transform.localScale.x > 0)
            this.gameObject.transform.localScale = new Vector2(this.gameObject.transform.localScale.x * -1, this.gameObject.transform.localScale.y);




    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(Horizontal * speed * Time.deltaTime, rb.linearVelocity.y);
        if (Input.GetKey(KeyCode.Space) && canJump)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump * Time.deltaTime);
    }
}
