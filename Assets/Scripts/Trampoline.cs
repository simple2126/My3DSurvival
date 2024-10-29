using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float force;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterManager.Instance.Player.controller.rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }
}
