﻿using BattleBoats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ExplorableWorld
{
    public class Game
    {
        //my massive list of very important global variables 

        private World MyWorld;
        private AiWorld MyAiWorld;
        private Player CurrentPlayer;
        private AiPlayer CurrentAiPlayer;
        string[,] grid;
        string[,] aigrid;
        public int BoatCount = 0;
        int PlayerMovesCount = 0; 
        int AiBoatCount = 0; //specifies how many boats the ai has.
        int AiMovesCount = 0; //specifies how many moves the ai has made
        int EnemyShipsSunk = 0; //specifies how many of the ai ships you have killed - used for declaring a winner
        int PlayerShipsSunk = 0; //specifies how many of your ships have been killed by the ai - used for declaring a winner
        public bool BoatToggle; //specifies if ai boats are visible or not


        //This declares the two main grids, initialises our essential classes, and then runs the main menu 
        public void Start() 
        {
            


            Title = "BattleBoats";
            CursorVisible = false; //makes cursor invisible to make game look nicer
            grid = new string[,] { {"~", "~", "~", "~", "~", "~", "~", "~"},
                                    {"~", "~", "~", "~", "~", "~", "~", "~"},
                                    {"~", "~", "~", "~", "~", "~", "~", "~"},
                                    {"~", "~", "~", "~", "~", "~", "~", "~"},
                                    {"~", "~", "~", "~", "~", "~", "~", "~"},
                                    {"~", "~", "~", "~", "~", "~", "~", "~"},
                                    {"~", "~", "~", "~", "~", "~", "~", "~"},
                                    {"~", "~", "~", "~", "~", "~", "~", "~"},
            }; //main player grid

            aigrid = new string[,] { {"~", "~", "~", "~", "~", "~", "~", "~"},
                                    {"~", "~", "~", "~", "~", "~", "~", "~"},
                                    {"~", "~", "~", "~", "~", "~", "~", "~"},
                                    {"~", "~", "~", "~", "~", "~", "~", "~"},
                                    {"~", "~", "~", "~", "~", "~", "~", "~"},
                                    {"~", "~", "~", "~", "~", "~", "~", "~"},
                                    {"~", "~", "~", "~", "~", "~", "~", "~"},
                                    {"~", "~", "~", "~", "~", "~", "~", "~"},
            };

            MyWorld = new World(grid);
            MyAiWorld = new AiWorld(aigrid);

            CurrentPlayer = new Player(0, 0);

            CurrentAiPlayer = new AiPlayer(0, 0);

            DisplayIntro();

        }



        //Menus

        private void DisplayIntro()
        {
            string prompt = @"
  ____        _   _   _        ____              _       
 |  _ \      | | | | | |      |  _ \            | |      
 | |_) | __ _| |_| |_| | ___  | |_) | ___   __ _| |_ ___ 
 |  _ < / _` | __| __| |/ _ \ |  _ < / _ \ / _` | __/ __|
 | |_) | (_| | |_| |_| |  __/ | |_) | (_) | (_| | |_\__ \
 |____/ \__,_|\__|\__|_|\___| |____/ \___/ \__,_|\__|___/
                                                         
(Use the arrow keys to cycle through options, then press enter to select.)
";
            string[] options = { "New Game", "Instructions", "Settings", "Quit" }; //menu using the MainMenu Class
            Menu mainMenu = new Menu(prompt, options, ConsoleColor.Blue);
            int SelectedIndex = mainMenu.RunOptions();

            switch (SelectedIndex)
            {
                case 0:
                    RunGameLoop();
                    break;
                case 1:
                    Instructions();
                    break;
                case 2:
                    Settings();
                    break;
                case 3:
                    QuitGame();
                    break;
            }
            
        }
        static void QuitGame()
        {
            Clear();
            Environment.Exit(0);
        }


        //Main settings menu and subcategories
        void Settings()
        {
            Clear();
            Title = "BattleBoats - Settings";
            string prompt = @"
   _____      _   _   _                 
  / ____|    | | | | (_)                
 | (___   ___| |_| |_ _ _ __   __ _ ___ 
  \___ \ / _ \ __| __| | '_ \ / _` / __|
  ____) |  __/ |_| |_| | | | | (_| \__ \
 |_____/ \___|\__|\__|_|_| |_|\__, |___/
                               __/ |    
                              |___/  

(Sneaky bonus developer settings.)
";
            WriteLine(prompt);

            string[] options = { "Show Ai Boats", "Main Menu" };
            Menu InstructionMenu = new Menu(prompt, options, ConsoleColor.Cyan);
            int SelectedIndex = InstructionMenu.RunOptions();

            switch (SelectedIndex)
            {
                case 0:
                    ToggleVisibility();
                    break;
                case 1:
                    DisplayIntro();
                    break;
            }


            ReadKey(true);
            DisplayIntro();
        }
        void ToggleVisibility()
        {
            Clear();
            Write("Do you want to make opponents boats visible\nwhen starting a game? [true/false]: ");
            string converttobool = ReadLine().ToLower();
            if (converttobool == "true")
            {
                BoatToggle = Convert.ToBoolean(converttobool);
                WriteLine($"\nAi Boat Visibility is now: {BoatToggle}\n\nPress anything to return...");
                ReadKey(true);
                Settings();
            }
            else
            {
                Settings();
            }
        }



        //Main instructions menu and subcategories

        private void Instructions()
        {
            Clear();
            Title = "BattleBoats - Info";
            string prompt = @"
  _____        __      
 |_   _|      / _|     
   | |  _ __ | |_ ___  
   | | | '_ \|  _/ _ \ 
  _| |_| | | | || (_) |
 |_____|_| |_|_| \___/ 
                                                         
(Instructions on how different parts of the game work.)
";
            WriteLine(prompt);
            string[] options = { "Controls", "Aim of the game", "How to play", "About", "Main Menu" };
            Menu InstructionMenu = new Menu(prompt, options, ConsoleColor.Yellow);
            int SelectedIndex = InstructionMenu.RunOptions();
            switch (SelectedIndex)
            {
                case 0:
                    Controls();
                    break;
                case 1:
                    AimOfGame();
                    break;
                case 2:
                    HowToGame();
                    break;
                case 3:
                    AboutGame();
                    break;
                case 4:
                    DisplayIntro();
                    break;
            }

            void Controls()
            {
                Clear();
                ForegroundColor = ConsoleColor.Black;
                BackgroundColor = ConsoleColor.White;
                WriteLine("Controls:\n");
                ResetColor();
                WriteLine(@"Movement: Use the arrow keys (←, →, ↑, ↓) in menus or in the game, wasd also works here. Press enter to place boats or use a menu. Pressing Escape while in game will take you to the main menu, and pressing 'new game' will return you to your current game.");
                ForegroundColor = ConsoleColor.Black;
                BackgroundColor = ConsoleColor.White;
                WriteLine("\nPress any key to return to the previous menu.");
                ResetColor();
                ReadKey(true);
                Instructions();
            }

            void AimOfGame()
            {
                Clear();
                ForegroundColor = ConsoleColor.Black;
                BackgroundColor = ConsoleColor.White;
                WriteLine("Aim of the game:\n");
                ResetColor();
                WriteLine(@"Battle boats is a turn based strategy game where players eliminate their opponents fleet of boats by ‘firing’ at a location on a grid in an attempt to sink them. The first player to sink all of their opponents’ battle boats is declared the winner. 

Each player has two eight by eight grids. One grid is used for their own battle boats and the other is used to record any hits or misses placed on their opponents. At the beginning of the game, players decide where they wish to place their fleet of five battle boats. 

During game play, players take it in turns to fire at a location on their opponent’s board. They do this by stating the coordinates for their target. If a player hits their opponent's boat then this is recorded as a hit. If they miss then this is recorded as a miss. 

The game ends when a player's fleet of boats have been sunk. The winner is the player with boats remaining at the end of the game.  
");
                ForegroundColor = ConsoleColor.Black;
                BackgroundColor = ConsoleColor.White;
                WriteLine("\nPress any key to return to the previous menu.");
                ResetColor();
                ReadKey(true);
                Instructions();
            }
            void HowToGame()
            {
                Clear();
                ForegroundColor = ConsoleColor.Black;
                BackgroundColor = ConsoleColor.White;
                WriteLine("How to play the game:\n");
                ResetColor();
                WriteLine(@"Select 'New Game' on the main menu, then enter your coordinates for each boat.
then enter coordinates for where you think the computers boats are.

Repeat until you either win or lose.");
                ForegroundColor = ConsoleColor.Black;
                BackgroundColor = ConsoleColor.White;
                WriteLine("\nPress any key to return to the previous menu.");
                ResetColor();
                ReadKey(true);
                Instructions();
            }
            void AboutGame()
            {
                Clear();
                ForegroundColor = ConsoleColor.Black;
                BackgroundColor = ConsoleColor.White;
                WriteLine("About the Game:\n");
                ResetColor();
                WriteLine(@"Licensed under GPL 3, Here is the github repository: https://github.com/Blade-Axe/BattleBoatsRelease");
                ForegroundColor = ConsoleColor.Black;
                BackgroundColor = ConsoleColor.White;
                WriteLine("\nPress any key to return to the previous menu.");
                ResetColor();
                ReadKey(true);
                Instructions();
            }
        }





        //The brains of the operation, aka essential ai boat placement, and player movement handling


        private void HandleAiInput()
        {
            if (PlayerMovesCount > 0)
            {
                while (true)
                {
                    int new_x = CurrentAiPlayer.Next_X();
                    int new_y = CurrentAiPlayer.Next_Y();
                    string elementAtAiPos = MyWorld.GetElementAt(new_x, new_y);
                    if (elementAtAiPos == "~")
                    {
                        AiMovesCount++;
                        grid[new_y, new_x] = "#";
                        break;
                    }
                    else if (elementAtAiPos == "@")
                    {
                        if (OperatingSystem.IsWindows())
                        {
                            SoundPlayer menuMusic = new SoundPlayer("placement.wav");
                            menuMusic.LoadAsync();
                            menuMusic.Play();
                        }
                        AiMovesCount++;
                        PlayerShipsSunk++;
                        grid[new_y, new_x] = "X";
                        break;
                    }
                    else if (elementAtAiPos == "X" || elementAtAiPos == "#")
                    {
                        continue;
                    }
                }
            }
            else
            {
                while (true)
                {
                    if (AiBoatCount != 5)
                    {
                        for (int i = 0; i < 5; i++)
                        {

                            while (true)
                            {
                                int new_x = CurrentAiPlayer.Next_X();
                                int new_y = CurrentAiPlayer.Next_Y();
                                string elementAtAiPos = MyAiWorld.GetElementAt(new_x, new_y);
                                if (elementAtAiPos == "~")
                                {
                                    AiBoatCount++;
                                    aigrid[new_y, new_x] = "@";
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }


                        }
                    }

                    break;
                }                
            }
        }

        private void HandlePlayerInput()
        {
            ConsoleKeyInfo keyInfo = ReadKey(true);
            ConsoleKey key = keyInfo.Key;
            switch (key)
            {
                case ConsoleKey.Enter:
                    
                    if (BoatCount == 5)
                    {
                        string elementAtPlayerPos = MyAiWorld.GetElementAt(CurrentPlayer.X, CurrentPlayer.Y);
                        if (elementAtPlayerPos == "~")
                        {
                            HandleAiInput();
                            PlayerMovesCount++;
                            aigrid[CurrentPlayer.Y, CurrentPlayer.X] = "#";
                        }
                        else if (elementAtPlayerPos == "@")
                        {
                            HandleAiInput();
                            PlayerMovesCount++;
                            EnemyShipsSunk++;
                            if (OperatingSystem.IsWindows())
                            {
                                SoundPlayer menuMusic = new SoundPlayer("placement.wav");
                                menuMusic.LoadAsync();
                                menuMusic.Play();
                            }
                            aigrid[CurrentPlayer.Y, CurrentPlayer.X] = "X";
                        }
                        else if (elementAtPlayerPos == "X")
                        {
                            break;
                        }
                        else if (elementAtPlayerPos == "#")
                        {
                            break;
                        }
                        break;
                    }
                    else 
                    {
                        if (OperatingSystem.IsWindows())
                        {
                            SoundPlayer menuMusic = new SoundPlayer("boatsplash.wav");
                            menuMusic.LoadAsync();
                            menuMusic.Play();
                        }
                        string elementAtPlayerPos = MyWorld.GetElementAt(CurrentPlayer.X, CurrentPlayer.Y);
                        if (elementAtPlayerPos == "~")
                        {
                            BoatCount++;
                            grid[CurrentPlayer.Y, CurrentPlayer.X] = "@";
                        }
                        break;
                    }
                    
                
                case ConsoleKey.UpArrow:
                    if (OperatingSystem.IsWindows())
                    {
                        SoundPlayer menuMusic = new SoundPlayer("hover.wav");
                        menuMusic.LoadAsync();
                        menuMusic.Play();
                    }
                    if (MyWorld.IsPositionWalkable(CurrentPlayer.X, CurrentPlayer.Y - 1))
                    {
                        CurrentPlayer.Y -= 1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (OperatingSystem.IsWindows())
                    {
                        SoundPlayer menuMusic = new SoundPlayer("hover.wav");
                        menuMusic.LoadAsync();
                        menuMusic.Play();
                    }
                    if (MyWorld.IsPositionWalkable(CurrentPlayer.X, CurrentPlayer.Y + 1))
                    {
                        CurrentPlayer.Y += 1;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (OperatingSystem.IsWindows())
                    {
                        SoundPlayer menuMusic = new SoundPlayer("hover.wav");
                        menuMusic.LoadAsync();
                        menuMusic.Play();
                    }
                    if (MyWorld.IsPositionWalkable(CurrentPlayer.X - 1, CurrentPlayer.Y))
                    {
                        CurrentPlayer.X -= 1;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (OperatingSystem.IsWindows())
                    {
                        SoundPlayer menuMusic = new SoundPlayer("hover.wav");
                        menuMusic.LoadAsync();
                        menuMusic.Play();
                    }
                    if (MyWorld.IsPositionWalkable(CurrentPlayer.X + 1, CurrentPlayer.Y))
                    {
                        CurrentPlayer.X += 1;
                    }
                    break;

                case ConsoleKey.W:
                    if (OperatingSystem.IsWindows())
                    {
                        SoundPlayer menuMusic = new SoundPlayer("hover.wav");
                        menuMusic.LoadAsync();
                        menuMusic.Play();
                    }
                    if (MyWorld.IsPositionWalkable(CurrentPlayer.X, CurrentPlayer.Y - 1))
                    {
                        CurrentPlayer.Y -= 1;
                    }
                    break;
                case ConsoleKey.S:
                    if (OperatingSystem.IsWindows())
                    {
                        SoundPlayer menuMusic = new SoundPlayer("hover.wav");
                        menuMusic.LoadAsync();
                        menuMusic.Play();
                    }
                    if (MyWorld.IsPositionWalkable(CurrentPlayer.X, CurrentPlayer.Y + 1))
                    {
                        CurrentPlayer.Y += 1;
                    }
                    break;
                case ConsoleKey.A:
                    if (OperatingSystem.IsWindows())
                    {
                        SoundPlayer menuMusic = new SoundPlayer("hover.wav");
                        menuMusic.LoadAsync();
                        menuMusic.Play();
                    }
                    if (MyWorld.IsPositionWalkable(CurrentPlayer.X - 1, CurrentPlayer.Y))
                    {
                        CurrentPlayer.X -= 1;
                    }
                    break;
                case ConsoleKey.D:
                    if (OperatingSystem.IsWindows())
                    {
                        SoundPlayer menuMusic = new SoundPlayer("hover.wav");
                        menuMusic.LoadAsync();
                        menuMusic.Play();
                    }
                    if (MyWorld.IsPositionWalkable(CurrentPlayer.X + 1, CurrentPlayer.Y))
                    {
                        CurrentPlayer.X += 1;
                    }
                    break;
                case ConsoleKey.Escape:
                    if (OperatingSystem.IsWindows())
                    {
                        SoundPlayer menuMusic = new SoundPlayer("hover.wav");
                        menuMusic.LoadAsync();
                        menuMusic.Play();
                    }
                    DisplayIntro();
                    break;
                default: 
                    break;
            }
        }


        //Game Statistics

        private void Stats()
        {
            SetCursorPosition(2, 22);
            WriteLine("\n  Statistics: \n");
            WriteLine($"  Boats Placed: {BoatCount}/5");
            WriteLine($"  Enemy Boats Sunk: {EnemyShipsSunk}/5");
            WriteLine($"  Turns Taken: {PlayerMovesCount}");
            WriteLine($"  Ai Turns Taken: {AiMovesCount}");
        }



        //Game endings, aka win or lose

        private void DisplayWinOutro()
        {
            Clear();
            if (OperatingSystem.IsWindows())
            {
                SoundPlayer menuMusic = new SoundPlayer("win.wav");
                menuMusic.LoadAsync();
                menuMusic.Play();
            }
            string prompt = @"
$$\     $$\                         $$\      $$\                     $$\ 
\$$\   $$  |                        $$ | $\  $$ |                    $$ |
 \$$\ $$  /$$$$$$\  $$\   $$\       $$ |$$$\ $$ | $$$$$$\  $$$$$$$\  $$ |
  \$$$$  /$$  __$$\ $$ |  $$ |      $$ $$ $$\$$ |$$  __$$\ $$  __$$\ $$ |
   \$$  / $$ /  $$ |$$ |  $$ |      $$$$  _$$$$ |$$ /  $$ |$$ |  $$ |\__|
    $$ |  $$ |  $$ |$$ |  $$ |      $$$  / \$$$ |$$ |  $$ |$$ |  $$ |    
    $$ |  \$$$$$$  |\$$$$$$  |      $$  /   \$$ |\$$$$$$  |$$ |  $$ |$$\ 
    \__|   \______/  \______/       \__/     \__| \______/ \__|  \__|\__|
";
            ForegroundColor = ConsoleColor.Green;
            WriteLine(prompt);
            ResetColor();


            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("  Statistics: \n");
            ResetColor();
            WriteLine($"  Enemy Boats Killed: {EnemyShipsSunk}/5");
            WriteLine($"  Turns Taken: {PlayerMovesCount}");
            WriteLine("\n  Thanks for playing! \n  Press E to return to exit.");
           
            Thread.Sleep(5000);

            ReadKey(true);
            QuitGame();
        }

        private void DisplayLostOutro()
        {
            Clear();
            if (OperatingSystem.IsWindows())
            {
                SoundPlayer menuMusic = new SoundPlayer("lost.wav");
                menuMusic.LoadAsync();
                menuMusic.Play();
            }
            string prompt = @"
 __   __  _______  __   __    ___      _______  _______  _______ 
|  | |  ||       ||  | |  |  |   |    |       ||       ||       |
|  |_|  ||   _   ||  | |  |  |   |    |   _   ||  _____||_     _|
|       ||  | |  ||  |_|  |  |   |    |  | |  || |_____   |   |  
|_     _||  |_|  ||       |  |   |___ |  |_|  ||_____  |  |   |  
  |   |  |       ||       |  |       ||       | _____| |  |   |  
  |___|  |_______||_______|  |_______||_______||_______|  |___|  
";
            ForegroundColor = ConsoleColor.DarkRed;
            WriteLine(prompt);
            ResetColor();


            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("  Statistics: \n");
            ResetColor();
            WriteLine($"  Enemy Boats Killed: {EnemyShipsSunk}/5");
            WriteLine($"  Turns Taken: {PlayerMovesCount}");
            WriteLine("\n  Thanks for playing! \n  Press E to return to exit.");

            Thread.Sleep(5000);

            ReadKey(true);
            QuitGame();
        }




        //Brings everything together here in order to draw to the screen and run the main game loop

        private void DrawFrame() //collects all elements together and draws them to the screen
        {
            Clear();
            MyWorld.Draw();
            MyAiWorld.Draw(BoatToggle);
            CurrentPlayer.Draw(BoatCount);
            CurrentAiPlayer.Draw();
            Stats();
        }
        private void RunGameLoop()
        {
            Title = "BattleBoats - The Game";
            HandleAiInput();
            //DisplayIntro();
            while (true)
            {
                // Draw everything
                DrawFrame();

                //HandleAiInput();
                // Check for player input and then move player
                
                
                HandlePlayerInput();
                

                // Check if player has killed all ai boats or vice versa and then end game
                if (EnemyShipsSunk == 5)
                {
                    break;
                }
                else if (PlayerShipsSunk == 5) 
                {
                    break;
                }

                // Give the player a chance to render
                System.Threading.Thread.Sleep(20);

                
            }
            if (EnemyShipsSunk == 5)
            {
                DisplayWinOutro();
            }
            else if (PlayerShipsSunk == 5)
            {
                DisplayLostOutro();
            }
            
        }
    }
}
