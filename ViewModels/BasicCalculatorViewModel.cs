using CommunityToolkit.Mvvm.Input;

namespace Calculator.ViewModels
{
    public partial class BasicCalculatorViewModel : BaseViewModel
    {
        private BasicCalculatorModel basicCalculatorModel;

        [ObservableProperty] private string calculationString;
        [ObservableProperty] private string solutionDetails;

        public BasicCalculatorViewModel(BasicCalculatorModel basicCalculatorModel)
        {
            Title = "Basic Calculator";
            this.basicCalculatorModel = basicCalculatorModel;
        }


        private void PreCalculation()
        {
            double.TryParse(basicCalculatorModel.FirstInput, out double firstInput);
            double.TryParse(basicCalculatorModel.LastInput, out double lastInput);

            var answer = 0.0d;
            var value = string.Empty;
            switch (basicCalculatorModel.ActionEnum)
            {
                case BasicActionEnum.Add:
                    answer = firstInput + lastInput;
                    value = " + ";
                    break;
                case BasicActionEnum.Substract:
                    answer = firstInput - lastInput;
                    value = " - ";
                    break;
                case BasicActionEnum.Multiply:
                    answer = firstInput * lastInput;
                    value = " x ";
                    break;
                case BasicActionEnum.Divide:
                    answer = firstInput / lastInput;
                    value = " ÷ ";
                    break;
            }

            SolutionDetails = $"{basicCalculatorModel.FirstInput} {value} {basicCalculatorModel.LastInput} = {answer}";

            basicCalculatorModel.ActionEnum = BasicActionEnum.None;
            basicCalculatorModel.PreviousResult = answer;

            basicCalculatorModel.FirstInput = answer.ToString();

            basicCalculatorModel.LastInput = string.Empty;
            CalculationString = basicCalculatorModel.FirstInput;
        }
        private void Calculation(BasicActionEnum basicActionEnum, string value)
        {
            double.TryParse(basicCalculatorModel.FirstInput, out double firstInput);
            double.TryParse(basicCalculatorModel.LastInput, out double lastInput);

            var answer = 0.0d;
            switch (basicCalculatorModel.ActionEnum)
            {
                case BasicActionEnum.Add:
                    answer = firstInput + lastInput;
                    break;
                case BasicActionEnum.Substract:
                    answer = firstInput - lastInput;
                    break;
                case BasicActionEnum.Multiply:
                    answer = firstInput * lastInput;
                    break;
                case BasicActionEnum.Divide:
                    answer = firstInput / lastInput;
                    break;
            }

            SolutionDetails = $"{basicCalculatorModel.FirstInput} {value} {basicCalculatorModel.LastInput} = {answer}";

            basicCalculatorModel.PreviousResult = answer;
            basicCalculatorModel.FirstInput = answer.ToString();
            basicCalculatorModel.tempResultLoop = lastInput;
            basicCalculatorModel.LastInput = string.Empty;

            if (string.IsNullOrEmpty(value))
            {
                basicCalculatorModel.PreviousActionEnum = basicCalculatorModel.ActionEnum;
                basicCalculatorModel.ActionEnum = BasicActionEnum.None;
            }


            CalculationString = basicCalculatorModel.FirstInput + value;
        }
        private bool InputCheck(BasicActionEnum basicActionEnum, string symbol)
        {
            if (!string.IsNullOrEmpty(basicCalculatorModel.FirstInput) &&
                !string.IsNullOrEmpty(basicCalculatorModel.LastInput) &&
                basicCalculatorModel.ActionEnum != BasicActionEnum.None)
            {
                PreCalculation();
                basicCalculatorModel.ActionEnum = basicActionEnum;
                CalculationString += symbol;
                return false;
            }

            if (string.IsNullOrEmpty(basicCalculatorModel.LastInput))
            {
                if (basicCalculatorModel.ActionEnum == basicActionEnum)
                {
                    CalculationString += symbol;
                    return false;
                }
                if (basicCalculatorModel.ActionEnum is BasicActionEnum.None)
                {
                    basicCalculatorModel.ActionEnum = basicActionEnum;
                    CalculationString += symbol;

                    return false;
                }

                if (basicCalculatorModel.ActionEnum != basicActionEnum)
                {
                    basicCalculatorModel.ActionEnum = basicActionEnum;
                    CalculationString = CalculationString.Remove(CalculationString.Length - 3);
                    CalculationString += symbol;
                    return false;
                }

                return false;
            }

            return true;
        }


        [RelayCommand]
        public void ClearInput()
        {
            basicCalculatorModel.ActionEnum = BasicActionEnum.None;
            basicCalculatorModel.PreviousActionEnum = BasicActionEnum.None;
            basicCalculatorModel.LastInput = string.Empty;
            basicCalculatorModel.FirstInput = string.Empty;
            CalculationString = string.Empty;
            SolutionDetails = "0";
        }

        [RelayCommand]
        public void InputButton(string inputValue)
        {


            if (basicCalculatorModel.ActionEnum == BasicActionEnum.None)
            {
                if (inputValue.Equals(".", StringComparison.OrdinalIgnoreCase) && basicCalculatorModel.FirstInput.Contains("."))
                {

                    return;
                }

                basicCalculatorModel.FirstInput += inputValue;

                CalculationString += inputValue;
                return;
            }


            if (inputValue.Equals(".", StringComparison.OrdinalIgnoreCase) && basicCalculatorModel.LastInput.Contains("."))
            {

                return;
            }
            basicCalculatorModel.LastInput += inputValue;
            CalculationString += inputValue;
        }

        [RelayCommand]
        public void SignInput()
        {

            if (basicCalculatorModel.ActionEnum == BasicActionEnum.None)
            {
                double.TryParse(basicCalculatorModel.FirstInput, out double firstInput);
                if (firstInput == 0)
                {
                    return;
                }
                firstInput *= -1;
               
                CalculationString = CalculationString.Remove(0, basicCalculatorModel.FirstInput.Length);
                CalculationString = firstInput.ToString() + CalculationString;

                basicCalculatorModel.FirstInput = firstInput.ToString();

                return;
            }

            double.TryParse(basicCalculatorModel.LastInput, out double lastinput);

            if(lastinput == 0)
            {
                return;
            }
            lastinput *= -1;

            CalculationString = CalculationString.Remove(CalculationString.Length - basicCalculatorModel.LastInput.Length, basicCalculatorModel.LastInput.Length);
            CalculationString = CalculationString + lastinput.ToString();

            basicCalculatorModel.LastInput = lastinput.ToString();



        }
        [RelayCommand]
        public void Percentage()
        {
            if (string.IsNullOrEmpty(basicCalculatorModel.FirstInput))
            {
                return;
            }

            if (!string.IsNullOrEmpty(basicCalculatorModel.FirstInput) &&
               !string.IsNullOrEmpty(basicCalculatorModel.LastInput) &&
               basicCalculatorModel.ActionEnum != BasicActionEnum.None)
            {
                PreCalculation();
                basicCalculatorModel.ActionEnum = BasicActionEnum.None;
            }

            double.TryParse(basicCalculatorModel.FirstInput, out double result);

            var answer = result / 100;
            basicCalculatorModel.FirstInput = answer.ToString();
            basicCalculatorModel.PreviousResult = answer;
            basicCalculatorModel.LastInput = string.Empty;
            SolutionDetails = $"{result}% = {answer}";
            CalculationString = answer.ToString();
        }

        [RelayCommand]
        public void Add(string value)
        {

            if (!InputCheck(BasicActionEnum.Add, value))
            {
                return;
            }

            if (!string.IsNullOrEmpty(basicCalculatorModel.LastInput) && !string.IsNullOrEmpty(basicCalculatorModel.FirstInput))
            {
                Calculation(BasicActionEnum.Add, value);
            }

        }

        [RelayCommand]
        public void Substract(string value)
        {
            if (!InputCheck(BasicActionEnum.Substract, value))
            {
                return;
            }

            if (!string.IsNullOrEmpty(basicCalculatorModel.LastInput) && !string.IsNullOrEmpty(basicCalculatorModel.FirstInput))
            {
                Calculation(BasicActionEnum.Substract, value);
            }
        }

        [RelayCommand]
        public void Multiply(string value)
        {
            if (!InputCheck(BasicActionEnum.Multiply, value))
            {
                return;
            }

            if (!string.IsNullOrEmpty(basicCalculatorModel.LastInput) && !string.IsNullOrEmpty(basicCalculatorModel.FirstInput))
            {
                Calculation(BasicActionEnum.Multiply, value);
            }
        }

        [RelayCommand]
        public void Divide(string value)
        {
            if (!InputCheck(BasicActionEnum.Divide, value))
            {
                return;
            }

            if (!string.IsNullOrEmpty(basicCalculatorModel.LastInput) && !string.IsNullOrEmpty(basicCalculatorModel.FirstInput))
            {
                Calculation(BasicActionEnum.Divide, value);
            }
        }

        [RelayCommand]
        public void Equals()
        {
            if (basicCalculatorModel.ActionEnum == BasicActionEnum.None && basicCalculatorModel.PreviousActionEnum != BasicActionEnum.None)
            {
                basicCalculatorModel.ActionEnum = basicCalculatorModel.PreviousActionEnum;
                basicCalculatorModel.LastInput = basicCalculatorModel.tempResultLoop.ToString();
                CalculationString = string.Empty;
            }

            switch (basicCalculatorModel.ActionEnum)
            {
                case BasicActionEnum.Add:
                    Add(string.Empty);
                    break;
                case BasicActionEnum.Substract:
                    Substract(string.Empty);
                    break;
                case BasicActionEnum.Multiply:
                    Multiply(string.Empty);
                    break;
                case BasicActionEnum.Divide:
                    Divide(string.Empty);
                    break;
                case BasicActionEnum.None:
                    break;
            }

            basicCalculatorModel.ActionEnum = BasicActionEnum.None;


        }


    }
}
