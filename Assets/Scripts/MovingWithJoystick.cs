using UnityEngine;

[RequireComponent (typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class MovingWithJoystick : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;

    private float angle;
    private Vector2 lastJoystickDirection;
    private Vector2 checkBoxSize;
    private LayerMask wallLayer;
    private LayerMask halfWallLayer;
    public void Awake()
    {
        checkBoxSize = new Vector2(0.01f, 0.01f);
        wallLayer = LayerMask.GetMask("Wall");
        halfWallLayer = LayerMask.GetMask("HalfWall");
    }
    public void OnButtonClick()
    {
        
        Vector2 playerPosition = transform.position;
        Vector2 nextTile = new Vector2(playerPosition.x + lastJoystickDirection.x, playerPosition.y + lastJoystickDirection.y);
        Vector2 nextTileToNext = new Vector2(playerPosition.x + 2 * lastJoystickDirection.x, playerPosition.y + 2 * lastJoystickDirection.y);
        if(Physics2D.OverlapBox(nextTile, checkBoxSize, angle, halfWallLayer) != null && Physics2D.OverlapBox(nextTileToNext, checkBoxSize, 0, wallLayer) == null && Physics2D.OverlapBox(nextTileToNext, checkBoxSize, 0, halfWallLayer) == null)
        {
            transform.position = nextTileToNext;
        }
    }
    private void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(joystick.Horizontal) + Mathf.Abs(joystick.Vertical));


        if (joystick.Horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            //transform.Rotate(0, 0, 0);
        }
        else if (joystick.Horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            //transform.Rotate(0, 180, 0);
        }
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            angle = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
            lastJoystickDirection = new Vector2(joystick.Horizontal, joystick.Vertical);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnButtonClick();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, checkBoxSize);
    }
    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(joystick.Horizontal * moveSpeed, joystick.Vertical * moveSpeed);
    }
}
