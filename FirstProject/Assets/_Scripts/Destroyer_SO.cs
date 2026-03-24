using UnityEngine;

[CreateAssetMenu(fileName = "Destroyer_SO", menuName = "Scriptable Objects/Destroyer_SO")]
public class Destroyer_SO : ScriptableObject
{
	public float damage;
	public EnumTags targetTag;
	public bool destroyOnCollisionEnter;
}
