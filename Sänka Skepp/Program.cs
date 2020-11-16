using System;
using Raylib_cs;
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
            int distance = 100;
            int[] playerBoats = new int[5];
            while (!Raylib.WindowShouldClose())
            {
                Rectangle r1=new Rectangle(0,1,99,99);
                Rectangle r2=new Rectangle(100,1,99,99);
                Rectangle r3=new Rectangle(200,1,99,99);
                Rectangle r4=new Rectangle(300,1,99,99);
                Rectangle r5=new Rectangle(400,1,99,99);

                Rectangle r6=new Rectangle(0,101,99,99);
                Rectangle r7=new Rectangle(100,101,99,99);
                Rectangle r8=new Rectangle(200,101,99,99);
                Rectangle r9=new Rectangle(300,101,99,99);
                Rectangle r10=new Rectangle(400,101,99,99);


                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.BLACK);

                if (state == "menu")
                {
                    Raylib.DrawText("Welcome to Battleship", width / 2 - 130, 200, 25, Color.WHITE);
                    Raylib.DrawText("Press enter to continue", width / 2 - 125, 250, 20, Color.WHITE);
                    Raylib.DrawText("Press H for rules", width / 2 - 110, 300, 20, Color.WHITE);
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                    {
                        state = "game";
                    }
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_H))
                    {
                        state="rules";
                    }
                }
                if (state == "game")
                {
                    for (int i = 0; i < 6; i++)
                    {
                        Raylib.DrawLine(i * distance, 1, i * distance, 500, Color.WHITE);
                        Raylib.DrawLine(1, i * distance, 500, i * distance, Color.WHITE);
                        
                        Raylib.DrawRectangleRec(r6,Color.ORANGE);
                        Raylib.DrawRectangleRec(r7,Color.RED);
                        Raylib.DrawRectangleRec(r8,Color.MAGENTA);
                        Raylib.DrawRectangleRec(r9,Color.MAROON);
                        Raylib.DrawRectangleRec(r10,Color.SKYBLUE);
                        
                    }

                }
                if (state=="rules")
                {
                    Raylib.DrawText("Rules:",225,250,20,Color.WHITE);
                    Raylib.DrawText("The rules are simple. First you will place your boats.",50,270,15,Color.WHITE);
                    Raylib.DrawText("When placing the boats the square you put your boat in will",20,290,15,Color.WHITE);
                    Raylib.DrawText("become red. When all your boats are placed in the enemy will",20,310,15,Color.WHITE);
                    Raylib.DrawText("randomize a tile and shoot it. If there is a boat there the tile",20,330,15,Color.WHITE);
                    Raylib.DrawText("will become red but if it misses the tile will turn blue. Now it's your",20,350,15,Color.WHITE);
                    Raylib.DrawText("time to attack. Press a tile and the tile will become red if you hit ",20,370,15,Color.WHITE);
                    Raylib.DrawText("and blue if you miss.",200,390,15,Color.WHITE);
                    Raylib.DrawText("Press Enter to go back",100,410,25,Color.WHITE);
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                    {
                        state="menu";
                    }
                }



                Raylib.EndDrawing();
            }
        }
    }
}
