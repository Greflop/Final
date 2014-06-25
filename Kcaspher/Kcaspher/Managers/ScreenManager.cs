using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Projet_2._0.Menus;
using Projet_2._0.Levels;
using Projet_2._0.AI;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Kcaspher.AI;
using Kcaspher;
using Kcaspher.Menus;


namespace Projet_2._0
{
    public enum GameType   // A laisser ici, pas dans la classe
    {
        Exit,
        Menu_Base_Type,
        Menu_Play_Type,
        Menu_Play_Solo_Type,
        Menu_Play_Solo_World1_Type,
        Menu_Play_Solo_World2_Type,
        Menu_Play_Multi_Type,
        Menu_Play_Multi_Type2,
        Menu_Option_Type,
        Menu_Pause,
        Menu_Pause_Option,
        Menu_Play_Solo_world1_lvl1,
        Menu_Play_Solo_world1_lvl2,
        Menu_Play_Solo_world1_lvl3,
        Menu_Play_Solo_world2_lvl1,
        Menu_Play_Solo_world2_lvl2,
        Menu_Play_Solo_world2_lvl3,
        Intro1,
        Intro2,
        Intro3,
        Trans1,
        Trans2,
        Trans3,
        End1,
        End2,
        End3,
        Dead
    }

    public class ScreenManager
    {
        int delta, deltaboss;
        public Boolean respawn, boutonlvl3w1;
        public Casper casper;
        public Casper casper2;
        public Casper player2;
        DyingScreen cinematic;
        AI_basic BigMonster;        
        AI_basic Boss;
        Boolean bigM;
        Menu_Base menubase;
        Menu_Options menuoptions;
        public GameType gametype, previousgametype;
        public Menu_Play menuplay;
        Menu_Play_Solo menuSolo;
        Menu_Play_Multi menuMulti;
        Menu_Play_Solo_World1 menusolo1;
        Menu_Play_Solo_World2 menusolo2;
        Menu_Pause menupause;
        Menu_Pause_Options menupauseoption;
        Menu_multi2 menumulti2;
        Decors d_w2l1, d_w2l2, d_w2l3, d_w1l1_1, d_w1l1_2, d_w1l2_1, d_w1l2_2, d_w1l3;
        public Camera camera;
        //AI_basic AI1;
        AI_W1L1 AI_w1l1;
        AI_W1L2 AI_w1l2;
        AI_W2L1 AI_w2l1;
        AI_W2L2_2 AI_w2l2_2;
        AI_W2L2 AI_w2l2;
        AI_W2L3 AI_w2l3;
        KeyboardState keyboardstate, previouskeyboardstate;
        public Controls controls, controlsPlayer2, controlsWorld2;
        W2L1 w2l1;
        W2L2 w2l2;
        W2L3 w2l3;
        W1L1 w1l1;
        W1L2 w1l2;
        Rectangle teleport1, teleport2;
        W1L3 w1l3;
        Spikes spikes;
        Sw1l2 sw1l2;
        Sw1l3 sw1l3;
        public ParticleEngine tp1, tp2, tp3, tp4;
        Shots shots, shots2, shots3;
        Rectangle finLvl1, finLvl2,finLvl3, finLvl4, finLvl5, finLvl6;
        public ScreenManager(GameType gametype, Game1 game)
        {
            menubase = new Menu_Base(Content_Manager.getInstance().Textures["menubase"]);
            menuoptions = new Menu_Options(Content_Manager.getInstance().Textures["menuoptions"]);
            menuplay = new Menu_Play(Content_Manager.getInstance().Textures["menuplay"]);
            menuSolo = new Menu_Play_Solo(Content_Manager.getInstance().Textures["menusolo"]);
            menusolo1 = new Menu_Play_Solo_World1(Content_Manager.getInstance().Textures["solo1"]);
            menusolo2 = new Menu_Play_Solo_World2(Content_Manager.getInstance().Textures["solo2"]);
            menuMulti = new Menu_Play_Multi(Content_Manager.getInstance().Textures["menumulti"]);
            menupauseoption = new Menu_Pause_Options(Content_Manager.getInstance().Textures["menupauseoption"]);
            menumulti2 = new Menu_multi2(Content_Manager.getInstance().Textures["menumulti2"]);

            delta = 0;
            camera = new Camera(Game1.GetGame().GraphicsDevice.Viewport);
            //isThere = false;

            d_w1l1_1 = new Decors(Content_Manager.getInstance().Textures["W1L1_1"], new Rectangle(0, 0, Res.gI().ScaleX(2520), Res.gI().ScaleY(1050)));
            d_w1l1_2 = new Decors(Content_Manager.getInstance().Textures["W1L1_2"], new Rectangle(Res.gI().ScaleX(2520), 0, Res.gI().ScaleX(2520), Res.gI().ScaleY(1050)));
            d_w1l2_1 = new Decors(Content_Manager.getInstance().Textures["W1L2_1"], new Rectangle(0, 0, Res.gI().ScaleX(2520), Res.gI().ScaleY(1050)));
            d_w1l2_2 = new Decors(Content_Manager.getInstance().Textures["W1L2_2"], new Rectangle(Res.gI().ScaleX(2520), 0, Res.gI().ScaleX(2520), Res.gI().ScaleY(1050)));
            d_w1l3 = new Decors(Content_Manager.getInstance().Textures["W1L3"], new Rectangle(0, 0, Res.gI().ScaleX(2240), Res.gI().ScaleY(1050)));

            teleport1 = new Rectangle(Res.gI().ScaleX(1240), Res.gI().ScaleY(400), Res.gI().ScaleX(80), Res.gI().ScaleY(80));
            teleport2 = new Rectangle(Res.gI().ScaleX(3480), Res.gI().ScaleY(480), Res.gI().ScaleX(80), Res.gI().ScaleY(280));
            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(Content_Manager.getInstance().Textures["tp"]);
            tp1 = tp2 = tp3 = tp4 = new ParticleEngine(textures, new Vector2(0, 0));

            cinematic = new DyingScreen();


            Game1.GetGame().casperr = casper;
            d_w2l1 = new Decors(Content_Manager.getInstance().Textures["W2L1"], new Rectangle(0, 0, Res.gI().ScaleX(2520), Res.gI().ScaleY(1050)));
            d_w2l2 = new Decors(Content_Manager.getInstance().Textures["W2L2"], new Rectangle(0, 0, Res.gI().ScaleX(2520), Res.gI().ScaleY(1050)));
            d_w2l3 = new Decors(Content_Manager.getInstance().Textures["W2L3"], new Rectangle(0, 0, Res.gI().ScaleX(2520), Res.gI().ScaleY(1050)));
            menupause = new Menu_Pause(Content_Manager.getInstance().Textures["menupause"]);
            w1l1 = new W1L1();
            w1l2 = new W1L2();
            w1l3 = new W1L3();
            w2l1 = new W2L1();
            w2l2 = new W2L2();
            w2l3 = new W2L3();
            spikes = new Spikes();
            sw1l2 = new Sw1l2();
            sw1l3 = new Sw1l3();
            previousgametype = GameType.Exit;
            this.gametype = gametype;



            finLvl1 = new Rectangle(Res.gI().ScaleX(4620), Res.gI().ScaleY(270), Res.gI().ScaleX(200), Res.gI().ScaleY(50));
            finLvl2 = new Rectangle(Res.gI().ScaleX(4470), Res.gI().ScaleY(700), Res.gI().ScaleX(200), Res.gI().ScaleY(60));
            finLvl4 = new Rectangle(Res.gI().ScaleX(2410), Res.gI().ScaleY(60), Res.gI().ScaleX(70), Res.gI().ScaleY(90));
            finLvl5 = new Rectangle(Res.gI().ScaleX(40), Res.gI().ScaleY(900), Res.gI().ScaleX(70), Res.gI().ScaleY(90));
            finLvl3 = new Rectangle(Res.gI().ScaleX(1838), Res.gI().ScaleY(300), Res.gI().ScaleX(24), Res.gI().ScaleY(24));
            boutonlvl3w1 = true;


            //List<Rectangle> enemies = spikes.getList().Concat<Rectangle>(AI_w1l1.getListRectangle()).ToList<Rectangle>;
        }

        public void update(GameTime gametime)
        {
            camera.update(gametime, new Vector2(Res.gI().ScaleX(840), 0));
            keyboardstate = Keyboard.GetState();

            switch (gametype)
            {
               
                case GameType.Menu_Base_Type:

                    menubase.update(gametime, ref gametype, ref previousgametype);
                    previousgametype = GameType.Exit;

                    casper = new Casper(Content_Manager.getInstance().Textures["Casper"], new Rectangle(0, 0, Res.gI().ScaleX(16), Res.gI().ScaleY(34)));
                    controls = new Controls(casper.Position, casper.Velocity, casper.Speed, Keys.W, Keys.A, Keys.D, Keys.S);
                    respawn = true;
                    player2 = new Casper(Content_Manager.getInstance().Textures["Casper"], new Rectangle(Res.gI().ScaleX(50), Res.gI().ScaleY(50), Res.gI().ScaleX(16), Res.gI().ScaleY(34)));
                    casper2 = new Casper(Content_Manager.getInstance().Textures["Player1"], new Rectangle(Res.gI().ScaleX(9000), Res.gI().ScaleY(9000), Res.gI().ScaleX(31), Res.gI().ScaleY(50)));
                    controlsPlayer2 = new Controls(player2.Position, player2.Velocity, player2.Speed, Keys.Up, Keys.Left, Keys.Right, Keys.Down);
                    controlsWorld2 = new Controls(casper2.Position, casper2.Velocity, casper2.Speed, Keys.W, Keys.A, Keys.D, Keys.S);

                    shots = new Shots();
                    shots2 = new Shots();
                    shots3 = new Shots();

                    deltaboss = 0;

                    bigM = true;

                    finLvl1 = new Rectangle(Res.gI().ScaleX(4620),Res.gI().ScaleY(270),Res.gI().ScaleX(200),Res.gI().ScaleY(50));
                    finLvl2 = new Rectangle(Res.gI().ScaleX(4600),Res.gI().ScaleY(700),Res.gI().ScaleX(200),Res.gI().ScaleY(60));
                    finLvl6 = new Rectangle(Res.gI().ScaleX(40), Res.gI().ScaleY(700), Res.gI().ScaleX(70), Res.gI().ScaleY(90));
                    finLvl4 = new Rectangle(Res.gI().ScaleX(2410),Res.gI().ScaleY(60),Res.gI().ScaleX(70),Res.gI().ScaleY(90));
                    finLvl5 = new Rectangle(Res.gI().ScaleX(40),Res.gI().ScaleY(900),Res.gI().ScaleX(70),Res.gI().ScaleY(90));
                    //finLvl3 = new Rectangle(Res.gI().ScaleX(1838), Res.gI().ScaleY(259), Res.gI().ScaleX(23), Res.gI().ScaleY(24));

                    AI_w1l1 = new AI_W1L1();
                    AI_w1l2 = new AI_W1L2();
                    AI_w2l1 = new AI_W2L1();
                    AI_w2l2 = new AI_W2L2();
                    AI_w2l2_2 = new AI_W2L2_2();
                    AI_w2l3 = new AI_W2L3();
                    Boss = new AI_basic(Content_Manager.getInstance().Textures["PlayerDroite1"], Content_Manager.getInstance().Textures["PlayerGauche1"], new Rectangle(Res.gI().ScaleX(820), Res.gI().ScaleY(150), Res.gI().ScaleX(30), Res.gI().ScaleY(50)), new Vector2(4, 0), Res.gI().ScaleX(610));
                    // 820x y150 x1430 y150 30 50
                    Game1.GetGame().IsMouseVisible = true;

                    casper.healthpoint.healthpoint = 15;
                    casper2.healthpoint.healthpoint = 5;
                    //player2.healthpoint.healthpoint = 13;
                    break;
                case GameType.Menu_Play_Type:
                    menuplay.update(gametime, ref gametype, ref previousgametype);
                    previousgametype = GameType.Menu_Base_Type;
                    break;
                case GameType.Menu_Play_Solo_Type:
                    menuSolo.update(gametime, ref gametype, ref previousgametype);
                    previousgametype = GameType.Menu_Play_Type;
                    break;
                case GameType.Menu_Play_Solo_World1_Type:
                    menusolo1.update(gametime, ref gametype, ref previousgametype);
                    previousgametype = GameType.Menu_Play_Solo_Type;
                    break;
                case GameType.Menu_Play_Solo_World2_Type:
                    menusolo2.update(gametime, ref gametype, ref previousgametype);
                    previousgametype = GameType.Menu_Play_Solo_Type;
                    break;
                case GameType.Menu_Play_Multi_Type:
                    // menuMulti.update(gametime, ref gametype, ref previousgametype);
                    previousgametype = GameType.Menu_Play_Solo_World1_Type;
                    if (respawn)
                    {
                        controls.Position = new Vector2(Res.gI().ScaleX(200), Res.gI().ScaleY(924));
                        respawn = false;
                    }
                    if (casper.healthpoint.respawn)
                    {
                        controls.Position = controlsPlayer2.Position;
                        casper.healthpoint.respawn = false;
                    }
                    if (player2.healthpoint.respawn)
                    {
                        controlsPlayer2.Position = controls.Position;
                        player2.healthpoint.respawn = false;
                    }
                    if (casper.Position.X > Res.gI().ScaleX(840))
                        camera.update(gametime, casper.Position);
                    if (casper.Position.X > Res.gI().ScaleX(4200))
                        camera.update(gametime, new Vector2(Res.gI().ScaleX(4200), 0));
                    AI_w1l1.update(gametime);
                    casper.update(gametime, controls, gametype, w1l1.getList(), spikes.getList().Concat<Rectangle>(AI_w1l1.getListRectangle()));
                    player2.update(gametime, controlsPlayer2, gametype, w1l1.getList(), spikes.getList().Concat<Rectangle>(AI_w1l1.getListRectangle()));

                    Game1.GetGame().IsMouseVisible = false;
                    if (keyboardstate.IsKeyDown(Keys.Escape) && previouskeyboardstate.IsKeyUp(Keys.Escape))
                    {
                        previousgametype = GameType.Menu_Play_Multi_Type;
                        casper.update(gametime, controls, gametype, w1l1.getList(), spikes.getList().Concat<Rectangle>(AI_w1l1.getListRectangle()));
                        player2.update(gametime, controlsPlayer2, gametype, w1l1.getList(), spikes.getList().Concat<Rectangle>(AI_w1l1.getListRectangle()));

                        Game1.GetGame().IsMouseVisible = true;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(SoundManager.pause);
                        gametype = GameType.Menu_Pause;

                    }
                    previouskeyboardstate = keyboardstate;
                    break;
                case GameType.Menu_Option_Type:
                    menuoptions.update(gametime, ref gametype, ref previousgametype);
                    previousgametype = GameType.Menu_Base_Type;
                    break;
                case GameType.Menu_Play_Solo_world1_lvl1:
                    Game1.GetGame().IsMouseVisible = false;
                    previousgametype = GameType.Menu_Play_Solo_World1_Type;
                    if (respawn || casper.healthpoint.respawn)
                    {
                        controls.Position = new Vector2(Res.gI().ScaleX(200), Res.gI().ScaleY(924));
                        respawn = false;
                        casper.healthpoint.respawn = false;
                    }
                    if (casper.Position.X > Res.gI().ScaleX(840))
                        camera.update(gametime, casper.Position);
                    if (casper.Position.X > Res.gI().ScaleX(4200))
                        camera.update(gametime, new Vector2(Res.gI().ScaleX(4200), 0));

                    Game1.GetGame().casperr = casper;
                    AI_w1l1.update(gametime);
                    casper.update(gametime, controls, gametype, w1l1.getList(), spikes.getList().Concat<Rectangle>(AI_w1l1.getListRectangle()));
                    if (keyboardstate.IsKeyDown(Keys.Escape) && previouskeyboardstate.IsKeyUp(Keys.Escape))
                    {
                        Game1.GetGame().IsMouseVisible = true;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(SoundManager.pause);
                        gametype = GameType.Menu_Pause;
                        previousgametype = GameType.Menu_Play_Solo_world1_lvl1;
                    }
                    previouskeyboardstate = keyboardstate;

                    /// fin du lvl
                    if (casper.Hitbox.Intersects(finLvl1))
                    {
                        controls.Position = new Vector2(Res.gI().ScaleX(200), Res.gI().ScaleY(244));
                        bigM = true;
                        gametype = GameType.Menu_Play_Solo_world1_lvl2;
                    }
                    /// 
                    break;
                case GameType.Menu_Play_Solo_world1_lvl2:
                    previousgametype = GameType.Menu_Play_Solo_World1_Type;
                    if (respawn || casper.healthpoint.respawn)
                    {
                        controls.Position = new Vector2(Res.gI().ScaleX(200), Res.gI().ScaleY(244));
                        respawn = false;
                        casper.healthpoint.respawn = false;
                        bigM = true;
                    }
                    if (casper.Position.X > Res.gI().ScaleX(840))
                        camera.update(gametime, casper.Position);
                    if (casper.Position.X > Res.gI().ScaleX(4200))
                        camera.update(gametime, new Vector2(Res.gI().ScaleX(4200), 0));

                    Game1.GetGame().casperr = casper;
                    AI_w1l2.update(gametime);
                    tp1.EmitterLocation = new Vector2(Res.gI().ScaleX(1280), Res.gI().ScaleY(450));
                    tp1.Update();
                    tp2.EmitterLocation = new Vector2(Res.gI().ScaleX(3490), Res.gI().ScaleY(530));
                    tp2.Update();
                    tp3.EmitterLocation = new Vector2(Res.gI().ScaleX(3490), Res.gI().ScaleY(630));
                    tp3.Update();
                    tp4.EmitterLocation = new Vector2(Res.gI().ScaleX(3490), Res.gI().ScaleY(730));
                    tp4.Update();
                    casper.update(gametime, controls, gametype, w1l2.getList(), sw1l2.getList().Concat<Rectangle>(AI_w1l2.getListRectangle()));
                    Game1.GetGame().IsMouseVisible = false;
                    if (casper.Hitbox.Intersects(teleport1))
                        controls.Position = new Vector2(Res.gI().ScaleX(500), Res.gI().ScaleY(726));
                    if (casper.Hitbox.Intersects(teleport2))
                        controls.Position = new Vector2(Res.gI().ScaleX(3025), Res.gI().ScaleY(366));
                    if (controls.Position.X > 1320 && bigM)
                    {
                        BigMonster = new AI_basic(Content_Manager.getInstance().Textures["Wall"], Content_Manager.getInstance().Textures["enemy2"], new Rectangle(Res.gI().ScaleX(500), Res.gI().ScaleY(0), Res.gI().ScaleX(160), Res.gI().ScaleY(1050)), new Vector2(6, 0), 30000);
                        bigM = false;
                    }
                    if (BigMonster != null)
                    {
                        BigMonster.update(gametime);
                        if (casper.Hitbox.Intersects(BigMonster.Hitbox))
                        {
                            respawn = true;
                            casper.healthpoint.healthpoint -= 1;
                            SoundManager.hp.Play();
                        }
                    }
                    if (keyboardstate.IsKeyDown(Keys.Escape) && previouskeyboardstate.IsKeyUp(Keys.Escape))
                    {
                        Game1.GetGame().IsMouseVisible = true;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(SoundManager.pause);
                        gametype = GameType.Menu_Pause;
                        previousgametype = GameType.Menu_Play_Solo_world1_lvl2;
                    }
                    previouskeyboardstate = keyboardstate;
                    
                    // fin lvl
                    if (casper.Hitbox.Intersects(finLvl2))
                    {
                        controls.Position = new Vector2(Res.gI().ScaleX(80), Res.gI().ScaleY(644));
                        gametype = GameType.Menu_Play_Solo_world1_lvl3;
                    }
                    break;
                case GameType.Menu_Play_Solo_world1_lvl3:
                    previousgametype = GameType.Menu_Play_Solo_World1_Type;
                    if (respawn || casper.healthpoint.respawn)
                    {
                        controls.Position = new Vector2(Res.gI().ScaleX(80), Res.gI().ScaleY(644));//spawn position
                        respawn = false;
                        casper.healthpoint.respawn = false;
                    }
                    if (casper.Position.X > Res.gI().ScaleX(840))
                        camera.update(gametime, casper.Position);
                    if (casper.Position.X > Res.gI().ScaleX(1400))
                        camera.update(gametime, new Vector2(Res.gI().ScaleX(1400), 0));
                    deltaboss++;
                    if (deltaboss > 60)
                        deltaboss = 0;

                    Game1.GetGame().casperr = casper;
                    Boss.update(gametime);
                    casper.update(gametime, controls, gametype, w1l3.getList(), sw1l3.getList());

                    Game1.GetGame().IsMouseVisible = false;
                    // IA
                    if (keyboardstate.IsKeyDown(Keys.Escape) && previouskeyboardstate.IsKeyUp(Keys.Escape))
                    {
                        Game1.GetGame().IsMouseVisible = true;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(SoundManager.pause);
                        gametype = GameType.Menu_Pause;
                        previousgametype = GameType.Menu_Play_Solo_world1_lvl3;
                    }
                    previouskeyboardstate = keyboardstate;

                    if (casper.Hitbox.Intersects(finLvl3))
                    {
                        gametype = GameType.Trans1;
                        controlsWorld2.Position = new Vector2(Res.gI().ScaleX(55), Res.gI().ScaleY(924));
                    }
                    break;
                case GameType.Menu_Play_Solo_world2_lvl1:
                    previousgametype = GameType.Menu_Play_Solo_World1_Type;
                    if (respawn || casper2.healthpoint.respawn)
                    {
                        controlsWorld2.Position = new Vector2(Res.gI().ScaleX(55), Res.gI().ScaleY(924));
                        respawn = false;
                        casper2.healthpoint.respawn = false;
                        AI_w2l1 = new AI_W2L1();
                    }
                    if (casper2.Position.X > Res.gI().ScaleX(840))
                        camera.update(gametime, casper2.Position);
                    if (casper2.Position.X > Res.gI().ScaleX(1680))
                        camera.update(gametime, new Vector2(Res.gI().ScaleX(1680), 0));
                    AI_w2l1.update(gametime, casper2);
                    Game1.GetGame().casperr = casper2;
                    casper2.update(gametime, controlsWorld2, gametype, w2l1.getList(), AI_w2l1.getListRectangle());
                    Game1.GetGame().IsMouseVisible = true;
                    shots.update(gametime, casper2.Position, AI_w2l1.AI_w2l1);
                    if (keyboardstate.IsKeyDown(Keys.Escape) && previouskeyboardstate.IsKeyUp(Keys.Escape))
                    {
                        Game1.GetGame().IsMouseVisible = true;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(SoundManager.pause);
                        //MediaPlayer.Volume = 0.7f;
                        gametype = GameType.Menu_Pause;
                        previousgametype = GameType.Menu_Play_Solo_world2_lvl1;
                    }
                    previouskeyboardstate = keyboardstate;

                    //fin lvl
                    if (casper2.Hitbox.Intersects(finLvl4))
                    {
                        gametype = GameType.Menu_Play_Solo_world2_lvl2;
                        controlsWorld2.Position = new Vector2(Res.gI().ScaleX(2420), Res.gI().ScaleY(80));
                    }
                    break;
                case GameType.Menu_Play_Solo_world2_lvl2:
                    previousgametype = GameType.Menu_Play_Solo_World2_Type;
                    if (respawn || casper2.healthpoint.respawn)
                    {
                        controlsWorld2.Position = new Vector2(Res.gI().ScaleX(2420), Res.gI().ScaleY(80));
                        respawn = false;
                        casper2.healthpoint.respawn = false;
                        AI_w2l2 = new AI_W2L2();
                    }
                    if (casper2.Position.X > Res.gI().ScaleX(840))
                        camera.update(gametime, casper2.Position);
                    if (casper2.Position.X > Res.gI().ScaleX(1680))
                        camera.update(gametime, new Vector2(Res.gI().ScaleX(1680), 0));
                    AI_w2l2.update(gametime, casper2);
                    AI_w2l2_2.update(gametime);
                    Game1.GetGame().casperr = casper2;
                    Game1.GetGame().IsMouseVisible = true;
                    shots2.update(gametime, casper2.Position, AI_w2l2.AI_w2l2);
                    casper2.update(gametime, controlsWorld2, gametype, w2l2.getList(), AI_w2l2_2.getListRectangle().Concat<Rectangle>(AI_w2l2.getListRectangle()));
                    if (keyboardstate.IsKeyDown(Keys.Escape) && previouskeyboardstate.IsKeyUp(Keys.Escape))
                    {
                        //casper.update(gametime);
                        Game1.GetGame().IsMouseVisible = true;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(SoundManager.pause);
                        gametype = GameType.Menu_Pause;
                        previousgametype = GameType.Menu_Play_Solo_world2_lvl2;
                    }
                    previouskeyboardstate = keyboardstate;

                    //fin lvl

                    if (casper2.Hitbox.Intersects(finLvl5))
                    {
                        gametype = GameType.Menu_Play_Solo_world2_lvl3;
                        controlsWorld2.Position = new Vector2(Res.gI().ScaleX(55), Res.gI().ScaleY(924));
                    }

                    break;
                case GameType.Menu_Play_Solo_world2_lvl3:
                    previousgametype = GameType.Menu_Play_Solo_World2_Type;
                    if (respawn || casper2.healthpoint.respawn)
                    {
                        controlsWorld2.Position = new Vector2(Res.gI().ScaleX(55), Res.gI().ScaleY(924));
                        respawn = false;
                        casper2.healthpoint.respawn = false;
                        AI_w2l3 = new AI_W2L3();
                    }
                    if (casper2.Position.X > Res.gI().ScaleX(840))
                        camera.update(gametime, casper2.Position);
                    if (casper2.Position.X > Res.gI().ScaleX(1680))
                        camera.update(gametime, new Vector2(Res.gI().ScaleX(1680), 0));
                    AI_w2l3.update(gametime, casper2);
                    Game1.GetGame().casperr = casper2;

                    casper2.update(gametime, controlsWorld2, gametype, w2l3.getList(), AI_w2l3.getListRectangle());
                    Game1.GetGame().IsMouseVisible = true;
                    shots3.update(gametime, casper2.Position, AI_w2l3.AI_w2l3);
                    if (keyboardstate.IsKeyDown(Keys.Escape) && previouskeyboardstate.IsKeyUp(Keys.Escape))
                    {
                        Game1.GetGame().IsMouseVisible = true;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(SoundManager.pause);
                        gametype = GameType.Menu_Pause;
                        previousgametype = GameType.Menu_Play_Solo_world2_lvl3;
                    }
                    previouskeyboardstate = keyboardstate;
                    if (AI_w2l3.getListRectangle().Count==0)
                    {
                        gametype = GameType.End1;
                    }
                    
                    break;
                case GameType.Exit:
                    Game1.GetGame().Exit();
                    break;
                case GameType.Menu_Pause:
                    if (previousgametype == GameType.Menu_Play_Solo_world2_lvl1 || previousgametype == GameType.Menu_Play_Solo_world2_lvl2 || previousgametype == GameType.Menu_Play_Solo_world2_lvl3)
                    {
                        if (casper2.Position.X > Res.gI().ScaleX(840))
                            camera.update(gametime, casper2.Position);
                        if (casper2.Position.X > Res.gI().ScaleX(1680))
                            camera.update(gametime, new Vector2(Res.gI().ScaleX(1680), 0));
                    }
                    if (previousgametype == GameType.Menu_Play_Solo_world1_lvl3)
                    {
                        if (casper.Position.X > Res.gI().ScaleX(840))
                            camera.update(gametime, casper.Position);
                        if (casper.Position.X > Res.gI().ScaleX(1400))
                            camera.update(gametime, new Vector2(Res.gI().ScaleX(1400), 0));
                    }
                    if (previousgametype == GameType.Menu_Play_Solo_world1_lvl1 || previousgametype == GameType.Menu_Play_Solo_world1_lvl2)
                    {
                        if (casper.Position.X > Res.gI().ScaleX(840))
                            camera.update(gametime, casper.Position);
                        if (casper.Position.X > Res.gI().ScaleX(4200))
                            camera.update(gametime, new Vector2(Res.gI().ScaleX(4200), 0));
                    }

                    //camera.update(gametime, casper.Position);
                    if (keyboardstate.IsKeyDown(Keys.Escape) && previouskeyboardstate.IsKeyUp(Keys.Escape))
                    {

                        ////////// test avec previous game stqt
                        Game1.GetGame().IsMouseVisible = false;
                        MediaPlayer.Stop();
                        gametype = previousgametype;
                        if (previousgametype == GameType.Menu_Play_Solo_world1_lvl2)
                        {
                            MediaPlayer.Play(SoundManager.ingame2);
                        }
                        if (previousgametype == GameType.Menu_Play_Solo_world1_lvl3)
                        {
                            MediaPlayer.Play(SoundManager.ingame3);
                        }
                        if (previousgametype == GameType.Menu_Play_Solo_world2_lvl3)
                        {
                            MediaPlayer.Play(SoundManager.ingame4);
                        }
                        if (previousgametype == GameType.Menu_Play_Solo_world2_lvl1)
                        {
                            MediaPlayer.Play(SoundManager.ingame6);
                        }
                        if (previousgametype == GameType.Menu_Play_Solo_world2_lvl2)
                        {
                            MediaPlayer.Play(SoundManager.ingame5);
                        }

                    }
                    menupause.update(gametime, ref gametype, ref previousgametype, camera.centre);
                    previouskeyboardstate = keyboardstate;
                    break;
                case GameType.Menu_Pause_Option:
                    //camera.update(gametime, casper.Position);
                    if (previousgametype == GameType.Menu_Play_Solo_world2_lvl1 || previousgametype == GameType.Menu_Play_Solo_world2_lvl2 || previousgametype == GameType.Menu_Play_Solo_world2_lvl3)
                    {
                        if (casper2.Position.X > Res.gI().ScaleX(840))
                            camera.update(gametime, casper2.Position);
                        if (casper2.Position.X > Res.gI().ScaleX(1680))
                            camera.update(gametime, new Vector2(Res.gI().ScaleX(1680), 0));
                    }
                    if (previousgametype == GameType.Menu_Play_Solo_world1_lvl3)
                    {
                        if (casper.Position.X > Res.gI().ScaleX(840))
                            camera.update(gametime, casper.Position);
                        if (casper.Position.X > Res.gI().ScaleX(1400))
                            camera.update(gametime, new Vector2(Res.gI().ScaleX(1400), 0));
                    }
                    if (previousgametype == GameType.Menu_Play_Solo_world1_lvl1 || previousgametype == GameType.Menu_Play_Solo_world1_lvl2)
                    {
                        if (casper.Position.X > Res.gI().ScaleX(840))
                            camera.update(gametime, casper.Position);
                        if (casper.Position.X > Res.gI().ScaleX(4200))
                            camera.update(gametime, new Vector2(Res.gI().ScaleX(4200), 0));
                    }
                    menupauseoption.update(gametime, ref gametype, ref previousgametype, camera.centre);
                    break;
                case GameType.Intro1:
                    cinematic.update(GameType.Intro2);
                    break;
                case GameType.Intro2:
                    cinematic.update(GameType.Intro3);
                    break;
                case GameType.Intro3:
                    cinematic.update(GameType.Menu_Play_Solo_world1_lvl1);
                    break;
                case GameType.Trans1:
                    cinematic.update(GameType.Trans2);
                    break;
                case GameType.Trans2:
                    cinematic.update(GameType.Trans3);
                    break;
                case GameType.Trans3:
                    cinematic.update(GameType.Menu_Play_Solo_world2_lvl1);
                    break;
                case GameType.End1:
                    cinematic.update(GameType.End2);
                    break;
                case GameType.End2:
                    cinematic.update(GameType.End3);
                    break;
                case GameType.End3:
                    cinematic.update(GameType.Menu_Base_Type);
                    break;
                case GameType.Dead:
                    cinematic.update(GameType.Menu_Base_Type);
                    break;
                case GameType.Menu_Play_Multi_Type2:
                    menumulti2.update(gametime, ref gametype, ref previousgametype);
                    previousgametype = GameType.Menu_Play_Multi_Type;
                    break;
                default:
                    menubase.update(gametime, ref gametype, ref previousgametype);
                    break;
            }
        }
        public void Draw(SpriteBatch spritebatch)
        {
            switch (gametype)
            {
                case GameType.Menu_Base_Type:
                    menubase.Draw(spritebatch);
                    break;
                case GameType.Menu_Play_Type:
                    menuplay.Draw(spritebatch);
                    break;
                case GameType.Menu_Play_Solo_Type:
                    menuSolo.Draw(spritebatch);
                    break;
                case GameType.Menu_Play_Solo_World1_Type:
                    menusolo1.Draw(spritebatch);
                    break;
                case GameType.Menu_Play_Solo_World2_Type:
                    menusolo2.Draw(spritebatch);
                    break;
                // world1 lvl 3
                case GameType.Menu_Play_Multi_Type:
                    d_w1l1_1.Draw(spritebatch);
                    d_w1l1_2.Draw(spritebatch);
                    AI_w1l1.Draw(spritebatch);
                    casper.Draw(spritebatch, Color.White);
                    player2.Draw(spritebatch, Color.CornflowerBlue);
                    casper.healthpoint.draw(spritebatch, camera);
                    break;
                case GameType.Menu_Option_Type:
                    menuoptions.Draw(spritebatch);
                    break;
                case GameType.Menu_Play_Solo_world1_lvl1:
                    d_w1l1_1.Draw(spritebatch);
                    d_w1l1_2.Draw(spritebatch);
                    casper.Draw(spritebatch, Color.White);
                    AI_w1l1.Draw(spritebatch);
                    casper.healthpoint.draw(spritebatch, camera);
                    break;
                case GameType.Menu_Play_Solo_world1_lvl2:
                    d_w1l2_1.Draw(spritebatch);
                    d_w1l2_2.Draw(spritebatch);
                    casper.Draw(spritebatch, Color.White);
                    tp1.Draw(spritebatch);
                    tp2.Draw(spritebatch);
                    tp3.Draw(spritebatch);
                    tp4.Draw(spritebatch);
                    if (BigMonster != null)
                        BigMonster.Draw(spritebatch);

                    AI_w1l2.Draw(spritebatch);
                    casper.healthpoint.draw(spritebatch, camera);
                    break;
                case GameType.Menu_Play_Solo_world1_lvl3:
                    d_w1l3.Draw(spritebatch);
                    casper.Draw(spritebatch, Color.White);
                    casper.healthpoint.draw(spritebatch, camera);
                    if (Boss.Dtexture == Content_Manager.getInstance().Textures["PlayerDroite1"])
                    {
                        if (deltaboss < 15)
                        spritebatch.Draw(Content_Manager.getInstance().Textures["PlayerDroite1"], Boss.Hitbox, Color.White);
                    else if (deltaboss < 30)
                        spritebatch.Draw(Content_Manager.getInstance().Textures["PlayerDroite2"], Boss.Hitbox, Color.White);
                    else if (deltaboss < 45)
                        spritebatch.Draw(Content_Manager.getInstance().Textures["PlayerDroite3"], Boss.Hitbox, Color.White);
                    else if (deltaboss <= 60)
                        spritebatch.Draw(Content_Manager.getInstance().Textures["PlayerDroite4"], Boss.Hitbox, Color.White);
                    }
                    else
                    {
                        if (deltaboss < 15)
                        spritebatch.Draw(Content_Manager.getInstance().Textures["PlayerGauche1"], Boss.Hitbox, Color.White);
                    else if (deltaboss < 30)
                        spritebatch.Draw(Content_Manager.getInstance().Textures["PlayerGauche2"], Boss.Hitbox, Color.White);
                    else if (deltaboss < 45)
                        spritebatch.Draw(Content_Manager.getInstance().Textures["PlayerGauche3"], Boss.Hitbox, Color.White);
                    else if (deltaboss <= 60)
                        spritebatch.Draw(Content_Manager.getInstance().Textures["PlayerGauche4"], Boss.Hitbox, Color.White);
                    }
                    delta++;
                    if (delta > 30)
                        delta = 0;
                    if (casper.Hitbox.Intersects(finLvl6) )//&& boutonlvl3w1)
                    {
                        boutonlvl3w1 = false;
                        if (delta < 10)
                            spritebatch.Draw(Content_Manager.getInstance().Textures["feu1"], new Rectangle(Res.gI().ScaleX(190), Res.gI().ScaleY(-70), Res.gI().ScaleX(1680), Res.gI().ScaleY(1050)), Color.White);
                        else if (delta < 20)
                            spritebatch.Draw(Content_Manager.getInstance().Textures["feu2"], new Rectangle(Res.gI().ScaleX(220), Res.gI().ScaleY(-70), Res.gI().ScaleX(1680), Res.gI().ScaleY(1050)), Color.White);
                        else if (delta < 30)
                            spritebatch.Draw(Content_Manager.getInstance().Textures["feu3"], new Rectangle(Res.gI().ScaleX(150), Res.gI().ScaleY(-7), Res.gI().ScaleX(1680), Res.gI().ScaleY(1050)), Color.White);
                    }
                    break;
                case GameType.Menu_Play_Solo_world2_lvl1:
                    d_w2l1.Draw(spritebatch);
                    casper2.Draw(spritebatch, Color.White);
                    AI_w2l1.Draw(spritebatch);
                    casper2.healthpoint.draw(spritebatch, camera);
                    shots.Draw(spritebatch);
                    break;
                case GameType.Menu_Play_Solo_world2_lvl2:
                    d_w2l2.Draw(spritebatch);
                    casper2.Draw(spritebatch, Color.White);
                    casper2.healthpoint.draw(spritebatch, camera);
                    shots2.Draw(spritebatch);
                    AI_w2l2.Draw(spritebatch);
                    AI_w2l2_2.Draw(spritebatch);
                    break;
                case GameType.Menu_Play_Solo_world2_lvl3:
                    d_w2l3.Draw(spritebatch);
                    casper2.Draw(spritebatch, Color.White);
                    casper2.healthpoint.draw(spritebatch, camera);
                    shots3.Draw(spritebatch);
                    AI_w2l3.Draw(spritebatch);
                    break;
                case GameType.Menu_Pause:
                    switch (previousgametype)
                    {
                        case GameType.Menu_Play_Solo_world1_lvl1:
                            d_w1l1_1.Draw(spritebatch);
                            d_w1l1_2.Draw(spritebatch);
                            casper.Draw(spritebatch, Color.White);
                            // IA
                            break;
                        case GameType.Menu_Play_Solo_world1_lvl2:
                            d_w1l2_1.Draw(spritebatch);
                            d_w1l2_2.Draw(spritebatch);
                            casper.Draw(spritebatch, Color.White);
                            // IA
                            break;
                        case GameType.Menu_Play_Solo_world1_lvl3:
                            d_w1l3.Draw(spritebatch);
                            casper.Draw(spritebatch, Color.White);
                            // IA
                            break;
                        case GameType.Menu_Play_Solo_world2_lvl1:
                            d_w2l1.Draw(spritebatch);
                            casper2.Draw(spritebatch, Color.White);
                            // IA
                            break;
                        case GameType.Menu_Play_Solo_world2_lvl2:
                            d_w2l2.Draw(spritebatch);
                            casper2.Draw(spritebatch, Color.White);
                            // IA
                            break;
                        case GameType.Menu_Play_Solo_world2_lvl3:
                            d_w2l3.Draw(spritebatch);
                            casper2.Draw(spritebatch, Color.White);
                            // IA
                            break;
                        case GameType.Menu_Play_Multi_Type:
                            d_w1l1_1.Draw(spritebatch);
                            d_w1l1_2.Draw(spritebatch);
                            casper.Draw(spritebatch, Color.White);
                            player2.Draw(spritebatch, Color.CornflowerBlue);
                            break;
                        default:
                            break;
                    }
                    //casper.Draw(spritebatch, Color.White);
                    //player2.Draw(spritebatch, Color.CornflowerBlue);
                    menupause.Draw(spritebatch);
                    break;
                case GameType.Menu_Pause_Option:
                    switch (previousgametype)
                    {
                        case GameType.Menu_Play_Solo_world1_lvl1:
                            d_w1l1_1.Draw(spritebatch);
                            d_w1l1_2.Draw(spritebatch);
                            casper.Draw(spritebatch, Color.White);
                            // IA
                            break;
                        case GameType.Menu_Play_Solo_world1_lvl2:
                            d_w1l2_1.Draw(spritebatch);
                            d_w1l2_2.Draw(spritebatch);
                            casper.Draw(spritebatch, Color.White);
                            // IA
                            break;
                        case GameType.Menu_Play_Solo_world1_lvl3:
                            d_w1l3.Draw(spritebatch);
                            casper.Draw(spritebatch, Color.White);
                            // IA
                            break;
                        case GameType.Menu_Play_Solo_world2_lvl1:
                            d_w2l1.Draw(spritebatch);
                            casper2.Draw(spritebatch, Color.White);
                            //IA
                            break;
                        case GameType.Menu_Play_Solo_world2_lvl2:
                            d_w2l2.Draw(spritebatch);
                            casper2.Draw(spritebatch, Color.White);
                            //IA
                            break;
                        case GameType.Menu_Play_Solo_world2_lvl3:
                            d_w2l3.Draw(spritebatch);
                            casper2.Draw(spritebatch, Color.White);
                            //IA
                            break;
                        case GameType.Menu_Play_Multi_Type:
                            d_w1l1_1.Draw(spritebatch);
                            d_w1l1_2.Draw(spritebatch);
                            casper.Draw(spritebatch, Color.White);
                            player2.Draw(spritebatch, Color.CornflowerBlue);
                            //IA
                            break;
                        default:
                            break;
                    }
                    //casper.Draw(spritebatch, Color.White);
                    //player2.Draw(spritebatch, Color.CornflowerBlue);
                    menupauseoption.Draw(spritebatch);
                    break;
                case GameType.Intro1:
                    cinematic.Draw(spritebatch, Content_Manager.getInstance().Textures["Intro1"]);
                    break;
                case GameType.Intro2:
                    cinematic.Draw(spritebatch, Content_Manager.getInstance().Textures["Intro2"]);
                    break;
                case GameType.Intro3:
                    cinematic.Draw(spritebatch, Content_Manager.getInstance().Textures["Intro3"]);
                    break;
                case GameType.Trans1:
                    cinematic.Draw(spritebatch, Content_Manager.getInstance().Textures["Trans1"]);
                    break;
                case GameType.Trans2:
                    cinematic.Draw(spritebatch, Content_Manager.getInstance().Textures["Trans2"]);
                    break;
                case GameType.Trans3:
                    cinematic.Draw(spritebatch, Content_Manager.getInstance().Textures["Trans3"]);
                    break;
                case GameType.End1:
                    cinematic.Draw(spritebatch, Content_Manager.getInstance().Textures["End1"]);
                    break;
                case GameType.End2:
                    cinematic.Draw(spritebatch, Content_Manager.getInstance().Textures["End2"]);
                    break;
                case GameType.End3:
                    cinematic.Draw(spritebatch, Content_Manager.getInstance().Textures["End3"]);
                    break;
                case GameType.Dead:
                    cinematic.Draw(spritebatch, Content_Manager.getInstance().Textures["Dead"]);
                    break;
                case GameType.Menu_Play_Multi_Type2:
                    menumulti2.Draw(spritebatch);
                    break;
                default:
                    menubase.Draw(spritebatch);
                    break;
            }
        }
    }
}
