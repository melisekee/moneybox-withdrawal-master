using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Moneybox.App.Features;
using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;

namespace Moneybox.App.Test
{
	public class AccountRepository : IAccountRepository
	{
		private List<Account> bankAccounts = new List<Account>();

		public Account GetAccountById(Guid accountId)
		{
			Account userAccount = bankAccounts.Where(x => x.Id == accountId).FirstOrDefault();
			return userAccount;
		}

		public void Update(Account account)
		{
			bankAccounts.Add(account);
		}
	}

	public class NotificationService : INotificationService
	{
		public void NotifyApproachingPayInLimit(string emailAddress)
		{
			Console.WriteLine("Sending email to: " + emailAddress + " Approaching pay in limit");
		}

		public void NotifyFundsLow(string emailAddress)
		{
			Console.WriteLine("Sending email to: " + emailAddress + "Funds are low");
		}
	}

	[TestClass]
	public class TestMoneyboxMethods
	{
		[TestMethod]
		public void TestTransfer()
		{
			IAccountRepository accountRepository = new AccountRepository();
			INotificationService notificationService = new NotificationService();

			Account fromAccount = new Account();
			User fromUser = new User();
			fromAccount.UpdateId(Guid.NewGuid());
			fromUser.UpdateId(Guid.NewGuid());
			fromAccount.UpdateBalance(500m);
			fromUser.UpdateEmail(fromUser.Id + "@gmail.com");
			fromAccount.UpdateUser(fromUser);

			Account toAccount = new Account();
			User toUser = new User();
			toAccount.UpdateId(Guid.NewGuid());
			toUser.UpdateId(Guid.NewGuid());
			toAccount.UpdateBalance(0m);
			toUser.UpdateEmail(toUser.Id + "@gmail.com");
			toAccount.UpdateUser(toUser);

			accountRepository.Update(fromAccount);
			accountRepository.Update(toAccount);

			TransferMoney transferMoney = new TransferMoney(accountRepository, notificationService);

			transferMoney.Execute(fromAccount.Id, toAccount.Id, 250m);

			Assert.AreEqual(accountRepository.GetAccountById(fromAccount.Id).Balance, 250m);
			Assert.AreEqual(accountRepository.GetAccountById(toAccount.Id).Balance, 250m);
		}

		[TestMethod]
		public void TestWithdrawal()
		{
			IAccountRepository accountRepository = new AccountRepository();
			INotificationService notificationService = new NotificationService();

			Account fromAccount = new Account();
			User fromUser = new User();
			fromAccount.UpdateId(Guid.NewGuid());
			fromUser.UpdateId(Guid.NewGuid());
			fromAccount.UpdateBalance(500m);
			fromUser.UpdateEmail(fromUser.Id + "@gmail.com");
			fromAccount.UpdateUser(fromUser);

			accountRepository.Update(fromAccount);

			WithdrawMoney withdrawMoney = new WithdrawMoney(accountRepository, notificationService);

			withdrawMoney.Execute(fromAccount.Id, 250m);

			Assert.AreEqual(accountRepository.GetAccountById(fromAccount.Id).Balance, 250m);
		}
	}
}
