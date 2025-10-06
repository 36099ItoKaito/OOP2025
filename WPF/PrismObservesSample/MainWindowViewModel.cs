using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismObservesSample {
    internal class MainWindowViewModel : BindableBase{
        private string _input1 = "";
        public string Input1 {
            get => _input1;
            set => SetProperty(ref _input1, value);//処理を追加
        }

        private string _input2 = "";
        public string Input2 {
            get => _input2;
            set => SetProperty(ref _input2, value);//処理を追加
        }

        private string _result = "";
        public string Result {
            get => _result;
            set => SetProperty(ref _result, value);//処理を追加
        }

        public DelegateCommand SumCommand { get; }

        //コンストラクタ
        public MainWindowViewModel() {
            SumCommand = new DelegateCommand(ExcuteSum,canExcuteSum)
                .ObservesProperty(()=>Input1)
                .ObservesProperty(() => Input2);
        }

        //足し算の処理
        private void ExcuteSum() {
            //足し算の処理を記述
            Result = (int.Parse(Input1) + int.Parse(Input2)).ToString();
        }

        //足し算処理を実行できるか否かのチェック
        private bool canExcuteSum() {
            return (int.TryParse(Input1, out _) && (int.TryParse(Input2, out _))) ;
        }
    }
}
