using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Movement")]
    public float horizontalForce;
    public float verticalForce;
    public bool isGrounded;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public Transform GroundOrigin;

    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            Move();
        }

        CheckIfGrounded();
    }

    private void Move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        float jumpInput = Input.GetAxisRaw("Jump");

        Vector2 worldTouch = default;

        foreach (var touch in Input.touches)
        {
            worldTouch = Camera.main.ScreenToWorldPoint(touch.position);
        }

        float horizontalMoveForce = xInput * horizontalForce * Time.deltaTime;
        float verticalMoveForce = jumpInput * verticalForce * Time.deltaTime;
        rigidBody.AddForce(new Vector2(horizontalMoveForce, verticalMoveForce));
    }

    private void CheckIfGrounded()
    {
        var hit = Physics2D.CircleCast(GroundOrigin.position, groundRadius, Vector2.down, groundRadius, groundLayerMask);

        isGrounded = (hit ? true : false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(GroundOrigin.position, groundRadius);
    }
}
