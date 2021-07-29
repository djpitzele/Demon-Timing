using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface RangedClass : EntityClass
{
    //ALWAYS HAVE A COOLDOWN VARIABLE*
    public IEnumerator attack();
    public MonoBehaviour returnMB();

}
