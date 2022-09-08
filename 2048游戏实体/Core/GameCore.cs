using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    class GameCore
    {
        /// <summary>
        /// 字段
        /// </summary>
        private int[,] arr;//2048本体数组
        private int[] mergeArr;//合并时的辅助数组
        private int[] emptyArr;//去零时的辅助数组
        private Random random;
        private List<Index> list;
        private bool ischange;
        private int[,] arrSame;
        private MoveDirection direction;
        //public List<MoveData> moveDataList;
        //public List<Index> mergeDataList;
        public int moveRow, moveColumn;
        /// <summary>
        /// 属性
        /// </summary>
        public int[,] Arr
        {
            get { return this.arr; }
        }
        public bool IsChange
        {
            get { return ischange; }
            set { this.ischange = value; }
        }
        /// <summary>
        /// 构造器
        /// </summary>
        public GameCore()
        {
            arr = new int[4, 4];
            mergeArr = new int[4];
            emptyArr = new int[4];
            random = new Random();
            list = new List<Index>(16);
            arrSame = new int[4, 4];
            //moveDataList = new List<MoveData>(16);
            //mergeDataList = new List<Index>(16);       
        }
        /// <summary>
        /// 将0放到数组后
        /// </summary>
        private void Empty()
        {
            Array.Clear(emptyArr, 0, 4);
            int index = 0;
            for (int i = 0; i < mergeArr.Length; i++)
            {
                if (mergeArr[i] != 0)
                    emptyArr[index++] = mergeArr[i];
            }
            emptyArr.CopyTo(mergeArr, 0);
        }
        /// <summary>
        /// 相邻相同元素相加
        /// </summary>
        private void Merge()
        {
            //CreateMoveData();
            Empty();
            for (int i = 0; i < mergeArr.Length - 1; i++)
            {
                if (mergeArr[i] == mergeArr[i + 1])
                {
                    mergeArr[i] += mergeArr[i + 1];
                    mergeArr[i + 1] = 0;
                    //LogMergeLocation(i);
                }
            }
            Empty();
        }
        /// <summary>
        ///计算每个数据移动的方向
        /// </summary>
        //private void CreateMoveData()
        //{
        //    int zeroCount = 0;
        //    for (int i = 0; i < 4; i++)
        //    {
        //        if (mergeArr[i] == 0)
        //        {
        //            zeroCount++;
        //        }
        //        else
        //        {
        //            if (zeroCount != 0)
        //            {
        //                Index original = new Index();
        //                Index target = new Index();
        //                switch (direction)
        //                {
        //                    case MoveDirection.Up:
        //                        original = new Index(i, moveColumn);
        //                        target = new Index(i - zeroCount, moveColumn);
        //                        break;
        //                    case MoveDirection.Down:
        //                        original = new Index(3 - i, moveColumn);
        //                        target = new Index(3 - i + zeroCount, moveColumn);
        //                        break;
        //                    case MoveDirection.Left:
        //                        original = new Index(moveRow, i);
        //                        target = new Index(moveRow, i - zeroCount);
        //                        break;
        //                    case MoveDirection.Right:
        //                        original = new Index(moveRow, i);
        //                        target = new Index(moveRow, 3 - i + zeroCount);
        //                        break;
        //                }
        //                moveDataList.Add(new MoveData(original, target));
        //            }
        //        }
        //    }
        //}
        /// <summary>
        /// 记录要合并的坐标
        /// </summary>
        /// <param name="mergeIndex"></param>
        //private void LogMergeLocation(int mergeIndex)
        //{
        //    switch (direction)
        //    {
        //        case MoveDirection.Up:
        //            if (mergeIndex > 0 && mergeArr[mergeIndex - 1] == 0)
        //                mergeDataList.Add(new Index(mergeIndex - 1, moveColumn));
        //            else
        //                mergeDataList.Add(new Index(mergeIndex, moveColumn));
        //            break;
        //        case MoveDirection.Down:
        //            if (mergeIndex > 0 && mergeArr[mergeIndex - 1] == 0)
        //                mergeDataList.Add(new Index(4 - mergeIndex, moveColumn));
        //            else
        //                mergeDataList.Add(new Index(3 - mergeIndex, moveColumn));
        //            break;
        //        case MoveDirection.Left:
        //            if (mergeIndex > 0 && mergeArr[mergeIndex - 1] == 0)
        //                mergeDataList.Add(new Index(moveRow, mergeIndex - 1));
        //            else
        //                mergeDataList.Add(new Index(moveRow, mergeIndex));
        //            break;
        //        case MoveDirection.Right:
        //            if (mergeIndex > 0 && mergeArr[mergeIndex - 1] == 0)
        //                mergeDataList.Add(new Index(moveRow, 4 - mergeIndex));
        //            else
        //                mergeDataList.Add(new Index(moveRow, 3 - mergeIndex));
        //            break;
        //    }
        //}
        /// <summary>
        /// 向上移动
        /// </summary>
        private void Up()
        {
            for (moveColumn = 0; moveColumn < 4; moveColumn++)
            {
                for (int i = 0; i < 4; i++)
                    mergeArr[i] = arr[i, moveColumn];
                Merge();
                for (int i = 0; i < 4; i++)
                    arr[i, moveColumn] = mergeArr[i];
            }
        }
        /// <summary>
        /// 向下移动
        /// </summary>
        private void Down()
        {
            for (moveColumn = 0; moveColumn < 4; moveColumn++)
            {
                for (int i = 0; i < 4; i++)
                    mergeArr[i] = arr[3 - i, moveColumn];
                Merge();
                for (int i = 0; i < 4; i++)
                    arr[3 - i, moveColumn] = mergeArr[i];
            }

        }
        /// <summary>
        /// 向右移动
        /// </summary>
        private void Right()
        {
            for (moveRow = 0; moveRow < 4; moveRow++)
            {
                for (int i = 0; i < 4; i++)
                    mergeArr[i] = arr[moveRow, 3 - i];
                Merge();
                for (int i = 0; i < 4; i++)
                    arr[moveRow, 3 - i] = mergeArr[i];
            }
        }
        /// <summary>
        /// 向左移动
        /// </summary>
        private void Left()
        {
            for (moveRow = 0; moveRow < 4; moveRow++)
            {
                for (int i = 0; i < 4; i++)
                    mergeArr[i] = arr[moveRow, i];
                Merge();
                for (int i = 0; i < 4; i++)
                    arr[moveRow, i] = mergeArr[i];
            }

        }
        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="direction">枚举常量</param>
        public void Move(MoveDirection direction)
        {
            //moveDataList.Clear();
            //mergeDataList.Clear();
            this.direction = direction;
            Array.Copy(arr, arrSame, arr.Length);
            this.ischange = false;
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
                        this.ischange = true;
                        return;
                    }

                }
            }
        }
        /// <summary>
        /// 空白处随机产生2或者4
        /// </summary>
        public void RandomEmpty(out Index? location, out int? a)
        {
            list.Clear();
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] == 0)
                        list.Add(new Index(i, j));
                }
            }//计算还是0位置的地方
            if (list.Count > 0)
            {
                int i = random.Next(0, list.Count);
                location = list[i];
                a = arr[location.Value.RIndex, location.Value.CIndex] = (random.Next(0, 10) == 1) ? 4 : 2;
            }
            else
            {
                a = null;
                location = null;
            }
        }
        public bool successFlag = false, loseFlag = false;
        /// <summary>
        /// 判断游戏是否结束
        /// </summary>
        /// <returns></returns>
        public void Check()
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] == 2048)
                    {
                        successFlag = true;
                        return;
                    }
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (arr[i, j] == 0)
                        return;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (arr[i, j] == arr[i, j + 1] || arr[j, i] == arr[j + 1, i])
                    {
                        loseFlag = false;
                        return;
                    }
                }
            }
            loseFlag = true;
        }
        /// <summary>
        /// 游戏重新开始
        /// </summary>
        public void ReStart()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    arr[i, j] = 0;
                }
            }
            successFlag = false;
            loseFlag = false;
            score = 0;
        }
        public int score;
        /// <summary>
        /// 计算分数
        /// </summary>
        public void Score()
        {
            score = 0;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    score += arr[i, j];


        }
    }
}
