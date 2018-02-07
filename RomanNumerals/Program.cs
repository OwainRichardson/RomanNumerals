namespace RomanNumerals
{
    public class Program
    {
        static void Main(string[] args)
        {
            NumeralLogic.NumeralService service = new NumeralLogic.NumeralService();
            service.HandleInput();
        }
    }
}
