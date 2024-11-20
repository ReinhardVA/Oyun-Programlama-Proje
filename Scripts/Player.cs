using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 50.0f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpForce = 3f;
    private Vector3 _velocity;
    private bool _isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        PlayerMovement();
        ApplyGravity();
    }

    
    private void GroundCheck()
    {
        // Player nesnesinin altında ufak bir Sphere oluşturuyor ve bunun Ground ile çarpışması halinde true, aksi halde false döndürüyor
        // Player'ın Ground üzerinde olduğunu anlıyoruz ve zıplamasına izin veriyoruz
        // eğer velocity'i burada resetlemezsek sürekli artıyor, gravity'i arttırıyor.
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
    }

    private void ApplyGravity()
    {
        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }

    private void PlayerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(speed * Time.deltaTime * move);

        if(Input.GetButtonDown("Jump") && _isGrounded){
            _velocity.y = Mathf.Sqrt(jumpForce * (-2f) * gravity);
        }
    }
}
