using BankingApp.Common;
using BankingApp.Entities;
using BankingApp.Process;
using System.Text;

namespace BankingApp
{
    internal class Program
    {
        static int DisplayMenuAndGetChoice()
        {
            Console.WriteLine("************** Banking System ***********");
            Console.WriteLine("******* 1. Create a New Account ");
            Console.WriteLine("******* 2. List All the Accounts");
            Console.WriteLine("******* 3. Find Account by ID ");
            Console.WriteLine("******* 4. Update Account Details ");
            Console.WriteLine("******* 5. Remove an account");
            Console.WriteLine("******* 6. Perform Transactions ");
            Console.WriteLine("******* ");
            Console.WriteLine("******* 0. Quit ");
            Console.WriteLine("***********************************");
            Console.Write("\nEnter Choice: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice > 0 && choice < 9) return choice;
            }
            else
            {
                Console.WriteLine("Invalid Choice.");
            }
            return choice;
        }
        static void PrintAccountDetails(AccountInfo info)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"Account Details:\n\tId: {info.Id}")
                .AppendLine($"\tName: {info.Name}")
                .AppendLine($"\tType: {info.Type}")
                .AppendLine($"\tAmount: {info.Balance}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(builder.ToString());
            Console.ResetColor();
        }
            static void AddNewAccount()
        {
            Console.Clear();
            Console.WriteLine("*********** Banking Operations :: Add New Account ********");
            Console.WriteLine("\nAccount holder name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Account Type [1-Savings,2-Current] : ");
            int accType = int.Parse(Console.ReadLine());
            Console.WriteLine("Initial Amount: ");
            double amount = double.Parse(Console.ReadLine());

            BankingProcess process = new BankingProcess();
            var info = process.CreateNewAccount(0, name, (AccountType)accType, amount);
            if (info is not null)
            {
                PrintAccountDetails(info);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nAccount created successfully.");
                Console.ResetColor();

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAccount could not be created. Try again with different values.");
                Console.ResetColor();
            }

        }
        static void ListAllAccounts()
        {
            Console.Clear();
            Console.WriteLine("************ Banking Operations ::List of accounts  ****** ");
            BankingProcess process = new BankingProcess();
            var accs = process.GetAllAccounts();
            if (accs.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No Accounts found");
                Console.ResetColor();
                return;
            }
            foreach (AccountInfo acc in accs)
            {
                PrintAccountDetails(acc);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nOperation Completed successfully. ");
            Console.ResetColor();
        }
        static void FindAccountById()
        {
            Console.Clear();
            Console.WriteLine("************ Banking Operations ::Find Acocunt by ID ****** ");
            BankingProcess process = new BankingProcess();
            Console.Write("Enter the ID of user: ");
            int id = int.Parse(Console.ReadLine());
            AccountInfo acc = process.GetAccountByID(id);
            if (acc is null)
            {
                Console.WriteLine("Account Not Found");
                return;
            }
            PrintAccountDetails(acc);
        }
        static void Main(string[] args)
        {

            int choice = 0;
            do
            {
                Console.Clear();
                choice = DisplayMenuAndGetChoice();
                if (choice == 1)
                {
                    AddNewAccount();
                }
                else if (choice == 2)
                {
                    //*  Console.WriteLine("\n\nList all accounts selected");*//*
                    ListAllAccounts();
                }
                else if (choice == 3)
                {
                    FindAccountById();
                }
                else if (choice == 4)
                {
                    UpdateAccount();
                }
                else if (choice == 5)
                {
                    RemoveAccount();
                }
                else if (choice == 6)
                {
                    PerformTransaction();
                }
                Console.WriteLine("Press a key to continue...");
                Console.ReadKey();
            } while (choice != 0);
            Console.WriteLine("\n\nBye!");



            /* Account acc = new Account(11);
             acc.Withdraw(); *//**/

            //Account acc = new Account(101, "Sample1", Common.AccountType.Savings, 10000);
            //IAccount acc = AccountFactory.Create(101, "Sample1", Common.AccountType.Savings, 20000);
            //acc.info = new Entities.AccountInfo(1, "", Common.AccountType.None, 123); 
            //  IAccount acc=null!,acc2=null!;

            //  try
            //  {
            //      acc= AccountFactory.Create(101, "Sample1", Common.AccountType.Savings, 20000);
            //  }
            //  catch(MinimumBalanceException mbe)
            //  {
            //      Console.WriteLine(mbe.Message);
            //  }
            //  if (acc is null)
            //  {
            //      Console.WriteLine("One or more objects could not be created.");
            //      return; //terminate the execution, in this case. 
            //  }
            //  acc.DepositEvent += (s, e) => Console.WriteLine(e);
            //  acc.WithdrawEvent += (s, e) => Console.WriteLine(e);
            //  Console.WriteLine($"New account:\n{acc}");
            //  acc.Deposit(10000);
            //  Console.WriteLine($"After deposit:\n{acc}");
            //  try
            //  {
            //      acc.Withdraw(5678);
            //      Console.WriteLine($"After withdraw:\n{acc}");

            //  } catch(MinimumBalanceException mbe)
            //  {
            //      Console.WriteLine(mbe.Message);
            //  }


            //  //create another account 
            //  //Account acc2 = new Account(102, "Sample2", Common.AccountType.Current, 25000);
            //  try
            //  {


            //      acc2 = AccountFactory.Create(102, "Sample2", Common.AccountType.Current, 210000);
            //  }
            //  catch(MinimumBalanceException mbe)
            //  {
            //      Console.WriteLine(mbe.Message);
            //  }
            //  if (acc2 is null)
            //  {
            //      Console.WriteLine("One or more objects could not be created.");
            //      return; //terminate the execution, in this case. 
            //  }
            //  acc2.DepositEvent += (s, e) => Console.WriteLine(e);
            //  acc2.WithdrawEvent += (s, e) => Console.WriteLine(e);
            //  Console.WriteLine($"Another account:\n{acc2}");
            //  try
            //  {
            //      acc.FundTransfer(acc2, 55212);
            //  }catch(MinimumBalanceException mbe)
            //  {
            //      Console.ForegroundColor=ConsoleColor.Red;
            //      Console.WriteLine(mbe.Message);
            //      Console.ResetColor();
            //  }
            //  Console.WriteLine($"After fund transfer:\nSource:{acc}\nDestination:{acc2}");

            ////  acc = AccountFactory.Create(101, "", Common.AccountType.None, 0);
            // // Console.WriteLine($"\n\nFrom the Array: {acc}");


            //  Console.WriteLine("Press a key to terminate.");
            //  Console.ReadKey();
        }
        static void UpdateAccount()
        {
            Console.Clear();
            Console.WriteLine("************ Banking Operations ::Update Account Details  ****** ");
            Console.Write("Enter Account ID: ");
            int id = int.Parse(Console.ReadLine());
            BankingProcess process = new BankingProcess();
            var acc = process.GetAccountByID(id);

            if (acc is null)
            {
                Console.WriteLine("Account Not Found");
                return;
            }
            Console.WriteLine("Update the details");
            Console.WriteLine($"/t/tID: {acc.Id}");
            Console.Write($"/t/tName ({acc.Name}): ");
            string name = Console.ReadLine();
            Console.WriteLine($"/t/tAccount Type: {acc.Type}");
            // string accType = Console.ReadLine();
            var temp = process.GetUpdateAccount(id, name);
            if (temp is null)
            {
                Console.WriteLine("Can't update!");
            }
            else
            {
                PrintAccountDetails(temp);
            }





        }
        static void RemoveAccount()
        {

            Console.Clear();
            Console.WriteLine("************ Banking Operations ::Remove an Account ****** ");
            Console.Write("Enter Account ID: ");
            int id = int.Parse(Console.ReadLine());
            BankingProcess process = new BankingProcess();
            var acc = process.GetAccountByID(id);
            if (acc is null)
            {
                Console.WriteLine("Account not found!");
                return;
            }
            process.DeleteAccountByID(id);
            Console.WriteLine($"Account of id {id} is removed succesfully");

        }
        static void PerformTransaction()
        {
            Console.Clear();
            Console.WriteLine("************ Banking Operations ::Transactions ****** ");
            Console.Write("Enter Account ID: ");
            int id = int.Parse(Console.ReadLine());
            BankingProcess process = new BankingProcess();
            var acc = process.GetAccountByID(id);
            if (acc is null)
            {
                Console.WriteLine("Account not found!");
                return;
            }

            Console.WriteLine("/t/t/tOperation Types");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Fund Transfer");
            int choice = 0;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid option!");
                return;
            }
            int ToAccID = 0;
            double amount;
            if (choice == 1 || choice == 2)
            {
                Console.Write("Enter Amount: ");
                amount = int.Parse(Console.ReadLine());




            }
            else if (choice == 3)
            {

                Console.Write("Enter To Account Id: ");
                ToAccID = int.Parse(Console.ReadLine());
                var toAcc = process.GetAccountByID(ToAccID);
                if (toAcc is null)
                {
                    Console.WriteLine("Account not found!");
                    return;

                }
                Console.Write("Enter Amount: ");
                amount = int.Parse(Console.ReadLine());
                /*
                                Console.Write("Are you sure to execute the transaction? Y/N: ")
                                string confirmation = Console.ReadLine();*/

            }
            else
            {
                Console.WriteLine("Invalid option!");
                return;
            }
            Console.Write("Are you sure to execute the transaction? Y/N: ");
            string confirmation = Console.ReadLine();

            if (confirmation == "Y")
            {

                var temp = process.ExecuteTransaction(id, ToAccID, amount, choice);
                if (temp is null)
                {
                    Console.WriteLine("Transaction Failed!");
                    return;
                }

                PrintAccountDetails(acc);


            }
            else
            {
                Console.WriteLine("Thankyou!");
            }

        }
    }
}
