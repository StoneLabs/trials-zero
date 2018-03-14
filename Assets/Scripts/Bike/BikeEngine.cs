using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BikeEngine : MonoBehaviour
{
	[LabelOverride("Invert Engine")]
	public bool inverse = true;

    void Start() {}
    public virtual void SetEngineState(float thrust, bool reverse) {}
}