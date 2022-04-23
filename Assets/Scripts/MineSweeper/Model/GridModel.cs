using QFramework;

namespace MineSweeper {
    public class GridModel : AbstractModel {
        public readonly BindableProperty<int> MineNum = new BindableProperty<int>();
        public readonly BindableProperty<int> RowNum = new BindableProperty<int>();
        public readonly BindableProperty<int> LineNum = new BindableProperty<int>();

        public readonly BindableProperty<bool[,]> IsMine = new BindableProperty<bool[,]>();
        public readonly BindableProperty<int[,]> AroundMineNum = new BindableProperty<int[,]>();
        public readonly BindableProperty<bool[,]> IsShowed = new BindableProperty<bool[,]>();
        public readonly BindableProperty<bool[,]> IsMarked = new BindableProperty<bool[,]>();

        public BindableProperty<float> GridInterval = new BindableProperty<float> {Value = 0.55f};
        public BindableProperty<float> ResolutionAdjustInterval = new BindableProperty<float> {Value = 1f};

        protected override void OnInit() { }
    }
}