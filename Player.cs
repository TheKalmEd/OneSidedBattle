using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float Health = 100;
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    public bool isGrounded;

    

    //HealthBar
    /*
    public Gradient TheHealthGradient;
    public Slider TheHealthSlider;
    public Image HealthFill;
    private bool HasDied = false;
    public GameObject DodgeDot1, DodgeDot2, DodgeDot3;
    public GameObject DodgeDotO1, DodgeDotO2, DodgeDotO3;
    */


    private void Awake()
    {

    }
    #region - Enable & Disable -
    private void OnEnable()
    {
       
    }
    private void OnDisable()
    {
      
    }
    #endregion
    void Start()
    {
        //TheHealthSlider.maxValue = 100;
        //TheHealthSlider.value = Health;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (HasDied == true)
        {
            return;
        }
        */

        //MOVEMENT
       

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float x = Input.GetAxis("Horizontal");

        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);


        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButton("Jump") && isGrounded)
         {
             velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
         }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        HealthLogic();
       
    }


    private void Attack()
    {
       
    }
  
    private void HealthLogic()
    {
       
    }
   
  
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   
   
    private void LateUpdate()
    {
        /* if (Input.GetKeyDown("h") || Input.GetKeyDown("u") || Input.GetKeyDown("j") || Input.GetKeyDown("i"))
         {}
         else
         {
             PlayerAnim.SetBool("IsAttack1", false);
             PlayerAnim.SetBool("IsAttack2", false);
             PlayerAnim.SetBool("IsAttack3", false);
             PlayerAnim.SetBool("IsAttack4", false);
             PlayerAnim.SetBool("IsAttack5", false);
             PlayerAnim.SetBool("IsIdle", true); 
         } */

    }
    private void OnTriggerStay(Collider Triginfo)
    {
       
    }

    private void OnTriggerExit(Collider Triginfo)
    {
       
    }


}
