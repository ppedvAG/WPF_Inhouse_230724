﻿namespace TDDBank
{
    public class BankAccount
    {

        public bool IsWeekend()
        {
            return DateTime.Now.DayOfWeek == DayOfWeek.Sunday ||
                   DateTime.Now.DayOfWeek == DayOfWeek.Saturday;
        }


        public decimal Balance { get; private set; }

        public void Deposit(decimal v)
        {
            if (v <= 0)
                throw new ArgumentException();
            Balance += v;
        }

        public void Withdraw(decimal v)
        {
            if (v > 100)
                throw new ArgumentException();

            if (v <= 0)
                throw new ArgumentException();

            if (v > Balance)
                throw new InvalidOperationException();

            Balance -= v;
        }
    }
}