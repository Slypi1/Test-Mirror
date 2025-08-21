using UnityEngine;

public class InputBindings
{
    public float Horizontal => Input.GetAxis("Horizontal");
    public float Vertical => Input.GetAxis("Vertical"); 
    public bool Space => Input.GetKeyDown(KeyCode.Space);
    public bool F => Input.GetKeyDown(KeyCode.F);
}
