using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerJoystickMovement))]

public class Player : MonoSingleton<Player>
{
    public Animator Animator { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
    }
}