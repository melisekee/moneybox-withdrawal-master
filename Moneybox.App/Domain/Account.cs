using System;

namespace Moneybox.App
{
    public class Account
    {
		public const decimal PayInLimit = 4000m;

		public Guid Id { get; private set; }

		public User User { get; private set; }

		public decimal Balance { get; private set; }

		public decimal Withdrawn { get; private set; }

		public decimal PaidIn { get; private set; }

		public void UpdateUser(User user)
		{
			User = user;
		}

		public void UpdateId(Guid id)
		{
			Id = id;
		}

		public void UpdateBalance(decimal amount)
		{
			Balance += amount;
		}

		public void UpdateWithdrawn(decimal amount)
		{
			Withdrawn += amount;
		}

		public void UpdatePaidIn(decimal amount)
		{
			PaidIn += amount;
		}
	}
}
