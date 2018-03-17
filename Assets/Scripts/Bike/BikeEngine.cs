using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Base class for all engines
 */
public abstract class BikeEngine : MonoBehaviour
{
    void Start() {}
    public virtual void SetEngineState(float thrust, bool reverse) {}
}