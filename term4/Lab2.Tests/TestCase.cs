namespace Lab2.Tests
{
    public class TestCase
    {
        public decimal[,] Coefficients { get; set; }
        public decimal[] RightPart { get; set; }
        public decimal[] Solution { get; set; }
        public decimal Accuracy { get; set; }
        
        public int? Length { get; set; }
        public decimal? Multiplier { get; set; }
    }
}