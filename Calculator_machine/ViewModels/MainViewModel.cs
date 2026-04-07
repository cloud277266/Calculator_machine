using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator_machine.Commands;
using Calculator_machine.Models;
using Calculator_machine.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Calculator_machine.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // 1. 공장 및 엔진 선언 (전역 필드)
        private readonly Tokenizer _tokenizer;
        private readonly Normalizer _normalizer;
        private readonly Calculator _calculator;
        private readonly Validator _validator;

        // 2. 바인딩 프로퍼티 (알람 시스템 장착)
        private string _inputExpression = "";
        public string InputExpression
        {
            get { return _inputExpression; }
            set { _inputExpression = value; OnPropertyChanged(); }
        }

        private string _resultExpression = "";
        public string ResultExpression
        {
            get { return _resultExpression; }
            set { _resultExpression = value; OnPropertyChanged(); }
        }

        private string _errorMessage = "";
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        // 3. 버튼 전선 (Commands)
        public RelayCommand<string> AddInputCommand { get; }
        public RelayCommand<object> CalculateCommand { get; }
        public RelayCommand<object> DeleteCommand { get; }

        // 4. 생성자 (사전 셋업 및 배선 연결)
        public MainViewModel()
        {
            _tokenizer = new Tokenizer();
            _normalizer = new Normalizer();
            _calculator = new Calculator();
            _validator = new Validator();

            AddInputCommand = new RelayCommand<string>(ExecuteAddInput);
            CalculateCommand = new RelayCommand<object>(ExecuteCalculate, CanExecuteCalculate);
            DeleteCommand = new RelayCommand<object>(ExecuteDelete);
        }

        // 5. 버튼 클릭 시 작동하는 실제 로직들
        private void ExecuteAddInput(string inputChar)
        {
            ErrorMessage = ""; // 새로운 입력이 들어오면 에러 문구 삭제

            // 검증기를 통과한 정상적인 입력만 반영
            if (_validator.IsValidInput(InputExpression, inputChar))
            {
                InputExpression += inputChar;
            }
        }

        private void ExecuteDelete(object obj)
        {
            if (InputExpression.Length > 0)
            {
                InputExpression = InputExpression.Substring(0, InputExpression.Length - 1);
            }
        }

        private bool CanExecuteCalculate(object obj)
        {
            // 수식이 비어있으면 계산(=) 버튼을 아예 비활성화 (클릭 금지)
            return !string.IsNullOrWhiteSpace(InputExpression);
        }

        private void ExecuteCalculate(object obj)
        {
            try
            {
                ErrorMessage = ""; // 에러 리셋

                // 우리가 설계한 '순차적 데이터 파이프라인'
                List<Token> originalTokens = _tokenizer.Tokenize(InputExpression);
                List<Token> normalizedTokens = _normalizer.Normalize(originalTokens);

                // 재귀 아키텍처 가동
                double finalResult = _calculator.Evaluate(normalizedTokens);

                ResultExpression = finalResult.ToString();
            }
            catch (Exception ex)
            {
                // 엔진룸에서 뱉어낸 예외를 조종석에서 안전하게 잡아서 화면에 알람
                ErrorMessage = ex.Message;
                ResultExpression = "Error";
            }
        }

        // --- WPF 화면 갱신 알람 엔진 ---
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
