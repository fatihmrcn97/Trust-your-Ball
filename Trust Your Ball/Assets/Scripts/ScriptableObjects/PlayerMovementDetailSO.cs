using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Player Movement Data")]
public class PlayerMovementDetailSO : ScriptableObject
{
   public float speed=4f;
    public Vector3 offset;
}
