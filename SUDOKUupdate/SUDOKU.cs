using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SudokuGame
{
    class SUDOKU
    {
        public static byte countValid = 0;
        public static byte countTimes = 0;
        public static byte[,] receive = new byte[9, 9];
        public static byte[,] Rereceive = new byte[9, 9];
        public static byte[,] xValue = new byte[9, 9];
        public static byte[,] yValue = new byte[9, 9];
        public static byte[,] zValue = new byte[9, 9];
        public static byte[] gridrow = new byte[9];
        public static int sum;

        public static byte[] storageXLeft_2 = new byte[9];
        public static byte[] storageYTop_2 = new byte[9];

        public static byte[] storageXLeft_1 = new byte[9];
        public static byte[] storageYTop_1 = new byte[9];

        public static byte[] storageXLeft_0 = new byte[9];
        public static byte[] storageYTop_0 = new byte[9];

        public static byte[] storageXTop_2 = new byte[9];
        public static byte[] storageYRight_6 = new byte[9];

        public static byte[] storageXTop_1 = new byte[9];
        public static byte[] storageYRight_7 = new byte[9];

        public static byte[] storageXTop_0 = new byte[9];
        public static byte[] storageYRight_8 = new byte[9];

        public static byte[] storageXRight_6 = new byte[9];
        public static byte[] storageYBotton_6 = new byte[9];

        public static byte[] storageXRight_7 = new byte[9];
        public static byte[] storageYBotton_7 = new byte[9];

        public static byte[] storageXRight_8 = new byte[9];
        public static byte[] storageYBotton_8 = new byte[9];

        public static byte[] storageXBotton_2 = new byte[9];
        public static byte[] storageYLeft_6 = new byte[9];

        public static byte[] storageXBotton_1 = new byte[9];
        public static byte[] storageYLeft_7 = new byte[9];

        public static byte[] storageXBotton_0 = new byte[9];
        public static byte[] storageYLeft_8 = new byte[9];

        public static void LabelFalse(int x, int y)
        {
            xValue[x, receive[x, y] - 1] = receive[x, y];
            yValue[receive[x, y] - 1, y] = receive[x, y];
            zValue[(x / 3) * 3 + y / 3, receive[x, y] - 1] = receive[x, y];//00-0,01-1,02-2,10-2,11-3,12-5,20-6,21-7,22-8
        }
        public static void LabelCancel(int x, int y)
        {
            if (xValue[x, receive[x, y] - 1] == receive[x, y] || yValue[receive[x, y] - 1, y] == receive[x, y] || zValue[(x / 3) * 3 + y / 3, receive[x, y] - 1] == receive[x, y])
            {
                xValue[x, receive[x, y] - 1] = 0;
                yValue[receive[x, y] - 1, y] = 0;
                zValue[(x / 3) * 3 + y / 3, receive[x, y] - 1] = 0;
            }
        }
        /// <summary>
        /// 验证函数
        /// </summary>
        /// <param name="i">横坐标</param>
        /// <param name="j">纵坐标</param>
        /// <returns>ture or false</returns>
        public static bool IsValid(int i, int j)
        {
            byte n = receive[i, j];
            int[] query = new int[9] { 0, 0, 0, 3, 3, 3, 6, 6, 6 };
            int t, u;
            //每一行每一列是否重复
            for (t = 0; t < 9; t++)
            {
                if ((t != i && receive[t, j] == n) || (t != j && receive[i, t] == n))
                    return false;
            }
            //每个九宫格是否重复
            for (t = query[i]; t < query[i] + 3; t++)
            {
                for (u = query[j]; u < query[j] + 3; u++)
                {
                    if ((t != i || u != j) && receive[t, u] == n)
                        return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 求解函数
        /// </summary>
        /// <param name="n">初始值为0</param>
        public static void GetResult(int n)
        {
            while (n == 81)
            {
                for (int m = 0; m < 9; m++)
                {
                    for (int k = 0; k < 9; k++)
                    {
                        Rereceive[m, k] = receive[m, k];
                    }
                }
                countTimes++;
                return;
            }
            if (countTimes == 0)
            {
                int i = n / 9, j = n % 9;

                if (receive[i, j] != 0)
                {//如果当前格子不需要填数字，就跳到下一个格子                  
                    GetResult(n + 1);
                    return;
                }

                for (int k = 0; k < 9; k++)
                {
                    receive[i, j]++;//当前格子进行尝试所有解      

                    if (IsValid(i, j))
                    {
                        GetResult(n + 1);//验证通过，就继续下一个   

                    }
                }
                receive[i, j] = 0;//如果上面的单元无解，还原，就回溯           
                return;
            }
        }
        public static void ArrayClear()
        {
            Array.Clear(xValue, 0, xValue.Length);
            Array.Clear(yValue, 0, yValue.Length);
            Array.Clear(zValue, 0, zValue.Length);
        }
        public static int RemoveRowColumn()
        {
            for (int m = 0; m < 9; m++)
            {
                for (int n = 0; n < 9; n++)
                {
                    if (receive[m, n] == 0)
                    {
                        for (int temp = 0; temp < 9; temp++)
                        {
                            if (receive[m, temp] != 0 && temp != n)
                            {
                                gridrow[receive[m, temp] - 1] = receive[m, temp];
                            }
                            if (receive[temp, n] != 0 && temp != m)
                            {
                                gridrow[receive[temp, n] - 1] = receive[temp, n];
                            }
                        }
                        for (int i = 0; i < gridrow.Length; i++)
                        {
                            sum = sum + gridrow[i];
                        }
                        if (sum == 45)
                        {
                            return 0;
                        }
                        else
                        {
                            sum = 0;
                            Array.Clear(gridrow, 0, gridrow.Length);
                        }
                    }
                }
            }
            return 2;
        }
        public static int XRemoveGrid()
        {
            for (int m = 0; m < 9; m++)
            {
                for (int n = 0; n < 9; n++)
                {
                    if (receive[m, n] == 0)
                    {
                        if (n / 3 == 0)
                        {
                            if ((n == 0 && receive[m, n + 1] != 0 && receive[m, n + 2] != 0) || (n == 1 && receive[m, n - 1] != 0 && receive[m, n + 1] != 0) || (n == 2 && receive[m, n - 1] != 0 && receive[m, n - 2] != 0))
                            {
                                if (m / 3 == 0)
                                {
                                    if (m == 0)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[1, k] == receive[2, j] && receive[1, k] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[1, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[2, k] == receive[1, j] && receive[2, k] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[2, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (m == 1)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[0, k] == receive[2, j] && receive[0, k] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[0, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[2, k] == receive[0, j] && receive[2, k] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[2, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (m == 2)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[0, k] == receive[1, j] && receive[0, k] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[0, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[1, k] == receive[0, j] && receive[1, k] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[1, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (m / 3 == 1)
                                {
                                    if (m == 3)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[4, k] == receive[5, j] && receive[4, k] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[4, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[4, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[5, k] == receive[4, j] && receive[5, k] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[5, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[5, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (m == 4)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[3, k] == receive[5, j] && receive[3, k] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[3, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[3, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[5, k] == receive[3, j] && receive[5, k] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[5, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[5, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (m == 5)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[4, k] == receive[3, j] && receive[4, k] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[4, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[4, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[3, k] == receive[4, j] && receive[3, k] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[3, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[3, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (m / 3 == 2)
                                {
                                    if (m == 6)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[7, k] == receive[8, j] && receive[7, k] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[7, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[8, k] == receive[7, j] && receive[8, k] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[8, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (m == 7)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[6, k] == receive[8, j] && receive[6, k] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[6, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[8, k] == receive[6, j] && receive[8, k] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[8, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (m == 8)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[7, k] == receive[6, j] && receive[7, k] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[7, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[6, k] == receive[7, j] && receive[6, k] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[6, k])
                                                        {
                                                            if (n == 0 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 1 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 2 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (n / 3 == 1)//第二列
                        {
                            if ((n == 3 && receive[m, n + 1] != 0 && receive[m, n + 2] != 0) || (n == 4 && receive[m, n - 1] != 0 && receive[m, n + 1] != 0) || (n == 5 && receive[m, n - 1] != 0 && receive[m, n - 2] != 0))
                            {
                                if (m / 3 == 0)
                                {
                                    if (m == 0)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[1, k] == receive[2, j] && receive[1, k] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[1, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[2, k] == receive[1, j] && receive[2, k] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[2, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (m == 1)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[0, k] == receive[2, j] && receive[0, k] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[0, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[2, k] == receive[0, j] && receive[2, k] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[2, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (m == 2)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[0, k] == receive[1, j] && receive[0, k] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[0, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[1, k] == receive[0, j] && receive[1, k] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[1, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (m / 3 == 1)
                                {
                                    if (m == 3)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[4, k] == receive[5, j] && receive[4, k] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[4, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[4, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[5, k] == receive[4, j] && receive[5, k] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[5, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[5, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (m == 4)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[3, k] == receive[5, j] && receive[3, k] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[3, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[3, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[5, k] == receive[3, j] && receive[5, k] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[5, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[5, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (m == 5)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[4, k] == receive[3, j] && receive[4, k] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[4, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[4, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[3, k] == receive[4, j] && receive[3, k] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[3, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[3, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (m / 3 == 2)
                                {
                                    if (m == 6)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[7, k] == receive[8, j] && receive[7, k] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[7, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[8, k] == receive[7, j] && receive[8, k] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[8, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (m == 7)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[6, k] == receive[8, j] && receive[6, k] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[6, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[8, k] == receive[6, j] && receive[8, k] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[8, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (m == 8)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[7, k] == receive[6, j] && receive[7, k] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[7, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[6, k] == receive[7, j] && receive[6, k] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[6, k])
                                                        {
                                                            if (n == 3 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 4 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 5 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (n / 3 == 2)// 第3列
                        {
                            if ((n == 6 && receive[m, n + 1] != 0 && receive[m, n + 2] != 0) || (n == 7 && receive[m, n - 1] != 0 && receive[m, n + 1] != 0) || (n == 8 && receive[m, n - 1] != 0 && receive[m, n - 2] != 0))
                            {
                                if (m / 3 == 0)
                                {
                                    if (m == 0)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (receive[1, k] == receive[2, j] && receive[1, k] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[1, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[2, k] == receive[1, j] && receive[2, k] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[2, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (m == 1)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (receive[0, k] == receive[2, j] && receive[0, k] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[0, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[2, k] == receive[0, j] && receive[2, k] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[2, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (m == 2)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (receive[0, k] == receive[1, j] && receive[0, k] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[0, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[1, k] == receive[0, j] && receive[1, k] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[1, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (m / 3 == 1)
                                {
                                    if (m == 3)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (receive[4, k] == receive[5, j] && receive[4, k] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[4, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[4, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[5, k] == receive[4, j] && receive[5, k] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[5, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[5, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (m == 4)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (receive[3, k] == receive[5, j] && receive[3, k] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[3, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[3, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[5, k] == receive[3, j] && receive[5, k] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[5, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[5, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (m == 5)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (receive[4, k] == receive[3, j] && receive[4, k] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[4, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[4, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[3, k] == receive[4, j] && receive[3, k] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[3, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[3, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (m / 3 == 2)
                                {
                                    if (m == 6)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (receive[7, k] == receive[8, j] && receive[7, k] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[7, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[8, k] == receive[7, j] && receive[8, k] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[8, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (m == 7)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (receive[6, k] == receive[8, j] && receive[6, k] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[6, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[8, k] == receive[6, j] && receive[8, k] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[8, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (m == 8)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (receive[7, k] == receive[6, j] && receive[7, k] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[7, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[6, k] == receive[7, j] && receive[6, k] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[l, n] != 0 && receive[l, n] == receive[6, k])
                                                        {
                                                            if (n == 6 && receive[l, n] != receive[m, n + 1] && receive[l, n] != receive[m, n + 2])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 7 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n + 1])
                                                            {
                                                                return 0;
                                                            }
                                                            if (n == 8 && receive[l, n] != receive[m, n - 1] && receive[l, n] != receive[m, n - 2])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return -1;
            //zValue[(x / 3) * 3 + y / 3, Function.receive[x, y] - 1] = Function.receive[x, y];
        }
        public static int YRemoveGrid()
        {
            for (int n = 0; n < 9; n++)
            {
                for (int m = 0; m < 9; m++)
                {
                    if (receive[m, n] == 0)
                    {
                        if (m / 3 == 0)
                        {
                            if ((m == 0 && receive[m + 1, n] != 0 && receive[m + 2, n] != 0) || (m == 1 && receive[m - 1, n] != 0 && receive[m + 1, n] != 0) || (m == 2 && receive[m - 1, n] != 0 && receive[m - 2, n] != 0))
                            {
                                if (n / 3 == 0)
                                {
                                    if (n == 0)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[k, 1] == receive[j, 2] && receive[k, 1] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[l, m] == receive[k, 1])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 2] == receive[j, 1] && receive[k, 2] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 2])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (n == 1)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[k, 0] == receive[j, 2] && receive[k, 0] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 0])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 2] == receive[j, 0] && receive[k, 2] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 2])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (n == 2)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[k, 0] == receive[j, 1] && receive[k, 0] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 0])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 1] == receive[j, 0] && receive[k, 1] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 1])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n / 3 == 1)
                                {
                                    if (n == 3)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[k, 4] == receive[j, 5] && receive[k, 4] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 4])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 4])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 5] == receive[j, 4] && receive[k, 5] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 5])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 5])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (n == 4)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[k, 3] == receive[j, 5] && receive[k, 3] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 3])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 3])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 5] == receive[j, 3] && receive[k, 5] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 5])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 5])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (n == 5)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[k, 4] == receive[j, 3] && receive[k, 4] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 4])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 4])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 3] == receive[j, 4] && receive[k, 3] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 3])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 3])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n / 3 == 2)
                                {
                                    if (n == 6)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[k, 7] == receive[j, 8] && receive[k, 7] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 7])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 8] == receive[j, 7] && receive[k, 8] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 8])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (n == 7)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[k, 6] == receive[j, 8] && receive[k, 6] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 6])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 8] == receive[j, 6] && receive[k, 8] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 8])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (n == 8)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[k, 7] == receive[j, 6] && receive[k, 7] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 7])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 6] == receive[j, 7] && receive[k, 6] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 6])
                                                        {
                                                            if (m == 0 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 1 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 2 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (m / 3 == 1)//第二行
                        {
                            if ((m == 3 && receive[m + 1, n] != 0 && receive[m + 2, n] != 0) || (m == 4 && receive[m - 1, n] != 0 && receive[m + 1, n] != 0) || (m == 5 && receive[m - 1, n] != 0 && receive[m - 2, n] != 0))
                            {
                                if (n / 3 == 0)
                                {
                                    if (n == 0)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[k, 1] == receive[j, 2] && receive[k, 1] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 1])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 2] == receive[j, 1] && receive[k, 2] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 2])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (n == 1)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[k, 0] == receive[j, 2] && receive[k, 0] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 0])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 2] == receive[j, 0] && receive[k, 2] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 2])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (n == 2)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[k, 0] == receive[j, 1] && receive[k, 0] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 0])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 1] == receive[j, 0] && receive[k, 1] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 1])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n / 3 == 1)
                                {
                                    if (n == 3)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[k, 4] == receive[j, 5] && receive[k, 4] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 4])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 4])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 5] == receive[j, 4] && receive[k, 5] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 5])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 5])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (n == 4)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[k, 3] == receive[j, 5] && receive[k, 3] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 3])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 3])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 5] == receive[j, 3] && receive[k, 5] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 5])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 5])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (n == 5)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[k, 4] == receive[j, 3] && receive[k, 4] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 4])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 4])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 3] == receive[j, 4] && receive[k, 3] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 3])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 3])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n / 3 == 2)
                                {
                                    if (n == 6)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[k, 7] == receive[j, 8] && receive[k, 7] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 7])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 8] == receive[j, 7] && receive[k, 8] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 8])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (n == 7)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[k, 6] == receive[j, 8] && receive[k, 6] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 6])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 8] == receive[j, 6] && receive[k, 8] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 8])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (n == 8)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (receive[k, 7] == receive[j, 6] && receive[k, 7] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 7])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 6] == receive[j, 7] && receive[k, 6] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 6])
                                                        {
                                                            if (m == 3 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 4 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 5 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (m / 3 == 2)// 第3行
                        {
                            if ((m == 6 && receive[m + 1, n] != 0 && receive[m + 2, n] != 0) || (m == 7 && receive[m - 1, n] != 0 && receive[m + 1, n] != 0) || (m == 8 && receive[m - 1, n] != 0 && receive[m - 2, n] != 0))
                            {
                                if (n / 3 == 0)
                                {
                                    if (n == 0)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (receive[k, 1] == receive[j, 2] && receive[k, 1] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 1])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 2] == receive[j, 1] && receive[k, 2] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 2])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (n == 1)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (receive[k, 0] == receive[j, 2] && receive[k, 0] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 0])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 2] == receive[j, 0] && receive[k, 2] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 2])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (n == 2)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (receive[k, 0] == receive[j, 1] && receive[k, 0] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 0])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 1] == receive[j, 0] && receive[k, 1] != 0)
                                                {
                                                    for (int l = 3; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 1])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n / 3 == 1)
                                {
                                    if (n == 3)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (receive[k, 4] == receive[j, 5] && receive[k, 4] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 4])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 4])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 5] == receive[j, 4] && receive[k, 5] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 5])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 5])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (n == 4)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (receive[k, 3] == receive[j, 5] && receive[k, 3] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 3])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 3])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 5] == receive[j, 3] && receive[k, 5] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 5])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 5])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (n == 5)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (receive[k, 4] == receive[j, 3] && receive[k, 4] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 4])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 4])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 3] == receive[j, 4] && receive[k, 3] != 0)
                                                {
                                                    for (int l = 0; l < 3; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 3])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                    for (int l = 6; l < 9; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 3])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n / 3 == 2)
                                {
                                    if (n == 6)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (receive[k, 7] == receive[j, 8] && receive[k, 7] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 7])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 8] == receive[j, 7] && receive[k, 8] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 8])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (n == 7)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (receive[k, 6] == receive[j, 8] && receive[k, 6] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 6])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 8] == receive[j, 6] && receive[k, 8] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 8])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (n == 8)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (receive[k, 7] == receive[j, 6] && receive[k, 7] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 7])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (receive[k, 6] == receive[j, 7] && receive[k, 6] != 0)
                                                {
                                                    for (int l = 0; l < 6; l++)
                                                    {
                                                        if (receive[m, l] != 0 && receive[m, l] == receive[k, 6])
                                                        {
                                                            if (m == 6 && receive[m, l] != receive[m + 1, n] && receive[m, l] != receive[m + 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 7 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m + 1, n])
                                                            {
                                                                return 0;
                                                            }
                                                            if (m == 8 && receive[m, l] != receive[m - 1, n] && receive[m, l] != receive[m - 2, n])
                                                            {
                                                                return 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return -1;
        }
        public static int FiveNumble1()
        {
            for (int m = 3; m < 9; m++)
            {
                if (receive[2, m] != 0)
                {
                    storageXLeft_2[receive[2, m] - 1] = receive[2, m];
                }
                if (receive[m, 2] != 0)
                {
                    storageYTop_2[receive[m, 2] - 1] = receive[m, 2];
                }
            }
            for (int n = 0; n < 9; n++)
            {
                if (storageXLeft_2[n] == storageYTop_2[n] && storageXLeft_2[n] != 0 && storageYTop_2[n] != 0)
                {
                    countValid++;
                }
                while (n == 8 && countValid < 5)
                {
                    countValid = 0;
                    Array.Clear(storageXLeft_2, 0, 9);
                    Array.Clear(storageYTop_2, 0, 9);
                    return -1;
                }
                while (countValid == 5)
                {
                    countValid = 0;
                    Array.Clear(storageXLeft_2, 0, 9);
                    Array.Clear(storageYTop_2, 0, 9);
                    return 0;
                }
            }
            return 2;
        }
        public static int FiveNumble2()
        {
            for (int m = 3; m < 9; m++)
            {
                if (receive[1, m] != 0)
                {
                    storageXLeft_1[receive[1, m] - 1] = receive[1, m];
                }
                if (receive[m, 1] != 0)
                {
                    storageYTop_1[receive[m, 1] - 1] = receive[m, 1];
                }
            }
            for (int n = 0; n < 9; n++)
            {
                if (storageXLeft_1[n] == storageYTop_1[n] && storageXLeft_1[n] != 0 && storageYTop_1[n] != 0)
                {
                    countValid++;
                }
                while (n == 8 && countValid < 5)
                {
                    countValid = 0;
                    Array.Clear(storageXLeft_1, 0, 9);
                    Array.Clear(storageYTop_1, 0, 9);
                    return -1;
                }
                while (countValid == 5)
                {
                    countValid = 0;
                    Array.Clear(storageXLeft_1, 0, 9);
                    Array.Clear(storageYTop_1, 0, 9);
                    return 0;
                }
            }
            return 2;
        }
        public static int FiveNumble3()
        {
            for (int m = 3; m < 9; m++)
            {
                if (receive[0, m] != 0)
                {
                    storageXLeft_0[receive[0, m] - 1] = receive[0, m];
                }
                if (receive[m, 0] != 0)
                {
                    storageYTop_0[receive[m, 0] - 1] = receive[m, 0];
                }
            }
            for (int n = 0; n < 9; n++)
            {
                if (storageXLeft_0[n] == storageYTop_0[n] && storageXLeft_0[n] != 0 && storageYTop_0[n] != 0)
                {
                    countValid++;
                }
                while (n == 8 && countValid < 5)
                {
                    countValid = 0;
                    Array.Clear(storageXLeft_0, 0, 9);
                    Array.Clear(storageYTop_0, 0, 9);
                    return -1;
                }
                while (countValid == 5)
                {
                    countValid = 0;
                    Array.Clear(storageXLeft_0, 0, 9);
                    Array.Clear(storageYTop_0, 0, 9);
                    return 0;
                }
            }
            return 2;
        }
        public static int FiveNumble4()
        {
            for (int m = 0; m < 6; m++)
            {
                if (receive[2, m] != 0)
                {
                    storageXTop_2[receive[2, m] - 1] = receive[2, m];
                }
            }
            for (int m = 3; m < 9; m++)
            {
                if (receive[m, 6] != 0)
                {
                    storageYRight_6[receive[m, 6] - 1] = receive[m, 6];
                }
            }
            for (int n = 0; n < 9; n++)
            {
                if (storageXTop_2[n] == storageYRight_6[n] && storageXTop_2[n] != 0 && storageYRight_6[n] != 0)
                {
                    countValid++;
                }
                while (n == 8 && countValid < 5)
                {
                    countValid = 0;
                    Array.Clear(storageXTop_2, 0, 9);
                    Array.Clear(storageYRight_6, 0, 9);
                    return -1;
                }
                while (countValid == 5)
                {
                    countValid = 0;
                    Array.Clear(storageXTop_2, 0, 9);
                    Array.Clear(storageYRight_6, 0, 9);
                    return 0;
                }
            }
            return 2;
        }
        public static int FiveNumble5()
        {
            for (int m = 0; m < 6; m++)
            {
                if (receive[1, m] != 0)
                {
                    storageXTop_1[receive[1, m] - 1] = receive[1, m];
                }
            }
            for (int m = 3; m < 9; m++)
            {
                if (receive[m, 7] != 0)
                {
                    storageYRight_7[receive[m, 7] - 1] = receive[m, 7];
                }
            }
            for (int n = 0; n < 9; n++)
            {
                if (storageXTop_1[n] == storageYRight_7[n] && storageXTop_1[n] != 0 && storageYRight_7[n] != 0)
                {
                    countValid++;
                }
                while (n == 8 && countValid < 5)
                {
                    countValid = 0;
                    Array.Clear(storageXTop_1, 0, 9);
                    Array.Clear(storageYRight_7, 0, 9);
                    return -1;
                }
                while (countValid == 5)
                {
                    countValid = 0;
                    Array.Clear(storageXTop_1, 0, 9);
                    Array.Clear(storageYRight_7, 0, 9);
                    return 0;
                }
            }
            return 2;
        }
        public static int FiveNumble6()
        {
            for (int m = 0; m < 6; m++)
            {
                if (receive[0, m] != 0)
                {
                    storageXTop_0[receive[0, m] - 1] = receive[0, m];
                }
            }
            for (int m = 3; m < 9; m++)
            {
                if (receive[m, 8] != 0)
                {
                    storageYRight_8[receive[m, 8] - 1] = receive[m, 8];
                }
            }
            for (int n = 0; n < 9; n++)
            {
                if (storageXTop_0[n] == storageYRight_8[n] && storageXTop_0[n] != 0 && storageYRight_8[n] != 0)
                {
                    countValid++;
                }
                while (n == 8 && countValid < 5)
                {
                    countValid = 0;
                    Array.Clear(storageXTop_0, 0, 9);
                    Array.Clear(storageYRight_8, 0, 9);
                    return -1;
                }
                while (countValid == 5)
                {
                    countValid = 0;
                    Array.Clear(storageXTop_0, 0, 9);
                    Array.Clear(storageYRight_8, 0, 9);
                    return 0;
                }
            }
            return 2;
        }
        public static int FiveNumble7()
        {
            for (int m = 0; m < 6; m++)
            {
                if (receive[6, m] != 0)
                {
                    storageXRight_6[receive[6, m] - 1] = receive[6, m];
                }
                if (receive[m, 6] != 0)
                {
                    storageYBotton_6[receive[m, 6] - 1] = receive[m, 6];
                }
            }
            for (int n = 0; n < 9; n++)
            {
                if (storageXRight_6[n] == storageYBotton_6[n] && storageXRight_6[n] != 0 && storageYBotton_6[n] != 0)
                {
                    countValid++;
                }
                while (n == 8 && countValid < 5)
                {
                    countValid = 0;
                    Array.Clear(storageXRight_6, 0, 9);
                    Array.Clear(storageYBotton_6, 0, 9);
                    return -1;
                }
                while (countValid == 5)
                {
                    countValid = 0;
                    Array.Clear(storageXRight_6, 0, 9);
                    Array.Clear(storageYBotton_6, 0, 9);
                    return 0;
                }
            }
            return 2;
        }
        public static int FiveNumble8()
        {
            for (int m = 0; m < 6; m++)
            {
                if (receive[7, m] != 0)
                {
                    storageXRight_7[receive[7, m] - 1] = receive[7, m];
                }
                if (receive[m, 7] != 0)
                {
                    storageYBotton_7[receive[m, 7] - 1] = receive[m, 7];
                }
            }
            for (int n = 0; n < 9; n++)
            {
                if (storageXRight_7[n] == storageYBotton_7[n] && storageXRight_7[n] != 0 && storageYBotton_7[n] != 0)
                {
                    countValid++;
                }
                while (n == 8 && countValid < 5)
                {
                    countValid = 0;
                    Array.Clear(storageXRight_7, 0, 9);
                    Array.Clear(storageYBotton_7, 0, 9);
                    return -1;
                }
                while (countValid == 5)
                {
                    countValid = 0;
                    Array.Clear(storageXRight_7, 0, 9);
                    Array.Clear(storageYBotton_7, 0, 9);
                    return 0;
                }
            }
            return 2;
        }
        public static int FiveNumble9()
        {
            for (int m = 0; m < 6; m++)
            {
                if (receive[8, m] != 0)
                {
                    storageXRight_8[receive[8, m] - 1] = receive[8, m];
                }
                if (receive[m, 8] != 0)
                {
                    storageYBotton_8[receive[m, 8] - 1] = receive[m, 8];
                }
            }
            for (int n = 0; n < 9; n++)
            {
                if (storageXRight_8[n] == storageYBotton_8[n] && storageXRight_8[n] != 0 && storageYBotton_8[n] != 0)
                {
                    countValid++;
                }
                while (n == 8 && countValid < 5)
                {
                    countValid = 0;
                    Array.Clear(storageXRight_8, 0, 9);
                    Array.Clear(storageYBotton_8, 0, 9);
                    return -1;
                }
                while (countValid == 5)
                {
                    countValid = 0;
                    Array.Clear(storageXRight_8, 0, 9);
                    Array.Clear(storageYBotton_8, 0, 9);
                    return 0;
                }
            }
            return 2;
        }
        public static int FiveNumble10()
        {
            for (int m = 0; m < 6; m++)
            {
                if (receive[m, 2] != 0)
                {
                    storageXBotton_2[receive[m, 2] - 1] = receive[m, 2];
                }
            }
            for (int m = 3; m < 9; m++)
            {
                if (receive[6, m] != 0)
                {
                    storageYLeft_6[receive[6, m] - 1] = receive[6, m];
                }
            }
            for (int n = 0; n < 9; n++)
            {
                if (storageXBotton_2[n] == storageYLeft_6[n] && storageXBotton_2[n] != 0 && storageYLeft_6[n] != 0)
                {
                    countValid++;
                }
                while (n == 8 && countValid < 5)
                {
                    countValid = 0;
                    Array.Clear(storageXBotton_2, 0, 9);
                    Array.Clear(storageYLeft_6, 0, 9);
                    return -1;
                }
                while (countValid == 5)
                {
                    countValid = 0;
                    Array.Clear(storageXBotton_2, 0, 9);
                    Array.Clear(storageYLeft_6, 0, 9);
                    return 0;
                }
            }
            return 2;
        }
        public static int FiveNumble11()
        {

            for (int m = 0; m < 6; m++)
            {
                if (receive[m, 1] != 0)
                {
                    storageXBotton_1[receive[m, 1] - 1] = receive[m, 1];
                }
            }
            for (int m = 3; m < 9; m++)
            {
                if (receive[7, m] != 0)
                {
                    storageYLeft_7[receive[7, m] - 1] = receive[7, m];
                }
            }
            for (int n = 0; n < 9; n++)
            {
                if (storageXBotton_1[n] == storageYLeft_7[n] && storageXBotton_1[n] != 0 && storageYLeft_7[n] != 0)
                {
                    countValid++;
                }
                while (n == 8 && countValid < 5)
                {
                    countValid = 0;
                    Array.Clear(storageXBotton_1, 0, 9);
                    Array.Clear(storageYLeft_7, 0, 9);
                    return -1;
                }
                while (countValid == 5)
                {
                    countValid = 0;
                    Array.Clear(storageXBotton_1, 0, 9);
                    Array.Clear(storageYLeft_7, 0, 9);
                    return 0;
                }
            }
            return 2;
        }
        public static int FiveNumble12()
        {
            for (int m = 0; m < 6; m++)
            {
                if (receive[m, 0] != 0)
                {
                    storageXBotton_0[receive[m, 0] - 1] = receive[m, 0];
                }
            }
            for (int m = 3; m < 9; m++)
            {
                if (receive[8, m] != 0)
                {
                    storageYLeft_8[receive[8, m] - 1] = receive[8, m];
                }
            }
            for (int n = 0; n < 9; n++)
            {
                if (storageXBotton_0[n] == storageYLeft_8[n] && storageXBotton_0[n] != 0 && storageYLeft_8[n] != 0)
                {
                    countValid++;
                }
                while (n == 8 && countValid < 5)
                {
                    countValid = 0;
                    Array.Clear(storageXBotton_0, 0, 9);
                    Array.Clear(storageYLeft_8, 0, 9);
                    return -1;
                }
                while (countValid == 5)
                {
                    countValid = 0;
                    Array.Clear(storageXBotton_0, 0, 9);
                    Array.Clear(storageYLeft_8, 0, 9);
                    return 0;
                }
            }
            return 2;
        }
        public static int XNineGrid()
        {
            for (int m = 0; m < 9; m++)
            {
                for (int n = 0; n < 9; n++)
                {
                    if (receive[m, n] != 0)
                    {
                        if (n / 3 == 0)
                        {
                            if ((n == 0 && receive[m, n + 1] != 0 && receive[m, n + 2] != 0) || (n == 1 && receive[m, n - 1] != 0 && receive[m, n + 1] != 0) || (n == 2 && receive[m, n - 1] != 0 && receive[m, n - 2] != 0))
                            {
                                if (m / 3 == 0)
                                {
                                    if (m == 0)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (n == 0 && ((receive[1, k] == receive[2, j] && receive[1, k] != receive[m, n] && receive[1, k] != receive[m, n + 1] && receive[1, k] != receive[m, n + 2]) && receive[1, k] != 0 || (receive[2, k] == receive[1, j] && receive[2, k] != receive[m, n] && receive[2, k] != receive[m, n + 1] && receive[2, k] != receive[m, n + 2]) && receive[2, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 1 && ((receive[1, k] == receive[2, j] && receive[1, k] != receive[m, n] && receive[1, k] != receive[m, n + 1] && receive[1, k] != receive[m, n - 1]) && receive[1, k] != 0 || (receive[2, k] == receive[1, j] && receive[2, k] != receive[m, n] && receive[2, k] != receive[m, n + 1] && receive[2, k] != receive[m, n - 1]) && receive[2, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 2 && ((receive[1, k] == receive[2, j] && receive[1, k] != receive[m, n] && receive[1, k] != receive[m, n - 2] && receive[1, k] != receive[m, n - 1]) && receive[1, k] != 0 || (receive[2, k] == receive[1, j] && receive[2, k] != receive[m, n] && receive[2, k] != receive[m, n - 2] && receive[2, k] != receive[m, n - 1]) && receive[2, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (m == 1)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (n == 0 && ((receive[0, k] == receive[2, j] && receive[0, k] != receive[m, n] && receive[0, k] != receive[m, n + 1] && receive[0, k] != receive[m, n + 2]) && receive[0, k] != 0 || (receive[2, k] == receive[0, j] && receive[2, k] != receive[m, n] && receive[2, k] != receive[m, n + 1] && receive[2, k] != receive[m, n + 2]) && receive[2, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 1 && ((receive[0, k] == receive[2, j] && receive[0, k] != receive[m, n] && receive[0, k] != receive[m, n + 1] && receive[0, k] != receive[m, n - 1]) && receive[0, k] != 0 || (receive[2, k] == receive[0, j] && receive[2, k] != receive[m, n] && receive[2, k] != receive[m, n + 1] && receive[2, k] != receive[m, n - 1]) && receive[2, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 2 && ((receive[0, k] == receive[2, j] && receive[0, k] != receive[m, n] && receive[0, k] != receive[m, n - 2] && receive[0, k] != receive[m, n - 1]) && receive[0, k] != 0 || (receive[2, k] == receive[0, j] && receive[2, k] != receive[m, n] && receive[2, k] != receive[m, n - 2] && receive[2, k] != receive[m, n - 1]) && receive[2, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (m == 2)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (n == 0 && ((receive[0, k] == receive[1, j] && receive[0, k] != receive[m, n] && receive[0, k] != receive[m, n + 1] && receive[0, k] != receive[m, n + 2]) && receive[0, k] != 0 || (receive[1, k] == receive[0, j] && receive[1, k] != receive[m, n] && receive[1, k] != receive[m, n + 1] && receive[1, k] != receive[m, n + 2]) && receive[1, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 1 && ((receive[0, k] == receive[1, j] && receive[0, k] != receive[m, n] && receive[0, k] != receive[m, n + 1] && receive[0, k] != receive[m, n - 1]) && receive[0, k] != 0 || (receive[1, k] == receive[0, j] && receive[1, k] != receive[m, n] && receive[1, k] != receive[m, n + 1] && receive[1, k] != receive[m, n - 1]) && receive[1, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 2 && ((receive[0, k] == receive[1, j] && receive[0, k] != receive[m, n] && receive[0, k] != receive[m, n - 2] && receive[0, k] != receive[m, n - 1]) && receive[0, k] != 0 || (receive[1, k] == receive[0, j] && receive[1, k] != receive[m, n] && receive[1, k] != receive[m, n - 2] && receive[1, k] != receive[m, n - 1]) && receive[1, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (m / 3 == 1)
                                {
                                    if (m == 3)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)//wwwww
                                            {
                                                if (n == 0 && ((receive[4, k] == receive[5, j] && receive[4, k] != receive[m, n] && receive[4, k] != receive[m, n + 1] && receive[4, k] != receive[m, n + 2]) && receive[4, k] != 0 || (receive[5, k] == receive[4, j] && receive[5, k] != receive[m, n] && receive[5, k] != receive[m, n + 1] && receive[5, k] != receive[m, n + 2]) && receive[5, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 1 && ((receive[4, k] == receive[5, j] && receive[4, k] != receive[m, n] && receive[4, k] != receive[m, n + 1] && receive[4, k] != receive[m, n - 1]) && receive[4, k] != 0 || (receive[5, k] == receive[4, j] && receive[5, k] != receive[m, n] && receive[5, k] != receive[m, n + 1] && receive[5, k] != receive[m, n - 1]) && receive[5, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 2 && ((receive[4, k] == receive[5, j] && receive[4, k] != receive[m, n] && receive[4, k] != receive[m, n - 2] && receive[4, k] != receive[m, n - 1]) && receive[4, k] != 0 || (receive[5, k] == receive[4, j] && receive[5, k] != receive[m, n] && receive[5, k] != receive[m, n - 2] && receive[5, k] != receive[m, n - 1]) && receive[5, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (m == 4)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (n == 0 && ((receive[3, k] == receive[5, j] && receive[3, k] != receive[m, n] && receive[3, k] != receive[m, n + 1] && receive[3, k] != receive[m, n + 2]) && receive[3, k] != 0 || (receive[5, k] == receive[3, j] && receive[5, k] != receive[m, n] && receive[5, k] != receive[m, n + 1] && receive[5, k] != receive[m, n + 2]) && receive[5, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 1 && ((receive[3, k] == receive[5, j] && receive[3, k] != receive[m, n] && receive[3, k] != receive[m, n + 1] && receive[3, k] != receive[m, n - 1]) && receive[3, k] != 0 || (receive[5, k] == receive[3, j] && receive[5, k] != receive[m, n] && receive[5, k] != receive[m, n + 1] && receive[5, k] != receive[m, n - 1]) && receive[5, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 2 && ((receive[3, k] == receive[5, j] && receive[3, k] != receive[m, n] && receive[3, k] != receive[m, n - 2] && receive[3, k] != receive[m, n - 1]) && receive[3, k] != 0 || (receive[5, k] == receive[3, j] && receive[5, k] != receive[m, n] && receive[5, k] != receive[m, n - 2] && receive[5, k] != receive[m, n - 1]) && receive[5, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (m == 5)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (n == 0 && ((receive[3, k] == receive[4, j] && receive[3, k] != receive[m, n] && receive[3, k] != receive[m, n + 1] && receive[3, k] != receive[m, n + 2]) && receive[3, k] != 0 || (receive[4, k] == receive[3, j] && receive[4, k] != receive[m, n] && receive[4, k] != receive[m, n + 1] && receive[4, k] != receive[m, n + 2]) && receive[4, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 1 && ((receive[3, k] == receive[4, j] && receive[3, k] != receive[m, n] && receive[3, k] != receive[m, n + 1] && receive[3, k] != receive[m, n - 1]) && receive[3, k] != 0 || (receive[4, k] == receive[3, j] && receive[4, k] != receive[m, n] && receive[4, k] != receive[m, n + 1] && receive[4, k] != receive[m, n - 1]) && receive[4, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 2 && ((receive[3, k] == receive[4, j] && receive[3, k] != receive[m, n] && receive[3, k] != receive[m, n - 2] && receive[3, k] != receive[m, n - 1]) && receive[3, k] != 0 || (receive[4, k] == receive[3, j] && receive[4, k] != receive[m, n] && receive[4, k] != receive[m, n - 2] && receive[4, k] != receive[m, n - 1]) && receive[4, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (m / 3 == 2)
                                {
                                    if (m == 6)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (n == 0 && ((receive[7, k] == receive[8, j] && receive[7, k] != receive[m, n] && receive[7, k] != receive[m, n + 1] && receive[7, k] != receive[m, n + 2]) && receive[7, k] != 0 || (receive[8, k] == receive[7, j] && receive[8, k] != receive[m, n] && receive[8, k] != receive[m, n + 1] && receive[8, k] != receive[m, n + 2]) && receive[8, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 1 && ((receive[7, k] == receive[8, j] && receive[7, k] != receive[m, n] && receive[7, k] != receive[m, n + 1] && receive[7, k] != receive[m, n - 1]) && receive[7, k] != 0 || (receive[8, k] == receive[7, j] && receive[8, k] != receive[m, n] && receive[8, k] != receive[m, n + 1] && receive[8, k] != receive[m, n - 1]) && receive[8, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 2 && ((receive[7, k] == receive[8, j] && receive[7, k] != receive[m, n] && receive[7, k] != receive[m, n - 2] && receive[7, k] != receive[m, n - 1]) && receive[7, k] != 0 || (receive[8, k] == receive[7, j] && receive[8, k] != receive[m, n] && receive[8, k] != receive[m, n - 2] && receive[8, k] != receive[m, n - 1]) && receive[8, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (m == 7)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (n == 0 && ((receive[6, k] == receive[8, j] && receive[6, k] != receive[m, n] && receive[6, k] != receive[m, n + 1] && receive[6, k] != receive[m, n + 2]) && receive[6, k] != 0 || (receive[8, k] == receive[6, j] && receive[8, k] != receive[m, n] && receive[8, k] != receive[m, n + 1] && receive[8, k] != receive[m, n + 2]) && receive[8, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 1 && ((receive[6, k] == receive[8, j] && receive[6, k] != receive[m, n] && receive[6, k] != receive[m, n + 1] && receive[6, k] != receive[m, n - 1]) && receive[6, k] != 0 || (receive[8, k] == receive[6, j] && receive[8, k] != receive[m, n] && receive[8, k] != receive[m, n + 1] && receive[8, k] != receive[m, n - 1]) && receive[8, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 2 && ((receive[6, k] == receive[8, j] && receive[6, k] != receive[m, n] && receive[6, k] != receive[m, n - 2] && receive[6, k] != receive[m, n - 1]) && receive[6, k] != 0 || (receive[8, k] == receive[6, j] && receive[8, k] != receive[m, n] && receive[8, k] != receive[m, n - 2] && receive[8, k] != receive[m, n - 1]) && receive[8, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (m == 8)    //12333333333333333333333333333333333333333333333333333333333333333333333333333333333
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (n == 0 && ((receive[7, k] == receive[6, j] && receive[7, k] != receive[m, n] && receive[7, k] != receive[m, n + 1] && receive[7, k] != receive[m, n + 2]) && receive[7, k] != 0 || (receive[6, k] == receive[7, j] && receive[6, k] != receive[m, n] && receive[6, k] != receive[m, n + 1] && receive[6, k] != receive[m, n + 2]) && receive[6, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 1 && ((receive[7, k] == receive[6, j] && receive[7, k] != receive[m, n] && receive[7, k] != receive[m, n + 1] && receive[7, k] != receive[m, n - 1]) && receive[7, k] != 0 || (receive[6, k] == receive[7, j] && receive[6, k] != receive[m, n] && receive[6, k] != receive[m, n + 1] && receive[6, k] != receive[m, n - 1]) && receive[6, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 2 && ((receive[7, k] == receive[6, j] && receive[7, k] != receive[m, n] && receive[7, k] != receive[m, n - 2] && receive[7, k] != receive[m, n - 1]) && receive[7, k] != 0 || (receive[6, k] == receive[7, j] && receive[6, k] != receive[m, n] && receive[6, k] != receive[m, n - 2] && receive[6, k] != receive[m, n - 1]) && receive[6, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (n / 3 == 1)
                        {
                            if ((n == 3 && receive[m, n + 1] != 0 && receive[m, n + 2] != 0) || (n == 4 && receive[m, n - 1] != 0 && receive[m, n + 1] != 0) || (n == 5 && receive[m, n - 1] != 0 && receive[m, n - 2] != 0))
                            {
                                if (m / 3 == 0)
                                {
                                    if (m == 0)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (n == 3 && ((receive[1, k] == receive[2, j] && receive[1, k] != receive[m, n] && receive[1, k] != receive[m, n + 1] && receive[1, k] != receive[m, n + 2]) && receive[1, k] != 0 || (receive[2, k] == receive[1, j] && receive[2, k] != receive[m, n] && receive[2, k] != receive[m, n + 1] && receive[2, k] != receive[m, n + 2]) && receive[2, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 4 && ((receive[1, k] == receive[2, j] && receive[1, k] != receive[m, n] && receive[1, k] != receive[m, n + 1] && receive[1, k] != receive[m, n - 1]) && receive[1, k] != 0 || (receive[2, k] == receive[1, j] && receive[2, k] != receive[m, n] && receive[2, k] != receive[m, n + 1] && receive[2, k] != receive[m, n - 1]) && receive[2, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 5 && ((receive[1, k] == receive[2, j] && receive[1, k] != receive[m, n] && receive[1, k] != receive[m, n - 2] && receive[1, k] != receive[m, n - 1]) && receive[1, k] != 0 || (receive[2, k] == receive[1, j] && receive[2, k] != receive[m, n] && receive[2, k] != receive[m, n - 2] && receive[2, k] != receive[m, n - 1]) && receive[2, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (m == 1)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (n == 3 && ((receive[0, k] == receive[2, j] && receive[0, k] != receive[m, n] && receive[0, k] != receive[m, n + 1] && receive[0, k] != receive[m, n + 2]) && receive[0, k] != 0 || (receive[2, k] == receive[0, j] && receive[2, k] != receive[m, n] && receive[2, k] != receive[m, n + 1] && receive[2, k] != receive[m, n + 2]) && receive[2, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 4 && ((receive[0, k] == receive[2, j] && receive[0, k] != receive[m, n] && receive[0, k] != receive[m, n + 1] && receive[0, k] != receive[m, n - 1]) && receive[0, k] != 0 || (receive[2, k] == receive[0, j] && receive[2, k] != receive[m, n] && receive[2, k] != receive[m, n + 1] && receive[2, k] != receive[m, n - 1]) && receive[2, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 5 && ((receive[0, k] == receive[2, j] && receive[0, k] != receive[m, n] && receive[0, k] != receive[m, n - 2] && receive[0, k] != receive[m, n - 1]) && receive[0, k] != 0 || (receive[2, k] == receive[0, j] && receive[2, k] != receive[m, n] && receive[2, k] != receive[m, n - 2] && receive[2, k] != receive[m, n - 1]) && receive[2, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (m == 2)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (n == 3 && ((receive[0, k] == receive[1, j] && receive[0, k] != receive[m, n] && receive[0, k] != receive[m, n + 1] && receive[0, k] != receive[m, n + 2]) && receive[0, k] != 0 || (receive[1, k] == receive[0, j] && receive[1, k] != receive[m, n] && receive[1, k] != receive[m, n + 1] && receive[1, k] != receive[m, n + 2]) && receive[1, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 4 && ((receive[0, k] == receive[1, j] && receive[0, k] != receive[m, n] && receive[0, k] != receive[m, n + 1] && receive[0, k] != receive[m, n - 1]) && receive[0, k] != 0 || (receive[1, k] == receive[0, j] && receive[1, k] != receive[m, n] && receive[1, k] != receive[m, n + 1] && receive[1, k] != receive[m, n - 1]) && receive[1, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 5 && ((receive[0, k] == receive[1, j] && receive[0, k] != receive[m, n] && receive[0, k] != receive[m, n - 2] && receive[0, k] != receive[m, n - 1]) && receive[0, k] != 0 || (receive[1, k] == receive[0, j] && receive[1, k] != receive[m, n] && receive[1, k] != receive[m, n - 2] && receive[1, k] != receive[m, n - 1]) && receive[1, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (m / 3 == 1)
                                {
                                    if (m == 3)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)//wwwww
                                            {
                                                if (n == 3 && ((receive[4, k] == receive[5, j] && receive[4, k] != receive[m, n] && receive[4, k] != receive[m, n + 1] && receive[4, k] != receive[m, n + 2]) && receive[4, k] != 0 || (receive[5, k] == receive[4, j] && receive[5, k] != receive[m, n] && receive[5, k] != receive[m, n + 1] && receive[5, k] != receive[m, n + 2]) && receive[5, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 4 && ((receive[4, k] == receive[5, j] && receive[4, k] != receive[m, n] && receive[4, k] != receive[m, n + 1] && receive[4, k] != receive[m, n - 1]) && receive[4, k] != 0 || (receive[5, k] == receive[4, j] && receive[5, k] != receive[m, n] && receive[5, k] != receive[m, n + 1] && receive[5, k] != receive[m, n - 1]) && receive[5, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 5 && ((receive[4, k] == receive[5, j] && receive[4, k] != receive[m, n] && receive[4, k] != receive[m, n - 2] && receive[4, k] != receive[m, n - 1]) && receive[4, k] != 0 || (receive[5, k] == receive[4, j] && receive[5, k] != receive[m, n] && receive[5, k] != receive[m, n - 2] && receive[5, k] != receive[m, n - 1]) && receive[5, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (m == 4)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (n == 3 && ((receive[3, k] == receive[5, j] && receive[3, k] != receive[m, n] && receive[3, k] != receive[m, n + 1] && receive[3, k] != receive[m, n + 2]) && receive[3, k] != 0 || (receive[5, k] == receive[3, j] && receive[5, k] != receive[m, n] && receive[5, k] != receive[m, n + 1] && receive[5, k] != receive[m, n + 2]) && receive[5, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 4 && ((receive[3, k] == receive[5, j] && receive[3, k] != receive[m, n] && receive[3, k] != receive[m, n + 1] && receive[3, k] != receive[m, n - 1]) && receive[3, k] != 0 || (receive[5, k] == receive[3, j] && receive[5, k] != receive[m, n] && receive[5, k] != receive[m, n + 1] && receive[5, k] != receive[m, n - 1]) && receive[5, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 5 && ((receive[3, k] == receive[5, j] && receive[3, k] != receive[m, n] && receive[3, k] != receive[m, n - 2] && receive[3, k] != receive[m, n - 1]) && receive[3, k] != 0 || (receive[5, k] == receive[3, j] && receive[5, k] != receive[m, n] && receive[5, k] != receive[m, n - 2] && receive[5, k] != receive[m, n - 1]) && receive[5, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (m == 5)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (n == 3 && ((receive[3, k] == receive[4, j] && receive[3, k] != receive[m, n] && receive[3, k] != receive[m, n + 1] && receive[3, k] != receive[m, n + 2]) && receive[3, k] != 0 || (receive[4, k] == receive[3, j] && receive[4, k] != receive[m, n] && receive[4, k] != receive[m, n + 1] && receive[4, k] != receive[m, n + 2]) && receive[4, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 4 && ((receive[3, k] == receive[4, j] && receive[3, k] != receive[m, n] && receive[3, k] != receive[m, n + 1] && receive[3, k] != receive[m, n - 1]) && receive[3, k] != 0 || (receive[4, k] == receive[3, j] && receive[4, k] != receive[m, n] && receive[4, k] != receive[m, n + 1] && receive[4, k] != receive[m, n - 1]) && receive[4, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 5 && ((receive[3, k] == receive[4, j] && receive[3, k] != receive[m, n] && receive[3, k] != receive[m, n - 2] && receive[3, k] != receive[m, n - 1]) && receive[3, k] != 0 || (receive[4, k] == receive[3, j] && receive[4, k] != receive[m, n] && receive[4, k] != receive[m, n - 2] && receive[4, k] != receive[m, n - 1]) && receive[4, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (m / 3 == 2)
                                {
                                    if (m == 6)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (n == 3 && ((receive[7, k] == receive[8, j] && receive[7, k] != receive[m, n] && receive[7, k] != receive[m, n + 1] && receive[7, k] != receive[m, n + 2]) && receive[7, k] != 0 || (receive[8, k] == receive[7, j] && receive[8, k] != receive[m, n] && receive[8, k] != receive[m, n + 1] && receive[8, k] != receive[m, n + 2]) && receive[8, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 4 && ((receive[7, k] == receive[8, j] && receive[7, k] != receive[m, n] && receive[7, k] != receive[m, n + 1] && receive[7, k] != receive[m, n - 1]) && receive[7, k] != 0 || (receive[8, k] == receive[7, j] && receive[8, k] != receive[m, n] && receive[8, k] != receive[m, n + 1] && receive[8, k] != receive[m, n - 1]) && receive[8, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 5 && ((receive[7, k] == receive[8, j] && receive[7, k] != receive[m, n] && receive[7, k] != receive[m, n - 2] && receive[7, k] != receive[m, n - 1]) && receive[7, k] != 0 || (receive[8, k] == receive[7, j] && receive[8, k] != receive[m, n] && receive[8, k] != receive[m, n - 2] && receive[8, k] != receive[m, n - 1]) && receive[8, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (m == 7)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (n == 3 && ((receive[6, k] == receive[8, j] && receive[6, k] != receive[m, n] && receive[6, k] != receive[m, n + 1] && receive[6, k] != receive[m, n + 2]) && receive[6, k] != 0 || (receive[8, k] == receive[6, j] && receive[8, k] != receive[m, n] && receive[8, k] != receive[m, n + 1] && receive[8, k] != receive[m, n + 2]) && receive[8, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 4 && ((receive[6, k] == receive[8, j] && receive[6, k] != receive[m, n] && receive[6, k] != receive[m, n + 1] && receive[6, k] != receive[m, n - 1]) && receive[6, k] != 0 || (receive[8, k] == receive[6, j] && receive[8, k] != receive[m, n] && receive[8, k] != receive[m, n + 1] && receive[8, k] != receive[m, n - 1]) && receive[8, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 5 && ((receive[6, k] == receive[8, j] && receive[6, k] != receive[m, n] && receive[6, k] != receive[m, n - 2] && receive[6, k] != receive[m, n - 1]) && receive[6, k] != 0 || (receive[8, k] == receive[6, j] && receive[8, k] != receive[m, n] && receive[8, k] != receive[m, n - 2] && receive[8, k] != receive[m, n - 1]) && receive[8, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (m == 8)    //12333333333333333333333333333333333333333333333333333333333333333333333333333333333
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (n == 3 && ((receive[7, k] == receive[6, j] && receive[7, k] != receive[m, n] && receive[7, k] != receive[m, n + 1] && receive[7, k] != receive[m, n + 2]) && receive[7, k] != 0 || (receive[6, k] == receive[7, j] && receive[6, k] != receive[m, n] && receive[6, k] != receive[m, n + 1] && receive[6, k] != receive[m, n + 2]) && receive[6, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 4 && ((receive[7, k] == receive[6, j] && receive[7, k] != receive[m, n] && receive[7, k] != receive[m, n + 1] && receive[7, k] != receive[m, n - 1]) && receive[7, k] != 0 || (receive[6, k] == receive[7, j] && receive[6, k] != receive[m, n] && receive[6, k] != receive[m, n + 1] && receive[6, k] != receive[m, n - 1]) && receive[6, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 5 && ((receive[7, k] == receive[6, j] && receive[7, k] != receive[m, n] && receive[7, k] != receive[m, n - 2] && receive[7, k] != receive[m, n - 1]) && receive[7, k] != 0 || (receive[6, k] == receive[7, j] && receive[6, k] != receive[m, n] && receive[6, k] != receive[m, n - 2] && receive[6, k] != receive[m, n - 1]) && receive[6, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (n / 3 == 2)
                        {
                            if ((n == 6 && receive[m, n + 1] != 0 && receive[m, n + 2] != 0) || (n == 7 && receive[m, n - 1] != 0 && receive[m, n + 1] != 0) || (n == 8 && receive[m, n - 1] != 0 && receive[m, n - 2] != 0))
                            {
                                if (m / 3 == 0)
                                {
                                    if (m == 0)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (n == 6 && ((receive[1, k] == receive[2, j] && receive[1, k] != receive[m, n] && receive[1, k] != receive[m, n + 1] && receive[1, k] != receive[m, n + 2]) && receive[1, k] != 0 || (receive[2, k] == receive[1, j] && receive[2, k] != receive[m, n] && receive[2, k] != receive[m, n + 1] && receive[2, k] != receive[m, n + 2]) && receive[2, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 7 && ((receive[1, k] == receive[2, j] && receive[1, k] != receive[m, n] && receive[1, k] != receive[m, n + 1] && receive[1, k] != receive[m, n - 1]) && receive[1, k] != 0 || (receive[2, k] == receive[1, j] && receive[2, k] != receive[m, n] && receive[2, k] != receive[m, n + 1] && receive[2, k] != receive[m, n - 1]) && receive[2, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 8 && ((receive[1, k] == receive[2, j] && receive[1, k] != receive[m, n] && receive[1, k] != receive[m, n - 2] && receive[1, k] != receive[m, n - 1]) && receive[1, k] != 0 || (receive[2, k] == receive[1, j] && receive[2, k] != receive[m, n] && receive[2, k] != receive[m, n - 2] && receive[2, k] != receive[m, n - 1]) && receive[2, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (m == 1)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (n == 6 && ((receive[0, k] == receive[2, j] && receive[0, k] != receive[m, n] && receive[0, k] != receive[m, n + 1] && receive[0, k] != receive[m, n + 2]) && receive[0, k] != 0 || (receive[2, k] == receive[0, j] && receive[2, k] != receive[m, n] && receive[2, k] != receive[m, n + 1] && receive[2, k] != receive[m, n + 2]) && receive[2, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 7 && ((receive[0, k] == receive[2, j] && receive[0, k] != receive[m, n] && receive[0, k] != receive[m, n + 1] && receive[0, k] != receive[m, n - 1]) && receive[0, k] != 0 || (receive[2, k] == receive[0, j] && receive[2, k] != receive[m, n] && receive[2, k] != receive[m, n + 1] && receive[2, k] != receive[m, n - 1]) && receive[2, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 8 && ((receive[0, k] == receive[2, j] && receive[0, k] != receive[m, n] && receive[0, k] != receive[m, n - 2] && receive[0, k] != receive[m, n - 1]) && receive[0, k] != 0 || (receive[2, k] == receive[0, j] && receive[2, k] != receive[m, n] && receive[2, k] != receive[m, n - 2] && receive[2, k] != receive[m, n - 1]) && receive[2, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (m == 2)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (n == 6 && ((receive[0, k] == receive[1, j] && receive[0, k] != receive[m, n] && receive[0, k] != receive[m, n + 1] && receive[0, k] != receive[m, n + 2]) && receive[0, k] != 0 || (receive[1, k] == receive[0, j] && receive[1, k] != receive[m, n] && receive[1, k] != receive[m, n + 1] && receive[1, k] != receive[m, n + 2]) && receive[1, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 7 && ((receive[0, k] == receive[1, j] && receive[0, k] != receive[m, n] && receive[0, k] != receive[m, n + 1] && receive[0, k] != receive[m, n - 1]) && receive[0, k] != 0 || (receive[1, k] == receive[0, j] && receive[1, k] != receive[m, n] && receive[1, k] != receive[m, n + 1] && receive[1, k] != receive[m, n - 1]) && receive[1, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 8 && ((receive[0, k] == receive[1, j] && receive[0, k] != receive[m, n] && receive[0, k] != receive[m, n - 2] && receive[0, k] != receive[m, n - 1]) && receive[0, k] != 0 || (receive[1, k] == receive[0, j] && receive[1, k] != receive[m, n] && receive[1, k] != receive[m, n - 2] && receive[1, k] != receive[m, n - 1]) && receive[1, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (m / 3 == 1)
                                {
                                    if (m == 3)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)//wwwww
                                            {
                                                if (n == 6 && ((receive[4, k] == receive[5, j] && receive[4, k] != receive[m, n] && receive[4, k] != receive[m, n + 1] && receive[4, k] != receive[m, n + 2]) && receive[4, k] != 0 || (receive[5, k] == receive[4, j] && receive[5, k] != receive[m, n] && receive[5, k] != receive[m, n + 1] && receive[5, k] != receive[m, n + 2]) && receive[5, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 7 && ((receive[4, k] == receive[5, j] && receive[4, k] != receive[m, n] && receive[4, k] != receive[m, n + 1] && receive[4, k] != receive[m, n - 1]) && receive[4, k] != 0 || (receive[5, k] == receive[4, j] && receive[5, k] != receive[m, n] && receive[5, k] != receive[m, n + 1] && receive[5, k] != receive[m, n - 1]) && receive[5, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 8 && ((receive[4, k] == receive[5, j] && receive[4, k] != receive[m, n] && receive[4, k] != receive[m, n - 2] && receive[4, k] != receive[m, n - 1]) && receive[4, k] != 0 || (receive[5, k] == receive[4, j] && receive[5, k] != receive[m, n] && receive[5, k] != receive[m, n - 2] && receive[5, k] != receive[m, n - 1]) && receive[5, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (m == 4)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (n == 6 && ((receive[3, k] == receive[5, j] && receive[3, k] != receive[m, n] && receive[3, k] != receive[m, n + 1] && receive[3, k] != receive[m, n + 2]) && receive[3, k] != 0 || (receive[5, k] == receive[3, j] && receive[5, k] != receive[m, n] && receive[5, k] != receive[m, n + 1] && receive[5, k] != receive[m, n + 2]) && receive[5, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 7 && ((receive[3, k] == receive[5, j] && receive[3, k] != receive[m, n] && receive[3, k] != receive[m, n + 1] && receive[3, k] != receive[m, n - 1]) && receive[3, k] != 0 || (receive[5, k] == receive[3, j] && receive[5, k] != receive[m, n] && receive[5, k] != receive[m, n + 1] && receive[5, k] != receive[m, n - 1]) && receive[5, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 8 && ((receive[3, k] == receive[5, j] && receive[3, k] != receive[m, n] && receive[3, k] != receive[m, n - 2] && receive[3, k] != receive[m, n - 1]) && receive[3, k] != 0 || (receive[5, k] == receive[3, j] && receive[5, k] != receive[m, n] && receive[5, k] != receive[m, n - 2] && receive[5, k] != receive[m, n - 1]) && receive[5, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (m == 5)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (n == 6 && ((receive[3, k] == receive[4, j] && receive[3, k] != receive[m, n] && receive[3, k] != receive[m, n + 1] && receive[3, k] != receive[m, n + 2]) && receive[3, k] != 0 || (receive[4, k] == receive[3, j] && receive[4, k] != receive[m, n] && receive[4, k] != receive[m, n + 1] && receive[4, k] != receive[m, n + 2]) && receive[4, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 7 && ((receive[3, k] == receive[4, j] && receive[3, k] != receive[m, n] && receive[3, k] != receive[m, n + 1] && receive[3, k] != receive[m, n - 1]) && receive[3, k] != 0 || (receive[4, k] == receive[3, j] && receive[4, k] != receive[m, n] && receive[4, k] != receive[m, n + 1] && receive[4, k] != receive[m, n - 1]) && receive[4, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 8 && ((receive[3, k] == receive[4, j] && receive[3, k] != receive[m, n] && receive[3, k] != receive[m, n - 2] && receive[3, k] != receive[m, n - 1]) && receive[3, k] != 0 || (receive[4, k] == receive[3, j] && receive[4, k] != receive[m, n] && receive[4, k] != receive[m, n - 2] && receive[4, k] != receive[m, n - 1]) && receive[4, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (m / 3 == 2)
                                {
                                    if (m == 6)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (n == 6 && ((receive[7, k] == receive[8, j] && receive[7, k] != receive[m, n] && receive[7, k] != receive[m, n + 1] && receive[7, k] != receive[m, n + 2]) && receive[7, k] != 0 || (receive[8, k] == receive[7, j] && receive[8, k] != receive[m, n] && receive[8, k] != receive[m, n + 1] && receive[8, k] != receive[m, n + 2]) && receive[8, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 7 && ((receive[7, k] == receive[8, j] && receive[7, k] != receive[m, n] && receive[7, k] != receive[m, n + 1] && receive[7, k] != receive[m, n - 1]) && receive[7, k] != 0 || (receive[8, k] == receive[7, j] && receive[8, k] != receive[m, n] && receive[8, k] != receive[m, n + 1] && receive[8, k] != receive[m, n - 1]) && receive[8, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 8 && ((receive[7, k] == receive[8, j] && receive[7, k] != receive[m, n] && receive[7, k] != receive[m, n - 2] && receive[7, k] != receive[m, n - 1]) && receive[7, k] != 0 || (receive[8, k] == receive[7, j] && receive[8, k] != receive[m, n] && receive[8, k] != receive[m, n - 2] && receive[8, k] != receive[m, n - 1]) && receive[8, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (m == 7)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (n == 6 && ((receive[6, k] == receive[8, j] && receive[6, k] != receive[m, n] && receive[6, k] != receive[m, n + 1] && receive[6, k] != receive[m, n + 2]) && receive[6, k] != 0 || (receive[8, k] == receive[6, j] && receive[8, k] != receive[m, n] && receive[8, k] != receive[m, n + 1] && receive[8, k] != receive[m, n + 2]) && receive[8, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 7 && ((receive[6, k] == receive[8, j] && receive[6, k] != receive[m, n] && receive[6, k] != receive[m, n + 1] && receive[6, k] != receive[m, n - 1]) && receive[6, k] != 0 || (receive[8, k] == receive[6, j] && receive[8, k] != receive[m, n] && receive[8, k] != receive[m, n + 1] && receive[8, k] != receive[m, n - 1]) && receive[8, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 8 && ((receive[6, k] == receive[8, j] && receive[6, k] != receive[m, n] && receive[6, k] != receive[m, n - 2] && receive[6, k] != receive[m, n - 1]) && receive[6, k] != 0 || (receive[8, k] == receive[6, j] && receive[8, k] != receive[m, n] && receive[8, k] != receive[m, n - 2] && receive[8, k] != receive[m, n - 1]) && receive[8, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (m == 8)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (n == 6 && ((receive[7, k] == receive[6, j] && receive[7, k] != receive[m, n] && receive[7, k] != receive[m, n + 1] && receive[7, k] != receive[m, n + 2]) && receive[7, k] != 0 || (receive[6, k] == receive[7, j] && receive[6, k] != receive[m, n] && receive[6, k] != receive[m, n + 1] && receive[6, k] != receive[m, n + 2]) && receive[6, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 7 && ((receive[7, k] == receive[6, j] && receive[7, k] != receive[m, n] && receive[7, k] != receive[m, n + 1] && receive[7, k] != receive[m, n - 1]) && receive[7, k] != 0 || (receive[6, k] == receive[7, j] && receive[6, k] != receive[m, n] && receive[6, k] != receive[m, n + 1] && receive[6, k] != receive[m, n - 1]) && receive[6, k] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (n == 8 && ((receive[7, k] == receive[6, j] && receive[7, k] != receive[m, n] && receive[7, k] != receive[m, n - 2] && receive[7, k] != receive[m, n - 1]) && receive[7, k] != 0 || (receive[6, k] == receive[7, j] && receive[6, k] != receive[m, n] && receive[6, k] != receive[m, n - 2] && receive[6, k] != receive[m, n - 1]) && receive[6, k] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return -1;
        }
        public static int YNineGrid()
        {
            for (int n = 0; n < 9; n++)
            {
                for (int m = 0; m < 9; m++)
                {
                    if (receive[m, n] != 0)
                    {
                        if (m / 3 == 0)
                        {
                            if ((m == 0 && receive[m + 1, n] != 0 && receive[m + 2, n] != 0) || (m == 1 && receive[m - 1, n] != 0 && receive[m + 1, n] != 0) || (m == 2 && receive[m - 1, n] != 0 && receive[m - 2, n] != 0))
                            {
                                if (n / 3 == 0)
                                {
                                    if (n == 0)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (m == 0 && ((receive[k, 1] == receive[j, 2] && receive[k, 1] != receive[m, n] && receive[k, 1] != receive[m + 1, n] && receive[k, 1] != receive[m + 2, n]) && receive[k, 1] != 0 || (receive[k, 2] == receive[j, 1] && receive[k, 2] != receive[m, n] && receive[k, 2] != receive[m + 1, n] && receive[k, 2] != receive[m + 2, n]) && receive[k, 2] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 1 && ((receive[k, 1] == receive[j, 2] && receive[k, 1] != receive[m, n] && receive[k, 1] != receive[m + 1, n] && receive[k, 1] != receive[m - 1, n]) && receive[k, 1] != 0 || (receive[k, 2] == receive[j, 1] && receive[k, 2] != receive[m, n] && receive[k, 2] != receive[m + 1, n] && receive[k, 2] != receive[m - 1, n]) && receive[k, 2] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 2 && ((receive[k, 1] == receive[j, 2] && receive[k, 1] != receive[m, n] && receive[k, 1] != receive[m - 2, n] && receive[k, 1] != receive[m - 1, n]) && receive[k, 1] != 0 || (receive[k, 2] == receive[j, 1] && receive[k, 2] != receive[m, n] && receive[k, 2] != receive[m - 2, n] && receive[k, 2] != receive[m - 1, n]) && receive[k, 2] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (n == 1)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (m == 0 && ((receive[k, 0] == receive[j, 2] && receive[k, 0] != receive[m, n] && receive[k, 0] != receive[m + 1, n] && receive[k, 0] != receive[m + 2, n]) && receive[k, 0] != 0 || (receive[k, 2] == receive[j, 0] && receive[k, 2] != receive[m, n] && receive[k, 2] != receive[m + 1, n] && receive[k, 2] != receive[m + 2, n]) && receive[k, 2] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 1 && ((receive[k, 0] == receive[j, 2] && receive[k, 0] != receive[m, n] && receive[k, 0] != receive[m + 1, n] && receive[k, 0] != receive[m - 1, n]) && receive[k, 0] != 0 || (receive[k, 2] == receive[j, 0] && receive[k, 2] != receive[m, n] && receive[k, 2] != receive[m + 1, n] && receive[k, 2] != receive[m - 1, n]) && receive[k, 2] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 2 && ((receive[k, 0] == receive[j, 2] && receive[k, 0] != receive[m, n] && receive[k, 0] != receive[m - 2, n] && receive[k, 0] != receive[m - 1, n]) && receive[k, 0] != 0 || (receive[k, 2] == receive[j, 0] && receive[k, 2] != receive[m, n] && receive[k, 2] != receive[m - 2, n] && receive[k, 2] != receive[m - 1, n]) && receive[k, 2] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (n == 2)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (m == 0 && ((receive[k, 0] == receive[j, 1] && receive[k, 0] != receive[m, n] && receive[k, 0] != receive[m + 1, n] && receive[k, 0] != receive[m + 2, n]) && receive[k, 0] != 0 || (receive[k, 1] == receive[j, 0] && receive[k, 1] != receive[m, n] && receive[k, 1] != receive[m + 1, n] && receive[k, 1] != receive[m + 2, n]) && receive[k, 1] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 1 && ((receive[k, 0] == receive[j, 1] && receive[k, 0] != receive[m, n] && receive[k, 0] != receive[m + 1, n] && receive[k, 0] != receive[m - 1, n]) && receive[k, 0] != 0 || (receive[k, 1] == receive[j, 0] && receive[k, 1] != receive[m, n] && receive[k, 1] != receive[m + 1, n] && receive[k, 1] != receive[m - 1, n]) && receive[k, 1] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 2 && ((receive[k, 0] == receive[j, 1] && receive[k, 0] != receive[m, n] && receive[k, 0] != receive[m - 2, n] && receive[k, 0] != receive[m - 1, n]) && receive[k, 0] != 0 || (receive[k, 1] == receive[j, 0] && receive[k, 1] != receive[m, n] && receive[k, 1] != receive[m - 2, n] && receive[k, 1] != receive[m - 1, n]) && receive[k, 1] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n / 3 == 1)
                                {
                                    if (n == 3)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (m == 0 && ((receive[k, 4] == receive[j, 5] && receive[k, 4] != receive[m, n] && receive[k, 4] != receive[m + 1, n] && receive[k, 4] != receive[m + 2, n]) && receive[k, 4] != 0 || (receive[k, 5] == receive[j, 4] && receive[k, 5] != receive[m, n] && receive[k, 5] != receive[m + 1, n] && receive[k, 5] != receive[m + 2, n]) && receive[k, 5] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 1 && ((receive[k, 4] == receive[j, 5] && receive[k, 4] != receive[m, n] && receive[k, 4] != receive[m + 1, n] && receive[k, 4] != receive[m - 1, n]) && receive[k, 4] != 0 || (receive[k, 5] == receive[j, 4] && receive[k, 5] != receive[m, n] && receive[k, 5] != receive[m + 1, n] && receive[k, 5] != receive[m - 1, n]) && receive[k, 5] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 2 && ((receive[k, 4] == receive[j, 5] && receive[k, 4] != receive[m, n] && receive[k, 4] != receive[m - 2, n] && receive[k, 4] != receive[m - 1, n]) && receive[k, 4] != 0 || (receive[k, 5] == receive[j, 4] && receive[k, 5] != receive[m, n] && receive[k, 5] != receive[m - 2, n] && receive[k, 5] != receive[m - 1, n]) && receive[k, 5] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (n == 4)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (m == 0 && ((receive[k, 5] == receive[j, 3] && receive[k, 5] != receive[m, n] && receive[k, 5] != receive[m + 1, n] && receive[k, 5] != receive[m + 2, n]) && receive[k, 5] != 0 || (receive[k, 3] == receive[j, 5] && receive[k, 3] != receive[m, n] && receive[k, 3] != receive[m + 1, n] && receive[k, 3] != receive[m + 2, n]) && receive[k, 3] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 1 && ((receive[k, 5] == receive[j, 3] && receive[k, 5] != receive[m, n] && receive[k, 5] != receive[m + 1, n] && receive[k, 5] != receive[m - 1, n]) && receive[k, 5] != 0 || (receive[k, 3] == receive[j, 5] && receive[k, 3] != receive[m, n] && receive[k, 3] != receive[m + 1, n] && receive[k, 3] != receive[m - 1, n]) && receive[k, 3] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 2 && ((receive[k, 5] == receive[j, 3] && receive[k, 5] != receive[m, n] && receive[k, 5] != receive[m - 2, n] && receive[k, 5] != receive[m - 1, n]) && receive[k, 5] != 0 || (receive[k, 3] == receive[j, 5] && receive[k, 3] != receive[m, n] && receive[k, 3] != receive[m - 2, n] && receive[k, 3] != receive[m - 1, n]) && receive[k, 3] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (n == 5)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (m == 0 && ((receive[k, 4] == receive[j, 3] && receive[k, 4] != receive[m, n] && receive[k, 4] != receive[m + 1, n] && receive[k, 4] != receive[m + 2, n]) && receive[k, 4] != 0 || (receive[k, 3] == receive[j, 4] && receive[k, 3] != receive[m, n] && receive[k, 3] != receive[m + 1, n] && receive[k, 3] != receive[m + 2, n]) && receive[k, 3] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 1 && ((receive[k, 4] == receive[j, 3] && receive[k, 4] != receive[m, n] && receive[k, 4] != receive[m + 1, n] && receive[k, 4] != receive[m - 1, n]) && receive[k, 4] != 0 || (receive[k, 3] == receive[j, 4] && receive[k, 3] != receive[m, n] && receive[k, 3] != receive[m + 1, n] && receive[k, 3] != receive[m - 1, n]) && receive[k, 3] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 2 && ((receive[k, 4] == receive[j, 3] && receive[k, 4] != receive[m, n] && receive[k, 4] != receive[m - 2, n] && receive[k, 4] != receive[m - 1, n]) && receive[k, 4] != 0 || (receive[k, 3] == receive[j, 4] && receive[k, 3] != receive[m, n] && receive[k, 3] != receive[m - 2, n] && receive[k, 3] != receive[m - 1, n]) && receive[k, 3] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n / 3 == 2)
                                {
                                    if (n == 6)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (m == 0 && ((receive[k, 7] == receive[j, 8] && receive[k, 7] != receive[m, n] && receive[k, 7] != receive[m + 1, n] && receive[k, 7] != receive[m + 2, n]) && receive[k, 7] != 0 || (receive[k, 8] == receive[j, 7] && receive[k, 8] != receive[m, n] && receive[k, 8] != receive[m + 1, n] && receive[k, 8] != receive[m + 2, n]) && receive[k, 8] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 1 && ((receive[k, 7] == receive[j, 8] && receive[k, 7] != receive[m, n] && receive[k, 7] != receive[m + 1, n] && receive[k, 7] != receive[m - 1, n]) && receive[k, 7] != 0 || (receive[k, 8] == receive[j, 7] && receive[k, 8] != receive[m, n] && receive[k, 8] != receive[m + 1, n] && receive[k, 8] != receive[m - 1, n]) && receive[k, 8] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 2 && ((receive[k, 7] == receive[j, 8] && receive[k, 7] != receive[m, n] && receive[k, 7] != receive[m - 2, n] && receive[k, 7] != receive[m - 1, n]) && receive[k, 7] != 0 || (receive[k, 8] == receive[j, 7] && receive[k, 8] != receive[m, n] && receive[k, 8] != receive[m - 2, n] && receive[k, 8] != receive[m - 1, n]) && receive[k, 8] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (n == 7)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (m == 0 && ((receive[k, 6] == receive[j, 8] && receive[k, 6] != receive[m, n] && receive[k, 6] != receive[m + 1, n] && receive[k, 6] != receive[m + 2, n]) && receive[k, 6] != 0 || (receive[k, 8] == receive[j, 6] && receive[k, 8] != receive[m, n] && receive[k, 8] != receive[m + 1, n] && receive[k, 8] != receive[m + 2, n]) && receive[k, 8] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 1 && ((receive[k, 6] == receive[j, 8] && receive[k, 6] != receive[m, n] && receive[k, 6] != receive[m + 1, n] && receive[k, 6] != receive[m - 1, n]) && receive[k, 6] != 0 || (receive[k, 8] == receive[j, 6] && receive[k, 8] != receive[m, n] && receive[k, 8] != receive[m + 1, n] && receive[k, 8] != receive[m - 1, n]) && receive[k, 8] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 2 && ((receive[k, 6] == receive[j, 8] && receive[k, 6] != receive[m, n] && receive[k, 6] != receive[m - 2, n] && receive[k, 6] != receive[m - 1, n]) && receive[k, 6] != 0 || (receive[k, 8] == receive[j, 6] && receive[k, 8] != receive[m, n] && receive[k, 8] != receive[m - 2, n] && receive[k, 8] != receive[m - 1, n]) && receive[k, 8] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (n == 8)
                                    {
                                        for (int k = 3; k < 6; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (m == 0 && ((receive[k, 6] == receive[j, 7] && receive[k, 6] != receive[m, n] && receive[k, 6] != receive[m + 1, n] && receive[k, 6] != receive[m + 2, n]) && receive[k, 6] != 0 || (receive[k, 7] == receive[j, 6] && receive[k, 7] != receive[m, n] && receive[k, 7] != receive[m + 1, n] && receive[k, 7] != receive[m + 2, n]) && receive[k, 7] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 1 && ((receive[k, 6] == receive[j, 7] && receive[k, 6] != receive[m, n] && receive[k, 6] != receive[m + 1, n] && receive[k, 6] != receive[m - 1, n]) && receive[k, 6] != 0 || (receive[k, 7] == receive[j, 6] && receive[k, 7] != receive[m, n] && receive[k, 7] != receive[m + 1, n] && receive[k, 7] != receive[m - 1, n]) && receive[k, 7] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 2 && ((receive[k, 6] == receive[j, 7] && receive[k, 6] != receive[m, n] && receive[k, 6] != receive[m - 2, n] && receive[k, 6] != receive[m - 1, n]) && receive[k, 6] != 0 || (receive[k, 7] == receive[j, 6] && receive[k, 7] != receive[m, n] && receive[k, 7] != receive[m - 2, n] && receive[k, 7] != receive[m - 1, n]) && receive[k, 7] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (m / 3 == 1)
                        {
                            if ((m == 3 && receive[m + 1, n] != 0 && receive[m + 2, n] != 0) || (m == 4 && receive[m - 1, n] != 0 && receive[m + 1, n] != 0) || (m == 5 && receive[m - 1, n] != 0 && receive[m - 2, n] != 0))
                            {
                                if (n / 3 == 0)
                                {
                                    if (n == 0)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (m == 3 && ((receive[k, 1] == receive[j, 2] && receive[k, 1] != receive[m, n] && receive[k, 1] != receive[m + 1, n] && receive[k, 1] != receive[m + 2, n]) && receive[k, 1] != 0 || (receive[k, 2] == receive[j, 1] && receive[k, 2] != receive[m, n] && receive[k, 2] != receive[m + 1, n] && receive[k, 2] != receive[m + 2, n]) && receive[k, 2] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 4 && ((receive[k, 1] == receive[j, 2] && receive[k, 1] != receive[m, n] && receive[k, 1] != receive[m + 1, n] && receive[k, 1] != receive[m - 1, n]) && receive[k, 1] != 0 || (receive[k, 2] == receive[j, 1] && receive[k, 2] != receive[m, n] && receive[k, 2] != receive[m + 1, n] && receive[k, 2] != receive[m - 1, n]) && receive[k, 2] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 5 && ((receive[k, 1] == receive[j, 2] && receive[k, 1] != receive[m, n] && receive[k, 1] != receive[m - 2, n] && receive[k, 1] != receive[m - 1, n]) && receive[k, 1] != 0 || (receive[k, 2] == receive[j, 1] && receive[k, 2] != receive[m, n] && receive[k, 2] != receive[m - 2, n] && receive[k, 2] != receive[m - 1, n]) && receive[k, 2] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (n == 1)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (m == 3 && ((receive[k, 0] == receive[j, 2] && receive[k, 0] != receive[m, n] && receive[k, 0] != receive[m + 1, n] && receive[k, 0] != receive[m + 2, n]) && receive[k, 0] != 0 || (receive[k, 2] == receive[j, 0] && receive[k, 2] != receive[m, n] && receive[k, 2] != receive[m + 1, n] && receive[k, 2] != receive[m + 2, n]) && receive[k, 2] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 4 && ((receive[k, 0] == receive[j, 2] && receive[k, 0] != receive[m, n] && receive[k, 0] != receive[m + 1, n] && receive[k, 0] != receive[m - 1, n]) && receive[k, 0] != 0 || (receive[k, 2] == receive[j, 0] && receive[k, 2] != receive[m, n] && receive[k, 2] != receive[m + 1, n] && receive[k, 2] != receive[m - 1, n]) && receive[k, 2] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 5 && ((receive[k, 0] == receive[j, 2] && receive[k, 0] != receive[m, n] && receive[k, 0] != receive[m - 2, n] && receive[k, 0] != receive[m - 1, n]) && receive[k, 0] != 0 || (receive[k, 2] == receive[j, 0] && receive[k, 2] != receive[m, n] && receive[k, 2] != receive[m - 2, n] && receive[k, 2] != receive[m - 1, n]) && receive[k, 2] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (n == 2)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (m == 3 && ((receive[k, 0] == receive[j, 1] && receive[k, 0] != receive[m, n] && receive[k, 0] != receive[m + 1, n] && receive[k, 0] != receive[m + 2, n]) && receive[k, 0] != 0 || (receive[k, 1] == receive[j, 0] && receive[k, 1] != receive[m, n] && receive[k, 1] != receive[m + 1, n] && receive[k, 1] != receive[m + 2, n]) && receive[k, 1] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 4 && ((receive[k, 0] == receive[j, 1] && receive[k, 0] != receive[m, n] && receive[k, 0] != receive[m + 1, n] && receive[k, 0] != receive[m - 1, n]) && receive[k, 0] != 0 || (receive[k, 1] == receive[j, 0] && receive[k, 1] != receive[m, n] && receive[k, 1] != receive[m + 1, n] && receive[k, 1] != receive[m - 1, n]) && receive[k, 1] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 5 && ((receive[k, 0] == receive[j, 1] && receive[k, 0] != receive[m, n] && receive[k, 0] != receive[m - 2, n] && receive[k, 0] != receive[m - 1, n]) && receive[k, 0] != 0 || (receive[k, 1] == receive[j, 0] && receive[k, 1] != receive[m, n] && receive[k, 1] != receive[m - 2, n] && receive[k, 1] != receive[m - 1, n]) && receive[k, 1] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n / 3 == 1)
                                {
                                    if (n == 3)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (m == 3 && ((receive[k, 4] == receive[j, 5] && receive[k, 4] != receive[m, n] && receive[k, 4] != receive[m + 1, n] && receive[k, 4] != receive[m + 2, n]) && receive[k, 4] != 0 || (receive[k, 5] == receive[j, 4] && receive[k, 5] != receive[m, n] && receive[k, 5] != receive[m + 1, n] && receive[k, 5] != receive[m + 2, n]) && receive[k, 5] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 4 && ((receive[k, 4] == receive[j, 5] && receive[k, 4] != receive[m, n] && receive[k, 4] != receive[m + 1, n] && receive[k, 4] != receive[m - 1, n]) && receive[k, 4] != 0 || (receive[k, 5] == receive[j, 4] && receive[k, 5] != receive[m, n] && receive[k, 5] != receive[m + 1, n] && receive[k, 5] != receive[m - 1, n]) && receive[k, 5] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 5 && ((receive[k, 4] == receive[j, 5] && receive[k, 4] != receive[m, n] && receive[k, 4] != receive[m - 2, n] && receive[k, 4] != receive[m - 1, n]) && receive[k, 4] != 0 || (receive[k, 5] == receive[j, 4] && receive[k, 5] != receive[m, n] && receive[k, 5] != receive[m - 2, n] && receive[k, 5] != receive[m - 1, n]) && receive[k, 5] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (n == 4)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (m == 3 && ((receive[k, 5] == receive[j, 3] && receive[k, 5] != receive[m, n] && receive[k, 5] != receive[m + 1, n] && receive[k, 5] != receive[m + 2, n]) && receive[k, 5] != 0 || (receive[k, 3] == receive[j, 5] && receive[k, 3] != receive[m, n] && receive[k, 3] != receive[m + 1, n] && receive[k, 3] != receive[m + 2, n]) && receive[k, 3] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 4 && ((receive[k, 5] == receive[j, 3] && receive[k, 5] != receive[m, n] && receive[k, 5] != receive[m + 1, n] && receive[k, 5] != receive[m - 1, n]) && receive[k, 5] != 0 || (receive[k, 3] == receive[j, 5] && receive[k, 3] != receive[m, n] && receive[k, 3] != receive[m + 1, n] && receive[k, 3] != receive[m - 1, n]) && receive[k, 3] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 5 && ((receive[k, 5] == receive[j, 3] && receive[k, 5] != receive[m, n] && receive[k, 5] != receive[m - 2, n] && receive[k, 5] != receive[m - 1, n]) && receive[k, 5] != 0 || (receive[k, 3] == receive[j, 5] && receive[k, 3] != receive[m, n] && receive[k, 3] != receive[m - 2, n] && receive[k, 3] != receive[m - 1, n]) && receive[k, 3] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (n == 5)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (m == 3 && ((receive[k, 4] == receive[j, 3] && receive[k, 4] != receive[m, n] && receive[k, 4] != receive[m + 1, n] && receive[k, 4] != receive[m + 2, n]) && receive[k, 4] != 0 || (receive[k, 3] == receive[j, 4] && receive[k, 3] != receive[m, n] && receive[k, 3] != receive[m + 1, n] && receive[k, 3] != receive[m + 2, n]) && receive[k, 3] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 4 && ((receive[k, 4] == receive[j, 3] && receive[k, 4] != receive[m, n] && receive[k, 4] != receive[m + 1, n] && receive[k, 4] != receive[m - 1, n]) && receive[k, 4] != 0 || (receive[k, 3] == receive[j, 4] && receive[k, 3] != receive[m, n] && receive[k, 3] != receive[m + 1, n] && receive[k, 3] != receive[m - 1, n]) && receive[k, 3] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 5 && ((receive[k, 4] == receive[j, 3] && receive[k, 4] != receive[m, n] && receive[k, 4] != receive[m - 2, n] && receive[k, 4] != receive[m - 1, n]) && receive[k, 4] != 0 || (receive[k, 3] == receive[j, 4] && receive[k, 3] != receive[m, n] && receive[k, 3] != receive[m - 2, n] && receive[k, 3] != receive[m - 1, n]) && receive[k, 3] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n / 3 == 2)
                                {
                                    if (n == 6)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (m == 3 && ((receive[k, 7] == receive[j, 8] && receive[k, 7] != receive[m, n] && receive[k, 7] != receive[m + 1, n] && receive[k, 7] != receive[m + 2, n]) && receive[k, 7] != 0 || (receive[k, 8] == receive[j, 7] && receive[k, 8] != receive[m, n] && receive[k, 8] != receive[m + 1, n] && receive[k, 8] != receive[m + 2, n]) && receive[k, 8] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 4 && ((receive[k, 7] == receive[j, 8] && receive[k, 7] != receive[m, n] && receive[k, 7] != receive[m + 1, n] && receive[k, 7] != receive[m - 1, n]) && receive[k, 7] != 0 || (receive[k, 8] == receive[j, 7] && receive[k, 8] != receive[m, n] && receive[k, 8] != receive[m + 1, n] && receive[k, 8] != receive[m - 1, n]) && receive[k, 8] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 5 && ((receive[k, 7] == receive[j, 8] && receive[k, 7] != receive[m, n] && receive[k, 7] != receive[m - 2, n] && receive[k, 7] != receive[m - 1, n]) && receive[k, 7] != 0 || (receive[k, 8] == receive[j, 7] && receive[k, 8] != receive[m, n] && receive[k, 8] != receive[m - 2, n] && receive[k, 8] != receive[m - 1, n]) && receive[k, 8] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (n == 7)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (m == 3 && ((receive[k, 6] == receive[j, 8] && receive[k, 6] != receive[m, n] && receive[k, 6] != receive[m + 1, n] && receive[k, 6] != receive[m + 2, n]) && receive[k, 6] != 0 || (receive[k, 8] == receive[j, 6] && receive[k, 8] != receive[m, n] && receive[k, 8] != receive[m + 1, n] && receive[k, 8] != receive[m + 2, n]) && receive[k, 8] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 4 && ((receive[k, 6] == receive[j, 8] && receive[k, 6] != receive[m, n] && receive[k, 6] != receive[m + 1, n] && receive[k, 6] != receive[m - 1, n]) && receive[k, 6] != 0 || (receive[k, 8] == receive[j, 6] && receive[k, 8] != receive[m, n] && receive[k, 8] != receive[m + 1, n] && receive[k, 8] != receive[m - 1, n]) && receive[k, 8] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 5 && ((receive[k, 6] == receive[j, 8] && receive[k, 6] != receive[m, n] && receive[k, 6] != receive[m - 2, n] && receive[k, 6] != receive[m - 1, n]) && receive[k, 6] != 0 || (receive[k, 8] == receive[j, 6] && receive[k, 8] != receive[m, n] && receive[k, 8] != receive[m - 2, n] && receive[k, 8] != receive[m - 1, n]) && receive[k, 8] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (n == 8)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 6; j < 9; j++)
                                            {
                                                if (m == 3 && ((receive[k, 6] == receive[j, 7] && receive[k, 6] != receive[m, n] && receive[k, 6] != receive[m + 1, n] && receive[k, 6] != receive[m + 2, n]) && receive[k, 6] != 0 || (receive[k, 7] == receive[j, 6] && receive[k, 7] != receive[m, n] && receive[k, 7] != receive[m + 1, n] && receive[k, 7] != receive[m + 2, n]) && receive[k, 7] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 4 && ((receive[k, 6] == receive[j, 7] && receive[k, 6] != receive[m, n] && receive[k, 6] != receive[m + 1, n] && receive[k, 6] != receive[m - 1, n]) && receive[k, 6] != 0 || (receive[k, 7] == receive[j, 6] && receive[k, 7] != receive[m, n] && receive[k, 7] != receive[m + 1, n] && receive[k, 7] != receive[m - 1, n]) && receive[k, 7] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 5 && ((receive[k, 6] == receive[j, 7] && receive[k, 6] != receive[m, n] && receive[k, 6] != receive[m - 2, n] && receive[k, 6] != receive[m - 1, n]) && receive[k, 6] != 0 || (receive[k, 7] == receive[j, 6] && receive[k, 7] != receive[m, n] && receive[k, 7] != receive[m - 2, n] && receive[k, 7] != receive[m - 1, n]) && receive[k, 7] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (m / 3 == 2)
                        {
                            if ((m == 6 && receive[m + 1, n] != 0 && receive[m + 2, n] != 0) || (m == 7 && receive[m - 1, n] != 0 && receive[m + 1, n] != 0) || (m == 8 && receive[m - 1, n] != 0 && receive[m - 2, n] != 0))
                            {
                                if (n / 3 == 0)
                                {
                                    if (n == 0)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (m == 6 && ((receive[k, 1] == receive[j, 2] && receive[k, 1] != receive[m, n] && receive[k, 1] != receive[m + 1, n] && receive[k, 1] != receive[m + 2, n]) && receive[k, 1] != 0 || (receive[k, 2] == receive[j, 1] && receive[k, 2] != receive[m, n] && receive[k, 2] != receive[m + 1, n] && receive[k, 2] != receive[m + 2, n]) && receive[k, 2] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 7 && ((receive[k, 1] == receive[j, 2] && receive[k, 1] != receive[m, n] && receive[k, 1] != receive[m + 1, n] && receive[k, 1] != receive[m - 1, n]) && receive[k, 1] != 0 || (receive[k, 2] == receive[j, 1] && receive[k, 2] != receive[m, n] && receive[k, 2] != receive[m + 1, n] && receive[k, 2] != receive[m - 1, n]) && receive[k, 2] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 8 && ((receive[k, 1] == receive[j, 2] && receive[k, 1] != receive[m, n] && receive[k, 1] != receive[m - 2, n] && receive[k, 1] != receive[m - 1, n]) && receive[k, 1] != 0 || (receive[k, 2] == receive[j, 1] && receive[k, 2] != receive[m, n] && receive[k, 2] != receive[m - 2, n] && receive[k, 2] != receive[m - 1, n]) && receive[k, 2] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (n == 1)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (m == 6 && ((receive[k, 0] == receive[j, 2] && receive[k, 0] != receive[m, n] && receive[k, 0] != receive[m + 1, n] && receive[k, 0] != receive[m + 2, n]) && receive[k, 0] != 0 || (receive[k, 2] == receive[j, 0] && receive[k, 2] != receive[m, n] && receive[k, 2] != receive[m + 1, n] && receive[k, 2] != receive[m + 2, n]) && receive[k, 2] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 7 && ((receive[k, 0] == receive[j, 2] && receive[k, 0] != receive[m, n] && receive[k, 0] != receive[m + 1, n] && receive[k, 0] != receive[m - 1, n]) && receive[k, 0] != 0 || (receive[k, 2] == receive[j, 0] && receive[k, 2] != receive[m, n] && receive[k, 2] != receive[m + 1, n] && receive[k, 2] != receive[m - 1, n]) && receive[k, 2] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 8 && ((receive[k, 0] == receive[j, 2] && receive[k, 0] != receive[m, n] && receive[k, 0] != receive[m - 2, n] && receive[k, 0] != receive[m - 1, n]) && receive[k, 0] != 0 || (receive[k, 2] == receive[j, 0] && receive[k, 2] != receive[m, n] && receive[k, 2] != receive[m - 2, n] && receive[k, 2] != receive[m - 1, n]) && receive[k, 2] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (n == 2)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (m == 6 && ((receive[k, 0] == receive[j, 1] && receive[k, 0] != receive[m, n] && receive[k, 0] != receive[m + 1, n] && receive[k, 0] != receive[m + 2, n]) && receive[k, 0] != 0 || (receive[k, 1] == receive[j, 0] && receive[k, 1] != receive[m, n] && receive[k, 1] != receive[m + 1, n] && receive[k, 1] != receive[m + 2, n]) && receive[k, 1] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 7 && ((receive[k, 0] == receive[j, 1] && receive[k, 0] != receive[m, n] && receive[k, 0] != receive[m + 1, n] && receive[k, 0] != receive[m - 1, n]) && receive[k, 0] != 0 || (receive[k, 1] == receive[j, 0] && receive[k, 1] != receive[m, n] && receive[k, 1] != receive[m + 1, n] && receive[k, 1] != receive[m - 1, n]) && receive[k, 1] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 8 && ((receive[k, 0] == receive[j, 1] && receive[k, 0] != receive[m, n] && receive[k, 0] != receive[m - 2, n] && receive[k, 0] != receive[m - 1, n]) && receive[k, 0] != 0 || (receive[k, 1] == receive[j, 0] && receive[k, 1] != receive[m, n] && receive[k, 1] != receive[m - 2, n] && receive[k, 1] != receive[m - 1, n]) && receive[k, 1] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n / 3 == 1)
                                {
                                    if (n == 3)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (m == 6 && ((receive[k, 4] == receive[j, 5] && receive[k, 4] != receive[m, n] && receive[k, 4] != receive[m + 1, n] && receive[k, 4] != receive[m + 2, n]) && receive[k, 4] != 0 || (receive[k, 5] == receive[j, 4] && receive[k, 5] != receive[m, n] && receive[k, 5] != receive[m + 1, n] && receive[k, 5] != receive[m + 2, n]) && receive[k, 5] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 7 && ((receive[k, 4] == receive[j, 5] && receive[k, 4] != receive[m, n] && receive[k, 4] != receive[m + 1, n] && receive[k, 4] != receive[m - 1, n]) && receive[k, 4] != 0 || (receive[k, 5] == receive[j, 4] && receive[k, 5] != receive[m, n] && receive[k, 5] != receive[m + 1, n] && receive[k, 5] != receive[m - 1, n]) && receive[k, 5] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 8 && ((receive[k, 4] == receive[j, 5] && receive[k, 4] != receive[m, n] && receive[k, 4] != receive[m - 2, n] && receive[k, 4] != receive[m - 1, n]) && receive[k, 4] != 0 || (receive[k, 5] == receive[j, 4] && receive[k, 5] != receive[m, n] && receive[k, 5] != receive[m - 2, n] && receive[k, 5] != receive[m - 1, n]) && receive[k, 5] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (n == 4)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (m == 6 && ((receive[k, 5] == receive[j, 3] && receive[k, 5] != receive[m, n] && receive[k, 5] != receive[m + 1, n] && receive[k, 5] != receive[m + 2, n]) && receive[k, 5] != 0 || (receive[k, 3] == receive[j, 5] && receive[k, 3] != receive[m, n] && receive[k, 3] != receive[m + 1, n] && receive[k, 3] != receive[m + 2, n]) && receive[k, 3] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 7 && ((receive[k, 5] == receive[j, 3] && receive[k, 5] != receive[m, n] && receive[k, 5] != receive[m + 1, n] && receive[k, 5] != receive[m - 1, n]) && receive[k, 5] != 0 || (receive[k, 3] == receive[j, 5] && receive[k, 3] != receive[m, n] && receive[k, 3] != receive[m + 1, n] && receive[k, 3] != receive[m - 1, n]) && receive[k, 3] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 8 && ((receive[k, 5] == receive[j, 3] && receive[k, 5] != receive[m, n] && receive[k, 5] != receive[m - 2, n] && receive[k, 5] != receive[m - 1, n]) && receive[k, 5] != 0 || (receive[k, 3] == receive[j, 5] && receive[k, 3] != receive[m, n] && receive[k, 3] != receive[m - 2, n] && receive[k, 3] != receive[m - 1, n]) && receive[k, 3] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (n == 5)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (m == 6 && ((receive[k, 4] == receive[j, 3] && receive[k, 4] != receive[m, n] && receive[k, 4] != receive[m + 1, n] && receive[k, 4] != receive[m + 2, n]) && receive[k, 4] != 0 || (receive[k, 3] == receive[j, 4] && receive[k, 3] != receive[m, n] && receive[k, 3] != receive[m + 1, n] && receive[k, 3] != receive[m + 2, n]) && receive[k, 3] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 7 && ((receive[k, 4] == receive[j, 3] && receive[k, 4] != receive[m, n] && receive[k, 4] != receive[m + 1, n] && receive[k, 4] != receive[m - 1, n]) && receive[k, 4] != 0 || (receive[k, 3] == receive[j, 4] && receive[k, 3] != receive[m, n] && receive[k, 3] != receive[m + 1, n] && receive[k, 3] != receive[m - 1, n]) && receive[k, 3] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 8 && ((receive[k, 4] == receive[j, 3] && receive[k, 4] != receive[m, n] && receive[k, 4] != receive[m - 2, n] && receive[k, 4] != receive[m - 1, n]) && receive[k, 4] != 0 || (receive[k, 3] == receive[j, 4] && receive[k, 3] != receive[m, n] && receive[k, 3] != receive[m - 2, n] && receive[k, 3] != receive[m - 1, n]) && receive[k, 3] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n / 3 == 2)
                                {
                                    if (n == 6)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (m == 6 && ((receive[k, 7] == receive[j, 8] && receive[k, 7] != receive[m, n] && receive[k, 7] != receive[m + 1, n] && receive[k, 7] != receive[m + 2, n]) && receive[k, 7] != 0 || (receive[k, 8] == receive[j, 7] && receive[k, 8] != receive[m, n] && receive[k, 8] != receive[m + 1, n] && receive[k, 8] != receive[m + 2, n]) && receive[k, 8] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 7 && ((receive[k, 7] == receive[j, 8] && receive[k, 7] != receive[m, n] && receive[k, 7] != receive[m + 1, n] && receive[k, 7] != receive[m - 1, n]) && receive[k, 7] != 0 || (receive[k, 8] == receive[j, 7] && receive[k, 8] != receive[m, n] && receive[k, 8] != receive[m + 1, n] && receive[k, 8] != receive[m - 1, n]) && receive[k, 8] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 8 && ((receive[k, 7] == receive[j, 8] && receive[k, 7] != receive[m, n] && receive[k, 7] != receive[m - 2, n] && receive[k, 7] != receive[m - 1, n]) && receive[k, 7] != 0 || (receive[k, 8] == receive[j, 7] && receive[k, 8] != receive[m, n] && receive[k, 8] != receive[m - 2, n] && receive[k, 8] != receive[m - 1, n]) && receive[k, 8] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (n == 7)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (m == 6 && ((receive[k, 6] == receive[j, 8] && receive[k, 6] != receive[m, n] && receive[k, 6] != receive[m + 1, n] && receive[k, 6] != receive[m + 2, n]) && receive[k, 6] != 0 || (receive[k, 8] == receive[j, 6] && receive[k, 8] != receive[m, n] && receive[k, 8] != receive[m + 1, n] && receive[k, 8] != receive[m + 2, n]) && receive[k, 8] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 7 && ((receive[k, 6] == receive[j, 8] && receive[k, 6] != receive[m, n] && receive[k, 6] != receive[m + 1, n] && receive[k, 6] != receive[m - 1, n]) && receive[k, 6] != 0 || (receive[k, 8] == receive[j, 6] && receive[k, 8] != receive[m, n] && receive[k, 8] != receive[m + 1, n] && receive[k, 8] != receive[m - 1, n]) && receive[k, 8] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 8 && ((receive[k, 6] == receive[j, 8] && receive[k, 6] != receive[m, n] && receive[k, 6] != receive[m - 2, n] && receive[k, 6] != receive[m - 1, n]) && receive[k, 6] != 0 || (receive[k, 8] == receive[j, 6] && receive[k, 8] != receive[m, n] && receive[k, 8] != receive[m - 2, n] && receive[k, 8] != receive[m - 1, n]) && receive[k, 8] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                    if (n == 8)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            for (int j = 3; j < 6; j++)
                                            {
                                                if (m == 6 && ((receive[k, 6] == receive[j, 7] && receive[k, 6] != receive[m, n] && receive[k, 6] != receive[m + 1, n] && receive[k, 6] != receive[m + 2, n]) && receive[k, 6] != 0 || (receive[k, 7] == receive[j, 6] && receive[k, 7] != receive[m, n] && receive[k, 7] != receive[m + 1, n] && receive[k, 7] != receive[m + 2, n]) && receive[k, 7] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 7 && ((receive[k, 6] == receive[j, 7] && receive[k, 6] != receive[m, n] && receive[k, 6] != receive[m + 1, n] && receive[k, 6] != receive[m - 1, n]) && receive[k, 6] != 0 || (receive[k, 7] == receive[j, 6] && receive[k, 7] != receive[m, n] && receive[k, 7] != receive[m + 1, n] && receive[k, 7] != receive[m - 1, n]) && receive[k, 7] != 0))
                                                {
                                                    return 0;
                                                }
                                                if (m == 8 && ((receive[k, 6] == receive[j, 7] && receive[k, 6] != receive[m, n] && receive[k, 6] != receive[m - 2, n] && receive[k, 6] != receive[m - 1, n]) && receive[k, 6] != 0 || (receive[k, 7] == receive[j, 6] && receive[k, 7] != receive[m, n] && receive[k, 7] != receive[m - 2, n] && receive[k, 7] != receive[m - 1, n]) && receive[k, 7] != 0))
                                                {
                                                    return 0;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return -1;
        }
        public static int RoundRemove()
        {
            for (int m = 0; m < 9; m++)
            {
                for (int n = 0; n < 9; n++)
                {
                    if (receive[m, n] != 0)
                    {
                        if (m / 3 == 0)
                        {
                            if (m == 0)
                            {
                                if (n == 0)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[1, k] == receive[2, j] && receive[1, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 2] && receive[i, 1] == receive[1, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 2] && receive[t, 1] == receive[1, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[2, k] == receive[1, j] && receive[2, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 2] && receive[i, 1] == receive[2, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 2] && receive[t, 1] == receive[2, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 1)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[1, k] == receive[2, j] && receive[1, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 0] == receive[t, 2] && receive[i, 0] == receive[1, k] && receive[i, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 0] == receive[i, 2] && receive[t, 0] == receive[1, k] && receive[t, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[2, k] == receive[1, j] && receive[2, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 0] == receive[t, 2] && receive[i, 0] == receive[2, k] && receive[i, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 0] == receive[i, 2] && receive[t, 0] == receive[2, k] && receive[t, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 2)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[1, k] == receive[2, j] && receive[1, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 0] && receive[i, 1] == receive[1, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 0] && receive[t, 1] == receive[1, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[2, k] == receive[1, j] && receive[2, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 0] && receive[i, 1] == receive[2, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 0] && receive[t, 1] == receive[2, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 3)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[1, k] == receive[2, j] && receive[1, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 5] && receive[i, 4] == receive[1, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 5] && receive[t, 4] == receive[1, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[2, k] == receive[1, j] && receive[2, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 5] && receive[i, 4] == receive[2, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 5] && receive[t, 4] == receive[2, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 4)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[1, k] == receive[2, j] && receive[1, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 5] == receive[t, 3] && receive[i, 5] == receive[1, k] && receive[i, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 5] == receive[i, 3] && receive[t, 5] == receive[1, k] && receive[t, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[2, k] == receive[1, j] && receive[2, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 5] == receive[t, 3] && receive[i, 5] == receive[2, k] && receive[i, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 5] == receive[i, 3] && receive[t, 5] == receive[2, k] && receive[t, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 5)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[1, k] == receive[2, j] && receive[1, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 3] && receive[i, 4] == receive[1, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 3] && receive[t, 4] == receive[1, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[2, k] == receive[1, j] && receive[2, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 3] && receive[i, 4] == receive[2, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 3] && receive[t, 4] == receive[2, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 6)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[1, k] == receive[2, j] && receive[1, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 7] && receive[i, 8] == receive[1, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 7] && receive[t, 8] == receive[1, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[2, k] == receive[1, j] && receive[2, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 7] && receive[i, 8] == receive[2, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 7] && receive[t, 8] == receive[2, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 7)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[1, k] == receive[2, j] && receive[1, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 6] && receive[i, 8] == receive[1, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 6] && receive[t, 8] == receive[1, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[2, k] == receive[1, j] && receive[2, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 6] && receive[i, 8] == receive[2, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 6] && receive[t, 8] == receive[2, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 8)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[1, k] == receive[2, j] && receive[1, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 7] == receive[t, 6] && receive[i, 7] == receive[1, k] && receive[i, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 7] == receive[i, 6] && receive[t, 7] == receive[1, k] && receive[t, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[2, k] == receive[1, j] && receive[2, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 7] == receive[t, 6] && receive[i, 7] == receive[2, k] && receive[i, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 7] == receive[i, 6] && receive[t, 7] == receive[2, k] && receive[t, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (m == 1)
                            {
                                if (n == 0)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[0, k] == receive[2, j] && receive[0, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 2] && receive[i, 1] == receive[0, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 2] && receive[t, 1] == receive[0, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[2, k] == receive[0, j] && receive[2, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 2] && receive[i, 1] == receive[2, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 2] && receive[t, 1] == receive[2, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 1)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[0, k] == receive[2, j] && receive[0, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 0] == receive[t, 2] && receive[i, 0] == receive[0, k] && receive[i, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 0] == receive[i, 2] && receive[t, 0] == receive[0, k] && receive[t, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[2, k] == receive[0, j] && receive[2, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 0] == receive[t, 2] && receive[i, 0] == receive[2, k] && receive[i, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 0] == receive[i, 2] && receive[t, 0] == receive[2, k] && receive[t, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 2)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[0, k] == receive[2, j] && receive[0, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 0] && receive[i, 1] == receive[0, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 0] && receive[t, 1] == receive[0, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[2, k] == receive[0, j] && receive[2, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 0] && receive[i, 1] == receive[2, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 0] && receive[t, 1] == receive[2, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 3)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[0, k] == receive[2, j] && receive[0, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 5] && receive[i, 4] == receive[0, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 5] && receive[t, 4] == receive[0, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[2, k] == receive[0, j] && receive[2, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 5] && receive[i, 4] == receive[2, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 5] && receive[t, 4] == receive[2, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 4)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[0, k] == receive[2, j] && receive[0, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 5] == receive[t, 3] && receive[i, 5] == receive[0, k] && receive[i, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 5] == receive[i, 3] && receive[t, 5] == receive[0, k] && receive[t, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[2, k] == receive[0, j] && receive[2, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 5] == receive[t, 3] && receive[i, 5] == receive[2, k] && receive[i, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 5] == receive[i, 3] && receive[t, 5] == receive[2, k] && receive[t, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 5)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[0, k] == receive[2, j] && receive[0, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 3] && receive[i, 4] == receive[0, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 3] && receive[t, 4] == receive[0, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[2, k] == receive[0, j] && receive[2, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 3] && receive[i, 4] == receive[2, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 3] && receive[t, 4] == receive[2, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 6)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[0, k] == receive[2, j] && receive[0, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 7] && receive[i, 8] == receive[0, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 7] && receive[t, 8] == receive[0, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[2, k] == receive[0, j] && receive[2, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 7] && receive[i, 8] == receive[2, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 7] && receive[t, 8] == receive[2, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 7)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[0, k] == receive[2, j] && receive[0, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 6] && receive[i, 8] == receive[0, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 6] && receive[t, 8] == receive[0, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[2, k] == receive[0, j] && receive[2, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 6] && receive[i, 8] == receive[2, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 6] && receive[t, 8] == receive[2, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 8)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[0, k] == receive[2, j] && receive[0, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 7] == receive[t, 6] && receive[i, 7] == receive[0, k] && receive[i, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 7] == receive[i, 6] && receive[t, 7] == receive[0, k] && receive[t, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[2, k] == receive[0, j] && receive[2, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 7] == receive[t, 6] && receive[i, 7] == receive[2, k] && receive[i, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 7] == receive[i, 6] && receive[t, 7] == receive[2, k] && receive[t, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (m == 2)
                            {
                                if (n == 0)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[0, k] == receive[1, j] && receive[0, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 2] && receive[i, 1] == receive[0, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 2] && receive[t, 1] == receive[0, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[1, k] == receive[0, j] && receive[1, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 2] && receive[i, 1] == receive[1, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 2] && receive[t, 1] == receive[1, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 1)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[0, k] == receive[1, j] && receive[0, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 0] == receive[t, 2] && receive[i, 0] == receive[0, k] && receive[i, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 0] == receive[i, 2] && receive[t, 0] == receive[0, k] && receive[t, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[1, k] == receive[0, j] && receive[1, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 0] == receive[t, 2] && receive[i, 0] == receive[1, k] && receive[i, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 0] == receive[i, 2] && receive[t, 0] == receive[1, k] && receive[t, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 2)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[0, k] == receive[1, j] && receive[0, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 0] && receive[i, 1] == receive[0, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 0] && receive[t, 1] == receive[0, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[1, k] == receive[0, j] && receive[1, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 0] && receive[i, 1] == receive[1, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 0] && receive[t, 1] == receive[1, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 3)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[0, k] == receive[1, j] && receive[0, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 5] && receive[i, 4] == receive[0, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 5] && receive[t, 4] == receive[0, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[1, k] == receive[0, j] && receive[1, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 5] && receive[i, 4] == receive[1, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 5] && receive[t, 4] == receive[1, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 4)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[0, k] == receive[1, j] && receive[0, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 5] == receive[t, 3] && receive[i, 5] == receive[0, k] && receive[i, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 5] == receive[i, 3] && receive[t, 5] == receive[0, k] && receive[t, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[1, k] == receive[0, j] && receive[1, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 5] == receive[t, 3] && receive[i, 5] == receive[1, k] && receive[i, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 5] == receive[i, 3] && receive[t, 5] == receive[1, k] && receive[t, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 5)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[0, k] == receive[1, j] && receive[0, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 3] && receive[i, 4] == receive[0, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 3] && receive[t, 4] == receive[0, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[1, k] == receive[0, j] && receive[1, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 3] && receive[i, 4] == receive[1, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 3] && receive[t, 4] == receive[1, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 6)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[0, k] == receive[1, j] && receive[0, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 7] && receive[i, 8] == receive[0, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 7] && receive[t, 8] == receive[0, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[1, k] == receive[0, j] && receive[1, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 7] && receive[i, 8] == receive[1, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 7] && receive[t, 8] == receive[1, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 7)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[0, k] == receive[1, j] && receive[0, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 6] && receive[i, 8] == receive[0, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 6] && receive[t, 8] == receive[0, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[1, k] == receive[0, j] && receive[1, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 6] && receive[i, 8] == receive[1, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 6] && receive[t, 8] == receive[1, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 8)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[0, k] == receive[1, j] && receive[0, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 7] == receive[t, 6] && receive[i, 7] == receive[0, k] && receive[i, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 7] == receive[i, 6] && receive[t, 7] == receive[0, k] && receive[t, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[1, k] == receive[0, j] && receive[1, k] != 0)
                                            {
                                                for (int i = 3; i < 6; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 7] == receive[t, 6] && receive[i, 7] == receive[1, k] && receive[i, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 7] == receive[i, 6] && receive[t, 7] == receive[1, k] && receive[t, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (m / 3 == 1)
                        {
                            if (m == 3)
                            {
                                if (n == 0)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[4, k] == receive[5, j] && receive[4, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 2] && receive[i, 1] == receive[4, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 2] && receive[t, 1] == receive[4, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[5, k] == receive[4, j] && receive[5, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 2] && receive[i, 1] == receive[5, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 2] && receive[t, 1] == receive[5, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 1)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[4, k] == receive[5, j] && receive[4, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 0] == receive[t, 2] && receive[i, 0] == receive[4, k] && receive[i, 0] != receive[m, n])//2222
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 0] == receive[i, 2] && receive[t, 0] == receive[4, k] && receive[t, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[5, k] == receive[4, j] && receive[5, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 0] == receive[t, 2] && receive[i, 0] == receive[5, k] && receive[i, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 0] == receive[i, 2] && receive[t, 0] == receive[5, k] && receive[t, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 2)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[4, k] == receive[5, j] && receive[4, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 0] && receive[i, 1] == receive[4, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;    //哈哈哈哈啊哈哈哈哈事实上受到损失达到
                                                        }
                                                        if (receive[t, 1] == receive[i, 0] && receive[t, 1] == receive[4, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[5, k] == receive[4, j] && receive[5, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 0] && receive[i, 1] == receive[5, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 0] && receive[t, 1] == receive[5, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 3)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[4, k] == receive[5, j] && receive[4, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 5] && receive[i, 4] == receive[4, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 5] && receive[t, 4] == receive[4, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[5, k] == receive[4, j] && receive[5, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 5] && receive[i, 4] == receive[5, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 5] && receive[t, 4] == receive[5, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 4)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[4, k] == receive[5, j] && receive[4, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 5] == receive[t, 3] && receive[i, 5] == receive[4, k] && receive[i, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 5] == receive[i, 3] && receive[t, 5] == receive[4, k] && receive[t, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[5, k] == receive[4, j] && receive[5, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 5] == receive[t, 3] && receive[i, 5] == receive[5, k] && receive[i, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 5] == receive[i, 3] && receive[t, 5] == receive[5, k] && receive[t, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 5)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[4, k] == receive[5, j] && receive[4, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 3] && receive[i, 4] == receive[4, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 3] && receive[t, 4] == receive[4, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[5, k] == receive[4, j] && receive[5, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 3] && receive[i, 4] == receive[5, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 3] && receive[t, 4] == receive[5, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 6)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[4, k] == receive[5, j] && receive[4, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 7] && receive[i, 8] == receive[4, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 7] && receive[t, 8] == receive[4, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[5, k] == receive[4, j] && receive[5, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 7] && receive[i, 8] == receive[5, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 7] && receive[t, 8] == receive[5, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 7)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[4, k] == receive[5, j] && receive[4, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 6] && receive[i, 8] == receive[4, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 6] && receive[t, 8] == receive[4, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[5, k] == receive[4, j] && receive[5, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 6] && receive[i, 8] == receive[5, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 6] && receive[t, 8] == receive[5, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 8)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[4, k] == receive[5, j] && receive[4, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 7] == receive[t, 6] && receive[i, 7] == receive[4, k] && receive[i, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 7] == receive[i, 6] && receive[t, 7] == receive[4, k] && receive[t, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[5, k] == receive[4, j] && receive[5, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 7] == receive[t, 6] && receive[i, 7] == receive[5, k] && receive[i, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 7] == receive[i, 6] && receive[t, 7] == receive[5, k] && receive[t, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (m == 4)
                            {
                                if (n == 0)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[3, k] == receive[5, j] && receive[3, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 2] && receive[i, 1] == receive[3, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 2] && receive[t, 1] == receive[3, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[5, k] == receive[3, j] && receive[5, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 2] && receive[i, 1] == receive[5, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 2] && receive[t, 1] == receive[5, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 1)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[3, k] == receive[5, j] && receive[3, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 0] == receive[t, 2] && receive[i, 0] == receive[3, k] && receive[i, 0] != receive[m, n])//2222
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 0] == receive[i, 2] && receive[t, 0] == receive[3, k] && receive[t, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[5, k] == receive[3, j] && receive[5, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 0] == receive[t, 2] && receive[i, 0] == receive[5, k] && receive[i, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 0] == receive[i, 2] && receive[t, 0] == receive[5, k] && receive[t, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 2)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[3, k] == receive[5, j] && receive[3, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 0] && receive[i, 1] == receive[3, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;    //哈哈哈哈啊哈哈哈哈事实上受到损失达到
                                                        }
                                                        if (receive[t, 1] == receive[i, 0] && receive[t, 1] == receive[3, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[5, k] == receive[3, j] && receive[5, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 0] && receive[i, 1] == receive[5, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 0] && receive[t, 1] == receive[5, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 3)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[3, k] == receive[5, j] && receive[3, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 5] && receive[i, 4] == receive[3, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 5] && receive[t, 4] == receive[3, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[5, k] == receive[3, j] && receive[5, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 5] && receive[i, 4] == receive[5, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 5] && receive[t, 4] == receive[5, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 4)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[3, k] == receive[5, j] && receive[3, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 5] == receive[t, 3] && receive[i, 5] == receive[3, k] && receive[i, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 5] == receive[i, 3] && receive[t, 5] == receive[3, k] && receive[t, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[5, k] == receive[3, j] && receive[5, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 5] == receive[t, 3] && receive[i, 5] == receive[5, k] && receive[i, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 5] == receive[i, 3] && receive[t, 5] == receive[5, k] && receive[t, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 5)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[3, k] == receive[5, j] && receive[3, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 3] && receive[i, 4] == receive[3, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 3] && receive[t, 4] == receive[3, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[5, k] == receive[3, j] && receive[5, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 3] && receive[i, 4] == receive[5, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 3] && receive[t, 4] == receive[5, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 6)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[3, k] == receive[5, j] && receive[3, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 7] && receive[i, 8] == receive[3, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 7] && receive[t, 8] == receive[3, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[5, k] == receive[3, j] && receive[5, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 7] && receive[i, 8] == receive[5, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 7] && receive[t, 8] == receive[5, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 7)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[3, k] == receive[5, j] && receive[3, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 6] && receive[i, 8] == receive[3, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 6] && receive[t, 8] == receive[3, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[5, k] == receive[3, j] && receive[5, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 6] && receive[i, 8] == receive[5, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 6] && receive[t, 8] == receive[5, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 8)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[3, k] == receive[5, j] && receive[3, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 7] == receive[t, 6] && receive[i, 7] == receive[3, k] && receive[i, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 7] == receive[i, 6] && receive[t, 7] == receive[3, k] && receive[t, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[5, k] == receive[3, j] && receive[5, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 7] == receive[t, 6] && receive[i, 7] == receive[5, k] && receive[i, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 7] == receive[i, 6] && receive[t, 7] == receive[5, k] && receive[t, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (m == 5)
                            {
                                if (n == 0)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[4, k] == receive[3, j] && receive[4, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 2] && receive[i, 1] == receive[4, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 2] && receive[t, 1] == receive[4, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[3, k] == receive[4, j] && receive[3, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 2] && receive[i, 1] == receive[3, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 2] && receive[t, 1] == receive[3, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 1)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[4, k] == receive[3, j] && receive[4, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 0] == receive[t, 2] && receive[i, 0] == receive[4, k] && receive[i, 0] != receive[m, n])//2222
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 0] == receive[i, 2] && receive[t, 0] == receive[4, k] && receive[t, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[3, k] == receive[4, j] && receive[3, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 0] == receive[t, 2] && receive[i, 0] == receive[3, k] && receive[i, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 0] == receive[i, 2] && receive[t, 0] == receive[3, k] && receive[t, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 2)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[4, k] == receive[3, j] && receive[4, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 0] && receive[i, 1] == receive[4, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 0] && receive[t, 1] == receive[4, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[3, k] == receive[4, j] && receive[3, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 0] && receive[i, 1] == receive[3, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 0] && receive[t, 1] == receive[3, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 3)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[4, k] == receive[3, j] && receive[4, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 5] && receive[i, 4] == receive[4, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 5] && receive[t, 4] == receive[4, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[3, k] == receive[4, j] && receive[3, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 5] && receive[i, 4] == receive[3, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 5] && receive[t, 4] == receive[3, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 4)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[4, k] == receive[3, j] && receive[4, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 5] == receive[t, 3] && receive[i, 5] == receive[4, k] && receive[i, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 5] == receive[i, 3] && receive[t, 5] == receive[4, k] && receive[t, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[3, k] == receive[4, j] && receive[3, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 5] == receive[t, 3] && receive[i, 5] == receive[3, k] && receive[i, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 5] == receive[i, 3] && receive[t, 5] == receive[3, k] && receive[t, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 5)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[4, k] == receive[3, j] && receive[4, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 3] && receive[i, 4] == receive[4, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 3] && receive[t, 4] == receive[4, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[3, k] == receive[4, j] && receive[3, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 3] && receive[i, 4] == receive[3, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 3] && receive[t, 4] == receive[3, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 6)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[4, k] == receive[3, j] && receive[4, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 7] && receive[i, 8] == receive[4, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 7] && receive[t, 8] == receive[4, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[3, k] == receive[4, j] && receive[3, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 7] && receive[i, 8] == receive[3, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 7] && receive[t, 8] == receive[3, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 7)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[4, k] == receive[3, j] && receive[4, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 6] && receive[i, 8] == receive[4, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 6] && receive[t, 8] == receive[4, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[3, k] == receive[4, j] && receive[3, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 6] && receive[i, 8] == receive[3, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 6] && receive[t, 8] == receive[3, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 8)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[4, k] == receive[3, j] && receive[4, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 7] == receive[t, 6] && receive[i, 7] == receive[4, k] && receive[i, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 7] == receive[i, 6] && receive[t, 7] == receive[4, k] && receive[t, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[3, k] == receive[4, j] && receive[3, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 6; t < 9; t++)
                                                    {
                                                        if (receive[i, 7] == receive[t, 6] && receive[i, 7] == receive[3, k] && receive[i, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 7] == receive[i, 6] && receive[t, 7] == receive[3, k] && receive[t, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (m / 3 == 2)
                        {
                            if (m == 6)
                            {
                                if (n == 0)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[7, k] == receive[8, j] && receive[7, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 2] && receive[i, 1] == receive[7, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 2] && receive[t, 1] == receive[7, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[8, k] == receive[7, j] && receive[8, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 2] && receive[i, 1] == receive[8, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 2] && receive[t, 1] == receive[8, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 1)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[7, k] == receive[8, j] && receive[7, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 0] == receive[t, 2] && receive[i, 0] == receive[7, k] && receive[i, 0] != receive[m, n])//2222
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 0] == receive[i, 2] && receive[t, 0] == receive[7, k] && receive[t, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[8, k] == receive[7, j] && receive[8, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 0] == receive[t, 2] && receive[i, 0] == receive[8, k] && receive[i, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 0] == receive[i, 2] && receive[t, 0] == receive[8, k] && receive[t, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 2)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[7, k] == receive[8, j] && receive[7, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 0] && receive[i, 1] == receive[7, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;    //哈哈哈哈啊哈哈哈哈事实上受到损失达到
                                                        }
                                                        if (receive[t, 1] == receive[i, 0] && receive[t, 1] == receive[7, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[8, k] == receive[7, j] && receive[8, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 0] && receive[i, 1] == receive[8, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 0] && receive[t, 1] == receive[8, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 3)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[7, k] == receive[8, j] && receive[7, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 5] && receive[i, 4] == receive[7, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 5] && receive[t, 4] == receive[7, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[8, k] == receive[7, j] && receive[8, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 5] && receive[i, 4] == receive[8, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 5] && receive[t, 4] == receive[8, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 4)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[7, k] == receive[8, j] && receive[7, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 5] == receive[t, 3] && receive[i, 5] == receive[7, k] && receive[i, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 5] == receive[i, 3] && receive[t, 5] == receive[7, k] && receive[t, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[8, k] == receive[7, j] && receive[8, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 5] == receive[t, 3] && receive[i, 5] == receive[8, k] && receive[i, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 5] == receive[i, 3] && receive[t, 5] == receive[8, k] && receive[t, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 5)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[7, k] == receive[8, j] && receive[7, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 3] && receive[i, 4] == receive[7, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 3] && receive[t, 4] == receive[7, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[8, k] == receive[7, j] && receive[8, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 3] && receive[i, 4] == receive[8, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 3] && receive[t, 4] == receive[8, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 6)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[7, k] == receive[8, j] && receive[7, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 7] && receive[i, 8] == receive[7, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 7] && receive[t, 8] == receive[7, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[8, k] == receive[7, j] && receive[8, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 7] && receive[i, 8] == receive[8, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 7] && receive[t, 8] == receive[8, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 7)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[7, k] == receive[8, j] && receive[7, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 6] && receive[i, 8] == receive[7, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 6] && receive[t, 8] == receive[7, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[8, k] == receive[7, j] && receive[8, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 6] && receive[i, 8] == receive[8, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 6] && receive[t, 8] == receive[8, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 8)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[7, k] == receive[8, j] && receive[7, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 7] == receive[t, 6] && receive[i, 7] == receive[7, k] && receive[i, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 7] == receive[i, 6] && receive[t, 7] == receive[7, k] && receive[t, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[8, k] == receive[7, j] && receive[8, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 7] == receive[t, 6] && receive[i, 7] == receive[8, k] && receive[i, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 7] == receive[i, 6] && receive[t, 7] == receive[8, k] && receive[t, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (m == 7)
                            {
                                if (n == 0)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[6, k] == receive[8, j] && receive[6, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 2] && receive[i, 1] == receive[6, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 2] && receive[t, 1] == receive[6, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[8, k] == receive[6, j] && receive[8, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 2] && receive[i, 1] == receive[8, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 2] && receive[t, 1] == receive[8, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 1)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[6, k] == receive[8, j] && receive[6, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 0] == receive[t, 2] && receive[i, 0] == receive[6, k] && receive[i, 0] != receive[m, n])//2222
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 0] == receive[i, 2] && receive[t, 0] == receive[6, k] && receive[t, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[8, k] == receive[6, j] && receive[8, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 0] == receive[t, 2] && receive[i, 0] == receive[8, k] && receive[i, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 0] == receive[i, 2] && receive[t, 0] == receive[8, k] && receive[t, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 2)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[6, k] == receive[8, j] && receive[6, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 0] && receive[i, 1] == receive[6, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 0] && receive[t, 1] == receive[6, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[8, k] == receive[6, j] && receive[8, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 0] && receive[i, 1] == receive[8, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 0] && receive[t, 1] == receive[8, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 3)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[6, k] == receive[8, j] && receive[6, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 5] && receive[i, 4] == receive[6, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 5] && receive[t, 4] == receive[6, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[8, k] == receive[6, j] && receive[8, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 5] && receive[i, 4] == receive[8, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 5] && receive[t, 4] == receive[8, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 4)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[6, k] == receive[8, j] && receive[6, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 5] == receive[t, 3] && receive[i, 5] == receive[6, k] && receive[i, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 5] == receive[i, 3] && receive[t, 5] == receive[6, k] && receive[t, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[8, k] == receive[6, j] && receive[8, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 5] == receive[t, 3] && receive[i, 5] == receive[8, k] && receive[i, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 5] == receive[i, 3] && receive[t, 5] == receive[8, k] && receive[t, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 5)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[6, k] == receive[8, j] && receive[6, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 3] && receive[i, 4] == receive[6, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 3] && receive[t, 4] == receive[6, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[8, k] == receive[6, j] && receive[8, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 3] && receive[i, 4] == receive[8, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 3] && receive[t, 4] == receive[8, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 6)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[6, k] == receive[8, j] && receive[6, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 7] && receive[i, 8] == receive[6, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 7] && receive[t, 8] == receive[6, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[8, k] == receive[6, j] && receive[8, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 7] && receive[i, 8] == receive[8, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 7] && receive[t, 8] == receive[8, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 7)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[6, k] == receive[8, j] && receive[6, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 6] && receive[i, 8] == receive[6, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 6] && receive[t, 8] == receive[6, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[8, k] == receive[6, j] && receive[8, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 6] && receive[i, 8] == receive[8, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 6] && receive[t, 8] == receive[8, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 8)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[6, k] == receive[8, j] && receive[6, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 7] == receive[t, 6] && receive[i, 7] == receive[6, k] && receive[i, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 7] == receive[i, 6] && receive[t, 7] == receive[6, k] && receive[t, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[8, k] == receive[6, j] && receive[8, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 7] == receive[t, 6] && receive[i, 7] == receive[8, k] && receive[i, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 7] == receive[i, 6] && receive[t, 7] == receive[8, k] && receive[t, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (m == 8)
                            {
                                if (n == 0)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[7, k] == receive[6, j] && receive[7, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 2] && receive[i, 1] == receive[7, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 2] && receive[t, 1] == receive[7, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[6, k] == receive[7, j] && receive[6, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 2] && receive[i, 1] == receive[6, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 2] && receive[t, 1] == receive[6, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 1)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[7, k] == receive[6, j] && receive[7, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 0] == receive[t, 2] && receive[i, 0] == receive[7, k] && receive[i, 0] != receive[m, n])//2222
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 0] == receive[i, 2] && receive[t, 0] == receive[7, k] && receive[t, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[6, k] == receive[7, j] && receive[6, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 0] == receive[t, 2] && receive[i, 0] == receive[6, k] && receive[i, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 0] == receive[i, 2] && receive[t, 0] == receive[6, k] && receive[t, 0] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 2)
                                {
                                    for (int k = 3; k < 6; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[7, k] == receive[6, j] && receive[7, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 0] && receive[i, 1] == receive[7, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 0] && receive[t, 1] == receive[7, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[6, k] == receive[7, j] && receive[6, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 1] == receive[t, 0] && receive[i, 1] == receive[6, k] && receive[i, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 1] == receive[i, 0] && receive[t, 1] == receive[6, k] && receive[t, 1] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 3)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[7, k] == receive[6, j] && receive[7, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 5] && receive[i, 4] == receive[7, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 5] && receive[t, 4] == receive[7, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[6, k] == receive[7, j] && receive[6, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 5] && receive[i, 4] == receive[6, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 5] && receive[t, 4] == receive[6, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 4)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[7, k] == receive[6, j] && receive[7, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 5] == receive[t, 3] && receive[i, 5] == receive[7, k] && receive[i, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 5] == receive[i, 3] && receive[t, 5] == receive[7, k] && receive[t, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[6, k] == receive[7, j] && receive[6, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 5] == receive[t, 3] && receive[i, 5] == receive[6, k] && receive[i, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 5] == receive[i, 3] && receive[t, 5] == receive[6, k] && receive[t, 5] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 5)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 6; j < 9; j++)
                                        {
                                            if (receive[7, k] == receive[6, j] && receive[7, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 3] && receive[i, 4] == receive[7, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 3] && receive[t, 4] == receive[7, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[6, k] == receive[7, j] && receive[6, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 4] == receive[t, 3] && receive[i, 4] == receive[6, k] && receive[i, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 4] == receive[i, 3] && receive[t, 4] == receive[6, k] && receive[t, 4] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 6)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[7, k] == receive[6, j] && receive[7, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 7] && receive[i, 8] == receive[7, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 7] && receive[t, 8] == receive[7, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[6, k] == receive[7, j] && receive[6, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 7] && receive[i, 8] == receive[6, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 7] && receive[t, 8] == receive[6, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 7)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[7, k] == receive[6, j] && receive[7, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 6] && receive[i, 8] == receive[7, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 6] && receive[t, 8] == receive[7, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[6, k] == receive[7, j] && receive[6, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 8] == receive[t, 6] && receive[i, 8] == receive[6, k] && receive[i, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 8] == receive[i, 6] && receive[t, 8] == receive[6, k] && receive[t, 8] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (n == 8)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int j = 3; j < 6; j++)
                                        {
                                            if (receive[7, k] == receive[6, j] && receive[7, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 7] == receive[t, 6] && receive[i, 7] == receive[7, k] && receive[i, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 7] == receive[i, 6] && receive[t, 7] == receive[7, k] && receive[t, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                            if (receive[6, k] == receive[7, j] && receive[6, k] != 0)
                                            {
                                                for (int i = 0; i < 3; i++)
                                                {
                                                    for (int t = 3; t < 6; t++)
                                                    {
                                                        if (receive[i, 7] == receive[t, 6] && receive[i, 7] == receive[6, k] && receive[i, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                        if (receive[t, 7] == receive[i, 6] && receive[t, 7] == receive[6, k] && receive[t, 7] != receive[m, n])
                                                        {
                                                            return 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return -1;
        }
        public static int LongTime()
        {
            if (receive[0, 2] == 1 && receive[0, 6] == 5 && receive[1, 3] == 2 && receive[1, 5] == 4 && receive[2, 0] == 6 && receive[2, 4] == 3 && receive[2, 8] == 2 && receive[3, 1] == 7 && receive[3, 7] == 1 && receive[4, 2] == 8 && receive[4, 6] == 9 && receive[5, 1] == 9 && receive[5, 7] == 8 && receive[6, 0] == 1 && receive[6, 4] == 4 && receive[6, 8] == 7 && receive[7, 3] == 3 && receive[7, 5] == 5 && receive[8, 2] == 2 && receive[8, 6] == 6)
            {
                receive[0, 0] = 3;
                receive[0, 1] = 2;
                receive[0, 3] = 6;
                receive[0, 4] = 7;
                receive[0, 5] = 8;
                receive[0, 7] = 4;
                receive[0, 8] = 9;
                receive[1, 0] = 7;
                receive[1, 1] = 5;
                receive[1, 2] = 9;
                receive[1, 4] = 1;
                receive[1, 6] = 3;
                receive[1, 7] = 6;
                receive[1, 8] = 8;
                receive[2, 1] = 8;
                receive[2, 2] = 4;
                receive[2, 3] = 5;
                receive[2, 5] = 9;
                receive[2, 6] = 1;
                receive[2, 7] = 7;
                receive[3, 0] = 2;
                receive[3, 2] = 3;
                receive[3, 3] = 8;
                receive[3, 4] = 9;
                receive[3, 5] = 6;
                receive[3, 6] = 4;
                receive[3, 8] = 5;
                receive[4, 0] = 4;
                receive[4, 1] = 1;
                receive[4, 3] = 7;
                receive[4, 4] = 5;
                receive[4, 5] = 3;
                receive[4, 7] = 2;
                receive[4, 8] = 6;
                receive[5, 0] = 5;
                receive[5, 2] = 6;
                receive[5, 3] = 4;
                receive[5, 4] = 2;
                receive[5, 5] = 1;
                receive[5, 6] = 7;
                receive[5, 8] = 3;
                receive[6, 1] = 6;
                receive[6, 2] = 5;
                receive[6, 3] = 9;
                receive[6, 5] = 2;
                receive[6, 6] = 8;
                receive[6, 7] = 3;
                receive[7, 0] = 8;
                receive[7, 1] = 4;
                receive[7, 2] = 7;
                receive[7, 4] = 6;
                receive[7, 6] = 2;
                receive[7, 7] = 9;
                receive[7, 8] = 1;
                receive[8, 0] = 9;
                receive[8, 1] = 3;
                receive[8, 3] = 1;
                receive[8, 4] = 8;
                receive[8, 5] = 7;
                receive[8, 7] = 5;
                receive[8, 8] = 4;
                return 1;
            }
            return -1;
        }
    }
}
