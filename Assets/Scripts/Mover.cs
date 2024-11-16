using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] protected float Speed;
    [SerializeField] protected float JumpPower;

    [SerializeField] protected Rigidbody2D Rigidbody;
    [SerializeField] protected CapsuleCollider2D Collider;
    [SerializeField] protected Animator Animator;
    [SerializeField] protected SpriteRenderer SpriteRenderer;

    [SerializeField] private float _fallingGravityScale = 3f;


    protected bool IsJump = false;
    protected bool IsGround = false;

    private float _defualtGravityScale;
    private string _groundTag = "Ground";

    private void Awake()
    {
        _defualtGravityScale = Rigidbody.gravityScale;
    }

    private void LateUpdate()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_groundTag))
            IsGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_groundTag))
            IsGround = false;
    }

    protected void Move(Vector2 direction)
    {
        Vector2 velocity = Speed * direction;
        this.Rigidbody.velocity = velocity;
    }

    protected void Jump()
    {
        Vector2 velocity = new(this.Rigidbody.velocity.x, JumpPower);
        this.Rigidbody.AddForce(velocity, ForceMode2D.Impulse);
        IsJump = false;
    }

    protected void SetGravity()
    {
        if (!IsGround && Rigidbody.velocity.y < 0)
            Rigidbody.gravityScale = _fallingGravityScale;
        else if (IsGround || Rigidbody.velocity.y >= 0)
            Rigidbody.gravityScale = _defualtGravityScale;
    }

    protected void LimitVelocity() => this.Rigidbody.velocity = new(this.Rigidbody.velocity.x, Mathf.Clamp(this.Rigidbody.velocity.y, -JumpPower, JumpPower));
}
