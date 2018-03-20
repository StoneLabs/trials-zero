using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Collection of generic UI function that can be called
 * from within the UI tree.
 */
public class GenericUI : MonoBehaviour 
{
    // Used by UI elements to change the current scene. TODO: rename to LoadLevel to be more descriptive
    public void SelectLevel(int index)
    {
        Application.LoadLevel(index);
    }

    // Used by UI elements to change the current scene. 
    public void SelectLevel(string index)
    {
        Application.LoadLevel(index);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
