using UnityEngine;

[CreateAssetMenu(menuName = "SO Dict/Single Attack")]
public class AttackSet : ScriptableObject
{
    public AnimationClip clip;
    public float damageMulti;
    public Projectile projectile;
}