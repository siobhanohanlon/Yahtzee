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
        //Create instance of random
        Random random;

        public MainPage()
        {
            InitializeComponent();

            //Roll Dice
            RollDice();
        }

        //RollDice
        private void RollDice()
        {
            for(int i=0; i<5; i++)
            {
                //Variables
                int diceRoll = 0;

                //If Random not initialized
                if (random == null)
                {
                    random = new Random();
                }

                //Generate a random number between 1 and 6
                diceRoll = random.Next(1, 7);

                //Display Dice
                DisplayDice(diceRoll);
            }
        }

        //Display Dice on Screen
        private void DisplayDice(int roll)
        {
            switch(roll)
            {
                case 1:

                break;
            }
        }

        //Upper Section
        //Select Ones
        private void Btn_Ones_Clicked(object sender, EventArgs e)
        {
            string ButtonText = ((Button)sender).Text;

            

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
