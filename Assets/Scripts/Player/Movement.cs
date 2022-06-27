using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private CharacterController _characterController;

    private Vector3 _moveDirection;
    private float _gravity;

    public Vector3 MoveDirection => _moveDirection;

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        _moveDirection = new Vector3(horizontalInput, 0, verticalInput);

        float magnitude = Mathf.Clamp01(_moveDirection.magnitude) * _speed;
        _gravity += Physics.gravity.y * Time.deltaTime;
        Vector3 velocity = _moveDirection * magnitude;
        velocity = AddJustVelocityToSlope(velocity);
        velocity.y += _gravity;
        _characterController.Move(velocity * Time.deltaTime);
    }

    private void Rotate()
    {
        if (_moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed);
        }
    }

    private Vector3 AddJustVelocityToSlope(Vector3 velocity)
    {
        var ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1f))
        {
            var adJustedVelocity = Quaternion.FromToRotation(Vector3.up, hitInfo.normal) * velocity;

            if (adJustedVelocity.y < 0)
            {
                return adJustedVelocity;
            }
        }

        return velocity;
    }
}