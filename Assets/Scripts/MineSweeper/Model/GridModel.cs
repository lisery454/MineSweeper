using QFramework;

namespace MineSweeper {
    public class GridModel : AbstractModel {
        public readonly BindableProperty<int> MineNum = new();
        public readonly BindableProperty<int> RowNum = new();
        public readonly BindableProperty<int> LineNum = new();

        public readonly BindableProperty<bool[,]> IsMine = new();
        public readonly BindableProperty<int[,]> AroundMineNum = new();
        public readonly BindableProperty<bool[,]> IsShowed = new();
        public readonly BindableProperty<bool[,]> IsMarked = new();

        public readonly BindableProperty<float> GridInterval = new() {Value = 0.55f};
        public readonly BindableProperty<float> ResolutionAdjustInterval = new() {Value = 1f};

        protected override void OnInit() { }
    }
}