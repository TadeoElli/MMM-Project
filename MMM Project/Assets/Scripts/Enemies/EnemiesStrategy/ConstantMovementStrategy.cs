using UnityEngine;

public class ConstantMovementStrategy : IMovementStrategy {
    public void MoveForward(float speed, Rigidbody2D rigidbody2D) {
       // transform.Translate(Vector2.up * speed * Time.deltaTime);
        rigidbody2D.AddForce(rigidbody2D.transform.up * speed);
        Vector3 clampedVelocity = rigidbody2D.velocity;
        clampedVelocity.x = Mathf.Clamp(rigidbody2D.velocity.x, -speed, speed);
        clampedVelocity.y = Mathf.Clamp(rigidbody2D.velocity.y, -speed * 2, speed * 2);
        rigidbody2D.velocity = clampedVelocity;
    }

    public void Rotate(float rotationSpeed, float rotation, Rigidbody2D rigidbody2D) {
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, rotation), rotationSpeed * Time.deltaTime);
        rigidbody2D.MoveRotation(Quaternion.Slerp(rigidbody2D.transform.rotation, Quaternion.Euler(0f, 0f, rotation), rotationSpeed * Time.deltaTime));
    }
}
