namespace AFAS.Models.SpiralMaze
{
    /// <summary>
    /// 螺旋迷宫配置
    /// </summary>
    public class SpiralMazeForm
    {
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; } = 0;

        /// <summary>
        /// 间距
        /// </summary>
        public int Spacing { get; set; } = 0;

        /// <summary>
        /// 波动程度
        /// </summary>
        public int Perturbation { get; set; } = 0;

        /// <summary>
        /// 圈数
        /// </summary>
        public int RingNumber { get; set; } = 0;

    }
}
