using UnityEngine;
using System.Collections.Generic; 

public class Randomization : MonoBehaviour
{


    private string sequence = "";
    private string order = "";

    private string conditions = "0123456";
    private string materials = "0123";

    void Start()
    {
        // Shuffle conditions and assign to sequence
        sequence = ShuffleString(conditions);

        // For each condition (7 total)
        for (int i = 0; i < 7; i++)
        {
            // Create a temporary list to hold 8 materials

            // Fill materialList with 8 items, repeating materials as needed
            for (int j = 0; j < 8; j++)
            {
                order += ShuffleString(materials);
            }


        }

        Debug.Log("Sequence: " + sequence);
        Debug.Log("Order: " + order);

        // float timers = System.DateTime.Now.Ticks; 

    }

    // Helper function to shuffle a string
    private string ShuffleString(string input)
    {
        List<char> chars = new List<char>(input);
        return ShuffleList(chars);
    }

    // Helper function to shuffle a list of chars and return as string
    private string ShuffleList(List<char> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            // System.Random randObj = new(234);

            int rnd = Random.Range(0, i + 1);
            char temp = list[i];
            list[i] = list[rnd];
            list[rnd] = temp;
        }
        return new string(list.ToArray());
    }
}
