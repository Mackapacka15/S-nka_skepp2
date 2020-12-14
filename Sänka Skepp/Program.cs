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
            bool playerHasShot = false;
            float timer = 1;
            Random generator = new Random();
            List<int> playerBoats = new List<int>();
            List<int> botBoats = new List<int>();
            List<int> playerShots = new List<int>();
            List<int> botShots = new List<int>();
            List<int> playerHits = new List<int>();
            List<int> botHits = new List<int>();
            int whichBoat = 1;
            int botsShot = 0;
            int playersShot = 0;
            bool didItHit = false;
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
                if (state == "rules")
                {
                    Raylib.DrawText("Rules:", 225, 250, 20, Color.WHITE);
                    Raylib.DrawText("The rules are simple. First you will place your boats.", 50, 270, 15, Color.WHITE);
                    Raylib.DrawText("When placing the boats the square you put your boat in will", 20, 290, 15, Color.WHITE);
                    Raylib.DrawText("become red. When all your boats are placed in the enemy will", 20, 310, 15, Color.WHITE);
                    Raylib.DrawText("randomize a tile and shoot it. If there is a boat there the tile", 20, 330, 15, Color.WHITE);
                    Raylib.DrawText("will become red but if it misses the tile will turn yellow. Now it's your", 20, 350, 15, Color.WHITE);
                    Raylib.DrawText("time to attack. Press a tile and the tile will become red if you hit ", 20, 370, 15, Color.WHITE);
                    Raylib.DrawText("and yellow if you miss.", 200, 390, 15, Color.WHITE);
                    Raylib.DrawText("Press Enter to go back", 100, 410, 25, Color.WHITE);
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                    {
                        state = "menu";
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
                        if (whichBoat < 6)
                        {
                            Raylib.DrawText("Place boat nr: " + whichBoat, 50, 510, 20, Color.WHITE);
                        }
                        Vector2 mousePosition = new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY());

                        int whichRectangleWasClicked = 0;
                        if (whichBoat < 6)
                        {
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
                        }
                        //System.Console.WriteLine(String.Join(',', playerBoats));
                        for (int i = 0; i < playerBoats.Count; i++)
                        {
                            Raylib.DrawRectangleRec(grid[playerBoats[i]], Color.RED);
                        }
                        if (whichBoat >= 6)
                        {
                            timer -= Raylib.GetFrameTime();
                            if (timer < 0)
                            {

                                state2 = "botplace";
                            }
                        }
                    }

                    if (state2 == "botplace")
                    {

                        //System.Console.WriteLine(String.Join(',', playerBoats));
                        for (int i = 0; i < 5; i++)
                        {
                            int p = generator.Next(0, 25);
                            while (botBoats.Contains(p))
                            {
                                p = generator.Next(0, 25);
                            }
                            botBoats.Add(p);

                        }
                        /*
                        for (int i = 0; i < 5; i++)
                        {
                            Raylib.DrawRectangleRec(grid[botBoats[i]], Color.RED);
                        }
                        */
                    }
                    if (botBoats.Count == 5 && playerBoats.Count == 5)
                    {
                        state = "battle";
                        state2 = "botShot";
                        timer = 5;
                    }
                }
                if (state == "battle")
                {
                    //System.Console.WriteLine(String.Join(',', playerBoats));
                    //System.Console.WriteLine("Test");
                    for (int i = 0; i < 6; i++)
                    {
                        Raylib.DrawLine(i * distance, 1, i * distance, 500, Color.WHITE);
                        Raylib.DrawLine(1, i * distance, 500, i * distance, Color.WHITE);
                    }
                    for (int i = 0; i < 25; i++)
                    {
                        Raylib.DrawRectangleRec(grid[i], Color.BLUE);
                    }

                    if (state2 == "botShot")
                    {
                        int p = generator.Next(0, 25);
                        while (botHits.Contains(p) || botShots.Contains(p))
                        {
                            p = generator.Next(0, 25);
                        }
                        if (playerBoats.Contains(p))
                        {
                            botHits.Add(p);
                            didItHit = true;
                        }
                        else
                        {
                            botShots.Add(p);
                        }

                        botsShot = p;
                        state2 = "showBotShot";
                        timer = 5;

                    }

                    if (state2 == "showBotShot")
                    {
                        if (didItHit)
                        {
                            Raylib.DrawText("The bot shot at square " + botsShot + " and hit", 50, 530, 20, Color.WHITE);
                        }
                        else
                        {
                            Raylib.DrawText("The bot shot at square " + botsShot + " and missed", 50, 530, 20, Color.WHITE);
                        }
                        if (botShots.Count > 0)
                        {
                            for (int i = 0; i < botShots.Count; i++)
                            {
                                Raylib.DrawRectangleRec(grid[botShots[i]], Color.YELLOW);
                            }

                        }
                        if (botHits.Count > 0)
                        {
                            for (int i = 0; i < botHits.Count; i++)
                            {
                                Raylib.DrawRectangleRec(grid[botHits[i]], Color.RED);
                            }
                        }
                        timer -= Raylib.GetFrameTime();
                        if (timer < 0)
                        {
                            state2 = "playerShot";
                            playerHasShot = false;
                            didItHit = false;
                        }
                    }


                    if (state2 == "playerShot")
                    {

                        Raylib.DrawText("Your turn to shoot.", 50, 510, 20, Color.WHITE);

                        if (playerShots.Count > 0)
                        {
                            for (int i = 0; i < playerShots.Count; i++)
                            {

                                Raylib.DrawRectangleRec(grid[playerShots[i]], Color.YELLOW);
                            }

                        }
                        if (playerHits.Count > 0)
                        {
                            for (int i = 0; i < playerHits.Count; i++)
                            {
                                Raylib.DrawRectangleRec(grid[playerHits[i]], Color.RED);
                            }
                        }
                        Vector2 mousePosition = new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY());

                        for (int i = 0; i < 25; i++)
                        {
                            if (Raylib.CheckCollisionPointRec(mousePosition, grid[i]) && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                            {
                                if (!playerShots.Contains(i) && !playerHits.Contains(i))
                                {
                                    if (botBoats.Contains(i))
                                    {
                                        playerHits.Add(i);
                                        playerHasShot = true;
                                        playersShot = i;
                                        didItHit = true;
                                    }
                                    else
                                    {
                                        playerShots.Add(i);
                                        playerHasShot = true;
                                        playersShot = i;
                                    }

                                }
                            }
                        }
                        for (int i = 0; i < playerShots.Count; i++)
                        {
                            Raylib.DrawRectangleRec(grid[playerShots[i]], Color.RED);
                        }

                        Raylib.DrawText(playerShots.Count.ToString(), 50, 560, 20, Color.WHITE);
                        if (playerHasShot)
                        {
                            state2 = "showPlayerShots";
                            System.Console.WriteLine("Test");
                            timer = 5;
                        }

                    }
                    if (state2 == "showPlayerShots")
                    {
                        if (didItHit)
                        {
                            Raylib.DrawText("You shot at square " + playersShot + " and hit", 50, 530, 20, Color.WHITE);
                        }
                        else
                        {
                            Raylib.DrawText("You shot at square " + playersShot + " and missed", 50, 530, 20, Color.WHITE);
                        }

                        if (playerShots.Count > 0)
                        {
                            for (int i = 0; i < playerShots.Count; i++)
                            {

                                Raylib.DrawRectangleRec(grid[playerShots[i]], Color.YELLOW);
                            }

                        }
                        if (playerHits.Count > 0)
                        {
                            for (int i = 0; i < playerHits.Count; i++)
                            {
                                Raylib.DrawRectangleRec(grid[playerHits[i]], Color.RED);
                            }
                        }
                        timer -= Raylib.GetFrameTime();
                        if (timer < 0)
                        {
                            state2 = "botShot";
                            didItHit = false;
                        }
                    }

                }

                Raylib.EndDrawing();
            }
        }

    }
}
