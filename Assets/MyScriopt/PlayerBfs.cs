using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBfs : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        Bfs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Bfs()
    {
        var stack = new Stack<int[]>();
        stack.Push(this.start);
        var visited = new Dictionary<string, bool>();
        visited.Add(string.Concat(this.start),true);
        var d = new int[,] { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };

        while (stack.Count > 0)
        {
            int[] current = stack.Pop();

            if (current.SequenceEqual(this.goal))
	        {
                Debug.Log(current);
                Debug.Log("goal");
                transform.position = new Vector3(current[0] + 0.5f, 0, current[1] + 0.5f);
                break;
	        }


            for (int i = 0; i < d.GetLength(0); i++)
            {
                int x = current[0] + d[i, 0];
                int z = current[1] + d[i, 1];
                int[] next = { x,z };
                if (visited.ContainsKey(string.Concat(next)))
                {
                    continue;
		        }

                if (IsOutside(x,z))
                {
                    continue;
		        }

                if (IsConflict(x,z))
                {
                    continue;
		        }

                stack.Push(next);
                visited.Add(string.Concat(next), true);
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
