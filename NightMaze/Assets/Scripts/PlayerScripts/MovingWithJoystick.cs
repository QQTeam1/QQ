using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class MovingWithJoystick : MonoBehaviour
{
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private Animator animator;
    [SerializeField] private Slider slider;
    [SerializeField] private Button sliderButton;
    [SerializeField] private TMP_Text sliderText;
    [SerializeField] private Health health;

    [SerializeField] private float teleportSpeed; // Швидкість телепортації
    [SerializeField] private bool isJumping = false; // прапорець, що показує, чи рухається персонаж
    [SerializeField] private bool jumpIsAllowed;
    public float moveSpeed = 3;
    private int numberOfJumps = 0;
    private Vector2 targetPosition; // Позиція, на яку буде телепортуватися персонаж
    public int maxNumberOfJumps = 2;
    private float angle;
    private Vector2 lastJoystickDirection;
    private Vector2 checkBoxSize;
    private LayerMask wallLayer;
    private LayerMask halfWallLayer;


    public void Awake()
    {
        checkBoxSize = new Vector2(0.04f, 0.04f);
        wallLayer = LayerMask.GetMask("Wall");
        halfWallLayer = LayerMask.GetMask("HalfWall");
        jumpIsAllowed = false;
        slider.value = 0;

        health = GetComponent<Health>();
    }
    public void OnButtonClick()
    {
        if (!isJumping)
        {
            Vector2 playerPosition = transform.position;
            Vector2 nextTileToNext = new Vector2(playerPosition.x + 2 * lastJoystickDirection.x, playerPosition.y + 2 * lastJoystickDirection.y);
            if (jumpIsAllowed && numberOfJumps > 0)
            {
                targetPosition = nextTileToNext;
                dust.Play();
                capsuleCollider.isTrigger = true;
                numberOfJumps--;
                StartCoroutine(MoveToTarget());
            }
        }
    }
    private void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(joystick.Horizontal) + Mathf.Abs(joystick.Vertical));
        // Оберт персонажа
        if (joystick.Horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (joystick.Horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            angle = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
            lastJoystickDirection = new Vector2(joystick.Horizontal, joystick.Vertical);
        }


        Vector2 playerPosition = transform.position;
        Vector2 nextTile = new Vector2(playerPosition.x + lastJoystickDirection.x, playerPosition.y + lastJoystickDirection.y);
        Vector2 nextTileToNext = new Vector2(playerPosition.x + 2 * lastJoystickDirection.x, playerPosition.y + 2 * lastJoystickDirection.y);
        jumpIsAllowed = Physics2D.OverlapBox(nextTile, checkBoxSize, angle, halfWallLayer) != null && Physics2D.OverlapBox(nextTileToNext, checkBoxSize, 0, wallLayer) == null && Physics2D.OverlapBox(nextTileToNext, checkBoxSize, 0, halfWallLayer) == null;

        sliderText.text = numberOfJumps.ToString();
        if (jumpIsAllowed)
        {
            sliderButton.interactable = true;
        }
        else
        {
            sliderButton.interactable = false;
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
        if (slider.value < 1 && numberOfJumps < maxNumberOfJumps)
        {
            slider.value += 0.002f;
        }
        else if (slider.value >= 1)
        {
            slider.value = 0;
            numberOfJumps++;
        }
        if (!isJumping)
        {
            rigidbody2d.velocity = new Vector2(joystick.Horizontal * moveSpeed, joystick.Vertical * moveSpeed);
        }
    }
    private IEnumerator MoveToTarget()
    {
        isJumping = true; // встановлення прапорця, що персонаж рухається
        animator.SetBool("isJumping", true);

        while ((Vector2)transform.position != targetPosition) // поки персонаж не дійшов до цільової позиції
        {
            // Рухаємо персонажа в напрямку цільової позиції з заданою швидкістю
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, teleportSpeed * Time.deltaTime);

            yield return null; // чекаємо до наступного кадру
        }
        dust.Play();
        animator.SetBool("isJumping", false);
        capsuleCollider.isTrigger = false;
        isJumping = false; // скидаємо прапорець, що персонаж рухається
    }

}
