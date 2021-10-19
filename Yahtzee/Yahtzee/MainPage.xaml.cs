using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Yahtzee
{
    public partial class MainPage : ContentPage
    {
        //Constant variables
        const string ROLL_DICE = "Roll Dice";
        const string NEW_GAME = "New Game";
        const string END_GO = "End of Go";
        int clicks = 0;
        int dieSelect1 = 0, dieSelect2 = 0, dieSelect3 = 0, dieSelect4 = 0, dieSelect5 = 0;
        int ones, twos, threes, fours, fives, sixes;
        int upSubtotal, upBonus, upTOTAL;
        int kind3, kind4, fullHouse, smStr, lgStr, yahtzee, chance;
        int lowerTotal, grandTOTAL;

        //Create instance of random
        Random random;

        public MainPage()
        {
            InitializeComponent();
        }

        //Start New Game
        private void SetUpNewGame()
        {
            //Make sure counter has reset
            clicks = 0;

            //Change button Text
            Btn_RollDice.Text = "Roll Dice";

            //Enable all buttons
            //Set everything to 0
        }


        //End of Go
        private void EndGo()
        {
            //Change Text of Button
            Btn_RollDice.Text = "End of Go";

            //Disable Button 
            Btn_RollDice.IsEnabled = false;

            //Reset Counter
            clicks = 0;
        }

        //New Go
        private void NewGo()
        {
            RollDice(0);
        }




        //----------------------------------------------------------------------------------------------------
        
        //RollDice
        private void Btn_RollDice_Clicked(object sender, EventArgs e)
        {
            //Update Counter
            clicks++;

            //Read Button Text
            string ButtonText = ((Button)sender).Text;

            //switch to change objective depending on button text
            switch (ButtonText)
            {
                //If Text is to roll Dice
                case ROLL_DICE:
                {
                    //Roll Dice
                    RollDice(clicks);
                    break;
                }

                //If New Game is Text
                case END_GO:
                {
                    //Start new Go
                    NewGo();
                    break;
                }

                //If New Game is Text
                case NEW_GAME:
                {
                    //Start new Game
                    SetUpNewGame();
                    break;
                }
            }
        }

        //Roll Dice 3 Times
        private void RollDice(int i)
        {
            //Variables
            int diceRoll1 = 0, diceRoll2 = 0, diceRoll3 = 0, diceRoll4 = 0, diceRoll5 = 0;
            ones = 0;
            twos = 0;
            threes = 0;
            fours = 0;
            fives = 0;
            sixes = 0;

            //If Random not initialized
            if (random == null)
            {
                random = new Random();
            }

            //Check if Dice are Selected
            //Dice 1
            if (dieSelect1 == 0)
            {
                //Generate a random number between 1 and 6
                diceRoll1 = random.Next(1, 7);
            }

            //Dice 2
            if (dieSelect2 == 0)
            {
                //Generate a random number between 1 and 6
                diceRoll2 = random.Next(1, 7);
            }

            //Dice 3
            if (dieSelect3 == 0)
            {
                //Generate a random number between 1 and 6
                diceRoll3 = random.Next(1, 7);
            }

            //Dice 4
            if (dieSelect4 == 0)
            {
                //Generate a random number between 1 and 6
                diceRoll4 = random.Next(1, 7);
            }

            //Dice 5
            if (dieSelect5 == 0)
            {
                //Generate a random number between 1 and 6
                diceRoll5 = random.Next(1, 7);
            }

            //Display Dice
            DisplayDice(diceRoll1, 1);
            DisplayDice(diceRoll2, 2);
            DisplayDice(diceRoll3, 3);
            DisplayDice(diceRoll4, 4);
            DisplayDice(diceRoll5, 5);

            if (i >= 3)
            {
                EndGo();
            }
         }

        //Display Dice on Screen
        private void DisplayDice(int roll, int dice)
        {
            //Depending on which dice it is
            switch (dice)
            {
                //Dice 1
                case 1:
                    //Select dice face image depending on random number
                    switch (roll)
                    {
                        case 1:
                            DiceRoll1.Source = "DiceImages/Dice1.png";
                            if(Btn_Ones.IsEnabled == true)
                            {
                                ones += 1;
                            }
                            break;

                        case 2:
                            DiceRoll1.Source = "DiceImages/Dice2.png";
                            twos += 2;
                            break;

                        case 3:
                            DiceRoll1.Source = "DiceImages/Dice3.png";
                            threes += 3;
                            break;

                        case 4:
                            DiceRoll1.Source = "DiceImages/Dice4.png";
                            fours += 4;
                            break;

                        case 5:
                            DiceRoll1.Source = "DiceImages/Dice5.png";
                            fives += 5;
                            break;

                        case 6:
                            DiceRoll1.Source = "DiceImages/Dice6.png";
                            sixes += 6;
                            break;
                    }
                break;

                //Dice 2
                case 2:
                    //Select dice face image depending on random number
                    switch (roll)
                    {
                        case 1:
                            DiceRoll2.Source = "DiceImages/Dice1.png";
                            break;

                        case 2:
                            DiceRoll2.Source = "DiceImages/Dice2.png";
                            break;

                        case 3:
                            DiceRoll2.Source = "DiceImages/Dice3.png";
                            break;

                        case 4:
                            DiceRoll2.Source = "DiceImages/Dice4.png";
                            break;

                        case 5:
                            DiceRoll2.Source = "DiceImages/Dice5.png";
                            break;

                        case 6:
                            DiceRoll2.Source = "DiceImages/Dice6.png";
                            break;
                    }
                break;

                //Dice 3
                case 3:
                    //Select dice face image depending on random number
                    switch (roll)
                    {
                        case 1:
                            DiceRoll3.Source = "DiceImages/Dice1.png";
                            break;

                        case 2:
                            DiceRoll3.Source = "DiceImages/Dice2.png";
                            break;

                        case 3:
                            DiceRoll3.Source = "DiceImages/Dice3.png";
                            break;

                        case 4:
                            DiceRoll3.Source = "DiceImages/Dice4.png";
                            break;

                        case 5:
                            DiceRoll3.Source = "DiceImages/Dice5.png";
                            break;

                        case 6:
                            DiceRoll3.Source = "DiceImages/Dice6.png";
                            break;
                    }
                break;

                //Dice 4
                case 4:
                    //Select dice face image depending on random number
                    switch (roll)
                    {
                        case 1:
                            DiceRoll4.Source = "DiceImages/Dice1.png";
                            break;

                        case 2:
                            DiceRoll4.Source = "DiceImages/Dice2.png";
                            break;

                        case 3:
                            DiceRoll4.Source = "DiceImages/Dice3.png";
                            break;

                        case 4:
                            DiceRoll4.Source = "DiceImages/Dice4.png";
                            break;

                        case 5:
                            DiceRoll4.Source = "DiceImages/Dice5.png";
                            break;

                        case 6:
                            DiceRoll4.Source = "DiceImages/Dice6.png";
                            break;
                    }
                break;

                //Dice 5
                case 5:
                    //Select dice face image depending on random number
                    switch (roll)
                    {
                        case 1:
                            DiceRoll5.Source = "DiceImages/Dice1.png";
                            break;

                        case 2:
                            DiceRoll5.Source = "DiceImages/Dice2.png";
                            break;

                        case 3:
                            DiceRoll5.Source = "DiceImages/Dice3.png";
                            break;

                        case 4:
                            DiceRoll5.Source = "DiceImages/Dice4.png";
                            break;

                        case 5:
                            DiceRoll5.Source = "DiceImages/Dice5.png";
                            break;

                        case 6:
                            DiceRoll5.Source = "DiceImages/Dice6.png";
                            break;
                    }
                break;
            }
            LblBtnOne.Text = ones.ToString();
            LblBtnTwo.Text = twos.ToString();
            LblBtnThree.Text = threes.ToString();
            LblBtnFour.Text = fours.ToString();
            LblBtnFive.Text = fives.ToString();
            LblBtnSix.Text = sixes.ToString();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        //Display what you can Select




        //-----------------------------------------------------------------------------------------------------------------------------
        //Select and Unselect Die

        //Dice 1 when Selected
        private void Dice1Select_Clicked(object sender, EventArgs e)
        {
            //Keep Selected Die
            if (dieSelect1 == 0)
            {
                SelectDie(1);
            }

            //Deselect die
            else
            {
                dieSelect1 = 0;
            }
        }

        //Dice 2 when Selected
        private void Dice2Select_Clicked(object sender, EventArgs e)
        {
            //Keep Selected Die
            if (dieSelect2 == 0)
            {
                SelectDie(2);
            }

            //Deselect die
            else
            {
                dieSelect2 = 0;
            }
        }

        //Dice 3 when Selected
        private void Dice3Select_Clicked(object sender, EventArgs e)
        {
            //Keep Selected Die
            if (dieSelect3 == 0)
            {
                SelectDie(3);
            }

            //Deselect die
            else
            {
                dieSelect3 = 0;
            }
        }

        //Dice 4 when Selected
        private void Dice4Select_Clicked(object sender, EventArgs e)
        {
            //Keep Selected Die
            if (dieSelect4 == 0)
            {
                SelectDie(4);
            }

            //Deselect die
            else
            {
                dieSelect4 = 0;
            }
        }

        //Dice 5 when Selected
        private void Dice5Select_Clicked(object sender, EventArgs e)
        {
            //Keep Selected Die
            if (dieSelect5 == 0)
            {
                SelectDie(5);
            }

            //Deselect die
            else
            {
                dieSelect5 = 0;
            }
        }



        //----------------------------------------------------------------------------------------------------------------

        //Select Die
        private void SelectDie(int die)
        {
            switch (die)
            {
                case 1:
                    dieSelect1 = 1;
                break;

                case 2:
                    dieSelect2 = 1;
                break;

                case 3:
                    dieSelect3 = 1;
                break;

                case 4:
                    dieSelect4 = 1;
                break;

                case 5:
                    dieSelect5 = 1;
                break;
            }
        }



        //----------------------------------------------------------------------------------------------------------------
        //Select Buttons for Scoring

        //Upper Section
        //Select Ones
        private void Btn_Ones_Clicked(object sender, EventArgs e)
        {
            Btn_Ones.IsEnabled = false;
        }

        //Select Twos
        private void Btn_Twos_Clicked(object sender, EventArgs e)
        {

        }

        //Select Threes
        private void Btn_Threes_Clicked(object sender, EventArgs e)
        {

        }

        //Select Fours
        private void Btn_Fours_Clicked(object sender, EventArgs e)
        {

        }

        //Select Fives
        private void Btn_Fives_Clicked(object sender, EventArgs e)
        {

        }

        //Select Sixes
        private void Btn_Sixes_Clicked(object sender, EventArgs e)
        {

        }

        //Lower Section
        //Select 3Kind
        private void Btn_3Kind_Clicked(object sender, EventArgs e)
        {

        }

        //Select 4Kind
        private void Btn_4Kind_Clicked(object sender, EventArgs e)
        {

        }

        //Select FullHouse
        private void Btn_FullHouse_Clicked(object sender, EventArgs e)
        {

        }

        //Select smStraight
        private void Btn_SmStraight_Clicked(object sender, EventArgs e)
        {

        }

        //Select lgStraight
        private void Btn_LgStraight_Clicked(object sender, EventArgs e)
        {

        }

        //Select Yahtzee
        private void Btn_YAHTZEE_Clicked(object sender, EventArgs e)
        {

        }

        //Select Chance
        private void Btn_Chance_Clicked(object sender, EventArgs e)
        {

        }
    }
}
