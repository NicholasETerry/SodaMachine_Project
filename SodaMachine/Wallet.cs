using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Wallet
    {
        //Member Variables (Has A)
        public List<Coin> Coins;
        Quarter newQuarter;
        Dime newDime;
        Nickel newNickel;
        Penny newPenny;
        //Constructor (Spawner)
        public Wallet()
        {
            Coins = new List<Coin>();
            FillWallet();// FillRegister();
        }
        //Member Methods (Can Do)
        //Fills wallet with starting money
        private void FillWallet()// FillRegister()
        {
            // starting wallet amount is $5.00 in mixed change
            int startingAmount = 0;
            while (startingAmount != 12)
            // 12 quarters
            {
                Coins.Add(newQuarter);
                startingAmount++;
            }
            startingAmount = 0;
            while (startingAmount != 10)
            {
                // 10 dimes
                Coins.Add(newDime);
                startingAmount++;
            }
            startingAmount = 0;
            while (startingAmount != 15)
            {
                // 15 nickels
                Coins.Add(newNickel);
                startingAmount++;
            }
            startingAmount = 0;
            while (startingAmount != 25)
            {
                // 25 pennies
                Coins.Add(newPenny);
                startingAmount++;
            }
        }
    }
}
