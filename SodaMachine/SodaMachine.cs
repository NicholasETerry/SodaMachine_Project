using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class SodaMachine
    {
        //Member Variables (Has A)
        private List<Coin> _register;
        private List<Can> _inventory;
        Quarter newQuarter;
        Dime newDime;
        Nickel newNickel;
        Penny newPenny;
        RootBeer newRootBeer;
        Cola newCola;
        OrangeSoda newOrangeSoda;

        //Constructor (Spawner)
        public SodaMachine()
        {
            newQuarter = new Quarter();
            newDime = new Dime();
            newNickel = new Nickel();
            newPenny = new Penny();
            newRootBeer = new RootBeer();
            newCola = new Cola();
            newOrangeSoda = new OrangeSoda();
            _register = new List<Coin>();
            _inventory = new List<Can>();
            FillInventory(newRootBeer,20,newCola,20,newOrangeSoda,20);
            FillRegister(newQuarter,20,newDime,10,newNickel,20,newPenny,50);
        }

        //Member Methods (Can Do)

        //A method to fill the sodamachines register with coin objects.
        public void FillRegister(Coin quarters,int howManyQuarters,Coin dimes,int howManyDimes,Coin nickels, int howManyNickels,Coin pennies, int howManyPennies)
        {
            int startingAmount = 0;
            while (startingAmount != howManyQuarters)
            {
                _register.Add(newQuarter);
                startingAmount++;
            }
            startingAmount = 0;
            while (startingAmount != howManyDimes)
            {
                _register.Add(newDime);
                startingAmount++;
            }
            startingAmount = 0;
            while (startingAmount != howManyNickels)
            {
                _register.Add(newNickel);
                startingAmount++;
            }
            startingAmount = 0;
            while (startingAmount != howManyPennies)
            {
                _register.Add(newPenny);
                startingAmount++;
            }
        }
        //A method to fill the sodamachines inventory with soda can objects.
        public void FillInventory(Can rootBeer,int howManyRootBeer,Can cola,int howManyCola,Can orange,int howManyOrange)
        {
            int startingAmount = 0;
            while (startingAmount != howManyRootBeer)
            {
                _inventory.Add(newRootBeer);
                startingAmount++;
            }
            startingAmount = 0;
            while (startingAmount != howManyCola)
            {
                _inventory.Add(newCola);
                startingAmount++;
            }
            startingAmount = 0;
            while (startingAmount != howManyOrange)
            {
                _inventory.Add(newOrangeSoda);
                startingAmount++;
            }
        }
        //Method to be called to start a transaction.
        //Takes in a customer which can be passed freely to which ever method needs it.
        public void BeginTransaction(Customer customer)
        {
            bool willProceed = UserInterface.DisplayWelcomeInstructions(_inventory);
            if (willProceed)
            {
                Transaction(customer);
            }
        }
        
        //This is the main transaction logic think of it like "runGame".  This is where the user will be prompted for the desired soda.
        //grab the desired soda from the inventory.
        //get payment from the user.
        //pass payment to the calculate transaction method to finish up the transaction based on the results.
        private void Transaction(Customer customer)
        {
            string userSelected = UserInterface.SodaSelection(_inventory);
            UserInterface.CoinSelection(GetSodaFromInventory(userSelected),customer.Wallet.Coins);
            CalculateTransaction(customer.GatherCoinsFromWallet(GetSodaFromInventory(userSelected)), GetSodaFromInventory(userSelected), customer);
        }
        //Gets a soda from the inventory based on the name of the soda.
        private Can GetSodaFromInventory(string nameOfSoda)
        {
            List<Can> HowMany = new List<Can>();
            foreach (var item in _inventory)
            {
                if (nameOfSoda == item.Name)
                {
                    HowMany.Add(item);   
                }
            }
            return HowMany.First(); // returning soda object to either be added to backpack or removed from sodamachine
            
        }

        //This is the main method for calculating the result of the transaction.
        //It takes in the payment from the customer, the soda object they selected, and the customer who is purchasing the soda.
        //This is the method that will determine the following:
        //If the payment is greater than the price of the soda, and if the sodamachine has enough change to return: Dispense soda, and change to the customer.
        //If the payment is greater than the cost of the soda, but the machine does not have ample change: Dispense payment back to the customer.
        //If the payment is exact to the cost of the soda:  Dispense soda.
        //If the payment does not meet the cost of the soda: dispense payment back to the customer.
        private void CalculateTransaction(List<Coin> payment, Can chosenSoda, Customer customer)
        {
            double totalValue = TotalCoinValue(payment);

            if (totalValue < chosenSoda.Price)
            {
                Console.WriteLine("You did not supply enough money. Returning coins to your wallet.");
                customer.AddCoinsIntoWallet(payment);
            }

            else if (totalValue == chosenSoda.Price)
            {
                DepositCoinsIntoRegister(payment);
                customer.AddCanToBackpack(chosenSoda);
                _inventory.Remove(chosenSoda);
            }

            else if (totalValue > chosenSoda.Price && _inventory.Count > 0)
            {
                double getChange = 0;
                DepositCoinsIntoRegister(payment);
                getChange = DetermineChange(totalValue, chosenSoda.Price);
                customer.AddCoinsIntoWallet(GatherChange(getChange));
                customer.AddCanToBackpack(chosenSoda);
                _inventory.Remove(chosenSoda);
            }
            else if (totalValue > chosenSoda.Price && _inventory.Count == 0)
            {
                Console.WriteLine("Soda out of stock. Returning coins to your wallet");
                customer.AddCoinsIntoWallet(payment);
            }
            //TotalCoinValue(GatherChange(DetermineChange(totalValue,chosenSoda.Price)));
        }
        //Takes in the value of the amount of change needed.
        //Attempts to gather all the required coins from the sodamachine's register to make change.
        //Returns the list of coins as change to despense.
        //If the change cannot be made, return null.
        private List<Coin> GatherChange(double changeValue)
        {
            List<Coin> GatherList = new List<Coin>();
            if(changeValue == 0)
            {
                return GatherList;
            }
            double quartersNeeded = changeValue / 25;
            if(quartersNeeded > 0 && RegisterHasCoin(newQuarter.Name) == true)
            {
                for (int i = 0; i < quartersNeeded; i++)
                {
                        GatherList.Add(GetCoinFromRegister(newQuarter.Name));
                }
            }
            changeValue -= (quartersNeeded * 25);
            double dimesNeeded = changeValue / 10;
            if (dimesNeeded > 0 && RegisterHasCoin(newDime.Name) == true)
            {
                for (int i = 0; i < quartersNeeded; i++)
                {
                    GatherList.Add(GetCoinFromRegister(newDime.Name));
                }
            }
            changeValue -= (dimesNeeded * 10);
            double nickelsNeeded = changeValue / 5;
            if (nickelsNeeded > 0 && RegisterHasCoin(newNickel.Name) == true)
            {
                for (int i = 0; i < quartersNeeded; i++)
                {
                    GatherList.Add(GetCoinFromRegister(newNickel.Name));
                }
            }
            changeValue -= (nickelsNeeded * 5);
            double penniesNeeded = changeValue;
            if (penniesNeeded > 0 && RegisterHasCoin(newPenny.Name) == true)
            {
                for (int i = 0; i < quartersNeeded; i++)
                {
                    GatherList.Add(GetCoinFromRegister(newPenny.Name));
                }
            }
            return GatherList;
        }
        //Reusable method to check if the register has a coin of that name.
        //If it does have one, return true.  Else, false.
        private bool RegisterHasCoin(string name)
        {
            string testName = "";
            while (testName != name)
            {
                foreach (var item in _register)
                {
                    if(name == item.Name)
                    {
                        testName = name;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            return true;
        }
        //Reusable method to return a coin from the register.
        //Returns null if no coin can be found of that name.
        private Coin GetCoinFromRegister(string name)
        {
            List<Coin> holdList = new List<Coin>();
            foreach (var item in _register)
            {
                if(item.Name == name)
                {
                    holdList.Add(item);
                }
            }
            return holdList.First();
        }
        //Takes in the total payment amount and the price of can to return the change amount.
        private double DetermineChange(double totalPayment, double canPrice)
        {
            double change = totalPayment - canPrice;
            return change;
        }
        //Takes in a list of coins to return the total value of the coins as a double.
        private double TotalCoinValue(List<Coin> payment)
        {
            DepositCoinsIntoRegister(payment);
            double totalValue = 0;
            foreach (var item in payment)
            {
                totalValue += item.Value;
            }
            return totalValue;
        }
        //Puts a list of coins into the soda machines register.
        private void DepositCoinsIntoRegister(List<Coin> coins)
        {
            foreach (var item in coins)
            {
                _register.Add(item);
            }
        }
    }
}
