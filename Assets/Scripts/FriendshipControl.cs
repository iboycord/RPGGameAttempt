using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendshipControl : MonoBehaviour
{
    public int overallFriendship = 0;
    public int maxFriendship = 1000;
    public List<int> milestones = new List<int>();
    public int rank = 0;

    public void IncrementFriendship(int increment)
    {
        if (overallFriendship + increment <= maxFriendship) { overallFriendship += increment; }
        if (overallFriendship > maxFriendship) { overallFriendship = maxFriendship; }
        if (overallFriendship < 0) { overallFriendship = 0; }
        RankCheck();
    }
    
    public void RankCheck()
    {
        if(overallFriendship >= milestones[rank + 1])
        {
            rank++;
        }
        if(overallFriendship < milestones[rank])
        {
            rank--;
        }
    }

}
