using BankingApp.Entities;
using BankingApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BankingApp.DataAcesss
{
    public class AccountInfoFileCollection
    {
        public void AddNew(AccountInfo item)
        {
            string folder = @"C:\TestFolder";
            string fileName = "Accounts.txt";
            string filePath = Path.Combine(folder, fileName);


            try
            {
                int nextID;
                if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
                {
                    // File does not exist or is empty, start with ID 101
                    nextID = 101;
                }
                else
                {
                    // File has content, determine the next ID based on the last entry
                    string lastLine = File.ReadLines(filePath).Last();
                    int lastID = int.Parse(lastLine.Split('|')[0]);
                    nextID = lastID + 1;
                }

                using (StreamWriter sw = File.AppendText(filePath))
                {
                    // Format the data as ID|Name|AccountType|Balance
                    string data = $"{nextID}|{item.Name}|{item.Type}|{item.Balance}";

                    // Write the data to the file
                    sw.WriteLine(data);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred while storing account data:");
                Console.WriteLine(e.Message);
            }
        }

        public ArrayList GetAllAccounts()
        {
            ArrayList accounts= new ArrayList();
           // List<AccountInfo> accounts = new List<AccountInfo>();
            string folder = @"C:\TestFolder";
            string fileName = "Accounts.txt";
            string filePath = Path.Combine(folder, fileName);

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        AccountInfo account = new AccountInfo(
                            int.Parse(parts[0]),
                            parts[1],
                            (AccountType)Enum.Parse(typeof(AccountType), parts[2]),
                            double.Parse(parts[3])
                        );
                        accounts.Add(account);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred while reading account data:");
                Console.WriteLine(e.Message);
            }

            return accounts;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public AccountInfo FindAccountBy(int id)
        {
            string folder = @"C:\TestFolder";
            string fileName = "Accounts.txt";
            string filePath = Path.Combine(folder, fileName);

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        if (int.Parse(parts[0]) == id)
                        {
                            return new AccountInfo(
                                int.Parse(parts[0]),
                                parts[1],
                                (AccountType)Enum.Parse(typeof(AccountType), parts[2]),
                                double.Parse(parts[3])
                            );
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred while searching for account:");
                Console.WriteLine(e.Message);
            }

            return null;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public AccountInfo UpdateAccountBy(int id)
        {
            string folder = @"C:\TestFolder";
            string fileName = "Accounts.txt";
            string filePath = Path.Combine(folder, fileName);

            try
            {
                bool accountFound = false;

                List<string> lines = new List<string>();
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        if (int.Parse(parts[0]) == id)
                        {
                            accountFound = true;
                            Console.Write("Enter new name for the account: ");
                            string newName = Console.ReadLine();
                            parts[1] = newName;
                            line = string.Join("|", parts);
                        }
                        lines.Add(line);
                    }
                }

                if (accountFound)
                {
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        foreach (string line in lines)
                        {
                            sw.WriteLine(line);
                        }
                    }

                    return FindAccountBy(id); // Return updated account
                }
                else
                {
                    Console.WriteLine($"Account with ID {id} not found.");
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred while updating account:");
                Console.WriteLine(e.Message);
                return null;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public AccountInfo RemoveAccountBy(int id)
        {
            string folder = @"C:\TestFolder";
            string fileName = "Accounts.txt";
            string filePath = Path.Combine(folder, fileName);

            try
            {
                List<string> updatedLines = new List<string>();
                AccountInfo removedAccount = null;

                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        if (int.Parse(parts[0]) == id)
                        {
                            // Found the account, store it and continue to the next line
                            removedAccount = new AccountInfo
                            (
                                int.Parse(parts[0]),
                                 parts[1],
                                (AccountType)Enum.Parse(typeof(AccountType), parts[2]),
                                double.Parse(parts[3])
                            );
                            continue; // Skip adding this line to updatedLines
                        }
                        updatedLines.Add(line);
                    }
                }

                if (removedAccount != null)
                {
                    // Rewrite the file without the removed line
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        foreach (string line in updatedLines)
                        {
                            sw.WriteLine(line);
                        }
                    }

                    Console.WriteLine($"Account with ID {id} removed successfully.");
                    return removedAccount;
                }
                else
                {
                    Console.WriteLine($"Account with ID {id} not found.");
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred while removing account:");
                Console.WriteLine(e.Message);
                return null;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        public AccountInfo DepositBy(int id, double amount)
        {
            string folder = @"C:\TestFolder";
            string fileName = "Accounts.txt";
            string filePath = Path.Combine(folder, fileName);

            try
            {
                List<string> updatedLines = new List<string>();
                AccountInfo updatedAccount = null;

                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        if (int.Parse(parts[0]) == id)
                        {
                            double currentBalance = double.Parse(parts[3]);
                            double updatedBalance = currentBalance + amount;
                            parts[3] = updatedBalance.ToString();
                            line = string.Join("|", parts);
                            updatedAccount = new AccountInfo
                            (
                                 int.Parse(parts[0]),
                                parts[1],
                                 (AccountType)Enum.Parse(typeof(AccountType), parts[2]),
                                updatedBalance
                            );
                        }
                        updatedLines.Add(line);
                    }
                }

                if (updatedAccount != null)
                {
                    // Rewrite the file with updated balance
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        foreach (string line in updatedLines)
                        {
                            sw.WriteLine(line);
                        }
                    }

                    Console.WriteLine($"Deposited {amount} into account with ID {id} successfully.");
                    return updatedAccount;
                }
                else
                {
                    Console.WriteLine($"Account with ID {id} not found.");
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred while depositing into account:");
                Console.WriteLine(e.Message);
                return null;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        public AccountInfo WithdrawBy(int id, double amount)
        {
            string folder = @"C:\TestFolder";
            string fileName = "Accounts.txt";
            string filePath = Path.Combine(folder, fileName);

            try
            {
                List<string> updatedLines = new List<string>();
                AccountInfo updatedAccount = null;

                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        if (int.Parse(parts[0]) == id)
                        {
                            double currentBalance = double.Parse(parts[3]);
                            double updatedBalance = currentBalance - amount;
                            if (updatedBalance >= 0)
                            {
                                parts[3] = updatedBalance.ToString();
                                line = string.Join("|", parts);
                                updatedAccount = new AccountInfo
                                (
                                    int.Parse(parts[0]),
                                     parts[1],
                                     (AccountType)Enum.Parse(typeof(AccountType), parts[2]),
                                     updatedBalance
                                );
                            }
                            else
                            {
                                Console.WriteLine("Insufficient funds to withdraw.");
                                updatedLines.Add(line); // Keep the line unchanged
                            }
                        }
                        updatedLines.Add(line);
                    }
                }

                if (updatedAccount != null)
                {
                    // Rewrite the file with updated balance
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        foreach (string line in updatedLines)
                        {
                            sw.WriteLine(line);
                        }
                    }

                    Console.WriteLine($"Withdrawn {amount} from account with ID {id} successfully.");
                    return updatedAccount;
                }
                else
                {
                    Console.WriteLine($"Account with ID {id} not found.");
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred while withdrawing from account:");
                Console.WriteLine(e.Message);
                return null;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public AccountInfo TransferBy(int id1, int id2, double amount)
        {
            string folder = @"C:\TestFolder";
            string fileName = "Accounts.txt";
            string filePath = Path.Combine(folder, fileName);
            try
            {
                List<string> updatedLines = new List<string>();
                AccountInfo updatedAccount1 = null;
                AccountInfo updatedAccount2 = null;

                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        if (int.Parse(parts[0]) == id1)
                        {
                            double currentBalance = double.Parse(parts[3]);
                            double updatedBalance = currentBalance - amount;
                            if (updatedBalance >= 0)
                            {
                                parts[3] = updatedBalance.ToString();
                                line = string.Join("|", parts);
                                updatedAccount1 = new AccountInfo
                                (
                                    int.Parse(parts[0]),
                                     parts[1],
                                     (AccountType)Enum.Parse(typeof(AccountType), parts[2]),
                                     updatedBalance
                                );
                            }
                            else
                            {
                                Console.WriteLine("Insufficient funds to transfer.");
                                updatedLines.Add(line); // Keep the line unchanged
                            }
                        }
                        else if (int.Parse(parts[0]) == id2 && updatedAccount1 != null)
                        {
                            double currentBalance = double.Parse(parts[3]);
                            double updatedBalance = currentBalance + amount;
                            parts[3] = updatedBalance.ToString();
                            line = string.Join("|", parts);
                            updatedAccount2 = new AccountInfo
                            (
                                    int.Parse(parts[0]),
                                     parts[1],
                                     (AccountType)Enum.Parse(typeof(AccountType), parts[2]),
                                     updatedBalance
                                );
                        }
                        updatedLines.Add(line);
                    }
                }

                if (updatedAccount1 != null && updatedAccount2 != null)
                {
                    // Rewrite the file with updated balances
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        foreach (string line in updatedLines)
                        {
                            sw.WriteLine(line);
                        }
                    }

                    Console.WriteLine($"Transferred {amount} from account with ID {id1} to account with ID {id2} successfully.");
                    return updatedAccount1;
                }
                else
                {
                    Console.WriteLine($"One or both of the accounts with IDs {id1} and {id2} not found.");
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred while transferring between accounts:");
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
