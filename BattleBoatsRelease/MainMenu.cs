using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Media;

namespace BattleBoats
{
    class Menu
    {
        private int SelectedIndex; //starts at 0 in the array
        private string[] Options; //menu options
        private string Prompt; //effectively the menu header
        private ConsoleColor promptColor; //allows a colour to be selected for the menu header "aka the prompt"

        public Menu(string prompt, string[] options, ConsoleColor menuColor)
        {
            promptColor = menuColor;
            Prompt = prompt;
            Options = options;
            SelectedIndex = 0;
        }

        private void DisplayOptions()
        {
            ForegroundColor = promptColor;
            WriteLine(Prompt);
            ResetColor();
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOptions = Options[i];
                string prefix;

                if (i == SelectedIndex)
                {
                    prefix = ">";
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = " ";
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                }

                WriteLine($"{prefix} {currentOptions}"); //prints out the prefix aka > along with whats specified in the array
            }
            ResetColor();
        }

        public int RunOptions()
        {
            ConsoleKey keyPressed;
            do
            {
                Clear();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                // Update SelectedIndex based on arrow keys

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    if (OperatingSystem.IsWindows())
                    {
                        SoundPlayer menuMusic = new SoundPlayer("hover.wav");
                        menuMusic.LoadAsync();
                        menuMusic.Play();
                    }
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.W)
                {
                    if (OperatingSystem.IsWindows())
                    {
                        SoundPlayer menuMusic = new SoundPlayer("hover.wav");
                        menuMusic.LoadAsync();
                        menuMusic.Play();
                    }
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    if (OperatingSystem.IsWindows())
                    {
                        SoundPlayer menuMusic = new SoundPlayer("hover.wav");
                        menuMusic.LoadAsync();
                        menuMusic.Play();
                    }
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }
                else if (keyPressed == ConsoleKey.S)
                {
                    if (OperatingSystem.IsWindows())
                    {
                        SoundPlayer menuMusic = new SoundPlayer("hover.wav");
                        menuMusic.LoadAsync();
                        menuMusic.Play();
                    }
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }

            } while (keyPressed != ConsoleKey.Enter);
            if (OperatingSystem.IsWindows())
            {
                SoundPlayer menuMusic = new SoundPlayer("selected.wav");
                menuMusic.LoadAsync();
                menuMusic.Play();
            }

            return SelectedIndex;
        }
    }
}
