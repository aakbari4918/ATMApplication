using System;
using System.Collections.Generic;

public class Account
{
    // Attributes
    private string _acctHolderName;
    private int _acctNo;
    private float _annualIntrRate;
    private float _balance;
    private List<string> _transactions;

    // Constructor
    public Account(string acctHolderName, int acctNo, float annualIntrRate, float balance)
    {
        _acctHolderName = acctHolderName;
        _acctNo = acctNo;
        _annualIntrRate = annualIntrRate;
        _balance = balance;
        _transactions = new List<string>();
    }

    // Operations

    // Deposit method
    public float deposit(float amount)
    {
        _balance += amount;
        _transactions.Add($"Deposit: +{amount:C}, Balance: {_balance:C}");
        return _balance;
    }

    // Get Account Holder Name method
    public string getAccountHolderName()
    {
        return _acctHolderName;
    }

    // Get Account Number method
    public int getAccountNumber()
    {
        return _acctNo;
    }

    // Get Balance method
    public float getBalance()
    {
        return _balance;
    }

    // Withdraw method
    public float Withdraw(float amount)
    {
        if (_balance >= amount)
        {
            _balance -= amount;
            _transactions.Add($"Withdrawal: -{amount:C}, Balance: {_balance:C}");
        }
        else
        {
            Console.WriteLine("Insufficient funds.");
        }
        return _balance;
    }

    // Display Transactions method
    public void displayTransactions()
    {
        foreach (var transaction in _transactions)
        {
            Console.WriteLine(transaction);
        }
    }
}

public class Bank
{
    // Fields
    private List<Account> _accounts;

    // Constructor
    public Bank()
    {
        _accounts = new List<Account>();
    }

    // Method to add account to the bank's list
    public void AddAccount(Account account)
    {
        _accounts.Add(account);
    }

    // Method to retrieve an account by account number
    public Account RetrieveAccount(int accountNumber)
    {
        foreach (var account in _accounts)
        {
            if (account.getAccountNumber() == accountNumber)
            {
                return account;
            }
        }
        return null; // Account not found
    }
}

public class ATMApplication
{
    // Fields
    private Bank _bank;

    // Constructor
    public ATMApplication()
    {
        _bank = new Bank();
    }

    // Main menu implementation
    public void MainMenu()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("==========Welcome to the ATM Application==========");
            Console.WriteLine("choose the following options by number associated with the option");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Select Account");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    CreateAccount();
                    break;
                case 2:
                    SelectAccount();
                    break;
                case 3:
                    exit = ConfirmExit();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    // Method to create a new account
    private void CreateAccount()
    {
        Console.Write("Enter Account Holder's Name: ");
        string accountHolderName = Console.ReadLine();

        int accountNumber;
        do
        {
            Console.Write("Enter Account Number (between 100 and 1000): ");
            accountNumber = Convert.ToInt32(Console.ReadLine());
            if (accountNumber < 100 || accountNumber > 1000)
            {
                Console.WriteLine("Account number must be between 100 and 1000.");
            }
        } while (accountNumber < 100 || accountNumber > 1000);

        float initialBalance;
        Console.Write("Enter Initial Balance: ");
        initialBalance = Convert.ToSingle(Console.ReadLine());

        float annualInterestRate;
        do
        {
            Console.Write("Enter Annual Interest Rate (less than 3.0%): ");
            annualInterestRate = Convert.ToSingle(Console.ReadLine());
            if (annualInterestRate >= 3.0f)
            {
                Console.WriteLine("Interest rate must be less than 3.0%.");
            }
        } while (annualInterestRate >= 3.0f);

        Account newAccount = new Account(accountHolderName, accountNumber, annualInterestRate, initialBalance);
        _bank.AddAccount(newAccount);
        Console.WriteLine("Account created successfully.");
    }

    // Method to select an account
    private void SelectAccount()
    {
        Console.Write("Enter Account Number: ");
        int accountNumber = Convert.ToInt32(Console.ReadLine());
        Account selectedAccount = _bank.RetrieveAccount(accountNumber);
        if (selectedAccount != null)
        {
            AccountMenu(selectedAccount);
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }

    // Account menu implementation
    private void AccountMenu(Account account)
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine($"Account Menu - {account.getAccountHolderName()}'s Account");
            Console.WriteLine("1. Check Balance");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Display Transactions");
            Console.WriteLine("5. Exit Account");
            Console.Write("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine($"Current Balance: {account.getBalance():C}");
                    break;
                case 2:
                    Console.Write("Enter amount to deposit: ");
                    float depositAmount = Convert.ToSingle(Console.ReadLine());
                    account.deposit(depositAmount);
                    Console.WriteLine($"Deposit of {depositAmount:C} successful.");
                    break;
                case 3:
                    Console.Write("Enter amount to withdraw: ");
                    float withdrawAmount = Convert.ToSingle(Console.ReadLine());
                    account.Withdraw(withdrawAmount);
                    break;
                case 4:
                    Console.WriteLine("Transactions:");
                    account.displayTransactions();
                    break;
                case 5:
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    // Method to confirm exit
    private bool ConfirmExit()
    {
        Console.Write("Are you sure you want to exit? (Y/N): ");
        char choice = Char.ToUpper(Console.ReadKey().KeyChar);
        Console.WriteLine();
        if (choice == 'Y')
        {
            Console.WriteLine("Thank you for using the ATM Application.");
            return true;
        }
        else if (choice == 'N')
        {
            return false;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter Y or N.");
            return false;
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        ATMApplication atmApp = new ATMApplication();
        atmApp.MainMenu();
    }
}