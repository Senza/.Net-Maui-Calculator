namespace Calculator.Models
{
    public class BasicCalculatorModel
    {
        public double PreviousResult { get; set; }
        public double tempResultLoop { get; set; }
        public string FirstInput { get; set; } = string.Empty;
        public string LastInput { get; set; } = string.Empty;

        public BasicActionEnum ActionEnum { get; set; } = BasicActionEnum.None;
        public BasicActionEnum PreviousActionEnum { get; set; }

    }
}
