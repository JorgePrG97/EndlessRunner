using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour {

    public GameObject panel;

    private Rigidbody2D R2D;
    public float maxSpeed = 1;
    public Vector2 jumpForce = Vector2.up * 150;
    private bool jumped, grounded, thrown;
    public bool Grounded
    {
        get
        {
            return grounded;
        }
        set
        {
            grounded = value;
        }
    }
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if(instance==null)
            {
                instance = GameObject.FindObjectOfType<Player>().GetComponent<Player>();
            }
            return instance;
        }
    }

    public int maxHealth = 10;
    public int currentHealth;
    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
            if(currentHealth == 0)
            {
                Die();
            }
        }
    }
    private Camera cam;

    private bool isAlive
    {
        get
        {
            return currentHealth > 0;
        }
    }

    public GameObject daggerPrefab;
    private CapsuleCollider2D capsule;

    private Animator animator;

    void Awake ()
    {
        R2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        cam = Camera.main;
        capsule = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        //Incializacion del objeto
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //comprobar si el jugador salta
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumped = true;
        }
        if (Input.GetKeyDown(KeyCode.Q) /*&& grounded*/)
        {
            thrown = true;
            GameManager.Instance.Score--;
        }

        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    jumped = true;
                }
                else
                {
                    thrown = true;
                }
            }
        }
	}

    void FixedUpdate()
    {
        R2D.AddForce(Vector2.right * 8 * R2D.mass);
        if (jumped)
        {
            R2D.AddForce(jumpForce * R2D.mass, ForceMode2D.Impulse);
            jumped = false;
        } 
        if(thrown)
        {
            animator.SetTrigger("throw");
            thrown = false;
        }

        //Anyadimos una fuerza de uno hacia la dcha
        if(R2D.velocity.x > maxSpeed)
        {
            R2D.velocity = new Vector2(maxSpeed, R2D.velocity.y);
        }
    }

    public void Throw()
    {
        var go = Instantiate(daggerPrefab, capsule.bounds.center, Quaternion.identity);
        go.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5, ForceMode2D.Impulse);
    }

    private void LateUpdate()
    {
        if((transform.position.y < cam.OrtographicBounds().min.y - 3) || (transform.position.y > cam.OrtographicBounds().max.y + 10))
        {
            Die();
        }
        animator.SetFloat("xSpeed", Mathf.Abs(R2D.velocity.x));
    }
    private void Die()
    {
        R2D.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetTrigger("die");
        capsule.enabled = false;
        panel.SetActive(true);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        
    }
    void OnTriggerExit2D(Collider2D other)
    {
        grounded = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 9)
        {
            //Destroy(other.gameObject);
            other.gameObject.transform.DOScale(Vector3.zero, .3f);
            GameManager.Instance.CoinsPicked++;
        }
    }
}
