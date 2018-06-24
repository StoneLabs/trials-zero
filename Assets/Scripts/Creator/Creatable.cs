using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Creatable : MonoBehaviour
{
    public static Creatable unknown = new Creatable(-1, "UNKNOWN");

	[LabelOverride("Creatable ID")]
    public int id;
    
	[LabelOverride("Creatable Name")]
    public string name = "";

    public Creatable(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}

