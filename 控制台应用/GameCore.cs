using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    class GameCore
    {
        //
        private int[,] arr;//2048
        private int[] arr1;//从arr中抽取出来的一维数组
        private int[] emptyArr;
        private Random random;
        private List<Index> list;
        private bool flag = false;
        private int[,] arrSame;
        //
        public int[,] Arr
        {
            get { return this.arr; }
        }
        public bool Flag
        {
            get { return flag; }
        }
        //
        public GameCore()
        {
            arr = new int[4, 4];
            arr1 = new int[4];
            emptyArr = new int[4];
            random = new Random();
            list = new List<Index>(16);
            arrSame = new int[4, 4];
        }
        //
        /// <summary>
        /// 将0放到数组后
        /// </summary>
        private void Empty()
        {
            Array.Clear(emptyArr, 0, 4);
            int index = 0;
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] != 0)
                    emptyArr[index++] = arr1[i];
            }
            emptyArr.CopyTo(arr1, 0);
        }
        /// <summary>
        /// 相邻相同元素相加
        /// </summary>
        private void Merge()
        {
            Empty();
            for (int i = 0; i < arr1.Length - 1; i++)
            {
                if (arr1[i] == arr1[i + 1])
                {
                    arr1[i] += arr1[i + 1];
                    arr1[i + 1] = 0;
                }
            }
            Empty();
        }
        /// <summary>
        /// 向上移动
        /// </summary>
        private void Up()
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                    arr1[i] = arr[i, j];
                Merge();
                for (int i = 0; i < 4; i++)
                    arr[i, j] = arr1[i];
            }
        }
        /// <summary>
        /// 向下移动
        /// </summary>
        private void Down()
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                    arr1[i] = arr[3 - i, j];
                Merge();
                for (int i = 0; i < 4; i++)
                    arr[3 - i, j] = arr1[i];
            }

        }
        /// <summary>
        /// 向右移动
        /// </summary>
        private void Right()
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                    arr1[i] = arr[j, 3 - i];
                Merge();
                for (int i = 0; i < 4; i++)
                    arr[j, 3 - i] = arr1[i];
            }
        }
        /// <summary>
        /// 向左移动
        /// </summary>
        private void Left()
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                    arr1[i] = arr[j, i];
                Merge();
                for (int i = 0; i < 4; i++)
                    arr[j, i] = arr1[i];
            }

        }
        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="direction">枚举常量</param>
        public void Move(MoveDirection direction)
        {
            Array.Copy(arr, arrSame, arr.Length);
            this.flag = false;
            switch (direction)
            {
                case MoveDirection.Up:
                    Up();
                    break;
                case MoveDirection.Down:
                    Down();
                    break;
                case MoveDirection.Left:
                    Left();
                    break;
                case MoveDirection.Right:
                    Right();
                    break;
            }
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arrSame[i, j] != arr[i, j])
                    {
                        this.flag = true;
                        return;
                    }

                }
            }
        }
        /// <summary>
        /// 空白处随机产生2或者4
        /// </summary>
        public void RandomEmpty()
        {
            list.Clear();
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] == 0)
                        list.Add(new Index(i, j));
                }
            }
            if (list.Count > 0)
            {
                int i = random.Next(0, list.Count);
                Index index = list[i];
                arr[index.RIndex, index.CIndex] = (random.Next(0, 10) == 1) ? 4 : 2;
            }
        }
        /// <summary>
        /// 判断游戏是否结束
        /// </summary>
        /// <returns></returns>
        public bool Check()
        {
            int flag = 0, num = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] == 2048)
                        flag = 1;
                    if (arr[i, j] == 0)
                    {
                        num++;
                    }
                }
            }
            if (num == 0)
            {
                flag = 1;
            }
            if (flag == 1)
                return false;
            return true;
        }
    }
}
