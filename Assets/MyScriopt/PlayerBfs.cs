using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerBfs : MonoBehaviour
{
    private ThirdPersonCharacter character;
    int row = 7;
    int col = 8;
    int[] start = new int[] {1,1};
    int[] goal = new int[] {3,4};
    int[,] dist = new int[7, 8] { {0,0,0,0,0,0,0,0},
                                {0,1,1,1,1,1,1,0},
                                {0,1,0,0,0,0,0,0},
                                {0,1,1,0,1,1,1,0},
                                {0,1,1,0,0,1,1,0},
                                {0,0,1,1,1,1,1,0},
                                {0,0,0,0,0,0,0,0}};
    float currentTime = 0f;
    float spanTime = 1.0f;
    Stack<int[]> stack = new Stack<int[]>();
    Dictionary<string, bool> visited = new Dictionary<string, bool>();

    void Start()
    {
        character = GetComponent<ThirdPersonCharacter>();

        stack.Push(start);
        visited.Add(string.Join("-",start), true);
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= spanTime)
        {
            currentTime = 0;
            MovePlayer();
	    }
    }

    void MovePlayer()
    { 
        if (stack.Count > 0)
        {
            int[] current = stack.Pop();
            transform.position =
             Vector3.MoveTowards(transform.position, new Vector3(current[0] + 0.5f, 0, current[1] + 0.5f), Time.deltaTime);
            //character.Move(new Vector3(current[0] + 0.5f, 0, current[1] + 0.5f),false,false);
            //transform.Translate(, Space.World);
            Bfs(current);
        }
        else
        {
            Debug.Log("game over");
	    }
    }

    void Bfs(int[] current)
    {
        var d = new int[,] { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };

        if (current.SequenceEqual(this.goal))
	    {
            Debug.Log("goal");
	    }
        else 
	    {
            for (int i = 0; i < d.GetLength(0); i++)
            {
                int x = current[0] + d[i, 0];
                int z = current[1] + d[i, 1];
                int[] next = { x, z };
                if (visited.ContainsKey(string.Join("-",next)))
                {
                    continue;
                }

                if (IsOutside(x, z))
                {
                    continue;
                }

                if (IsConflict(x, z))
                {
                    continue;
                }

                stack.Push(next);
                visited.Add(string.Join("-", next), true);
            }
        }
        
    }

    bool IsOutside(int x, int z)
    {
        return ((0 > x || this.row < x) || (0 > z || this.col < z));
    }

    bool IsConflict(int x,int z)
    {
        return !Convert.ToBoolean(this.dist[x, z]);
    }
}
