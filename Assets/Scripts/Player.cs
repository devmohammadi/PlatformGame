using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // سرعت بازیکن
    public float speed = 5;
    private Rigidbody2D rb;
    public float jump = 7;
    // برای تشخیص اینکه به زمین برخورد کرده است یا خیر
    private bool isgrounded = false;
    private Animator anim;
    private Vector3 rotation;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        // جهت برای جابه جایی بازیکن به صورت افقی
        float direction = Input.GetAxis("Horizontal");

        if (direction != 0)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

        if (direction < 0)
        {
            transform.eulerAngles = rotation - new Vector3(0, 180, 0);
            // جابه جایی در جهت و فاصله دلخواه تعریف شده
            // در زمان بندی مشخص شده و واقعی این جابه جایی اتفاق می افتد
            transform.Translate(Vector2.right * speed * -direction * Time.deltaTime);
        }
        if (direction > 0)
        {
            transform.eulerAngles = rotation;
            transform.Translate(Vector2.right * speed * direction * Time.deltaTime);
        }

        if (isgrounded == false)
        {
            anim.SetBool("IsJumping", true);
        }
        else
        {
            anim.SetBool("IsJumping", false);
        }

        // اگر کلید Space زده شد و به زمین برخورد داشت شرط اجرا شود
        // چون اگر روی زمن نباشد نمی تواند پرش داشته باشد
        if (Input.GetKeyDown(KeyCode.Space) && isgrounded)
        {
            // به حالت نرم به بالا و یایین بپرد
            rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            isgrounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // اگر تصادمی با زمین اتفاق افتاد
        // زمانی که بازیکن به زمین برخورد کرد متغییر را True  می کنیم 
        if (collision.gameObject.tag == "ground")
        {
            isgrounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "coin")
        {
            Destroy(other.gameObject);
        }
    }
}
