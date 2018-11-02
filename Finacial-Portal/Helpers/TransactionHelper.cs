using Finacial_Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finacial_Portal.Helpers
{
    public class TransactionHelper
    {
        public static ApplicationDbContext db = new ApplicationDbContext();

        public static ICollection<Transaction> ListHouseholdTransactions(string userId)
        {
            return db.Users.Find(userId).Household.Accounts.SelectMany(t => t.Transactions.Where(v => v.Void == false)).ToList();
        }

        public static void AdjustAccountBalance(int transactionId)
        {
            var transaction = db.Transactions.Find(transactionId);
            var transactionType = db.TransactionTypes.Find(transaction.TransactionTypeId);
            var bankId = transaction.AccountId;

            var account = db.Accounts.Find(bankId);
            db.Accounts.Attach(account);

            if(transactionType.Name == "Withdrawal")
            {
                account.Balance -= transaction.Amount;
            }
            else if (transactionType.Name == "Deposit")
            {
                account.Balance += transaction.Amount;
            }

            
            db.SaveChanges();
        }

        public static void VoidATransaction(int transactionId)
        {
            var transaction = db.Transactions.Find(transactionId);
            var transactionType = db.TransactionTypes.Find(transaction.TransactionTypeId);
            var bankId = transaction.AccountId;

            var account = db.Accounts.Find(bankId);
            db.Accounts.Attach(account);

            account.Balance += transaction.Amount;
            transaction.Void = true;
            
            db.SaveChanges();
        }
    }
}