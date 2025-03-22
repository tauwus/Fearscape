using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    public AudioSource footstepAudio; // 🎵 Audio source for footstep sound

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (footstepAudio)
        {
            footstepAudio.loop = true; // Make sure it loops
            footstepAudio.playOnAwake = false; // Don't start playing automatically
        }
    }

    void Update()
    {
        // Get input from player
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Update animator parameters
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        // Play or stop footstep sound
        if (movement.magnitude > 0)
        {
            if (footstepAudio && !footstepAudio.isPlaying)
            {
                footstepAudio.Play(); // 🎵 Start playing when moving
            }
        }
        else
        {
            if (footstepAudio && footstepAudio.isPlaying)
            {
                footstepAudio.Stop(); // 🎵 Stop when not moving
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed; // Fix velocity (previously was linearVelocity)
    }
}
