
namespace Calculator.Tests
{
    public class CalcTests
    {
        [Fact]
        public void Sum_2_and_3_results_5()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(2, 3);

            //Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void Sum_0_and_0_results_0()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(0, 0);

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        public void Sum_MAX_and_1_throws_OverflowsException()
        {
            var calc = new Calc();

            Assert.Throws<OverflowException>(() => calc.Sum(int.MaxValue, 1));
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 2, 3)]
        [InlineData(-3, 3, 0)]
        [InlineData(-3, -3, -6)]
        public void Sum_with_expected_results(int a, int b, int exp)
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(a, b);

            //Assert
            Assert.Equal(exp, result);
        }

    }
}