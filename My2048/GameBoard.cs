using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2048
{
    class GameBoard : INotifyPropertyChanged
    {
        public static int steps = 5;
        private int _bestScore;
        public int BestScore 
        {
            get { return _bestScore; }
            set
            {
                _bestScore = value;
                Notify("BestScore");
            }
        }
        private int _currentScore;
        public int CurrentScore
        {
            get { return _currentScore; }
            set
            {
                _currentScore = value;
                Notify("CurrentScore");
            }
        }

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

            BestScore = 0;
            CurrentScore = 0;
        }

        public void ReNew()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (gameCells[i, j] != null)
                    {
                        Tiles.Add(gameCells[i, j]);
                        gameCells[i, j] = null;
                    }
                }
            }


            //BestScore = 0;
            CurrentScore = 0;
        }


        // for INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void Notify(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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
                {
                    tile.Upgrade();
                    CurrentScore += tile.Value;
                    if (BestScore < CurrentScore)
                        BestScore = CurrentScore;
                }
                    
            }
        }

        // 去掉tile中的各种标记，为下一轮移动做准备
        public void MovementOver()
        {
            foreach (TileControl tile in gameCells)
            {
                if (tile != null)
                    tile.MovementOver();
            }
        }

        // 移动tiles
        public void Move() 
        {
            foreach (TileControl tile in gameCells)
                if (tile != null)
                {
                    tile.Move();
                    tile.CurrentCell = tile.TargetCell.Clone();
                }
                    
        }

        public void MoveOneStep()
        {
            foreach (TileControl tile in gameCells)
                if (tile != null)
                {
                    tile.MoveOneStep();
                }
        }

        // 检查游戏结束
        public bool IsGameOver()
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
            return true;
        }

        public bool TryToMoveUp()
        {
            bool canMove = false;

            for (int x = 0; x < 4; x++)
            {
                // move one line
                int yIndex = 0;
                TileControl lastTile = null;
                for (int y = 0; y < 4; y++)
                {
                    if (gameCells[x, y] != null)
                    {
                        if (lastTile != null && lastTile.Value == gameCells[x, y].Value && lastTile.toRecycle == false)
                        {
                            gameCells[x, y].TargetCell = lastTile.TargetCell.Clone();
                            gameCells[x, y].hasMoved = true;
                            gameCells[x, y].toRecycle = true;
                            lastTile.toUpgrade = true;
                            lastTile = gameCells[x, y];
                        }
                        else
                        {
                            gameCells[x, y].TargetCell = gameCells[x, y].CurrentCell.Clone();
                            gameCells[x, y].TargetCell.Y = yIndex;
                            yIndex += 1;
                            gameCells[x, y].hasMoved = true;
                            lastTile = gameCells[x, y];
                        }
                    }
                }
            }

            foreach (TileControl tile in gameCells)
                if (tile!=null && !tile.TargetCell.IsSameAs(tile.CurrentCell))
                    canMove = true;

            if (canMove)
                CalculateStep();

            return canMove;
        }

        public bool TryToMoveDown()
        {
            bool canMove = false;

            for (int x = 0; x < 4; x++)
            {
                // move one line
                int yIndex = 3;
                TileControl lastTile = null;
                for (int y = 3; y >= 0; y--)
                {
                    if (gameCells[x, y] != null)
                    {
                        if (lastTile != null && lastTile.Value == gameCells[x, y].Value && lastTile.toRecycle == false)
                        {
                            gameCells[x, y].TargetCell = lastTile.TargetCell.Clone();
                            gameCells[x, y].hasMoved = true;
                            gameCells[x, y].toRecycle = true;
                            lastTile.toUpgrade = true;
                            lastTile = gameCells[x, y];
                        }
                        else
                        {
                            gameCells[x, y].TargetCell = gameCells[x, y].CurrentCell.Clone();
                            gameCells[x, y].TargetCell.Y = yIndex;
                            yIndex -= 1;
                            gameCells[x, y].hasMoved = true;
                            lastTile = gameCells[x, y];
                        }
                    }
                }
            }

            foreach (TileControl tile in gameCells)
                if (tile != null && !tile.TargetCell.IsSameAs(tile.CurrentCell))
                    canMove = true;

            if (canMove)
                CalculateStep();

            return canMove;
        }

        public bool TryToMoveRight()
        {
            bool canMove = false;

            for (int y = 0; y < 4; y++)
            {
                // move one line
                int xIndex = 3;
                TileControl lastTile = null;
                for (int x = 3; x >= 0; x--)
                {
                    if (gameCells[x, y] != null)
                    {
                        if (lastTile != null && lastTile.Value == gameCells[x, y].Value && lastTile.toRecycle == false)
                        {
                            gameCells[x, y].TargetCell = lastTile.TargetCell.Clone();
                            gameCells[x, y].hasMoved = true;
                            gameCells[x, y].toRecycle = true;
                            lastTile.toUpgrade = true;
                            lastTile = gameCells[x, y];
                        }
                        else
                        {
                            gameCells[x, y].TargetCell = gameCells[x, y].CurrentCell.Clone();
                            gameCells[x, y].TargetCell.X = xIndex;
                            xIndex -= 1;
                            gameCells[x, y].hasMoved = true;
                            lastTile = gameCells[x, y];
                        }
                    }
                }
            }

            foreach (TileControl tile in gameCells)
                if (tile != null && !tile.TargetCell.IsSameAs(tile.CurrentCell))
                    canMove = true;

            if (canMove)
                CalculateStep();

            return canMove;
        }

        public bool TryToMoveLeft()
        {
            bool canMove = false;

            for (int y = 0; y < 4; y++)
            {
                // move one line
                int xIndex = 0;
                TileControl lastTile = null;
                for (int x = 0; x < 4; x++)
                {
                    if (gameCells[x, y] != null)
                    {
                        if (lastTile != null && lastTile.Value == gameCells[x, y].Value)
                        {
                            gameCells[x, y].TargetCell = lastTile.TargetCell.Clone();
                            gameCells[x, y].hasMoved = true;
                            gameCells[x, y].toRecycle = true;
                            lastTile.toUpgrade = true;
                            lastTile = gameCells[x, y];
                        }
                        else
                        {
                            gameCells[x, y].TargetCell = gameCells[x, y].CurrentCell.Clone();
                            gameCells[x, y].TargetCell.X = xIndex;
                            xIndex += 1;
                            gameCells[x, y].hasMoved = true;
                            lastTile = gameCells[x, y];
                        }
                    }
                }
            }

            foreach (TileControl tile in gameCells)
                if (tile != null && !tile.TargetCell.IsSameAs(tile.CurrentCell))
                    canMove = true;

            if (canMove)
                CalculateStep();

            return canMove;
        }

        private void CalculateStep()
        {
            foreach (TileControl tile in gameCells)
                if (tile != null)
                    if (tile.TargetCell.IsSameAs(tile.CurrentCell))
                    {
                        tile.DeltaPosition.X = 0;
                        tile.DeltaPosition.Y = 0;
                    }
                    else
                    {
                        tile.DeltaPosition.X = (tile.TargetCell.X - tile.CurrentCell.X) * 80 / 5;
                        tile.DeltaPosition.Y = (tile.TargetCell.Y - tile.CurrentCell.Y) * 80 / 5;
                    }
        }
    }
}
