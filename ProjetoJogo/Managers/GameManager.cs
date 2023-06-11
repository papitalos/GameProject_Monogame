﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjetoJogo.Models.Enemy;
using ProjetoJogo.Models.Jogador;
using ProjetoJogo.Models.Rastro;
using ProjetoJogo.Models.UI;
using ProjetoJogo.Models.Weapons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjetoJogo.Game1;

namespace ProjetoJogo.Managers
{
    public class GameManager
    {

        public Menu menu; 
        public PlayerData playerData;
        public Player player;


        public static GameState state;

        public enum GameState
        {
            Menu,
            Playing,
            Quitting
        }

        public void Init()
        {
            //Estado inicial
            state = GameState.Menu;


         

            var bullet_tex = Globals.Content.Load<Texture2D>("bullet");
          

            BulletManager.Init(bullet_tex);
            UIManager.Init(bullet_tex);

            
            menu = new Menu();
            playerData = new PlayerData();
            player = new Player(playerData);

            EnemyManager.Init();


        }
        public void Restart()
        {
            BulletManager.Reset();
            EnemyManager.Reset();
            player.Reset();
        }
        public void Update()
        {
            if (playerData.Dead) Restart();
            //Verifica os inputs
            InputManager.Update();
            
            if (state == GameState.Menu)
            {
                menu.Update();
            }
            if (state == GameState.Playing)
            {
                player.Update();
                Camera.Follow(player);

                EnemyManager.Update(player);
                BulletManager.Update();
            }

        }

        public void Draw()
        {
            if (state == GameState.Menu)
            {
                menu.Draw();
            }
            if (state == GameState.Playing)
            {
                BulletManager.Draw();
                player.Draw();
                EnemyManager.Draw();
                UIManager.Draw(playerData);

            }

        }
    }
}
