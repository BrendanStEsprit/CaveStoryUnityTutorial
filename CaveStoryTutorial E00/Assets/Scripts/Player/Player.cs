using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Controller2D))]
public class Player : Entity {

	public float maxJumpHeight = 3f;
	public float minJumpHeight = 1.5f;
	public float timeToJumpApex = .4f;
	public float accelerationTimeGrounded = .1f;
	public float accelerationTimeAirborneMultiplier = 2f;

    public float timeInvincible = 2.0F;

    bool invincible;
    bool forceApplied;

    float moveSpeed = 6f;
    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    Vector2 movementInput;
    float velocityXSmoothing;
    Direction direction;
    bool facingRight;
    Animator animator;
    SpriteRenderer spriterenderer;

    public AudioSource audioSource;
    public AudioClip pickup;
    public AudioClip jump;
    public AudioClip hit;

    public GameObject Weapon;

    public static Player instance;

    Controller2D controller;

    public bool IsFacingRight()
    {
        return facingRight;
    }
    public Direction GetDirection()
    {
        return direction;
    }

    public void ApplyDamage(int damage)
    {
        if(!invincible)
        {
            SubstactLevel(damage);
            SubtractHealth(damage);
            audioSource.PlayOneShot(hit);
            SetVelocity(Vector2.up * 8.0f);
            StartCoroutine(SetInvincible());
            UIScript.instance.SetValue(health / (float)maxHealth);
            LevelUI.instance.SetValue(level / (float)maxLevel);
            if (health <=0)
            {
                SceneManager.LoadScene("GameOver Scene", LoadSceneMode.Single);
            }    
        }
    }
    

    protected override  void Awake()
    {
        base.Awake();
        instance = this;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<Controller2D>();
		//Initialize Vertical Values
		gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        Weapon.gameObject.SetActive(false);
        

    }

    private void Update()
    {
		GetInput ();
        Animation ();
		Horizontal ();
		Vertical ();
		ApplyMovement ();

    }

    private void GetInput()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));


        //if moving
        if(movementInput.x !=0)
        {
            direction = (movementInput.x > 0 ? Direction.RIGHT : Direction.LEFT);
            facingRight = movementInput.x > 0; 
        }
        float verticalAimFactor = movementInput.y;
        if (controller.collisions.below)
        {
           float VerticalAimFactor = Mathf.Clamp01(verticalAimFactor);
        }
        //if looking vertically 
        if (verticalAimFactor != 0)
        {
            direction = (verticalAimFactor > 0 ? Direction.UP : Direction.DOWN);
           
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    private void Animation()
    {
        spriterenderer.flipX = facingRight;
        animator.SetFloat("VelocityX", Mathf.Abs(movementInput.x));
        animator.SetFloat("VelocityY", Mathf.Sign(movementInput.y));
        animator.SetFloat("Looking", General.Direction2Vector(direction).y);
        animator.SetBool("Grounded", controller.collisions.below);

    }
    private void Vertical()
    {
        if (forceApplied)
        {
            forceApplied = false;

        }else if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }


        if (Input.GetButtonDown("Fire1") && controller.collisions.below)
        {
            audioSource.PlayOneShot(jump);
            velocity.y = maxJumpVelocity;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }

		velocity.y += gravity * Time.deltaTime;

    }

    private void Horizontal()
    {
        float targetVelocityX = movementInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing,
            accelerationTimeGrounded * (controller.collisions.below? 1.0f : accelerationTimeAirborneMultiplier));
    }

    private void ApplyMovement()
    {
        controller.Move(velocity * Time.deltaTime);
    }

    private void SetVelocity(Vector2 v)
    {
        velocity = v;
        forceApplied = true;
    }
    private IEnumerator SetInvincible()
    {
        invincible = true;
        float elapsedTime = 0f;
        while(elapsedTime < timeInvincible)
        {
            spriterenderer.enabled = !spriterenderer.enabled;
            elapsedTime += .04f;
            yield return new WaitForSeconds(.04f);
        }
        spriterenderer.enabled = true;
        invincible = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag=="Door1")
        {
            if(Input.GetKey(KeyCode.DownArrow))
            {
                transform.position = new Vector2(2, 2);
                //transform.position = new Vector2(1, 2);
                Debug.Log("I should teleport");
            }
            
        }
        if (other.gameObject.name == "Door2")
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.position = new Vector2(-2, -42);
                //transform.position = new Vector2(1, -42);
                Debug.Log("I should teleport");
            }

        }
        if (other.gameObject.name == "Door3")
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.position = new Vector2(-6, -96);
                //transform.position = new Vector2(-5, -96);
                Debug.Log("I should teleport");
            }

        }
        if (other.gameObject.name == "Door4")
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.position = new Vector2(7, -56);
                //transform.position = new Vector2(4, -59);
                Debug.Log("I should teleport");
            }

        }
        if (other.gameObject.name == "Door5")
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.position = new Vector2(40, -41);
                Debug.Log("I should teleport");
            }

        }
        if(other.gameObject.tag == "Powerup")
        {
            AddHealth(3);
            Destroy(other.gameObject);
            audioSource.PlayOneShot(pickup);
            UIScript.instance.SetValue(health / (float)maxHealth);
        }
        if(other.gameObject.tag == "WeaponUp")
        {
            Weapon.gameObject.SetActive(true);
        }
        if(other.gameObject.name == "Door7")
        {
            if(Input.GetKey(KeyCode.DownArrow))
            {
                Debug.Log("Playerhaswon");
                SceneManager.LoadScene("WinScene", LoadSceneMode.Single);
            }

            
        }
        

    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "ExpUp")
        {
            AddLevel(1);
            Destroy(other.gameObject);
            audioSource.PlayOneShot(pickup);
            LevelUI.instance.SetValue(level / (float)maxLevel);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
