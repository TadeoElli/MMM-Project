using UnityEngine;

public class ConstantMovementStrategy : IMovementStrategy {
    public void MoveForward(float speed, Transform transform) {
        transform.Translate(transform.right * speed * Time.deltaTime);
        
    }

    public void Rotate(float rotationSpeed, float rotation, Transform transform) {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, rotation), rotationSpeed * Time.deltaTime);
        //rigidbody2D.MoveRotation(Quaternion.Slerp(rigidbody2D.transform.rotation, Quaternion.Euler(0f, 0f, rotation), rotationSpeed * Time.deltaTime));
    }
}
