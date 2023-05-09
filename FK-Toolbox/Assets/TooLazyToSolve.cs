using UnityEngine;

/// <summary>
/// There is 20 people in a house. They need 20 roti for everyday breakfast.
/// Every male consumes 3 roti, every female consumes 2 roti and every child consumes 0.5f roti.
/// How may male, female and children are there in the house.
///
/// Answer: 1 + 5 + 14
/// </summary>
public class TooLazyToSolve : MonoBehaviour
{
    private void Start()
    {
        var solutionFound = false;
        for (var i = 1; i < 19; i++)
        {
            for (var j = 1; j < 19; j++)
            {
                for (var k = 1; k < 19; k++)
                {
                    if (i + j + k == 20 && ((i * 3) + (j * 2) + (k * 0.5f)) == 20)
                    {
                        solutionFound = true;
                        Debug.Log(i + " " + j + " " + k);
                        break;
                    }
                }

                if (solutionFound) break;
                
            }

            if (solutionFound) break;
        }
    }
}
