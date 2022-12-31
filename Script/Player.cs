using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ���ƽ�ɫ
/// </summary>
public class Player : MonoBehaviour
{
    private  Animator anim;//����
    private Rigidbody2D rbody;//����
    public float speed;//�ٶ�
    public float jumpForce;//��Ծ��
    public Transform groundCheck;

    public LayerMask ground;
    public Collider2D coll;
    public int Cherry;
    public Text CherryNum ;

    public bool isGround, isJump;

    bool jumpPressed;
    int jumpCount = 2;

    // Start is called before the first frame update
    void Start()
    {
       
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }
    }
    // Update is called once per frame
    /// <summary>
    /// 
    /// </summary>
    private  void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
        GrounMovement();
        Jump();

        SwitchAnim();
    }
    /// <summary>
    ///�ƶ�
    /// </summary>
    void GrounMovement()
    {
      
        //�����ƶ�
        float horizontalMove =Input.GetAxisRaw("Horizontal");

       
        rbody.velocity = new Vector2(horizontalMove * speed, rbody.velocity.y);
        //��������

        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
        
        
        
        
         
        
    }
    
    //��Ծ
    void Jump()
    {
        if (isGround)
        {
            jumpCount = 2;
            isJump = false;
           
        }
        if (jumpPressed && isGround)
        {
            isJump = true;
            rbody.velocity = new Vector2(rbody.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0 && isJump)
        {
            rbody.velocity = new Vector2(rbody.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
       

    }
    //�л�����Ч��
    void SwitchAnim()
    {
        anim.SetFloat("running", Mathf.Abs(rbody.velocity.x));
        if (isGround)
        {
            anim.SetBool("falling", false);
          
        }
        else if (!isGround && rbody.velocity.y > 0)
        {
            anim.SetBool("jumping", true);
        }
        else if (rbody.velocity.y < 0)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }
    }
    //�ռ���Ʒ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collection")
        {
            

            CherryNum.text = Cherry.ToString();
        }
    }

    public void CherryCount()
    {
        Cherry++;
    }
}
