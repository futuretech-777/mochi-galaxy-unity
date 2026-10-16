using UnityEngine;

namespace StitchApp
{
    /**
     * MochiCharacterController
     * 
     * A mobile-optimized character controller for the Mochi mascot.
     * Handles movement and basic aesthetic animations like "Idle Bounce".
     */
    public class MochiCharacterController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 10f;
        
        [Header("Mobile Joystick (Optional)")]
        public FloatingJoystick joystick; // Assuming a standard mobile joystick package
        
        private CharacterController _controller;
        private Vector3 _moveDirection;
        
        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            HandleMovement();
            ApplyAestheticBounce();
        }

        private void HandleMovement()
        {
            // Support both Keyboard (Editor) and Joystick (Mobile)
            float moveX = joystick != null ? joystick.Horizontal : Input.GetAxis("Horizontal");
            float moveZ = joystick != null ? joystick.Vertical : Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(moveX, 0, moveZ).normalized;

            if (direction.magnitude >= 0.1f)
            {
                // Smooth Rotation
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _rotationVelocity, 0.1f);
                transform.rotation = Quaternion.Euler(0, angle, 0);

                // Movement
                _controller.Move(direction * moveSpeed * Time.deltaTime);
            }
        }

        private float _rotationVelocity;

        private void ApplyAestheticBounce()
        {
            // Simple visual "Bounce" while idle
            if (_controller.velocity.magnitude < 0.1f)
            {
                float bounce = Mathf.Sin(Time.time * 2f) * 0.02f;
                transform.position = new Vector3(transform.position.x, transform.position.y + bounce, transform.position.z);
            }
        }
    }

    // Mock for Joystick if package not found
    public class FloatingJoystick : MonoBehaviour { public float Horizontal; public float Vertical; }
}
