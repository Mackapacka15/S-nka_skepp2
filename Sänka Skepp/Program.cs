using System.Security.Cryptography;
using System.Runtime.Serialization.Json;
using System;
using Raylib_cs;
using System.Numerics;
using System.Collections.Generic;

namespace Sänka_Skepp
{
    class Program
    {
        static void Main(string[] args)
        {
            Raylib.InitWindow(500, 700, "Sänka Skepp");
            Raylib.SetTargetFPS(60);
            int height = Raylib.GetScreenHeight();
            int width = Raylib.GetScreenWidth();
            string state = "menu";
            string state2 = "playerPlace";
            int distance = 100;
            List<int> playerBoats = new List<int>();
            List<int> botBoats = new List<int>();

            int whichBoat = 1;

            List<Rectangle> grid = new List<Rectangle>();

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    Rectangle r = new Rectangle(x * 100, y * 100, 99, 99);
                    grid.Add(r);
                }
            }

            while (!Raylib.WindowShouldClose())
            {

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.BLACK);

                if (state == "menu")
                {
                    Raylib.DrawText("Welcome to Battleship", width / 2 - 130, 200, 25, Color.WHITE);
                    Raylib.DrawText("Press enter to continue", width / 2 - 125, 250, 20, Color.WHITE);
                    Raylib.DrawText("Press H for rules", width / 2 - 110, 300, 20, Color.WHITE);
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                    {
                        state = "place";
                    }
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_H))
                    {
                        state = "rules";
                    }
                }
                if (state == "place")
                {
                    for (int i = 0; i < 6; i++)
                    {
                        Raylib.DrawLine(i * distance, 1, i * distance, 500, Color.WHITE);
                        Raylib.DrawLine(1, i * distance, 500, i * distance, Color.WHITE);
                    }
                    for (int i = 0; i < 25; i++)
                    {
                        Raylib.DrawRectangleRec(grid[i], Color.BLUE);
                    }

                    if (state2 == "playerPlace")
                    {
                        Raylib.DrawText("Place boat nr: " + whichBoat, 50, 510, 20, Color.WHITE);

                        Vector2 mousePosition = new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY());

                        int whichRectangleWasClicked = 0;
                        for (int i = 0; i < 25; i++)
                        {
                            if (Raylib.CheckCollisionPointRec(mousePosition, grid[i]) && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                            {
                                if (!playerBoats.Contains(i))
                                {
                                    whichRectangleWasClicked = i;
                                    playerBoats.Add(whichRectangleWasClicked);
                                    whichBoat++;
                                }

                            }
                        }

                        for (int i = 0; i < playerBoats.Count; i++)
                        {
                            Raylib.DrawRectangleRec(grid[playerBoats[i]], Color.RED);
                        }
                        if (whichBoat >= 6)
                        {
                            state2 = "botplace";
                        }
                    }

                    if (state2 == "botplace")
                    {
                        Random generator = new Random();

                        for (int i = 0; i < 5; i++)
                        {
                            int p = generator.Next(0, 25);
                            if (botBoats.Contains(p))
                            {
                                p = generator.Next(0, 25);
                            }
                            else if (!botBoats.Contains(p))
                            {
                                botBoats.Add(p);
                            }
                        }
                        for (int i = 0; i < 5; i++)
                        {
                            Raylib.DrawRectangleRec(grid[botBoats[i]], Color.RED);
                        }
                    }
                }
                if (state == "rules")
                {
                    Raylib.DrawText("Rules:", 225, 250, 20, Color.WHITE);
                    Raylib.DrawText("The rules are simple. First you will place your boats.", 50, 270, 15, Color.WHITE);
                    Raylib.DrawText("When placing the boats the square you put your boat in will", 20, 290, 15, Color.WHITE);
                    Raylib.DrawText("become red. When all your boats are placed in the enemy will", 20, 310, 15, Color.WHITE);
                    Raylib.DrawText("randomize a tile and shoot it. If there is a boat there the tile", 20, 330, 15, Color.WHITE);
                    Raylib.DrawText("will become red but if it misses the tile will turn blue. Now it's your", 20, 350, 15, Color.WHITE);
                    Raylib.DrawText("time to attack. Press a tile and the tile will become red if you hit ", 20, 370, 15, Color.WHITE);
                    Raylib.DrawText("and blue if you miss.", 200, 390, 15, Color.WHITE);
                    Raylib.DrawText("Press Enter to go back", 100, 410, 25, Color.WHITE);
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                    {
                        state = "menu";
                    }
                }



                Raylib.EndDrawing();
            }
        }
    }
}
