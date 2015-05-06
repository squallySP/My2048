using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2048
{
    class GameBoard
    {
        public bool IsBusy { get; set; }

        public TileControl[,] gameCells = new TileControl[4,4];

        public List<TileControl> Tiles = new List<TileControl>();

        public GameBoard()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    gameCells[i, j] = null;
                }
            }

            for (int i = 0; i < 16; i++)
            {
                Tiles.Add(new TileControl());
            }
        }

        // 向Cell的随机空格添加tile
        public void AddTileToCells()
        {
            Random random = new Random();
            int i, j;
            int index = random.Next(Tiles.Count);
            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                    if (gameCells[i, j] == null)
                    {
                        if (index > 0)
                            index--;
                        else
                        {
                            gameCells[i, j] = Tiles[0];
                            Tiles.Remove(Tiles[0]);
                            gameCells[i, j].SetToCell(i, j);
                            return;
                        }
                    }
        }

        // 将标记了 toRecycle 的 tile 全部会受到 Tiles 中，同时将所有的tile移动到targetCell
        public void RecycleTiles()
        {
            TileControl[,] tempCells = new TileControl[4, 4];

            foreach (TileControl tile in gameCells)
            {
                if (tile != null)
                {
                    if (tile.toRecycle == true)
                    {
                        tile.Recycle();
                        Tiles.Add(tile);
                    }
                    else
                        tempCells[tile.TargetCell.X, tile.TargetCell.Y] = tile;
                }
            }

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    gameCells[i, j] = tempCells[i, j];
        }

        // 对所有标记了 toUpgrade的tile进行 Upgrade（）；
        public void Upgrade()
        {
            foreach (TileControl tile in gameCells)
            {
                if (tile != null && tile.toUpgrade == true)
                    tile.Upgrade();
            }
        }

        // 去掉tile中的各种标记，为下一轮移动做准备
        public void MoveOver()
        {
            foreach (TileControl tile in gameCells)
            {
                if (tile != null)
                    tile.MoveOver();
            }
        }

        // 检查游戏结束
        public bool CheckGemeOver()
        {
            if (Tiles.Count > 0)
                return false;

            // 检查是否有相邻的Cell中的Till.Value数值一样的
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (i < 2 && gameCells[i, j].Value == gameCells[i + 1, j].Value)
                        return false;
                    if (j < 2 && gameCells[i, j].Value == gameCells[i, j + 1].Value)
                        return false;
                }
            // 如果没有相邻等值的，则游戏结束
            return false;
        }

        public bool TryToMoveUp()
        {
            bool MoveSuccess = false;


            return false;
        }
    }
}
