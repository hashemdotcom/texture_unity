using UnityEngine;

public class CubeSelector : MonoBehaviour
{
    public Renderer[] cubeRenderers = new Renderer[4]; // Assign the 4 cubes in Inspector
    public Color activeColor = Color.green;
    public Color defaultColor = Color.white;

    void Update()
    {
        int selectedIndex = -1;

        if (Input.GetKeyDown(KeyCode.Keypad0)) selectedIndex = 0;
        else if (Input.GetKeyDown(KeyCode.Keypad2)) selectedIndex = 1;
        else if (Input.GetKeyDown(KeyCode.Keypad5)) selectedIndex = 2;
        else if (Input.GetKeyDown(KeyCode.Keypad8)) selectedIndex = 3;

        if (selectedIndex != -1)
        {
            for (int i = 0; i < cubeRenderers.Length; i++)
            {
                if (cubeRenderers[i] != null)
                {
                    cubeRenderers[i].material.color = (i == selectedIndex) ? activeColor : defaultColor;
                }
            }
        }
    }
}
