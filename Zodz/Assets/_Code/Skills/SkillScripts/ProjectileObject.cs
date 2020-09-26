using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileObject : MonoBehaviour
{
    public abstract void InitializeProjectile(SkillUser user);
}
