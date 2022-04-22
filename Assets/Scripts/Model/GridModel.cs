using QFramework;

namespace MineSweeper {
    public class GridModel : AbstractModel {
        public BindableProperty<int> MineNum = new BindableProperty<int>();
        public BindableProperty<int> RowNum = new BindableProperty<int>();
        public BindableProperty<int> LineNum = new BindableProperty<int>();

        public BindableProperty<bool[,]> IsMine = new BindableProperty<bool[,]>();
        public BindableProperty<int[,]> AroundMineNum = new BindableProperty<int[,]>();
        public BindableProperty<bool[,]> IsShowed = new BindableProperty<bool[,]>();

        protected override void OnInit() { }
    }
}