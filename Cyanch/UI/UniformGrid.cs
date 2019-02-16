using C3;
using Microsoft.Xna.Framework;
using Okroma;

namespace Cyanch.UI
{
    /// <summary>
    /// Grid element.
    /// </summary>
    public class UniformGrid : Panel
    {
        Panel[,] _cells;

        public Point CellSize { get; private set; }
        public Point TableSize { get; private set; }

        bool _initialized;
        public void Initialize(int cellWidth, int cellHeight, int rows, int columns)
        {
            if (_initialized)
            {
                throw new System.InvalidOperationException("SetGrid(...) can only be called once");
            }

            this.CellSize = new Point(cellWidth, cellHeight);
            this.TableSize = new Point(rows, columns);

            GenerateGrid(cellWidth, cellHeight, rows, columns);

            _initialized = true;
        }

        public override void UpdateLayout(GameTime gameTime)
        {
            Width = CellSize.X * TableSize.X;
            Height = CellSize.Y * TableSize.Y;
            base.UpdateLayout(gameTime);
        }

        public Panel GetCell(int x, int y)
        {
            return _cells[x, y];
        }

        private void GenerateGrid(int cellWidth, int cellHeight, int x, int y)
        {
            _cells = new Panel[x, y];
            for (int iY = 0; iY < x; iY++)
            {
                for (int iX = 0; iX < y; iX++)
                {
                    var cell = AddElement<Panel>();
                    cell.LocalPosition = new Vector2(iX * cellWidth, iY * cellHeight);
                    cell.Width = cellWidth;
                    cell.Height = cellHeight;

                    _cells[iX, iY] = cell;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
#if DEBUG
            if (DebugSetting.Cyanch_UI_ShowGrid)
            {
                var gridColor = DebugSetting.Cyanch_UI_ShowGrid.GetArg<Color>(0);
                var cellColor = DebugSetting.Cyanch_UI_ShowGrid.GetArg<Color>(1);
                SpriteBatch.Begin();
                Primitives2D.DrawRectangle(SpriteBatch, GetEffectiveBounds(), gridColor);
                foreach (var cell in _cells)
                {
                    Primitives2D.DrawRectangle(SpriteBatch, cell.GetEffectiveBounds(), cellColor);
                }
                SpriteBatch.End();
            }
#endif
        }
    }
}
