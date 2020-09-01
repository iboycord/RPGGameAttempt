using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendshipControl : MonoBehaviour
{
    #region Singleton
    public static FriendshipControl instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one FriendshipControl found");
            return;
        }
        instance = this;
    }
    #endregion


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
        if(rank + 1 < milestones.Count && overallFriendship >= milestones[rank + 1])
        {
            rank++;
        }
        if(rank > 0 && overallFriendship < milestones[rank])
        {
            if(rank - 1 < 0)
            {
                rank = 0;
            }
            else
            {
                rank--;
            }
        }
    }

    public void PrintData()
    {
        Debug.Log("Overall Friendship: " + overallFriendship + ", Max Friendship: " + maxFriendship + ", Current Milestone: " + milestones[rank].ToString() + ", Rank: " + rank);
    }

}
