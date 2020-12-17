﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Customer
    {
        //Member Variables (Has A)
        public Wallet Wallet;
        public Backpack Backpack;

        //Constructor (Spawner)
        public Customer()
        {
            Wallet = new Wallet();
            Backpack = new Backpack();
        }
        //Member Methods (Can Do)

        //This method will be the main logic for a customer to retrieve coins form their wallet.
        //Takes in the selected can for price reference
        //Will need to get user input for coins they would like to add.
        //When all is said and done this method will return a list of coin objects that the customer will use a payment for their soda.
        public List<Coin> GatherCoinsFromWallet(Can selectedCan)
        {
            
            GetCoinFromWallet(UserInterface.CoinSelection(selectedCan, Wallet.Coins));
            // CUSTOMER IS PICKING COINS FROM WALLET TO USE, DOESNT MEAN THE COINS ARE REALLY THERE

        }
        //Returns a coin object from the wallet based on the name passed into it.
        //Returns null if no coin can be found
        public Coin GetCoinFromWallet(string coinName)
        {
            // CHECKS TO SEE IF WALLET CONTAINS THE GATHER COINS FROM WALLET CALL
            List<Coin> holdingArea = new List<Coin>();
            string holdingString = "";
            foreach (var item in Wallet.Coins)
            {
                if(item.Name == coinName)
                {
                    holdingString = coinName;
                }
            }return holdingArea.Add()

        }
        //Takes in a list of coin objects to add into the customers wallet.
        public void AddCoinsIntoWallet(List<Coin> coinsToAdd)
        {

        }
        //Takes in a can object to add to the customers backpack.
        public void AddCanToBackpack(Can purchasedCan)
        {
            
        }
    }
}
