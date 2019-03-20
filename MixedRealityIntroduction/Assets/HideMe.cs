using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMe : MonoBehaviour {

	public void HideMeh() {
        gameObject.SetActive(false);
    }
    public void ShowMe()
    {
        gameObject.SetActive(true);
    }
}
