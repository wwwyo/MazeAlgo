using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using MyScript;

namespace MyScript 
{ 
    public class PlayerBfs : MonoBehaviour
    {
        private CharacterMove character;
        int stop_counter = 0;
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
        bool is_arrived = true;
        Vector3 next_pos;
        Stack<int[]> stack = new Stack<int[]>();
        Dictionary<string, bool> visited = new Dictionary<string, bool>();

        void Start()
        {
            character = GetComponent<CharacterMove>();

            stack.Push(start);
            visited.Add(string.Join("-",start), true);
            transform.position = new Vector3(start[0] + .5f,0,start[1] + .5f);
        }

        void Update()
        {
            MovePlayer();
        }

        void MovePlayer()
        {
            stop_counter++;
            if (stop_counter < 1000)
            {
                return;
	        }
            if (is_arrived)
            {
                if (stack.Count > 0)
                {
                    is_arrived = false;
                    int[] current = stack.Pop();
                    next_pos = new Vector3(current[0]+ 0.5f, transform.position.y, current[1]+0.5f);
                    PushStack(current);
                }
            }
            character.MoveCoordinate(next_pos);
            //transform.position = Vector3.MoveTowards(transform.position, next_pos, Time.deltaTime);
            if (transform.position == next_pos)
            {
                is_arrived = true;
	        }
        }

        void PushStack(int[] current)
        {
            var d = new int[,] { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };
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

        bool IsOutside(int x, int z)
        {
            return ((0 > x || this.row < x) || (0 > z || this.col < z));
        }

        bool IsConflict(int x,int z)
        {
            return !Convert.ToBoolean(this.dist[x, z]);
        }

    }
}
