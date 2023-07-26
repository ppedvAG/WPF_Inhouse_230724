using Microsoft.QualityTools.Testing.Fakes;

namespace TDDBank.Tests
{
    public class BankAccountTests
    {
        /// <summary>
        /// Bankkonto
        ///	- Kontostand abfragen
        ///- Betrag einzahlen(nicht Negativ or 0)
        ///- Betrag abheben(nicht Negativ or 0)
        ///- Darf nicht unter 0 fallen
        ///- Neues Konto hat 0 als Kontostand
        /// </summary>

        [Fact]
        public void A_new_account_should_have_zero_as_balance()
        {
            var account = new BankAccount();

            Assert.Equal(0m, account.Balance);
        }

        [Fact]
        public void Deposit_should_add_to_balance()
        {
            var account = new BankAccount();

            account.Deposit(4m);
            account.Deposit(8m);

            Assert.Equal(12m, account.Balance);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Deposit_a_negative_amount_or_zero_throws_ArgumentEx(decimal amount)
        {
            var account = new BankAccount();

            Assert.Throws<ArgumentException>(() => account.Deposit(amount));
        }

        [Fact]
        public void Withdraw_should_substract_from_balance()
        {
            var account = new BankAccount();
            account.Deposit(10m);

            account.Withdraw(4m);

            Assert.Equal(6m, account.Balance);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Withdraw_a_negative_amount_or_zero_throws_ArgumentEx(decimal amount)
        {
            var account = new BankAccount();

            Assert.Throws<ArgumentException>(() => account.Withdraw(amount));
        }

        [Fact]
        public void Withdraw_below_balance_should_throw_InvalidOpEx()
        {
            var account = new BankAccount();
            account.Deposit(10m);

            Assert.Throws<InvalidOperationException>(() => account.Withdraw(11m));
        }

        [Fact]
        public void IsWeekend_Tests()
        {
            var account = new BankAccount();

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2000, 1, 3); //mo
                Assert.False(account.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2000, 1, 4); //di
                Assert.False(account.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2000, 1, 5); //mi
                Assert.False(account.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2000, 1, 6); //do
                Assert.False(account.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2000, 1, 7); //fr
                Assert.False(account.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2000, 1, 8); //sa
                Assert.True(account.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2000, 1, 9); //so
                Assert.True(account.IsWeekend());
            }
        }
    }
}