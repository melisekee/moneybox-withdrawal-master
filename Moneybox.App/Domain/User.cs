using System;

namespace Moneybox.App
{
    public class User
    {
		public Guid Id { get; private set; }

		public string Name { get; private set; }

		public string Email { get; private set; }

		public void UpdateId(Guid id)
		{
			Id = id;
		}

		public void UpdateName(string name)
		{
			Name = name;
		}

		public void UpdateEmail(string email)
		{
			Email = email;
		}
	}
}
