namespace Amplius.Experimental
{
    [Experimental]
    public sealed class Grid<T>
    {
        private static readonly FactoryFunction defaultFactory = (x, y) => default;

        public delegate T FactoryFunction(int x, int y);

        public int Width => width;
        public int Height => height;
        public Wrapper<T[]>[] Columns
        {
            get
            {
                var columns = new Wrapper<T[]>[height];

                for (int y = 0; y < height; y++)
                {
                    var cells = new T[width];

                    for (int x = 0; x < width; x++)
                    {
                        cells[x] = grid[x, y];
                    }

                    columns[y] = new Wrapper<T[]>(cells);
                }

                return columns;
            }
        }
        public Wrapper<T[]>[] Rows
        {
            get
            {
                var rows = new Wrapper<T[]>[height];

                for (int x = 0; x < width; x++)
                {
                    var cells = new T[height];

                    for (int y = 0; y < height; y++)
                    {
                        cells[x] = grid[x, y];
                    }

                    rows[x] = new Wrapper<T[]>(cells);
                }

                return rows;
            }
        }

        private readonly FactoryFunction factory;
        private readonly T[,] grid;
        private readonly int width;
        private readonly int height;

        public Grid(int width, int height, FactoryFunction factory = null)
        {
            this.factory = factory ?? defaultFactory;
            this.grid = new T[width, height];
            this.width = width;
            this.height = height;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    grid[x, y] = factory(x, y);
                }
            }

            T[] test = Columns[0];
        }

        public T this[int x, int y]
        {
            get => grid[x, y];
            set => grid[x, y] = value;
        }

        public T Get(int x, int y, bool safe = false)
        {
            if (x >= width || y >= height) return default;

            var val = grid[x, y];

            return safe ? val ?? default : val;
        }
        public void Set(int x, int y, T value, bool safe = false)
        {
            if (x >= width || y >= height) return;

            grid[x, y] = safe ? value ?? default : value;
        }
    }
}
